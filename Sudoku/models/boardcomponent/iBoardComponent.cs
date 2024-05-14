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
    }
}
