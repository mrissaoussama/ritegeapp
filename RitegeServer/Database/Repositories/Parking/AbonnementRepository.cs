using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Repositories
{
    public class AbonnementRepository : GenericRepository<Abonnement>, IAbonnementRepository
    {
        private string connectionString;
        public async Task<IEnumerable<Abonnement>> GetAllAsync()
        {
            List<Abonnement> Abonnements = new List<Abonnement>();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Abonnement";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        var affectationrepository = new AffectationabonnementRepository();
                        while (await sdr.ReadAsync())
                        {
                            var Abonnement = new Abonnement
                            {
                                NomAbonnement = Convert.ToString(sdr["NomAbonnement"]),
                                PeriodeAbonnement = Convert.ToString(sdr["PeriodeAbonnement"]),
                                Montant = Convert.ToDecimal(sdr["Montant"]),
                                IdIntervalle = Convert.ToInt32(sdr["IdIntervalle"]),
                                IdGroupeTarif = Convert.ToInt32(sdr["IdGroupeTarif"]),
                                NombrePeriode = Convert.ToInt32(sdr["NombrePeriode"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),

                            };
                            Abonnement.Affectationabonnements = await affectationrepository.GetAllByAbonnementIdAsync(Abonnement.IdAbonnement);

                            Abonnements.Add(Abonnement);
                        }
                    }
                    con.Close();
                }
            }
            return Abonnements;

        }

        public async Task<Abonnement> GetOneByIdAsync(long id)
        {
            Abonnement Abonnement = new Abonnement();
            using (SqlConnection con = new(connectionString))
            {
                string query = "SELECT * FROM parkingdb.Abonnement where idAbonnement=@idAbonnement";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idAbonnement", SqlDbType.Int).Value = id;

                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        var affectationrepository = new AffectationabonnementRepository();
                        while (await sdr.ReadAsync())
                        {
                            Abonnement = new Abonnement
                            {
                                NomAbonnement = Convert.ToString(sdr["NomAbonnement"]),
                                PeriodeAbonnement = Convert.ToString(sdr["PeriodeAbonnement"]),
                                Montant = Convert.ToDecimal(sdr["Montant"]),
                                IdIntervalle = Convert.ToInt32(sdr["IdIntervalle"]),
                                IdGroupeTarif = Convert.ToInt32(sdr["IdGroupeTarif"]),
                                NombrePeriode = Convert.ToInt32(sdr["NombrePeriode"]),
                                IdAbonnement = Convert.ToInt32(sdr["IdAbonnement"]),

                            };
                            Abonnement.Affectationabonnements = await affectationrepository.GetAllByAbonnementIdAsync(Abonnement.IdAbonnement);

                        }
                    }
                    con.Close();
                }
            }
            return Abonnement;
        }

        public AbonnementRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;
        }
    }
}
