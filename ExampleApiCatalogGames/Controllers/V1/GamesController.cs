using ExampleApiCatalogGames.Exceptions;
using ExampleApiCatalogGames.InputModel;
using ExampleApiCatalogGames.Services;
using ExampleApiCatalogGames.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApiCatalogGames.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Search all games in a paginated way
        /// </summary>
        /// <remarks>
        /// Unable to return games without paging
        /// </remarks>
        /// <param name="page">Indicates which page is being consulted. Minimum 1</param>
        /// <param name="amount">Indicates the number of records per page. Minimum 1 and maximum 50</param>
        /// <response code="200">Returns the game list</response>
        /// <response code="204">If there are no games</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.Get(page, amount);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// search for a game by its id
        /// </summary>
        /// <param name="idGame">Searched game id</param>
        /// <response code="200">Returns the filtered game</response>
        /// <response code="204">If there is no game with this id</response>   
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid idGame)
        {
            var game = await _gameService.Get(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Insert a game into the catalog
        /// </summary>
        /// <param name="gameInputModel">Game data to be entered</param>
        /// <response code="200">If the game is inserted successfully</response>
        /// <response code="422">If there is already a game with the same name for the same producer</response>   
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.Insert(gameInputModel);

                return Ok(game);
            }
            catch (GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("A game with this name already exists for this developer");
            }
        }

        /// <summary>
        /// Update a game in the catalog
        /// </summary>
        /// /// <param name="idGame">Game id to be updated</param>
        /// <param name="gameInputModel">New data to update the indicated game</param>
        /// <response code="200">If the game is successfully updated</response>
        /// <response code="404">If there is no game with this id</response>   
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist");
            }
        }

        /// <summary>
        /// Update the price of a game
        /// </summary>
        /// /// <param name="idGame">Game id to be updated</param>
        /// <param name="price">New game price</param>
        /// <response code="200">If the price is updated successfully</response>
        /// <response code="404">If there is no game with this id</response>   
        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist");
            }
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        /// /// <param name="idGame">Game id to delete</param>
        /// <response code="200">If the price is updated successfully</response>
        /// <response code="404">If there is no game with this id</response>   
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("This game doesn't exist");
            }
        }

    }
}
