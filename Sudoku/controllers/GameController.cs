using Sudoku;
using Sudoku.models.states;
using Sudoku.models.SudokuComponent;
using Sudoku.renderers;

public class GameController
{
	private static readonly int SQUARE_4X4 = 2;
	private static readonly int LENGTH_6X6 = 3;
	private static readonly int HEIGHT_6X6 = 2;
	private static readonly int SQUARE_9X9 = 3;

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
	public void startGame(SudokuType selectedType)
	{
        AddRenderers();
        LoadRenderer(selectedType);
        LoadBoard(selectedType);
        DrawStart();
        DisplayBoard(selectedType);
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

	public void AddRenderers()
	{
        boardFactory.addRenderType("JIGSAW", new JigsawRenderer());
        boardFactory.addRenderType("SAMURAI", new SamuraiRenderer());
        boardFactory.addRenderType("STANDARD", new StandardRenderer());
    }

	public void LoadBoard(SudokuType type)
	{
		board = importer.ReadSudokuFromFile(type, this);
		
		if (board == null)
		{
			Console.WriteLine("An error occurred. Please restart the application.");
		}
	}

    public void DisplayBoard(SudokuType type)
	{
		CheckValuesPlacement(type);

		switch (type)
		{
			case SudokuType.FOUR_BY_FOUR:
				board.State.DisplayBoard(renderer, board, SQUARE_4X4, SQUARE_4X4);
				break;
			case SudokuType.SIX_BY_SIX:
                board.State.DisplayBoard(renderer, board, LENGTH_6X6, HEIGHT_6X6);
				break;
			case SudokuType.NINE_BY_NINE:
            case SudokuType.SAMURAI:
            case SudokuType.JIGSAW:
                board.State.DisplayBoard(renderer, board, SQUARE_9X9, SQUARE_9X9);
				break;
		}

		board.State.DoAction(board, this);
    }

	private void CheckValuesPlacement(SudokuType type)
	{
		if (board.State is CorrectionState)
		{
			CorrectionState boardState = (CorrectionState)board.State;

			switch (type)
			{
				case SudokuType.SAMURAI:
					boardState.VisitVisitorsSamurai(board);
					break;
				case SudokuType.JIGSAW:
					boardState.VisitVisitors(board);
					break;
				default:
					boardState.VisitVisitors(board);
					break;
			}
		}
	}

	public void DrawStart()
	{
		string line = new string('-', START_LINE_LENGTH);
		Console.WriteLine($"\n{line}\nLet the game begin!\n{line}");

		board.State.PrintState();
	}
}