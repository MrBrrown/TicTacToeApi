using System;
namespace TicTacToeApi.Models
{
	public class Board
	{
        public int BoardId { get; set; }

        public string? Cell_1 { get; set; } = null;

        public string? Cell_2 { get; set; } = null;

        public string? Cell_3 { get; set; } = null;

        public string? Cell_4 { get; set; } = null;

        public string? Cell_5 { get; set; } = null;

        public string? Cell_6 { get; set; } = null;

        public string? Cell_7 { get; set; } = null;

        public string? Cell_8 { get; set; } = null;
        
        public string? Cell_9 { get; set; } = null;

        public bool FillCell(int move, string moveChar)
        {
            switch (move)
            {
                case 1:
                    if (Cell_1 == null)
                    {
                        Cell_1 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 2:
                    if (Cell_2 == null)
                    {
                        Cell_2 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 3:
                    if (Cell_3 == null)
                    {
                        Cell_3 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 4:
                    if (Cell_4 == null)
                    {
                        Cell_4 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 5:
                    if (Cell_5 == null)
                    {
                        Cell_5 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 6:
                    if (Cell_6 == null)
                    {
                        Cell_6 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 7:
                    if (Cell_7 == null)
                    {
                        Cell_7 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 8:
                    if (Cell_8 == null)
                    {
                        Cell_8 = moveChar;
                        return true;
                    }
                    else
                        return false;
                case 9:
                    if (Cell_9 == null)
                    {
                        Cell_9 = moveChar;
                        return true;
                    }
                    else
                        return false;
                default:
                    return false;
            }
        }

        public bool CheckFreeCells()
        {
            if (Cell_1 != null && Cell_2 != null && Cell_3 != null &&
                Cell_4 != null && Cell_5 != null && Cell_6 != null &&
                Cell_7 != null && Cell_8 != null && Cell_9 != null)
                return false;
            else
                return true;
        }

    }
}

