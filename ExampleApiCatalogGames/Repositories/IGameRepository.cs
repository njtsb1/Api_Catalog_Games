using ExampleApiCatalogGames.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleApiCatalogGames.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int amount);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}
