using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public class SamuraiRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new SamuraiRenderer();
        }
    }
}
