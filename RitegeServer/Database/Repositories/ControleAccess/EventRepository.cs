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

        public async Task<int> AddAsync(DateTime DateEvent, string HeureEvent, ushort DoorNumber, ushort? UserNumber, ushort CodeEvent,ushort codeController,ushort indiceController,bool selected, ushort? Flux)
        {
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "insert into [controleaccessdb].[controleaccessdb].event(dateEvent,heureevent,doornumber,usernumber,codeevent,codecontroller,indicecontroller,selected,flux) values(@DateEvent,@HeureEvent,@DoorNumber,@UserNumber,@CodeEvent,@codecontroller,@indicecontroller,@selected,@flux)";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@DateEvent", SqlDbType.Date).Value = DateEvent;
                    cmd.Parameters.Add("@HeureEvent", SqlDbType.Time).Value = TimeSpan.Parse( HeureEvent);
                    cmd.Parameters.Add("@DoorNumber", SqlDbType.SmallInt).Value = DoorNumber;
                    cmd.Parameters.Add("@UserNumber", SqlDbType.SmallInt).Value = UserNumber;
                    cmd.Parameters.Add("@CodeEvent", SqlDbType.SmallInt).Value = CodeEvent;
                    cmd.Parameters.Add("@codeController", SqlDbType.SmallInt).Value = codeController;
                    cmd.Parameters.Add("@indiceController", SqlDbType.SmallInt).Value = indiceController;
                    cmd.Parameters.Add("@selected", SqlDbType.SmallInt).Value = selected;
                    cmd.Parameters.Add("@Flux", SqlDbType.SmallInt).Value = Flux;
                 
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                   
                    return 1;
                }
            }
        }

        public  async Task<List<Event>> GetAllByDateAndIdCaisseAndEventCodeAsync(DateTime date, int idCaisse, int eventCode)
        {
            List<Event> Events = new();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "select * from controleaccessdb.event where (dateEvent between @dateStart and @dateEnd) and DoorNumber = (select c.IdDoor from parkingdb.parkingdb.caisse c where idCaisse = @idCaisse) and codeEvent = @eventCode";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = date.Date;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = date.Date.AddDays(1).AddTicks(-1);
                    cmd.Parameters.Add("@idCaisse", SqlDbType.Int).Value = idCaisse;
                    cmd.Parameters.Add("@eventCode", SqlDbType.Int).Value = eventCode;



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
                                NumAccessCard = (sdr["NumAccessCard"] != DBNull.Value) ? Convert.ToString(sdr["NumAccessCard"]) : null,
                                Data12 = (sdr["Data12"] != DBNull.Value) ? Convert.ToInt16(sdr["Data12"]) : null,
                                Flux = Convert.ToUInt16(sdr["Flux"]),
                            }); 
                          
                        }
                    }
                    con.Close();
                }
            }
            return Events;
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

        public async Task<int> UpdateEventDateAsync()
        {
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "UPDATE controleaccessdb.Event  SET dateEvent = cast(@Date as datetime)";

                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = DateTime.Today;

                    con.Open();
                    //should be always 1
                    var affectedqueriesawait = await cmd.ExecuteNonQueryAsync();
                    con.Close();

                    return affectedqueriesawait;
                }
            }
        }



    }
}
