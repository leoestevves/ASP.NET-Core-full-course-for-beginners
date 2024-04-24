using GameStore.API.Data;
using GameStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source=GameStore.db"; //Local que vai ser criado o db
builder.Services.AddSqlite<GameStoreContext>(connString);


var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
