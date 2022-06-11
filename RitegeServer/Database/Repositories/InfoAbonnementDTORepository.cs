using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class InfoAbonnementDTORepository : GenericRepository<InfoAbonnementDTO>, IInfoAbonnementDTORepository
    {
        private string connectionString;
        public InfoAbonnementDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        public async Task<IEnumerable<InfoAbonnementDTO?>> GetAllByNameAndDatesAsync(string? name, DateTime start, DateTime finish)
        {
            List<InfoAbonnementDTO> abonnes = new List<InfoAbonnementDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                if (string.IsNullOrEmpty(name))
                     query = "SELECT ab.dateAffectationabonnementcol," +
                        "ab.dateActivation," +
                        "ab.dateDesactivation," +
                        "ab.etatAffectation," +
                        "a.montant," + "nom,prenom ," +

                        "a.nomAbonnement," +
                        "pa.periodeAbonnement " +
                        "FROM parkingdb.abonnement a,parkingdb.affectationabonnement ab, parkingdb.periodeAbonnement pa,parkingdb.abonne" +
                        " where" +
                        " a.idAbonnement = ab.idAbonnement and" +
                        " pa.ordre = a.periodeAbonnement and " + 
                        "DateActivation >=@start and DateDesactivation<=@finish";
                else
                    query = "SELECT ab.dateAffectationabonnementcol," +
                        "ab.dateActivation," +
                        "ab.dateDesactivation," +
                        "ab.etatAffectation," +
                        "a.montant," +
                        "nom,prenom,"+
                        "a.nomAbonnement," +
                        "pa.periodeAbonnement " +
                        "FROM parkingdb.abonnement a,parkingdb.affectationabonnement ab, parkingdb.periodeAbonnement pa,parkingdb.abonne" +
                        " where" +
                        " a.idAbonnement = ab.idAbonnement and" +
                        " pa.ordre = a.periodeAbonnement and " +
                        "DateActivation>=@start" +
                        " and DateDesactivation<=@finish " +
                        "and nomAbonnement like @name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    if (!string.IsNullOrEmpty(name))
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = "%" + name + "%";
                    cmd.Parameters.Add("@start", SqlDbType.DateTime2).Value = start;
                    cmd.Parameters.Add("@finish", SqlDbType.DateTime2).Value = finish;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            string? EtatAffectationString = Convert.ToString(sdr["EtatAffectation"]);
                            string? TypeAbonnementString = Convert.ToString(sdr["periodeAbonnement"]);
                            var abonnement = new InfoAbonnementDTO
                            {
                                LibelleAbonnement = Convert.ToString(sdr["nomAbonnement"]),
                                NomPrenomAbonne = Convert.ToString(sdr["Prenom"]) + " " + Convert.ToString(sdr["Nom"]),
                                PrixAbonnement = Convert.ToDecimal(sdr["montant"]),
                                DateActivation = Convert.ToDateTime(sdr["DateActivation"]),
                                DateFinActivation = Convert.ToDateTime(sdr["DateDesactivation"]),
                                DateAffectation = Convert.ToDateTime(sdr["dateAffectationabonnementcol"]),
                                
                            };
                            if (EtatAffectationString is not null)
                            {
                                abonnement.Etat = (Etat)System.Enum.Parse(typeof(Etat), EtatAffectationString);
                            }
                            if (TypeAbonnementString is not null)
                            {
                                abonnement.TypeAbonnement = (TypeAbonnementEnum)System.Enum.Parse(typeof(TypeAbonnementEnum), TypeAbonnementString);
                            }
                           
                        
                            abonnes.Add(abonnement);
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;

        }

      
    }
}
