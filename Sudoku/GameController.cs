using Sudoku;
using Sudoku.models.BoardComponent;
using Sudoku.renderers;

public class GameController
{
	private static readonly int SQUARE_4X4 = 2;
	private static readonly int LENGTH_6X6 = 3;
	private static readonly int HEIGHT_6X6 = 2;
	private static readonly int SQUARE_9X9 = 3;
	private static readonly int SQUARE_JIGSAW = 0;

	public static readonly int START_LINE_LENGTH = 70;

	private SudokuImporter importer;
	private SudokuGroup board;
	private iBoardRenderer renderer;
	private BoardFactory boardFactory;

	public GameController()
	{
		this.importer = new SudokuImporter();
		this.boardFactory = new BoardFactory();
	}

	public void loadRenderer(SudokuType name)
	{
		string rendererName = name.ToString();

		if (name == SudokuType.FOUR_BY_FOUR || name == SudokuType.SIX_BY_SIX || name == SudokuType.NINE_BY_NINE)
		{
            rendererName = "STANDARD";
		}

		renderer = boardFactory.createRenderer(rendererName);
	}

	public void loadBoard(SudokuType type)
	{
		board = importer.ReadSudokuFromFile(type);

		if (board == null)
		{
			//TODO error file cant be found
		}
    }

	public void displayBoard(SudokuType type)
	{
        switch (type)
		{
			case SudokuType.FOUR_BY_FOUR:
				renderer.DrawBoard(board, SQUARE_4X4, SQUARE_4X4);
				break;
			case SudokuType.SIX_BY_SIX:
				renderer.DrawBoard(board, LENGTH_6X6, HEIGHT_6X6);
				break;
			case SudokuType.NINE_BY_NINE:
				renderer.DrawBoard(board, SQUARE_9X9, SQUARE_9X9);
				break;
			case SudokuType.SAMURAI:
				renderer.DrawBoard(board, SQUARE_9X9, SQUARE_9X9);
				break;
			case SudokuType.JIGSAW:
				renderer.DrawBoard(board, SQUARE_JIGSAW, SQUARE_JIGSAW);
				break;
		}

		board.State.doAction(board);
    }

	public void drawStart()
	{
        string line = new string('-', START_LINE_LENGTH);
        Console.WriteLine($"\n{line}\nLet the game begin!\n{line}");

        board.State.Handle();
    }

    public void solveBoard()
	{

	}

	public void fillCell(int row, int column, int value)
	{

	}
}