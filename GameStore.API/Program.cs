using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GET_GAME_ENDPOINT_NAME = "GetGame";

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

//CRUD

//(Read)  GET /games
app.MapGet("games", () => games);


//(Read)  GET /games/1
app.MapGet("games/{id}", (int id) => 
{
    var game = games.Find(game => game.Id == id);    

    return game is null ? Results.NotFound() : Results.Ok(game); //if game == null return results.NotFound(); else Results.Ok(game)
})
.WithName(GET_GAME_ENDPOINT_NAME); //Dando um nome


//(Create)  POST /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new
    (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new {id = game.Id}, game);
});


//(Update)  PUT /games/1
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => 
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto
    (
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});


//(Delete)  DELETE /games/1
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
