using System;
namespace TicTacToeApi.Models
{
	public class Game
	{
		public int GameId { get; set; }

		public int Player1Id { get; set; }

		public int Player2Id { get; set; }

		public int BoardId { get; set; }

		public bool IsEnd { get; set; } = false;
	}
}

