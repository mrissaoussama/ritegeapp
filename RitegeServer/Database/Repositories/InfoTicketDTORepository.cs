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
        public async Task<IEnumerable<InfoTicketDTO?>> GetAllByDatesAsync(DateTime start, DateTime finish)
        {
            List<InfoTicketDTO> tickets = new List<InfoTicketDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;

                query = "SELECT t.idticket,t.dateHeureDebutStationnement,t.dateHeureFinStationnement,t.Tarif,"+
                    " (select flux from parkingdb.borne where idBorneEntree=idBorne) as flux from parkingdb.ticket t where "+
                    "dateHeureDebutStationnement>=@start" +
                    " and dateHeureFinStationnement<=@finish ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            //string? Typeticketstring = Convert.ToString(sdr["EtatAffectation"]);
                            var ticket = new InfoTicketDTO
                            { CodeTicket = Convert.ToString(sdr["idTicket"]),
                            BorneEntree = Convert.ToString(sdr["flux"]),
                                MontantPaye = Convert.ToDecimal(sdr["tarif"]),
                                DateHeureSortie = Convert.ToDateTime(sdr["dateHeureFinStationnement"]),
                                DateHeureEntree = Convert.ToDateTime(sdr["dateHeureDebutStationnement"]),
                                TypeTicket = TypeTicket.TicketStationnement,
                        
                                
                            };

                            //if (Typeticketstring is not null)
                            //{
                            //    ticket.TypeAbonnement = (TypeAbonnementEnum)System.Enum.Parse(typeof(TypeAbonnementEnum), Typeticketstring);
                            //}
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
