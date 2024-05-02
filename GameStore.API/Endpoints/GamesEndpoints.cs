using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GET_GAME_ENDPOINT_NAME = "GetGame";    

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games") //Com isso nao precisa ficar escrevendo "games" como string em cada metodo do CRUD, substituindo "app" por "group".
                       .WithParameterValidation();  //Metodo importado do MinimalApis.Extension


        //CRUD

        //(Read)  GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                     .Include(game => game.Genre) //Colocando valor no Genre
                     .Select(game => game.ToGameSummaryDto())
                     .AsNoTracking()  //Otimizacao
                     .ToListAsync()); 


        //(Read)  GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => 
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto()); //if game == null return Results.NotFound(); else Results.Ok(game)
        })
        .WithName(GET_GAME_ENDPOINT_NAME); //Dando um nome


        //(Create)  POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => 
        {
            Game game = newGame.ToEntity();            

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(GET_GAME_ENDPOINT_NAME, new {id = game.Id}, game.ToGameDetailsDto());
        });        


        //(Update)  PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => 
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id)); //Trocando os dados antigos pelo novos

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        //(Delete)  DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }

}
