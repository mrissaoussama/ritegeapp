global using RitegeDomain.Database.IRepositories;
global using RitegeDomain.Database.Repositories;using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

using RitegeServer.Hubs;
using RitegeServer.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSignalR();
builder.Services.AddSignalRCore();
builder.Services.AddSingleton<IUserIdProvider, JWTUserIdProvider>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
builder.Services.AddRouting();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IInfoAbonnementDTORepository, InfoAbonnementDTORepository>();
builder.Services.AddTransient<IInfoSessionsDTORepository, InfoSessionsDTORepository>();
builder.Services.AddTransient<IInfoTicketDTORepository, InfoTicketDTORepository>();
builder.Services.AddTransient<IDashboardDTORepository, DashboardDTORepository>();
builder.Services.AddTransient<IEventDTORepository, EventDTORepository>();
builder.Services.AddTransient<IAbonneRepository, AbonneRepository>();
builder.Services.AddTransient<IAbonnementRepository, AbonnementRepository>();
builder.Services.AddTransient<IAffectationabonnementRepository, AffectationabonnementRepository>();
builder.Services.AddTransient<IEvenementRepository, EvenementRepository>();
builder.Services.AddTransient<ISessionRepository, SessionRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IUtilisateurRepository, UtilisateurRepository>();
builder.Services.AddTransient<IControllerRepository, ControllerRepository>();
builder.Services.AddTransient<IDoorRepository, DoorRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventtypeRepository, EventtypeRepository>();
builder.Services.AddTransient<IUsersystemRepository, UsersystemRepository>();
builder.Services.AddTransient<IBorneRepository, BorneRepository>();
builder.Services.AddTransient<ICaisseRepository, CaisseRepository>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ISocieteRepository, SocieteRepository>();
builder.Services.AddTransient<IParkingRepository, ParkingRepository>();
builder.Services.AddTransient<IQueryManager, QueryManager>();
builder.Services.AddSingleton<IMobileClientHandler, MobileClientHandler>();

builder.Services.AddControllers();
builder.Services.AddHostedService<SendDataHostedService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {ValidateLifetime=true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting(); 
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<DataHub>("Server");
});

app.Run();
