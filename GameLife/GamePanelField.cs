namespace GameLife
{
    sealed class GamePanelField : MainPanel
    {
        public override int Width { get; set; }
        public override int Height { get; set; }
        public GamePanelField(OutputMatrix output) : base(output)
        {

        }
    }
}
