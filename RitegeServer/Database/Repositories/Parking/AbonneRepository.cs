using Microsoft.Data.SqlClient;

using System.Data;
using RitegeDomain.Database.Entities.ParkingEntities;

namespace RitegeDomain.Database.Repositories;

public class AbonneRepository : GenericRepository<Abonne>, IAbonneRepository
{
    private string connectionString;
    public async Task<IEnumerable<Abonne>> GetAllAsync()
    {
        List<Abonne> abonnes = new List<Abonne>();
        using (SqlConnection con = new(connectionString))
        {
            string query = "SELECT * FROM parkingdb.abonne";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    var affectationrepository = new AffectationabonnementRepository();
                    while (await sdr.ReadAsync())
                    {
                        var abonne = new Abonne
                        {
                            IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                            ActiverAbonnement = Convert.ToBoolean(sdr["ActiverAbonnement"]),
                            MultiEntree = Convert.ToBoolean(sdr["MultiEntree"]),
                        };
                        abonne.Affectationabonnements = await affectationrepository.GetAllByAbonneIdAsync(abonne.IdAbonne);

                        abonnes.Add(abonne);
                    }
                }
                con.Close();
            }
        }
        return abonnes;

    }

    public async Task<Abonne> GetOneByIdAsync(long id)
    {
        Abonne abonne = new Abonne();
        using (SqlConnection con = new(connectionString))
        {
            string query = "SELECT * FROM parkingdb.abonne where idabonne=@idabonne";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Add("@idabonne", SqlDbType.Int).Value = id;

                con.Open();
                using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                {
                    var affectationrepository = new AffectationabonnementRepository();
                    while (await sdr.ReadAsync())
                    {
                        abonne = new Abonne
                        {
                            IdAbonne = Convert.ToInt32(sdr["IdAbonne"]),
                            ActiverAbonnement = Convert.ToBoolean(sdr["ActiverAbonnement"]),
                            MultiEntree = Convert.ToBoolean(sdr["MultiEntree"]),
                        };
                        abonne.Affectationabonnements = await affectationrepository.GetAllByAbonneIdAsync(abonne.IdAbonne);

                    }
                }
                con.Close();
            }
        }
        return abonne;
    }

    public AbonneRepository()
    {
        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;
    }
}
