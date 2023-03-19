using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Data;
using TicTacToeApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly ApiContext _context;

        public GameController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost ("Create Game")]
        public JsonResult CreateGame(int gameId, int player1Id, int player2Id)
        {
            if (player1Id == player2Id)
                return new JsonResult(BadRequest("Players ID must be unique"));

            var board = new Board()
            {
                BoardId = new Random().Next(0, 1000)
            };
            _context.Boards.Add(board);

            if (_context.Players.Find(player1Id) == null)
            {
                var player = new Player() { PlayerId = player1Id, PlayerChar = "X" };
                _context.Players.Add(player);
            }
            else
            {
                _context.Players.Find(player1Id).PlayerChar = "X";
                _context.Players.Find(player1Id).IsWin = false;
            }

            if (_context.Players.Find(player2Id) == null)
            {
                var player = new Player() { PlayerId = player2Id, PlayerChar = "O" };
                _context.Players.Add(player);
            }
            else
            {
                _context.Players.Find(player2Id).PlayerChar = "O";
                _context.Players.Find(player2Id).IsWin = false;
            }

            if (_context.Games.Find(gameId) == null)
            {
                var game = new Game() { GameId = gameId, Player1Id = player1Id, Player2Id = player2Id, BoardId = board.BoardId };
                _context.Games.Add(game);
            }
            else
            {
                _context.Games.Find(gameId).Player1Id = player1Id;
                _context.Games.Find(gameId).Player2Id = player2Id;
                _context.Games.Find(gameId).BoardId = board.BoardId;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(_context.Games.Find(gameId)).Value);
        }

        [HttpGet ("Get Game")]
        public JsonResult GetGame(int gameId)
        {
            if (_context.Games.Find(gameId) == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            return new JsonResult(Ok(_context.Games.Find(gameId)));
        }

        [HttpGet("Get Board")]
        public JsonResult GetBoard(int gameId)
        {
            if (_context.Games.Find(gameId) == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            return new JsonResult(Ok(_context.Boards.Find(_context.Games.Find(gameId).BoardId)));
        }

        [HttpGet("Get Player1")]
        public JsonResult GetPlayer1(int gameId)
        {
            if (_context.Games.Find(gameId) == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            return new JsonResult(Ok(_context.Players.Find(_context.Games.Find(gameId).Player1Id)));
        }

        [HttpGet("Get Player2")]
        public JsonResult GetPlayer2(int gameId)
        {
            if (_context.Games.Find(gameId) == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            return new JsonResult(Ok(_context.Players.Find(_context.Games.Find(gameId).Player2Id)));
        }

        [HttpGet("Get Winner")]
        public JsonResult GetWinner(int gameId)
        {
            var game = _context.Games.Find(gameId);

            if (game == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            if (game.IsEnd)
            {
                if (_context.Players.Find(game.Player1Id).IsWin)
                    return new JsonResult(Ok(game.Player1Id));

                if (_context.Players.Find(game.Player2Id).IsWin)
                    return new JsonResult(Ok(game.Player2Id));

                return new JsonResult(Ok("Drow!"));
            }
            return new JsonResult(BadRequest("Game dosen't end"));
        }

        [HttpPut ("Make Move")]
        public JsonResult MakeMove(int gameId, int plyerId, int cellToMove)
        {
            var game = _context.Games.Find(gameId);

            if (game == null)
                return new JsonResult(BadRequest("Game dosen't exist!"));

            if (game.Player1Id != plyerId && game.Player2Id != plyerId)
                return new JsonResult(BadRequest("Player dosen't exist"));

            if (game.IsEnd)
                return new JsonResult(BadRequest("Game end, check status!"));

            if (_context.Boards.Find(game.BoardId).FillCell(cellToMove, _context.Players.Find(plyerId).PlayerChar))
            {
                _context.SaveChanges();
                if (game.Player1Id == plyerId)
                {
                    if (_context.Players.Find(game.Player1Id).CheckWin(_context.Boards.Find(game.BoardId)))
                        game.IsEnd = true;
                }
                else
                {
                    if (_context.Players.Find(game.Player2Id).CheckWin(_context.Boards.Find(game.BoardId)))
                        game.IsEnd = true;
                }

                _context.SaveChanges();

                if (!_context.Boards.Find(game.BoardId).CheckFreeCells())
                {
                    game.IsEnd = true;
                    _context.SaveChanges();
                    return new JsonResult(BadRequest("Game end, check status!"));
                }
                return new JsonResult(Ok(_context.Boards.Find(game.BoardId)).Value);
            }  
            else
                return new JsonResult(BadRequest("Move must be in interval 1-9 and Cell must be free!"));
        }

        [HttpDelete ("Delete Game")]
        public JsonResult DeleteGame(int gameId)
        {
            var game = _context.Games.Find(gameId);
            if (game == null)
                return new JsonResult(BadRequest("Game dosen't exist"));

            _context.Players.Remove(_context.Players.Find(game.Player1Id));
            _context.Players.Remove(_context.Players.Find(game.Player2Id));
            _context.Boards.Remove(_context.Boards.Find(game.BoardId));
            _context.Games.Remove(game);

            _context.SaveChanges();

            return new JsonResult(Ok("Game removed"));
        }
    }
}

