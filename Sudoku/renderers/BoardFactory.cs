using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.renderers
{
    public class BoardFactory
    {
        private Dictionary<String, iBoardRenderer> prototypes = new Dictionary<String, iBoardRenderer>();

        public BoardFactory() {
            addRenderType("JIGSAW", new JigsawRenderer());
            addRenderType("SAMURAI", new SamuraiRenderer());
            addRenderType("STANDARD", new StandardRenderer());
        }   
       

        public void addRenderType(String name, iBoardRenderer renderer)
        {
            prototypes[name] = renderer;
        }

        public iBoardRenderer createRenderer(String name)
        {
            return (iBoardRenderer)prototypes[name].Clone();
        }

        public Dictionary<String, iBoardRenderer> Prototypes
        {
            get { return prototypes; }
        }
    }
}
