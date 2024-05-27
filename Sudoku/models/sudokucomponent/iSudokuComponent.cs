using Sudoku.models.visitors;

namespace Sudoku.models.BoardComponent
{
	public interface iSudokuComponent
    {
        void Accept(iBoardVisitor visitor);

		public int Value //TODO
		{
			get { return 0; }
		}

		public int? Block //TODO
		{
			get { return 0; }
		}

		public List<iSudokuComponent> Components
		{
			get { return new List<iSudokuComponent>(); }
		}
	}
}
