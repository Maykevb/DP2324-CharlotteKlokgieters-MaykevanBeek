using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public interface iSudokuComponent
    {
		void Accept(iBoardVisitor visitor, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard);

		public int Value 
		{
			get { return 0; }
			set { }
		}

		public int? Block 
		{
			get { return 0; }
			set { }
		}

		public bool IsFixed 
		{
			get { return true; }
			set { }
		}

		public bool IsCorrect 
		{
			get { return true; }
			set { }
		}

		public SudokuType Type 
		{
			get { return 0; }
			set { }
		}

		public List<iSudokuComponent> Components
		{
			get { return new List<iSudokuComponent>(); }
		}
	}
}
