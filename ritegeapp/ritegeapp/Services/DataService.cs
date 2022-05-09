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
        NetworkAccess networkAccess;
        public HubConnection hubConnection;
        public TimeSpan TokenExpiresIn = TimeSpan.FromDays(9999);
        public string Token { get; set; }
        public DataService()
        {
        }

        public async Task Initialize()
        {
            if (Initialized == false)
            {
                Initialized = true;
                await GetToken();
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
                            Token = await GetToken();

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
                hubConnection.On<DashBoardDTO>("GetDashboardData", (data) =>
                {
                    MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetDashboardData", data);

                });
                hubConnection.On<DashBoardDTO>("GetTicketData", (data) =>
                {
                    MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetTicketData", data);

                });
                hubConnection.On<DashBoardDTO>("ResetToken", async(data) =>
                {
                  await  Login();
                });
                await Connect();
                DependencyService.Get<IEventService>().StartService();

            }
        }
        public async Task Connect()
        {
            if (hubConnection is null)
            {
                await Initialize();

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
                        await Login();
                        System.Diagnostics.Debug.WriteLine("not connected " + ex);
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

            using (var dbcontext = new ApplicationDbContext())
            {
                var all = dbcontext.Token.Where(x => x.ID > 0).ToList();
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
                var tgf = t.Date.Add(TokenExpiresIn) > DateTime.UtcNow;
                if (t.Date.Add(TokenExpiresIn) > DateTime.UtcNow.AddMinutes(10))
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

            using (HttpClient httpClient = GetHttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                JsonContent content;
                try
                {
                    var login = new LoginQuery { Login = "oussama", MotDePasse = "mrissa" };
                    content = JsonContent.Create(login);
                    response = await httpClient.PostAsync(App.ServerURL + "/Utilisateur/login", content);
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
            httpclient.Timeout = TimeSpan.FromSeconds(10);
            return httpclient;
        }
        public async Task<T> GetData<T>(string DataURL, Dictionary<string, string> args)
        {
            _ = Task.Run(async () => await Connect());
            using (HttpClient httpClient = GetHttpClient())
            {
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
                        response = await httpClient.PostAsync(uri, null);
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
                            await Login();
                            return await GetData<T>(DataURL, args);
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
        public async Task<List<InfoAbonnementDTO>> GetFilteredAbonnementData(DateTime dateStart,DateTime dateEnd, string? abonneName)
        {
            //use utcnow.ticks for different timezones to work if this becomes popular worldwide

            var parameters = new Dictionary<string, string>
                {
                    { nameof(dateStart), dateStart.ToString("O") },
                    { nameof(dateEnd), dateEnd.ToString("O") },
                    { nameof(abonneName), abonneName},
                };

            var list = await GetData<List<InfoAbonnementDTO>>("/Parking/GetFilteredAbonnementData", parameters);
            return list;
        }
        public async Task<List<InfoSessionsDTO>> GetFilteredCashierData(DateTime dateStart, DateTime dateEnd, string? caissierName)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
            { nameof(caissierName), caissierName},
        };

            var list = await GetData<List<InfoSessionsDTO>>("/Parking/GetFilteredCashierData", parameters);
            return list;

        }
        public async Task<List<InfoTicketDTO>> GetFilteredTicketData(DateTime dateStart, DateTime dateEnd)
        {
            var parameters = new Dictionary<string, string>
        {
            { nameof(dateStart), dateStart.ToString("O") },
            { nameof(dateEnd), dateEnd.ToString("O") },
        };
            var list = await GetData<List<InfoTicketDTO>>("/Parking/GetFilteredTicketData", parameters);
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
            if (networkAccess == NetworkAccess.Internet)
            {
                if (hubConnection.State == HubConnectionState.Connected)
                {
                    await hubConnection.StopAsync();
                }
            }
        }
        private void SendErrorMessage(Exception ex)
        {
            if ((Application.Current as App).IsShowingAlert == false)
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CommunicationError", ex.Message);
        }
    }
}