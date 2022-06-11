using Microsoft.Data.SqlClient;
using RitegeDomain.DTO;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Repositories
{
    public class EventDTORepository : GenericRepository<EventDTO>, IEventDTORepository
    {
        private string connectionString;
        public EventDTORepository()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["parkingdb"].ConnectionString;

        }
        
        public async Task<List<EventDTO>> GetAllByIdSocieteAndDateAsync(int idSociete, DateTime dateStart, DateTime dateEnd)
        {
            List<EventDTO> abonnes = new List<EventDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                    query = "SELECT [indexEvent]      ,[dateEvent]      ,[HeureEvent]      ,[DoorNumber]      ,[userNumber]      ,[codeEvent]      ,[Flux]   FROM[controleaccessdb].[controleaccessdb].[event] e where e.doornumber in(select iddoor from [controleaccessdb].[controleaccessdb].door where idparking in (select idparking from parkingdb.dbo.parking where idparking=@idSociete)) and dateEvent between @dateStart and @dateEnd";
          
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                        cmd.Parameters.Add("@idSociete", SqlDbType.Int).Value = idSociete;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = dateStart;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = dateEnd;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                     
                            var abonnement = new EventDTO
                            {
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                IndexEvent = Convert.ToInt64(sdr["IndexEvent"]),
                                HeureEvent = Convert.ToString(sdr["HeureEvent"]),
                                DoorNumber = Convert.ToUInt16(sdr["DoorNumber"]),
                                CodeEvent = Convert.ToUInt16(sdr["CodeEvent"]),
                              

                            };
                            if (sdr["UserNumber"] != DBNull.Value)
                                abonnement.UserNumber = Convert.ToUInt16(sdr["UserNumber"]);
                            if (sdr["Flux"] != DBNull.Value)
                                abonnement.Flux = Convert.ToUInt16(sdr["Flux"]);

                            abonnes.Add(abonnement);
                        }
                    }
                    con.Close();
                }
            }
            return abonnes;
        }

        public async Task<List<EventDTO>> GetAlertsByIdSocieteAndDateAsync(int idSociete, DateTime dateStart, DateTime dateEnd)
        {
            List<EventDTO> abonnes = new List<EventDTO>();
            using (SqlConnection con = new(connectionString))
            {
                string query;
                query = "SELECT [indexEvent]      ,[dateEvent]      ,[HeureEvent]      ,[DoorNumber]      ,[userNumber]      ,[codeEvent]      ,[Flux]   FROM [controleaccessdb].[controleaccessdb].[event] e where e.doornumber in(select iddoor from [controleaccessdb].[controleaccessdb].door where idparking in (select idparking from parkingdb.dbo.parking where idparking=@idSociete)) and (dateEvent between @dateStart and @dateEnd) and codeevent " + AlertString.GetAlertSqlString(); 
                System.Diagnostics.Debug.WriteLine("SELECT [indexEvent]      ,[dateEvent]      ,[HeureEvent]      ,[DoorNumber]      ,[userNumber]      ,[codeEvent]      ,[Flux]   FROM [controleaccessdb].[controleaccessdb].[event] e where e.doornumber in(select iddoor from [controleaccessdb].[controleaccessdb].door where idparking in (select idparking from parkingdb.dbo.parking where idparking=@idSociete)) and (dateEvent between @dateStart and @dateEnd) and codeevent " + AlertString.GetAlertSqlString());
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add("@idSociete", SqlDbType.Int).Value = idSociete;
                    cmd.Parameters.Add("@dateStart", SqlDbType.DateTime2).Value = dateStart.Date;
                    cmd.Parameters.Add("@dateEnd", SqlDbType.DateTime2).Value = dateEnd.Date;
                    con.Open();
                    using (SqlDataReader sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {

                            var abonnement = new EventDTO
                            {
                                DateEvent = Convert.ToDateTime(sdr["DateEvent"]),
                                IndexEvent = Convert.ToInt64(sdr["IndexEvent"]),
                                HeureEvent = Convert.ToString(sdr["HeureEvent"]),
                                DoorNumber = Convert.ToUInt16(sdr["DoorNumber"]),
                                CodeEvent = Convert.ToUInt16(sdr["CodeEvent"]),
                              

                            };
                            if (sdr["UserNumber"] != DBNull.Value)
                                abonnement.UserNumber = Convert.ToUInt16(sdr["UserNumber"]);
                            if (sdr["Flux"] != DBNull.Value)
                                abonnement.Flux = Convert.ToUInt16(sdr["Flux"]);

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
