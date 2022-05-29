using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class InfoSessionsDTORepository : GenericRepository<InfoSessionsDTO>, IInfoSessionsDTORepository
    {//implement
        private string connectionString;
        public async Task<IEnumerable<InfoSessionsDTO?>> GetAllByNameAndDatesAsync(string? name, DateTime start, DateTime finish)
        {
            List<InfoSessionsDTO> abonnes = new List<InfoSessionsDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (string.IsNullOrEmpty(name))
                    query = "SELECT  s.idcaisse,s.idSessions ,s.montant,s.DateDebut,s.DateFin, Nomcaisse from parkingdb.caisse c,parkingdb.sessions s where s.idCaisse=c.idCaisse and  DateDebut >= @start and DateFin<=@finish";
                else
                    query = "SELECT  s.idcaisse,s.idSessions ,s.montant,s.DateDebut,s.DateFin, Nomcaisse from parkingdb.caisse c,parkingdb.sessions s where s.idCaisse=c.idCaisse and  DateDebut >= @start and DateFin<=@finish and nomAbonnement like @name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = "%" + name + "%";
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            int idcaisse = Convert.ToInt32(sdr["idcaisse"]);
                            var session = new InfoSessionsDTO
                            {
                                //LibelleAbonnement = Convert.ToString(sdr["nomAbonnement"]),
                                //PrixAbonnement = Convert.ToDecimal(sdr["montant"]),
                                //DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                //DateFinActivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                //DateAffectation = Convert.ToDateTime(sdr["dateAffectationabonnementcol"]),
                                //Etat = (Etat)System.Enum.Parse(typeof(Etat), value),
                                Recette = Convert.ToDecimal(sdr["montant"]),
                                DateStartSession = Convert.ToDateTime(sdr["DateDebut"]),
                                DateEndSession = Convert.ToDateTime(sdr["DateFin"]),
                                Caisse = Convert.ToString(sdr["Nomcaisse"]),
                                Index = Convert.ToInt32(sdr["idSessions"]),

                            };
                            string query2 = "SELECT  count (DISTINCT typeevent) as total,typeEvent from parkingdb.evenement  where " +
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
                                                session.NbTickets++;
                                                switch (typeevent)
                                                {
                                                    case "AuthorityEntrance": session.NbAutorite += Convert.ToInt32(sdr2["total"]); break;
                                                    case "AuthorityExit": session.NbAutorite += Convert.ToInt32(sdr2["total"]); break;
                                                    case "PersonnelEntrance": session.NbAdministratif += Convert.ToInt32(sdr2["total"]); break;
                                                    case "PersonnelExit": session.NbAutorite += Convert.ToInt32(sdr2["total"]); break;

                                                }
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
        }

        public InfoSessionsDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
