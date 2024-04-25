using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore"); //Local que vai ser criado o db
builder.Services.AddSqlite<GameStoreContext>(connString);


var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDb(); //Invocando o metodo

app.Run();
