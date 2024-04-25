using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Action RPG"},
            new {Id = 2, Name = "Strategy"},
            new {Id = 3, Name = "Sports"},
            new {Id = 4, Name = "MMORPG"},
            new {Id = 5, Name = "Kids and Family" }
        );
    }

}
