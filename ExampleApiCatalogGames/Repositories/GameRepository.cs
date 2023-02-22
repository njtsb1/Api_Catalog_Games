﻿using ExampleApiCatalogGames.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApiCatalogGames.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Street Fighter II", Producer = "Capcom", Price = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Mortal Kombat", Producer = "Midway", Price = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Choplifter III", Producer = "Super NES", Price = 180} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Name = "Need for Speed", Producer = "EA", Price = 170} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "The Lion King", Producer = "Super NES", Price = 80} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Fifa 23", Producer = "EA", Price = 190} }
        };

        public Task<List<Game>> Get(int page, int amount)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * amount).Take(amount).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task<List<Game>> GetWithoutLambda(string name, string producer)
        {
            var return = new List<Game>();

            foreach(var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Producer.Equals(producer))
                    return.Add(game);
            }

            return Task.FromResult(return);
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Close connection with the bank
        }
    }
}
