using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class InfoTicketDTORepository : GenericRepository<InfoTicketDTO>, IInfoTicketDTORepository
    {
        private string connectionString;
        public InfoTicketDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<IEnumerable<InfoTicketDTO?>> GetAllByDatesAsync(DateTime dateStart, DateTime dateEnd,int idParking)
        {
            List<InfoTicketDTO> tickets = new List<InfoTicketDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;

                query = "SELECT t.idticket,t.dateHeureDebutStationnement,t.dateHeureFinStationnement,t.Tarif,entree.nomBorne as BorneEntree,sortie.nomBorne as BorneSortie from parkingdb.ticket t LEFT JOIN parkingdb.borne as entree ON entree.idborne = t.idBorneEntree LEFT JOIN parkingdb.borne as sortie ON sortie.idborne = t.idBorneSortie where entree.idparking=@idParking and " +
                    "dateHeureDebutStationnement>=@dateStart" +
                    " and( dateHeureFinStationnement<=@dateEnd or  dateHeureFinStationnement is null) ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = dateStart;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = dateEnd;
                    cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = idParking;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            //string? Typeticketstring = Convert.ToString(sdr["EtatAffectation"]);
                            var ticket = new InfoTicketDTO
                            { CodeTicket = Convert.ToString(sdr["idTicket"]),
                            BorneEntree = Convert.ToString(sdr["BorneEntree"]),
                                MontantPaye = Convert.ToDecimal(sdr["tarif"]),
                                DateHeureEntree = Convert.ToDateTime(sdr["dateHeureDebutStationnement"]),
                                TypeTicket = TypeTicket.TicketStationnement,
                        
                                
                            };
                            if (sdr["dateHeureFinStationnement"] != DBNull.Value)
                                ticket.DateHeureSortie = Convert.ToDateTime(sdr["dateHeureFinStationnement"]);
                            if (sdr["BorneSortie"] != DBNull.Value)
                                ticket.BorneSortie = Convert.ToString(sdr["BorneSortie"]);


                            tickets.Add(ticket);
                        }
                    }
                    con.Close();
                }
            }
            return tickets;

        }

      
    }
}
