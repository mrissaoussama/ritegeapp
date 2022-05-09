using Microsoft.Data.SqlClient;
using RitegeDomain.Database.Entities.ControleAccess;
using System.Data;

namespace RitegeDomain.Database.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private string connectionString;
        public EventRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["controleaccessdb"].ConnectionString;

        }

        public async Task<List<Event>> GetAllByDateAsync(DateTime date)
        {
            List<Event> Events = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Event where dateevent=@dateevent";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dateevent", SqlDbType.DateTime2).Value = date;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Events.Add(new Event
                            {
                                IndexEvent = Convert.ToInt64(sdr["IndexEvent"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DoorNumber = Convert.ToUInt16(sdr["DoorNumber"]),
                                UserNumber = (sdr["UserNumber"] != DBNull.Value) ? Convert.ToUInt16(sdr["UserNumber"]) : null,
                                CodeEvent = Convert.ToUInt16(sdr["CodeEvent"]),
                                CodeController = Convert.ToUInt16(sdr["CodeController"]),
                                IndiceController = Convert.ToUInt16(sdr["IndiceController"]),
                                HeureEvent = Convert.ToString(sdr["HeureEvent"]),
                                Selected = Convert.ToBoolean(sdr["Selected"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Data12 = Convert.ToInt16(sdr["Data12"]),
                                Flux = Convert.ToUInt16(sdr["Flux"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Events;
        }

        public async Task<Event> GetOneByIdAsync(long id)
        {
            Event Event = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT * FROM controleaccessdb.Event where IndexEvent=@IndexEvent";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@IndexEvent", SqlDbType.Int).Value = id;


                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            Event = new Event
                            {
                                IndexEvent = Convert.ToInt64(sdr["IndexEvent"]),
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                DoorNumber = Convert.ToUInt16(sdr["DoorNumber"]),
                                UserNumber = (sdr["UserNumber"] != DBNull.Value) ? Convert.ToUInt16(sdr["UserNumber"]) : null,
                                CodeEvent = Convert.ToUInt16(sdr["CodeEvent"]),
                                CodeController = Convert.ToUInt16(sdr["CodeController"]),
                                IndiceController = Convert.ToUInt16(sdr["IndiceController"]),
                                HeureEvent = Convert.ToString(sdr["HeureEvent"]),
                                Selected = Convert.ToBoolean(sdr["Selected"]),
                                NumAccessCard = Convert.ToString(sdr["NumAccessCard"]),
                                Data12 = Convert.ToInt16(sdr["Data12"]),
                                Flux = Convert.ToUInt16(sdr["Flux"]),
                            };
                        }
                    }
                    con.Close();
                }
            }
            return Event;
        }




    }
}
