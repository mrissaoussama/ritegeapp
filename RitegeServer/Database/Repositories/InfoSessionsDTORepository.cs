using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class InfoSessionsDTORepository : GenericRepository<InfoSessionsDTO>, IInfoSessionsDTORepository
    {//implement
        private string connectionString;
        public async Task<IEnumerable<InfoSessionsDTO?>> GetAllByNameAndDatesAsync(int? idCaissier, DateTime start, DateTime finish)
        {
            List<InfoSessionsDTO> abonnes = new List<InfoSessionsDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (idCaissier==0)
                    query = "SELECT  s.idcaisse,s.idSessions,s.logCaissier ,s.montant,s.DateDebut,s.DateFin, Nomcaisse from parkingdb.caisse c,parkingdb.sessions s where s.idCaisse=c.idCaisse and  DateDebut >= @start and( DateFin<=@finish or DateFin is null)";
                else
                    query = "SELECT  s.idcaisse,s.idSessions,s.logCaissier ,s.montant,s.DateDebut,s.DateFin, Nomcaisse from parkingdb.caisse c, parkingdb.sessions s where s.idCaisse = c.idCaisse and s.logCaissier like (select login from parkingdb.utilisateur where idUtilisateur=@idCaissier) and  DateDebut >= @start and( DateFin<=@finish or DateFin is null)";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idCaissier", SqlDbType.Int).Value = idCaissier;
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            int idcaisse = Convert.ToInt32(sdr["idcaisse"]);
                            int total;
                            var session = new InfoSessionsDTO
                            {
                            
                                DateStartSession = Convert.ToDateTime(sdr["DateDebut"]),
                                Caisse = Convert.ToString(sdr["Nomcaisse"]),
                                Index = Convert.ToInt32(sdr["idSessions"]),
                                Caissier = Convert.ToString(sdr["logCaissier"]),

                            };
                            if (sdr["montant"] != DBNull.Value)
                                session.Recette = Convert.ToDecimal(sdr["montant"]);
                            if (sdr["DateFin"] != DBNull.Value)
                                session.DateEndSession = Convert.ToDateTime(sdr["DateFin"]);
                            string query2 = "SELECT  count ( typeevent) as total,typeEvent from parkingdb.evenement  where " +
                                 "idCaisse = @idcaisse and dateevent between @start and @finish group by typeEvent ";
                            using (SqlConnection con2 = new(connectionString))
                            {
                                using (SqlCommand cmd2 = new(query2))
                                {
                                    cmd2.Connection = con2;
                                    cmd2.Parameters.Add("@idcaisse", SqlDbType.Int).Value = idcaisse;
                                    cmd2.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                                    cmd2.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;
                                    con2.Open();
                                    using (SqlDataReader sdr2 = await cmd2.ExecuteReaderAsync())
                                    {
                                        while (await sdr2.ReadAsync())
                                        {
                                            string? typeevent = Convert.ToString(sdr2["typeEvent"]);
                                            if (!string.IsNullOrEmpty(typeevent))
                                            {
                                                total = Convert.ToInt32(sdr2["total"]);


                                                switch (typeevent)
                                                {
                                                    case "AuthorityEntrance": session.NbAutorite += total; break;
                                                    case "AuthorityExit": session.NbAutorite += total; break;
                                                    case "PersonnelEntrance": session.NbAdministratif += total; break;
                                                    case "PersonnelExit": session.NbAdministratif += total; break;
                                                    case "AbonneEntrance": session.NbAbonne += total; break;
                                                    case "AbonneExit": session.NbAbonne += total; break;

                                                }
                                            }
                                        }
                                    }
                                }


                            }
                            string query3 = "SELECT  count ( idticket) as total from parkingdb.ticket  where " +
                                "logcaissier = @logcaissier and dateHeureDebutStationnement>= @start and dateHeureFinStationnement<=@finish  ";
                            using (SqlConnection con3 = new(connectionString))
                            {
                                using (SqlCommand cmd3 = new(query3))
                                {
                                    cmd3.Connection = con3;
                                    cmd3.Parameters.Add("@logcaissier", SqlDbType.NVarChar).Value = session.Caissier;
                                    cmd3.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                                    cmd3.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;
                                    con3.Open();
                                    using (SqlDataReader sdr3 = await cmd3.ExecuteReaderAsync())
                                    {
                                        while (await sdr3.ReadAsync())
                                        {

                                            session.NbTickets = Convert.ToInt32(sdr3["total"]);

                                        }
                                    }
                                }
                            }

                            abonnes.Add(session);

                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }
    

        public InfoSessionsDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
