using chezzles.engine.Core.Game.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;

namespace chezzles.engine.Core.Game
{
    public delegate void PuzzleEventHandler(Board board, Move move);

    public sealed class Game
    {
        private Board board;
        private IEnumerable<Move> moves;
        private IEnumerator<Move> movesEnumerator;
        private IMessenger messenger = Messenger.Default;

        public event PuzzleEventHandler PuzzleSolved;
        public event PuzzleEventHandler PuzzleFailed;

        public Board Board
        {
            get
            {
                return this.board;
            }

            set
            {
                this.board = value;
                this.board.PieceMoved += OnPieceMoved;
            }
        }

        private void OnPieceMoved(Board board, Move move)
        {
            if (NextMove == move)
            {
                this.MakeNextMove();
            }
            else
            {
                // revert last move
                this.Board.MakeMove(move.TargetSquare, move.OriginalSquare, false);

                if (this.PuzzleFailed != null)
                {
                    this.PuzzleFailed(this.Board, this.movesEnumerator.Current);
                }

                messenger.Send<PuzzleFailedMessage>(new PuzzleFailedMessage());
            }
        }

        private void MakeNextMove()
        {
            if (this.movesEnumerator.MoveNext())
            {
                this.Board.MakeMove(this.NextMove);
            }
            else
            {
                if (this.PuzzleSolved != null)
                {
                    this.PuzzleSolved(this.Board, this.movesEnumerator.Current);
                }

                messenger.Send<PuzzleSolvedMessage>(new PuzzleSolvedMessage());
            }
        }

        public IEnumerable<Move> Moves
        {
            get
            {
                return this.moves;
            }

            set
            {
                this.moves = value;
                this.movesEnumerator = this.moves.GetEnumerator();
                this.movesEnumerator.MoveNext();
            }
        }

        public Move NextMove
        {
            get
            {
                return this.movesEnumerator.Current;
            }
        }
    }
}
