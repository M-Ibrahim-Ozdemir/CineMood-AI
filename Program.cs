using CineMoodAI.Domain.Entities;
using CineMoodAI.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection.Metadata.Ecma335;
using CineMoodAI.WebAPI.Controllers;
using CineMoodAI.Domain.Interface;
using CineMoodAI.Aplication.interfaces;
using CineMoodAI.Aplication.Services;
using CineMoodAI.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CineMoodAI.Infrastructure.Security;




var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDbContext<CineMoodAIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  //builder.Services.AddDbContext ile CineMoodAIDbContext diye bir veritaban m var sen bunu tan  uygulama ayaga kalkarken build edilirken ben bunu weapi ye soluyom tanimliyom. options olarqakta usesql server kullan sonra configuration icerisine git ordan 'defaultconnections' u al ama henuz bizim yok.yaratcaz. collectionstrimng yaratcaz. connectionstring veritaban  erisim . sql sunucu adresi. hangi veritabanu, uhanf  user ve password ile baglant y  yaz lcak. bunun icin once sqle gitmek lazi 
builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CineMoodAIDbContext>()
    .AddSignInManager();



//JWT token eklemek icin jwtbrearDefaults.Autocantication diyoz
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddAuthentication();
builder.Services.AddScoped<ITokenService, TokenService>();






builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //once interface sonra unun impmentasyonunu yazdık. yani ırepository interfacesine ıhtiyac duyanlara reposytory ver diykz
builder.Services.AddScoped<IRecommendationService, RecommendationService>();//IRecommendationService bekleyenlere de RecommendationService ver dedik
builder.Services.AddScoped<IAIService, OpenAIService>(); //bu da ole

//resos orgın seris guvenlik önlemidir. yai benim backand kodunu sadcece benim domainim cagırabikir. baska domaiın olmaz
//bu api yi yayınladıgımda ben hangi originlere izin verirsem biz allowanyogrin yani herkes cagısaribsin dedik. Fakat bylşe bırakmamaız laızm. yayınladıgımız er nereyse mseleöa selcukcırıt.com arayuzu o domainde bulunan frontend   bu backandi cagırabirlsim sacede icine selcukcirit.com yazılmalı
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });


});



var app = builder.Build();  //buildden once bu yualrda ki 3 deneyi vermek lazım 

app.UseCors("AllowAll");
app.MapDefaultEndpoints();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //sadece decelopment yabni product ta cal smasn  istiyosam if d s na c kar lmal 
{
    app.MapOpenApi(); //gidiyo arka planda bizim endpointleri contooleri map edip bir openapi standartlarda dokumantasyon olusturur. Swager, scaller o kutuphanelrde bunu kulln r
    app.MapScalarApiReference(opt =>
    {
        opt
            .WithTitle("Test Api")  //hagi basl k , tema falan ayar 
            .WithTheme(ScalarTheme.Default)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });


}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMovieEndpoints();

app.Run();
