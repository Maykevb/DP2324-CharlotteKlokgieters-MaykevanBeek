using Sudoku.models.visitors;

namespace Sudoku.models.BoardComponent
{
	public interface iBoardComponent
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

		public List<iBoardComponent> Components
		{
			get { return new List<iBoardComponent>(); }
		}
	}
}
