using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using AdamXNALibrary.Controllers;
using AdamXNALibrary.Factory;
using AdamXNALibrary.Inputs; 

using Checkers.Pieces;

namespace Checkers.Players
{
    public struct ValidPieceMoves
    {
        public CheckerPiece piece;
        public List<Point> ValidMoves;
    }

    public class CheckerPlayerController:PlayerController
    {
        public CheckerPlayer CheckerPlayer;
        public List<ValidPieceMoves> ValidPieceMoveList;

        public CheckerPlayerController(ContentManager contentManager, CheckerPlayer checkerPlayer)
        {
            CheckerPlayer = checkerPlayer;
            MouseInput = (new MouseInputFactory(@"Input\MouseGame.xml", contentManager)).Create();
        }


        public override int GetInput()
        {
            if (MouseInput.IsActionPressed("Left"))
            {
                CheckerPlayer.bDrag = true;
            }

            return 0;
        }

        public override void Update(GameTime time)
        {
            MouseInput.Update();
        }
    }
}