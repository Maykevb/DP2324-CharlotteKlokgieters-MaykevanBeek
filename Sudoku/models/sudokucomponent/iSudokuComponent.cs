using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public interface iSudokuComponent
    {
        void Accept(iBoardVisitor visitor);

		public int Value //TODO
		{
			get { return 0; }
			set { }
		}

		public int? Block //TODO
		{
			get { return 0; }
			set { }
		}

		public bool IsFixed //TODO
		{
			get { return true; }
			set { }
		}

		public List<iSudokuComponent> Components
		{
			get { return new List<iSudokuComponent>(); }
		}
	}
}
