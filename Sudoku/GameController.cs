using Sudoku;
using Sudoku.models.SudokuComponent;
using Sudoku.models.visitors;
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
	private RowVisitor rowVisitor;
	private ColumnVisitor columnVisitor;
	private SquareVisitor squareVisitor;

	public GameController()
	{
		this.importer = new SudokuImporter();
		this.boardFactory = new BoardFactory();
		this.rowVisitor = new RowVisitor();
		this.columnVisitor = new ColumnVisitor();
		this.squareVisitor = new SquareVisitor();
	}

	public void LoadRenderer(SudokuType name)
	{
		string rendererName = name.ToString();

		if (name == SudokuType.FOUR_BY_FOUR || name == SudokuType.SIX_BY_SIX || name == SudokuType.NINE_BY_NINE)
		{
            rendererName = "STANDARD";
		}

		renderer = boardFactory.createRenderer(rendererName);
	}

	public void LoadBoard(SudokuType type)
	{
		board = importer.ReadSudokuFromFile(type, this);
		
		if (board == null)
		{
			//TODO error file cant be found
		}
	}

    public void ClearConsole()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
    }

    public void DisplayBoard(SudokuType type)
	{
		SolveBoard(board, type, renderer); // TODO

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

		board.State.DoAction(board);
    }

	public void SolveBoard(SudokuGroup board, SudokuType type, iBoardRenderer renderer)
	{
		switch (type)
		{
			case SudokuType.SAMURAI:
				SolveSamurai(board);
				break;
			case SudokuType.JIGSAW:
				SolveJigsaw(board);
				break;
			default:
				SolveStandard(board, type, renderer);
				break;
		}
	}

	public void SolveStandard(SudokuGroup board, SudokuType type, iBoardRenderer renderer)
	{
		bool solved = false;

		do
		{
			board.Accept(rowVisitor);
			board.Accept(columnVisitor);
			board.Accept(squareVisitor);

			solved = board.Components.All(c => c.CorrectValue != 0);
		} 
		while (!solved);
	}
	public void SolveSamurai(SudokuGroup board)
	{
		//TODO
	}

	public void SolveJigsaw(SudokuGroup board)
	{
		//TODO
	}

	public void DrawStart()
	{
        string line = new string('-', START_LINE_LENGTH);
        Console.WriteLine($"\n{line}\nLet the game begin!\n{line}");

        board.State.PrintState();
    }

	public void FillCell(int row, int column, int value)
	{

	}
}