using Microsoft.EntityFrameworkCore;
using OpenAccount.Api.Infrastructure;
using OpenAccount.Entities.Publics.SettingDto;
using OpenAccount.Repository.Infrastructure;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers(x => { x.Filters.Add<HttpResponseExceptionFilter>(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(x =>
{
	x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
	x.EnableSensitiveDataLogging();
});

SetServiceLifetime(builder.Services, "OpenAccount.Bl", "OpenAccount.BlInterface");
SetServiceLifetime(builder.Services, "OpenAccount.Repository", "OpenAccount.RepositoryInterface");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Import seetings
builder.Services.Configure<WalletSetttingDto>(builder.Configuration.GetSection("Wallet"));
builder.Services.Configure<IdpSettingDto>(builder.Configuration.GetSection("Idp"));
builder.Services.Configure<BtmsSettingDto>(builder.Configuration.GetSection("Btms"));
builder.Services.Configure<UidsSettingDto>(builder.Configuration.GetSection("Uids"));
builder.Services.Configure<DssSettingDto>(builder.Configuration.GetSection("Dss"));
builder.Services.Configure<MinIoSettingDto>(builder.Configuration.GetSection("MinIo")); 
builder.Services.Configure<CardSettingDto>(builder.Configuration.GetSection("Card"));
#endregion

static void SetServiceLifetime(IServiceCollection services, string assemblyName, string iAssemblyName)
{
	var typesToRegister = Assembly.Load(assemblyName).GetTypes()
		.Where(x => x.IsClass && !x.IsAbstract && !x.IsNested && !x.IsGenericType && x.Namespace != null)
		.Where(x => (x.Namespace == null ? "" : x.Namespace).StartsWith(assemblyName)).ToList();

	foreach (var item in typesToRegister)
		foreach (var itm in item.GetInterfaces())
			if (!itm.IsGenericType && itm.Namespace != null && itm.Namespace.StartsWith(iAssemblyName))
				services.AddScoped(itm, item);
}

var app = builder.Build();

// Migration
/*using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
