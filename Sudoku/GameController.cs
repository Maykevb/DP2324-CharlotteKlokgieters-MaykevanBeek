using Sudoku;
using Sudoku.models.BoardComponent;
using Sudoku.renderers;

public class GameController
{
    private SudokuImporter importer;
	private SudokuBoard board;
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
		board = importer.readSudokuFromFile(type);

		if (board == null)
		{
			// TODO error file cant be found
		}
    }

	public void displayBoard(SudokuType type)
	{
		switch (type)
		{
			case SudokuType.FOUR_BY_FOUR:
				renderer.DrawBoard(board, 2, 2);
				break;
			case SudokuType.SIX_BY_SIX:
				renderer.DrawBoard(board, 3, 2);
				break;
			case SudokuType.NINE_BY_NINE:
				renderer.DrawBoard(board, 3, 3);
				break;
			case SudokuType.SAMURAI:
				//TODO
				renderer.DrawBoard(board, 3, 3);
				break;
			case SudokuType.JIGSAW:
				//TODO
				renderer.DrawBoard(board, 0, 0);
				break;
		}
		
	}

	public void solveBoard()
	{

	}

	public void fillCell(int row, int column, int value)
	{

	}
}