namespace GameStore.API.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; } //O ? mostra que aceita receber nulo

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
