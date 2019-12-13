namespace GameLife
{
    abstract class MainPanel : Panel
    {
        public MainPanel(OutputMatrix output) : base(output)
        {

        }
        public virtual void SetChar(int x, int y, char c) => output.SetChar(x, y, c);
    }
}
