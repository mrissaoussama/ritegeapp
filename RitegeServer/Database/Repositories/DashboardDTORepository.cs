using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class DashboardDTORepository : GenericRepository<DashBoardDTO>, IDashboardDTORepository
    {
        private string connectionString;
        public DashboardDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }

        public async Task<DashBoardDTO> GetByIdParkingAndIdCashRegister(int idParking, int idCaisse)
        {
            DashBoardDTO dashBoardDTO = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
              
                    query = "SELECT ab.dateAffectationabonnementcol," +
                        "ab.dateActivation," +
                        "ab.dateDesactivation," +
                        "ab.etatAffectation," +
                        "a.montant," +
                        "a.nomAbonnement," +
                        "pa.periodeAbonnement " +
                        "FROM parkingdb.abonnement a,parkingdb.affectationabonnement ab, parkingdb.periodeAbonnement pa" +
                        " where" +
                        " a.idAbonnement = ab.idAbonnement and" +
                        " pa.ordre = a.periodeAbonnement and " +
                        "DateActivation>=@start" +
                        " and DateDesactivation<=@finish " +
                        "and nomAbonnement like @name";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                        cmd.Parameters.Add("@idParking", SqlDbType.Int).Value = idParking;
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            string? EtatAffectationString = Convert.ToString(sdr["EtatAffectation"]);
                            string? TypeAbonnementString = Convert.ToString(sdr["periodeAbonnement"]);
                            var abonnement = new DashBoardDTO
                            {
                            

                            };
                         
                        }
                    }
                    con.Close();
                }
            }
            return dashBoardDTO;
        }
    }
}
