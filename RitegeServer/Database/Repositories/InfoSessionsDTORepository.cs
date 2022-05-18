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
                     query = "SELECT ab.dateAffectationabonnementcol,ab.dateActivation,ab.dateDesactivation,ab.etatAffectation,a.montant,a.nomAbonnement FROM parkingdb.abonnement a, parkingdb.affectationabonnement ab where a.idAbonnement = ab.idAbonnement and DateActivation >=@start and DateDesactivation<=@finish";
                else
                    query = "SELECT ab.dateAffectationabonnementcol,ab.dateActivation,ab.dateDesactivation,ab.etatAffectation,a.montant,a.nomAbonnement FROM parkingdb.abonnement a, parkingdb.affectationabonnement ab where a.idAbonnement = ab.idAbonnement and DateActivation>=@start and DateDesactivation<=@finish and nomAbonnement like @name";
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
                            string? value = Convert.ToString(sdr["EtatAffectation"]);
                            abonnes.Add(new InfoSessionsDTO
                            {
                                //LibelleAbonnement = Convert.ToString(sdr["nomAbonnement"]),
                                //PrixAbonnement = Convert.ToDecimal(sdr["montant"]),
                                //DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                //DateFinActivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                //DateAffectation = Convert.ToDateTime(sdr["dateAffectationabonnementcol"]),
                                //Etat = (Etat)System.Enum.Parse(typeof(Etat), value),
                            });
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
