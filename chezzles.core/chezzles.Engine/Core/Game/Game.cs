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
        private IEnumerable<MoveEntry> moves;
        private IEnumerator<MoveEntry> movesEnumerator;
        private IMessenger messenger = Messenger.Default;
        private bool completed;

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
            var correctMove = move.Color == PieceColor.White ? NextMoveEntry.WhiteMove : NextMoveEntry.BlackMove;
            if (correctMove == move)
            {
                // we just made blacks move. Time to grab next pair;
                if (move.Color == PieceColor.Black)
                {
                    this.movesEnumerator.MoveNext();
                }

                this.MakeNextMove(move.Color);
            }
            else
            {
                // revert last move
                this.Board.MakeMove(move.TargetSquare, move.OriginalSquare, false);

                if (this.PuzzleFailed != null)
                {
                    this.PuzzleFailed(this.Board, this.movesEnumerator.Current.WhiteMove);
                }

                messenger.Send<PuzzleFailedMessage>(new PuzzleFailedMessage());
            }
        }

        private void MakeNextMove(PieceColor prevColor)
        {
            var nextMove = prevColor == PieceColor.White ? this.NextMoveEntry.BlackMove : this.NextMoveEntry.WhiteMove;
            if (!this.movesEnumerator.Current.IsGameEnd && nextMove != null)
            {
                this.Board.MakeMove(nextMove);

                // black just moved
                if (prevColor == PieceColor.White)
                {
                    this.movesEnumerator.MoveNext();
                }
            }
            else
            {
                if (this.PuzzleSolved != null)
                {
                    this.PuzzleSolved(this.Board, this.movesEnumerator.Current.WhiteMove);
                }

                this.completed = true;
                messenger.Send<PuzzleSolvedMessage>(new PuzzleSolvedMessage());
            }
        }

        public IEnumerable<MoveEntry> MoveEntries
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

        public MoveEntry NextMoveEntry
        {
            get
            {
                return this.movesEnumerator.Current;
            }
        }
    }
}
