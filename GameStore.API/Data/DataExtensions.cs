using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app) //Toda vez que a aplicacao inicia, o banco atualiza
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
