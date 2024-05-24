using Sudoku.models.visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
