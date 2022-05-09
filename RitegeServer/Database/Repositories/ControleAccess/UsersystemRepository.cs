using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class UsersystemRepository : GenericRepository<Usersystem>, IUsersystemRepository
    {
        private string connectionString;
        public UsersystemRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;
        }

        public async Task<List<Usersystem>> GetAllAsync()
        {
            List<Usersystem> Usersystems = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Usersystem";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Usersystems.Add(new Usersystem
                            {
                                UserCode = Convert.ToInt16(sdr["UserCode"]),
                                UserApplication = Convert.ToInt16(sdr["UserApplication"]),
                                CodeProfil = Convert.ToInt16(sdr["CodeProfil"]),
                                FirstName = Convert.ToString(sdr["FirstName"]),
                                LastName = Convert.ToString(sdr["LastName"]),
                                Icn = Convert.ToString(sdr["Icn"]),
                                Tel1 = Convert.ToString(sdr["Tel1"]),
                                Tel2 = Convert.ToString(sdr["Tel2"]),
                                Email = Convert.ToString(sdr["Email"]),
                                Departement = Convert.ToString(sdr["Departement"]),
                                Fonction = Convert.ToString(sdr["Fonction"]),
                                Alias = Convert.ToString(sdr["Alias"]),
                                Adresse = Convert.ToString(sdr["Adresse"]),
                                PinCode = Convert.ToString(sdr["PinCode"]),
                                AccessMode = Convert.ToString(sdr["AccessMode"]),
                                Picture = Convert.ToString(sdr["Picture"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Active = Convert.ToBoolean(sdr["Active"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                                ActiveDate = Convert.ToBoolean(sdr["ActiveDate"]),
                                SiteAccessCard = Convert.ToBoolean(sdr["SiteAccessCard"]),
                                StartValidateDate = Convert.ToDateTime(sdr["StartValidateDate"]),
                                EndValidateDate = Convert.ToDateTime(sdr["EndValidateDate"]),
                                ControllerSyncStatus = Convert.ToInt32(sdr["ControllerSyncStatus"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Usersystems;
        }

        public async Task<Usersystem> GetOneByIdAsync(long id)
        {
            Usersystem Usersystem = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Usersystem where UserCode=@UserCode";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@UserCode", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Usersystem = new Usersystem
                            {

                                UserCode = Convert.ToInt16(sdr["UserCode"]),
                                UserApplication = Convert.ToInt16(sdr["UserApplication"]),
                                CodeProfil = Convert.ToInt16(sdr["CodeProfil"]),
                                FirstName = Convert.ToString(sdr["FirstName"]),
                                LastName = Convert.ToString(sdr["LastName"]),
                                Icn = Convert.ToString(sdr["Icn"]),
                                Tel1 = Convert.ToString(sdr["Tel1"]),
                                Tel2 = Convert.ToString(sdr["Tel2"]),
                                Email = Convert.ToString(sdr["Email"]),
                                Departement = Convert.ToString(sdr["Departement"]),
                                Fonction = Convert.ToString(sdr["Fonction"]),
                                Alias = Convert.ToString(sdr["Alias"]),
                                Adresse = Convert.ToString(sdr["Adresse"]),
                                PinCode = Convert.ToString(sdr["PinCode"]),
                                AccessMode = Convert.ToString(sdr["AccessMode"]),
                                Picture = Convert.ToString(sdr["Picture"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Active = Convert.ToBoolean(sdr["Active"]),
                                Sync = Convert.ToBoolean(sdr["Sync"]),
                                ActiveDate = Convert.ToBoolean(sdr["ActiveDate"]),
                                SiteAccessCard = Convert.ToBoolean(sdr["SiteAccessCard"]),
                                StartValidateDate = Convert.ToDateTime(sdr["StartValidateDate"]),
                                EndValidateDate = Convert.ToDateTime(sdr["EndValidateDate"]),
                                ControllerSyncStatus = Convert.ToInt32(sdr["ControllerSyncStatus"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Usersystem;
        }




    }
}
