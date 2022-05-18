namespace ritegeapp.Services
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Plugin.LocalNotification;
    using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;
    using RitegeDomain.DTO;
    using RitegeDomain.Model;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    public class DataService
    {
        public bool Initialized;
        public HubConnection hubConnection;
        public HttpClient httpClient;
        public TimeSpan TokenExpiresIn = TimeSpan.FromDays(9999);
        public string? Token { get; set; }
        public DataService()
        {
          httpClient = GetHttpClient();
        }
        public async Task Initialize()
        {
            if (Initialized == false)
            {
                Initialized = true;
            await    Task.Run(async () => { await Login(); });
                hubConnection = new HubConnectionBuilder()
                    .WithUrl(App.hubConnectionURL, options =>
                    {
                        options.HttpMessageHandlerFactory = (message) =>
                        {
                            if (message is HttpClientHandler clientHandler)
                                // always verify the SSL certificate
                                clientHandler.ServerCertificateCustomValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                            return message;
                        };
                        options.AccessTokenProvider = async () =>
                        {
                            return Token;
                        };
                    }).WithAutomaticReconnect()
                    .Build();
                hubConnection.On<ParkingEvent>("AlertReceived", async (data) =>
                {
                    var coderetriever = new XmlErrorCodeStringRetriever();
                    var codestring = coderetriever.GetErrorCodeStringAndType(data);
                    //var androidoptions = new Plugin.LocalNotification.AndroidOption.AndroidOptions();
                    //androidoptions.Ongoing = true;
                    var notification = new NotificationRequest
                    {
                        //Android= androidoptions,
                        NotificationId = (Application.Current as App).NotificationID,
                        Title = "Parking Alert",
                        Description = codestring.DescriptionEvent,
                        ReturningData = "Dummy data", // Returning data when tapped on notification.

                    };
                    (Application.Current as App).NotificationID++;
                    _ = NotificationCenter.Current.Show(notification);
                });
                hubConnection.Reconnecting += error =>
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "Reconnecting");
                return Task.CompletedTask;
            };
                ListenForNewData();
                hubConnection.On<DashBoardDTO>("ResetToken", async(data) =>
                {
                  await  Login();
                });
                await Connect();
               DependencyService.Get<IEventService>().StartService();

            }
        }
        public void StopListeningForNotImportantData()
        {
            Debug.WriteLine("dashboardunsub");

            hubConnection.Remove("GetDashboardData");
            hubConnection.Remove("GetTicketData");
        }
        public void ListenForNewData()
        {
            hubConnection.On<DashBoardDTO>("GetDashboardData", (data) =>
            {
                Debug.WriteLine("dashboard data received");
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetDashboardData", data);

            });
            hubConnection.On<DashBoardDTO>("GetTicketData", (data) =>
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetTicketData", data);

            });
        }

        public async Task Connect()
        {
            if (hubConnection is null)
            {
                await Task.Run(async () => { await Initialize(); });
                return;
            }
            if ((Application.Current as App).IsOnline)
            {
                if (hubConnection.State == HubConnectionState.Disconnected)
                {
                    try
                    {
                        await hubConnection.StartAsync();
                    }
                    catch (Exception ex)
                    {
                  }
                }
                if (hubConnection.State == HubConnectionState.Connected)
                {
                }

            }
        }
        public async Task NewTokenReceived(string token)
        {
            var tokentoinsert = new NotificationToken(token);

            using (var dbcontext = new ApplicationDbContext())
            {
                {
                    await dbcontext.Database.ExecuteSqlRawAsync("delete from Token");

                    await dbcontext.Token.AddAsync(tokentoinsert);
                    Token = token;
                    await dbcontext.SaveChangesAsync();
                }

            }
        }
        public async Task<string?> GetToken()
        {
            NotificationToken? t;
            using (var dbcontext = new ApplicationDbContext())
            {
                //await dbcontext.Database.ExecuteSqlRawAsync("delete from Token");
                t = await dbcontext.Token.FirstOrDefaultAsync();
            }
            if (t is not null)
            {
                var tgf = t.Date.Add(TokenExpiresIn) > DateTime.Now;
                if (t.Date.Add(TokenExpiresIn) > DateTime.Now.AddMinutes(10))
                {
                    Token = t.Token;
                    return t.Token;
                }
                else
                {
                    Token = await Login();   
                    await NewTokenReceived(t.Token);
                    return Token;
                }
            }
            else
            {
                Token = await Login();
                return Token;
            }
        }
        public async Task<string?> Login()
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
                    { "login", "oussama" },
                    { "motdepasse", "mrissa" }
                    };
                    uri = QueryHelpers.AddQueryString(uri, parameters);

                    response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var contentr = await response.Content.ReadAsStreamAsync();
                        if (contentr != null)
                        {
                            Token = Encoding.UTF8.GetString((contentr as MemoryStream).ToArray());
                            await NewTokenReceived(Token);
                            return Token;
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
            httpclient.Timeout = TimeSpan.FromSeconds(30);
            return httpclient;
        }
        public async Task<T> GetData<T>(string DataURL, Dictionary<string, string> args)
        {
            Debug.WriteLine("GetData Called //////////////////////");

            _ = Task.Run(async () => await Connect());
            {
                var httpClient = GetHttpClient();

                HttpResponseMessage response = new HttpResponseMessage();

                string uri = (App.ServerURL + DataURL);
                System.Diagnostics.Debug.WriteLine(App.ServerURL + DataURL);
                System.Diagnostics.Debug.WriteLine("Token:"+Token);

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
                            Console.WriteLine("unauthorized" );

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
        public async Task<List<ParkingEvent>> GetEventData(DateTime date)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(date), date.ToString("O") },

            };
            var list = await GetData<List<ParkingEvent>>("/Parking/GetEventData", parameters);
            return list;
        }
        public async Task<List<ParkingEvent>> GetLast10Events()
        {
            var parameters = new Dictionary<string, string>
            {
            };
            var list = await GetData<List<ParkingEvent>>("/Parking/GetLast10Events", parameters);
            return list;
        }
        public async Task<List<ParkingEvent>> GetAlertData(DateTime date)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(date), date.ToString("O") },

            };
            var list = await GetData<List<ParkingEvent>>("/Parking/GetAlertData", parameters);
            return list;
        }
        public async Task<List<string>> GetParkingList()
        {
            var parameters = new Dictionary<string, string>
            {
                //{ nameof(c), dateStart.ToString("O") },
                //	{ nameof(dateEnd), dateEnd.ToString("O") },

            };
            var list = await GetData<List<string>>("/Parking/GetParkingList", parameters);
            return list;

        }
        public async Task<List<InfoSessionsDTO>> GetCashierData()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();

                Uri uri = new Uri(App.ServerURL + "/Parking/GetCashierData");
                try
                {
                    response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStreamAsync();

                        if (content != null)
                        {
                            var serializer = new JsonSerializer();
                            using (var sr = new System.IO.StreamReader(content))
                            using (var jsonTextReader = new JsonTextReader(sr))
                            {
                                var list = serializer.Deserialize<List<InfoSessionsDTO>>(jsonTextReader);
                                return list;
                            }
                        }
                        return null;

                    }
                    else return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error getting data " + ex.Message);
                    return null;
                }
                finally
                {
                }

            }
        }
        public async Task<List<InfoAbonnementDTO>> GetAbonnementData(DateTime dateStart,DateTime dateEnd, string? abonneName)
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
        public async Task<List<InfoSessionsDTO>> GetCashierData(DateTime dateStart, DateTime dateEnd, string? caissierName)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
            { nameof(caissierName), caissierName},
        };

            var list = await GetData<List<InfoSessionsDTO>>("/Parking/GetCashierData", parameters);
            return list;

        }
        public async Task<List<InfoTicketDTO>> GetTicketData(DateTime dateStart, DateTime dateEnd)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
        };
            var list = await GetData<List<InfoTicketDTO>>("/Parking/GetTicketData", parameters);
            return list;
        }
        public async Task<DashBoardDTO> GetDashboardData(int idparking)
        {
            var parameters = new Dictionary<string, string>
            {
                 { nameof(idparking), idparking.ToString()}
            };
            var data = await GetData<DashBoardDTO>("/Parking/GetDashboardData", parameters);
            return data;
        }
        public async Task Disconnect()
        {
          
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    await hubConnection.StopAsync();
                }
            
        }
        private void SendErrorMessage(Exception ex)
        {
            if ((Application.Current as App).IsShowingAlert == false)
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CommunicationError", ex.Message);
        }
    }
}