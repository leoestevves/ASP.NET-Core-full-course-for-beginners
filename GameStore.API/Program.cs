using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
        1,
        "Lost Ark",
        "MMORPG",
        19.99M,
        new DateOnly(2018, 11, 18)),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)),
    new (
        3,
        "Honkai Star Rail",
        "Strategy",
        09.99M,
        new DateOnly(2023, 04, 26))    
];

app.MapGet("/", () => "Hello World!");

app.Run();
