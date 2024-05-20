using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public class JigsawRenderer : iBoardRenderer
    {
        public object Clone()
        {
            return new JigsawRenderer();
        }
    }
}
