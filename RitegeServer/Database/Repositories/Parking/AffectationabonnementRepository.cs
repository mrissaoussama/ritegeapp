using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RitegeDomain.Database.Entities.ParkingEntities;


namespace RitegeDomain.Database.Repositories
{
    public class AffectationabonnementRepository : GenericRepository<Affectationabonnement>, IAffectationabonnementRepository
    {
        private string connectionString;

        public async Task<IEnumerable<Affectationabonnement>> GetAllAsync()
        {
            List<Affectationabonnement> abonnes = new List<Affectationabonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Affectationabonnement";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes.Add(new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }
        public async Task<IEnumerable<Affectationabonnement>> GetAllByAbonneIdAsync(long id)
        {
            List<Affectationabonnement> abonnes = new List<Affectationabonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Affectationabonnement where idabonne=@idabonne";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idabonne", SqlDbType.Int).Value = id;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes.Add(new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }
        public async Task<IEnumerable<Affectationabonnement>> GetAllByAbonnementIdAsync(long id)
        {
            List<Affectationabonnement> abonnes = new List<Affectationabonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Affectationabonnement where  idabonnement=@idabonnement";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idabonnement", SqlDbType.Int).Value = id;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes.Add(new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }
        public async Task<IEnumerable<Affectationabonnement>> GetAllByIdWithDatesAsync(long? id, DateTime start, DateTime finish)
        {
            List<Affectationabonnement> abonnes = new List<Affectationabonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (id is null||id==0)
                    query = "SELECT * FROM parkingdb.Affectationabonnement where DateActivation>=@start and DateDesactivation<=@finish";
                else
                    query = "SELECT * FROM parkingdb.Affectationabonnement where idabonne=@idabonne " +
              "and DateActivation>=@start and DateDesactivation<=@finish";
            
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idabonne", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes.Add(new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }
        public async Task<IEnumerable<Affectationabonnement>> GetAllByNameAndDatesAsync(string name, DateTime start, DateTime finish)
        {
            List<Affectationabonnement> abonnes = new List<Affectationabonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (name is null)
                    query = "SELECT * FROM parkingdb.Affectationabonnement ab" +
                 " inner join parkingdb.abonnement a on (a.IdAbonnement = ab.IdAbonnement)" +
                 "where  DateActivation>=@start and DateDesactivation<=@finish ";
                else
                    query = "SELECT * FROM parkingdb.Affectationabonnement ab" +
                       " inner join parkingdb.abonnement a on (a.IdAbonnement = ab.IdAbonnement and a.NomAbonnement like @NomAbonnementsearch) " +
                       "where  DateActivation>=@start and DateDesactivation<=@finish ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@NomAbonnementsearch", SqlDbType.NVarChar).Value = "%" + name + "%";
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes.Add(new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }

        public async Task<Affectationabonnement> GetOneByIdAsync(long id)
        {
            Affectationabonnement abonnes = new Affectationabonnement();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Affectationabonnement where idabonne=@idabonne";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idabonne", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            abonnes = (new Affectationabonnement
                            {
                                IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                                Idaffectationabonnement = Convert.ToInt32(sdr["Idaffectationabonnement"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),
                                OccurenceAbonnement = Convert.ToInt32(sdr["OccurenceAbonnement"]),
                                DateAffectationabonnementcol = Convert.ToDateTime(sdr["DateAffectationabonnementcol"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateDesactivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                EtatSync = Convert.ToBoolean(sdr["EtatSync"]),
                                EtatAffectation = Convert.ToString(sdr["EtatAffectation"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;


        }

        public AffectationabonnementRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
    }
}
