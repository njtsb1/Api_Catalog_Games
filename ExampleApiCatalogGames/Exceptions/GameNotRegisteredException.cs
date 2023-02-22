using System;

namespace ExampleApiCatalogGames.Exceptions
{
    public class GameNotRegisteredException: Exception
    {
        public GameNotRegisteredException()
            :base("This game is not registered")
        {}
    }
}
