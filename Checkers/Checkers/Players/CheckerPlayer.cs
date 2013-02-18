using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;

using AdamXNALibrary.Players;

using Checkers.Pieces;

namespace Checkers.Players
{
    public class CheckerPlayer : Player
    {
        public CheckerPlayerController CheckerPlayerController;
        public List<CheckerPiece> Pieces = new List<CheckerPiece>();
        public PIECE_COLOR Color;

        public bool bDrag = false;

        public CheckerPlayer(ContentManager contentManager, PIECE_COLOR color)
        {
            CheckerPlayerController = new CheckerPlayerController(contentManager, this);
            Color = color;
        }
    }
}
