namespace Sudoku.renderers
{
    public class BoardFactory
    {
        private Dictionary<String, iBoardRenderer> prototypes = new Dictionary<String, iBoardRenderer>();

        public BoardFactory()
        {
            //TODO: low-binding
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
    }
}
