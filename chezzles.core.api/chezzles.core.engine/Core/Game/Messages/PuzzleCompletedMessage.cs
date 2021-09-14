namespace chezzles.core.engine.Core.Game.Messages
{
    public class PuzzleCompletedMessage
    {
        public PuzzleCompletedMessage(bool solved)
        {
            IsSolved = solved;
        }

        public bool IsSolved { get; private set; }
    }
}
