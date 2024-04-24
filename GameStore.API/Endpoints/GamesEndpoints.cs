using GameStore.API.Dtos;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GET_GAME_ENDPOINT_NAME = "GetGame";

    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games") //Com isso nao precisa ficar escrevendo "games" como string em cada metodo do CRUD, substituindo "app" por "group".
                       .WithParameterValidation();  //Metodo importado do MinimalApis.Extension


        //CRUD

        //(Read)  GET /games
        group.MapGet("/", () => games);


        //(Read)  GET /games/1
        group.MapGet("/{id}", (int id) => 
        {
            var game = games.Find(game => game.Id == id);    

            return game is null ? Results.NotFound() : Results.Ok(game); //if game == null return Results.NotFound(); else Results.Ok(game)
        })
        .WithName(GET_GAME_ENDPOINT_NAME); //Dando um nome


        //(Create)  POST /games
        group.MapPost("/", (CreateGameDto newGame) => {
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => 
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
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }

}
