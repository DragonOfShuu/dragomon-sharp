namespace DragoSharp.Models;

/// <summary>
/// Top-level game state container.
/// Holds the World (which holds the Player).
/// All game-flow logic lives in GameController; this is pure data.
/// </summary>
public class Game
{
    public World World { get; private set; }

    public Game(World world)
    {
        World = world;
    }
}
