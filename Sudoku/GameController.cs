using Sudoku;

public class GameController
{
    private SudokuImporter importer;
    /*	private SudokuBoard board;
		private BoardRenderer renderer;*/
    /*	private BoardFactory boardFactory;*/

    public GameController()
	{
		this.importer = new SudokuImporter();
		/*this.boardFactory = new BoardFactory();*/
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
}