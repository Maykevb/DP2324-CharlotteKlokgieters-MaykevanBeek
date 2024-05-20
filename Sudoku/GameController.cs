using Sudoku;
using Sudoku.renderers;

public class GameController
{
    private SudokuImporter importer;
	/*private SudokuBoard board;*/
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

		if(name == SudokuType.FOUR_BY_FOUR || name == SudokuType.SIX_BY_SIX || name == SudokuType.NINE_BY_NINE)
		{
            rendererName = "STANDARD";
		}

		renderer = boardFactory.createRenderer(rendererName);
	}

	public void loadBoard(SudokuType type)
	{
		importer.readSudokuFromFile(type);
    }

	public void displayBoard()
	{

	}

	public void solveBoard()
	{

	}

	public void fillCell(int row, int column, int value)
	{

	}
}