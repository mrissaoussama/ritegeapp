namespace ritegeapp.Services
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json;
    using Plugin.LocalNotification;
    using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;
    using RitegeDomain.DTO;
    using RitegeDomain.Model;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Xamarin.Essentials;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    public class DataService : IDataService
    {
        public bool Initialized;
        public HttpClient httpClient;
        public TimeSpan TokenExpiresIn = TimeSpan.FromDays(9999);
        public DataService()
        {
            httpClient = GetHttpClient();
        }
        public async Task Initialize()
        {
            if (Initialized == false)
            {
                await GetToken();

            }
        }



        public async Task NewTokenReceived(string token)
        {
            var tokentoinsert = new NotificationToken(token);
            try
            {
                await SecureStorage.SetAsync("ClientToken", tokentoinsert.Token);
                await SecureStorage.SetAsync("ClientTokenDate", tokentoinsert.Date.ToString());

                (Application.Current as App).Token = token;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Setting Token");

            }

        }
        public async Task<string?> GetToken()
        {
            NotificationToken? t = new();
            {
                //await dbcontext.Database.ExecuteSqlRawAsync("delete from Token");
                try
                {
                    t.Token = await SecureStorage.GetAsync("ClientToken");
                    if(!string.IsNullOrEmpty(await SecureStorage.GetAsync("ClientTokenDate")))
                    t.Date = DateTime.Parse(await SecureStorage.GetAsync("ClientTokenDate"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error Getting Token");
                    //await Login();
                    return null;

                }
            }
            if (!string.IsNullOrEmpty(t.Token))
            {
                var tgf = t.Date.Add(TokenExpiresIn) > DateTime.Now;
                if (t.Date.Add(TokenExpiresIn) > DateTime.Now.AddMinutes(10))
                {
                    (Application.Current as App).Token = t.Token;
                    return t.Token;
                }
                else
                {
                    return null;

                    //(Application.Current as App).Token = await Login();
                    //await NewTokenReceived(t.Token);
                    //return (Application.Current as App).Token;
                }
            }
            else
            {
                return null;
                //(Application.Current as App).Token = await Login();
                //return (Application.Current as App).Token;
            }
        }
        public async Task<string?> Login(string email,string password)
        {
            Debug.WriteLine("Login Called //////////////////////");
            {
                HttpResponseMessage response = new HttpResponseMessage();
                JsonContent content;
                try
                {
                    string uri = (App.ServerURL + "/Parking/login");
                    var parameters = new Dictionary<string, string>
                    {
                    { "login",email},
                    { "motdepasse", password }
                    };
                    uri = QueryHelpers.AddQueryString(uri, parameters);

                    
                    response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentr = await response.Content.ReadAsStreamAsync();
                        if (contentr != null)
                        {
                            (Application.Current as App).Token = Encoding.UTF8.GetString((contentr as MemoryStream).ToArray());
                            await NewTokenReceived((Application.Current as App).Token);
                            return (Application.Current as App).Token;
                        }
                        return null;
                    }
                    else
                    {
                        var contentr = await response.Content.ReadAsStreamAsync();
                        if (contentr != null)
                        {
                            Debug.WriteLine(Encoding.UTF8.GetString((contentr as MemoryStream).ToArray()));
                            return null;
                        }
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error logging in " + ex.Message);

                    return null;

                }

            }
        }
        public HttpClient GetHttpClient()
        {

#if DEBUG
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
#else
			var handler = new HttpClientHandler(); 
#endif
            var httpclient = new HttpClient(handler);
       //     httpclient.Timeout = TimeSpan.FromSeconds(3);
            return httpclient;
        }
        public async Task<T> GetData<T>(string DataURL, Dictionary<string, string> args)
        {
            Debug.WriteLine("GetData Called //////////////////////");

            {
                var httpClient = GetHttpClient();

                HttpResponseMessage response = new HttpResponseMessage();

                string uri = (App.ServerURL + DataURL);
                System.Diagnostics.Debug.WriteLine(App.ServerURL + DataURL);
                System.Diagnostics.Debug.WriteLine("Token:" + (Application.Current as App).Token);

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken());
                try
                {
                    if (args is null)
                        response = await httpClient.GetAsync(uri);
                    else
                    {
                        uri = QueryHelpers.AddQueryString(uri, args);
                        response = await httpClient.GetAsync(uri);
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("got http response");
                        var content = await response.Content.ReadAsStreamAsync();
                        if (content != null)
                        {
                            var serializer = new JsonSerializer();
                            using (var sr = new System.IO.StreamReader(content))
                            using (var jsonTextReader = new JsonTextReader(sr))
                            {
                                var data = serializer.Deserialize<T>(jsonTextReader);
                                return data;
                            }
                        }
                        else
                        {
                            if (response.Content != null)
                                Debug.WriteLine(Encoding.UTF8.GetString((content as MemoryStream).ToArray()));


                        }
                        return default;

                    }
                    else
                    {
                        Debug.WriteLine("error http response code: " + response.StatusCode);
                        Debug.WriteLine(response.StatusCode);
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            Console.WriteLine("unauthorized");

                        }
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    //  SendErrorMessage(ex);
                    Console.WriteLine("error getting data " + ex.Message);
                    return default;
                }



            }
        }
        public async Task<List<EventDTO>> GetEventData(DateTime dateStart, DateTime dateEnd)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(dateStart), dateStart.ToString("O") },
                { nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<List<EventDTO>>("/Parking/GetEventData", parameters);
            return list;
        }
        public async Task<List<EventDTO>> GetAlertData(DateTime dateStart, DateTime dateEnd)
        {
            var parameters = new Dictionary<string, string>
            {
         { nameof(dateStart), dateStart.ToString("O") },
                { nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<List<EventDTO>>("/Parking/GetAlertData", parameters);
            return list;
        }
        public async Task<Dictionary<int, string>> GetParkingList()
        {
            Debug.WriteLine("DS GetParkingList()");

            var parameters = new Dictionary<string, string>
            {
                //{ nameof(c), dateStart.ToString("O") },
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<Dictionary<int, string>>("/Parking/GetParkingList", parameters);
            return list;

        }

        public async Task<Dictionary<int, string>> GetCashierList()
        {
            Debug.WriteLine("DS GetCashierList()");

            var parameters = new Dictionary<string, string>
            {
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<Dictionary<int, string>>("/Parking/GetCashierList", parameters);
            return list;

        }
        public async Task<Dictionary<int, string>> GetCashierListByParking(int idParking)
        {
            Debug.WriteLine("DS GetCashierList()");

            var parameters = new Dictionary<string, string>
            {
                { nameof(idParking), idParking.ToString()},
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<Dictionary<int, string>>("/Parking/GetCashierList", parameters);
            return list;

        }
        public async Task<Dictionary<int, string>> GetCashRegisterList(int idParking)
        {
            Debug.WriteLine("DS GetCashRegisterList()");

            var parameters = new Dictionary<string, string>
            {
                { nameof(idParking), idParking.ToString()},
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<Dictionary<int, string>>("/Parking/GetCashRegisterList", parameters);
            return list;

        }
        public async Task<List<InfoAbonnementDTO>> GetAbonnementData(DateTime dateStart, DateTime dateEnd, string? abonneName)
        {
            //use Now.ticks for different timezones to work if this becomes popular worldwide

            var parameters = new Dictionary<string, string>
                {
                    { nameof(dateStart), dateStart.ToString("O") },
                    { nameof(dateEnd), dateEnd.ToString("O") },
                    { nameof(abonneName), abonneName},
                };

            var list = await GetData<List<InfoAbonnementDTO>>("/Parking/GetAbonnementData", parameters);
            return list;
        }
        public async Task<List<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, int? idCaissier)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
            { nameof(idCaissier), idCaissier.ToString()},
        };

            var list = await GetData<List<InfoSessionsDTO>>("/Parking/GetCashierData", parameters);
            return list;

        }
        public async Task<List<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd, int idParking)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
            { nameof(idParking), idParking.ToString()},

        };
            var list = await GetData<List<InfoTicketDTO>>("/Parking/GetTicketData", parameters);
            return list;
        }
        public async Task<DashBoardDTO> GetDashboardData(int idParking, int idCaisse)
        {
            Debug.WriteLine("DS GetDashboardData()");
            var parameters = new Dictionary<string, string>
            {
                 { nameof(idParking), idParking.ToString()},
                 { nameof(idCaisse), idCaisse.ToString()}
            };
            var data = await GetData<DashBoardDTO>("/Parking/GetDashboardData", parameters);
            return data;
        }

        private void SendErrorMessage(Exception ex)
        {
            if ((Application.Current as App).IsShowingAlert == false)
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CommunicationError", ex.Message);
        }
        public async Task<List<ParkingEvent>> GetLast10Events()
        {
            var parameters = new Dictionary<string, string>
            {
            };
            var list = await GetData<List<ParkingEvent>>("/Parking/GetLast10Events", parameters);
            return list;
        }

        public async Task Disconnect()
        {
            (Application.Current as App).Token = null;
            SecureStorage.RemoveAll();
            try
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "LogOut");
            }
            catch (Exception e)
            {

            }
        }

        public async Task<List<DoorData>> GetDoorList(int idParking)
        {

            var parameters = new Dictionary<string, string>
            {
                { nameof(idParking), idParking.ToString()},
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<List<DoorData>>("/Parking/GetDoors", parameters);
            return list;
        }
    }
}