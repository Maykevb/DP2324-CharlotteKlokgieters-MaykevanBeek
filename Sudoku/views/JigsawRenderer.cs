using Sudoku.models.BoardComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public class JigsawRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new JigsawRenderer();
        }

		public void DrawBoard(SudokuBoard board, int squareLength, int squareHeight)
        {
			double rowLength = Math.Sqrt(board.Cells.Count);

			DrawHorizontalSeparator(rowLength * 2, 1); //TODO
			

			for (int i = 0; i < board.Cells.Count; i++)
			{
				if ((i % rowLength == 0 || i % rowLength * 2 == 0))
				{
					Console.Write("|");
				}

				DrawCell(board.Cells[i].Value);

				if (i + 1 != board.Cells.Count && board.Cells[i].Block == board.Cells[i + 1].Block)
				{
					Console.ForegroundColor = ConsoleColor.Black;
					Console.Write("|");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					Console.Write("|");
				}

				if ((i + 1) % rowLength == 0 && (i + 1) < board.Cells.Count)
				{
					Console.WriteLine();
					Console.Write("|"); //TODO


					bool same = false;
					for (int j = Convert.ToInt32(rowLength); j > 0; j--)
					{
						if ((i + Convert.ToInt32(rowLength)) <= board.Cells.Count &&
							board.Cells[i + 1 - j].Block == board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block)
						{
							if (same)
							{
								Console.ForegroundColor = ConsoleColor.Black;
								Console.Write("-");
								Console.ForegroundColor = ConsoleColor.White;
							}

							//TODO space instead of -
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write("-");
							Console.ForegroundColor = ConsoleColor.White;
							same = true;

							

							if (i + 1 != board.Cells.Count &&
								board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block !=
								board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block 
								&&
								(i + 2 + (Convert.ToInt32(rowLength) - j)) % rowLength != 0
								)
							{
								Console.Write("|");
								same = false;
							}

							if (i + 1 != board.Cells.Count &&
								board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block == board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block &&
								board.Cells[i + 1 - j].Block != board.Cells[i + 2 - j].Block
								)
							{
								Console.Write("-");
							}
						}
						else
						{
							
							Console.Write("-"); 
							same = false;

							if ((i + 1 + Convert.ToInt32(rowLength)) < board.Cells.Count && 
								board.Cells[i + 1 + (Convert.ToInt32(rowLength) - j)].Block != 
								board.Cells[i + 2 + (Convert.ToInt32(rowLength) - j)].Block)
							{
								if ((i + 1 + (Convert.ToInt32(rowLength) - j)) % rowLength == 0)
								{
									Console.Write("|");
								}
								else if ((i + 2 + (Convert.ToInt32(rowLength) - j)) % rowLength != 0)
								{
									Console.Write("|");
								}
							}
							else
							{
								if (i + 2 + (Convert.ToInt32(rowLength) - j) < board.Cells.Count)
								{
									Console.Write("-"); 
								}

							}
						}
					}

					Console.WriteLine("|"); //TODO
				}
			}

			DrawHorizontalSeparator(rowLength * 2, 1); //TODO
		}

		private void DrawLineRow(int length)
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write("-");
			}
		}

		private void DrawEmptyRow(int length)
		{
			for (int j = 0; j < length; j++)
			{
				Console.Write(" ");
			}
		}

		public void DrawHorizontalSeparator(double rowLength, int extras)
		{
			Console.WriteLine("\n" + new string('-', (int)(rowLength + extras)));
		}

		public void DrawCell(int value)
		{
			Console.Write(value == 0 ? "0" : value.ToString()); //TODO 0 to space
		}
	}
}
