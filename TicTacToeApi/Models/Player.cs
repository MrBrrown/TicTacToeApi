using System;
namespace TicTacToeApi.Models
{
	public class Player
	{
		public int PlayerId { get; set; }

		public string PlayerChar { get; set; } = null!;

        public bool IsWin { get; set; } = false;

		public bool CheckWin(Board board)
		{
            bool isWin = false;

            if (board.Cell_1 == PlayerChar)     //первая строка/главная диагональ/первый столбец
                if ((board.Cell_2 == PlayerChar && board.Cell_3 == PlayerChar) || (board.Cell_5 == PlayerChar && board.Cell_9 == PlayerChar) || (board.Cell_4 == PlayerChar && board.Cell_7 == PlayerChar))
                    isWin = true;
            if (board.Cell_2 == PlayerChar)      //второй столбец
                if (board.Cell_5 == PlayerChar && board.Cell_8 == PlayerChar)
                    isWin = true;
            if (board.Cell_3 == PlayerChar)     //третий столбец/побочная диагональ
                if ((board.Cell_6 == PlayerChar && board.Cell_9 == PlayerChar) || (board.Cell_5 == PlayerChar && board.Cell_7 == PlayerChar))
                    isWin = true;
            if (board.Cell_4 == PlayerChar)     //вторая строка
                if (board.Cell_5 == PlayerChar && board.Cell_6 == PlayerChar)
                    isWin = true;
            if (board.Cell_7 == PlayerChar)     //третья строка
                if (board.Cell_8 == PlayerChar && board.Cell_9 == PlayerChar)
                    isWin = true;

            IsWin = isWin;

            return isWin;
        }
    }
}

