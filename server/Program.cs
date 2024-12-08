using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using server.Data;
using server.Mapping;
using server.Repositories.Classes;
using server.Repositories.Interfaces;
using server.Services;
using server.Services.Classes;
using server.Services.Interfaces;
using server.SignalRHub;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtVal = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtVal["Key"]);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>(); 

builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IAuctionResultRepository, AuctionResultRepository>();
builder.Services.AddScoped<IAuctionResultService, AuctionResultService>();
builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>(); 
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddHostedService<LiveAuctionUpdation>();
builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddMvc()
//        .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtVal["Issuer"],
        ValidAudience = jwtVal["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

    };
});


builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200")
            .AllowCredentials()));

// Allowing frontend to access
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();

//    });
//});


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://yourfrontenddomain.com") // Specify allowed origins
              .AllowAnyHeader()
        .AllowAnyMethod()
              .WithExposedHeaders("Authorization", "Referer")
              .AllowCredentials(); // Allow cookies or credentials if needed
    });
});

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", j => j.RequireRole("Admin"));
    options.AddPolicy("AuctioneerOnly", j => j.RequireRole("Auctioneer"));
    options.AddPolicy("TeamManagerOnly", j => j.RequireRole("Team Manager"));
    options.AddPolicy("PlayerAgentOnly", j => j.RequireRole("Player Agent"));
    options.AddPolicy("AnalystOnly", j => j.RequireRole("Analyst"));
    options.AddPolicy("ViewerOnly", j => j.RequireRole("Viewer"));
    options.AddPolicy("All", j => j.RequireRole(["Admin", "Team Manager", "Player Agent", "Viewer", "Analyst", "Auctioneer"]));
});


// DB CONTEXT

builder.Services.AddDbContext<SportsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ------------------------------------------ END OF SERVICES ------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}







app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapHub<AuctionHub>("/auctionHub");
//app.MapHub<AuctionHub>("/api/auction/create");
//app.UseCors(policy =>
//{
//    policy.WithOrigins("http://localhost:4200", "https://yourfrontenddomain.com")
//          .AllowAnyHeader()
//          .AllowAnyMethod()
//          .AllowCredentials();
//    //policy.AllowAnyOrigin();
//});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
