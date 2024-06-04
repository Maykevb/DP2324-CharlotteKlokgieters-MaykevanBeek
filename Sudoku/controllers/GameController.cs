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
	private static readonly int SUDOKUS_IN_SAMURAI = 5;

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
		
		DisplayEnd(type);
    }

	private bool CheckBoardFilled(SudokuGroup board)
	{
		int fillSamuraiCounter = 0;

		if (board.Type == SudokuType.SAMURAI)
		{
			foreach (var b in board.Components)
			{
				if (b.Components.All(c => c.Value > 0))
				{
					fillSamuraiCounter++;
				}
			}
			return (fillSamuraiCounter == SUDOKUS_IN_SAMURAI);
		}

		return board.Components.All(c => c.Value > 0);
	}

	private bool CheckBoardSolved(SudokuGroup board)
	{
		board.SwitchState(new CorrectionState());
		CorrectionState boardState = (CorrectionState)board.State;
		bool solved = false;
		int solveSamuraiCounter = 0;

		switch (board.Type)
		{
			case SudokuType.SAMURAI:
				boardState.VisitVisitorsSamurai(board);

				foreach (var b in board.Components)
				{
					if (b.Components.All(c => c.IsCorrect))
					{
						solveSamuraiCounter++;
					}
				}
				solved = (solveSamuraiCounter == SUDOKUS_IN_SAMURAI);
				break;
			default:
				boardState.VisitVisitors(board);
				solved = board.Components.All(c => c.IsCorrect);
				break;
		}

		board.SwitchState(new DefinitiveState());
		return solved;
	}

	private void CheckValuesPlacement(SudokuType type)
	{
		if (board.State is CorrectionState)
		{
			CorrectionState boardState = (CorrectionState) board.State;

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

	private void DrawEnd()
	{
		string line = new string('-', START_LINE_LENGTH);
		Console.WriteLine($"\n{line}\nCongrats, you solved the sudoku!\n{line}");
	}

	private void DisplayEnd(SudokuType type)
	{
		bool filled = CheckBoardFilled(board);
		bool solved = CheckBoardSolved(board);

		if (filled && solved)
		{
			DrawEnd();
			System.Environment.Exit(0);
		}
		else
		{
			board.State.DoAction(board, this);
		}
	}
}