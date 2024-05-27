using Sudoku.models.SudokuComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.models.visitors
{
    public interface iBoardVisitor
    {
        void Visit(SudokuCell cell);
        void Visit(SudokuGroup board);
    }
}
