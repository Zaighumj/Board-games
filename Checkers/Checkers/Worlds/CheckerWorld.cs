using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AdamXNALibrary.Camera;
using AdamXNALibrary.Miscellaneous;
using AdamXNALibrary.Visual;
using AdamXNALibrary.World;

using Checkers.Boards;
using Checkers.Pieces;
using Checkers.Players;

namespace Checkers.Worlds
{
    public class CheckerWorld:World
    {
        private CheckerBoard checkerBoard;
        private List<CheckerPiece> pieces = new List<CheckerPiece>();
        private Camera camera;
        private CheckerPlayer[] checkerPlayer = new CheckerPlayer[2];
        private int currentPlayerIndex;
        private CheckerPlayer currentPlayer { get { return checkerPlayer[currentPlayerIndex];} }

        public CheckerWorld()
        {
            WindowWidth = 1024;
            WindowHeight = 768;
            checkerBoard = new CheckerBoard(@"Board\board8x8");
        }

        private void SetCheckerPieces(ContentManager contentManager, ref CheckerPlayer checkerPlayer, PIECE_COLOR color, string textureName, int iStart, int iEnd)
        {
            checkerPlayer = new CheckerPlayer(contentManager, color);
            CheckerPiece piece = new CheckerPiece(color, textureName);
            piece.LoadContent(contentManager); 
            for (int i = iStart; i < iEnd; ++i)
            {
                int jStart = i & 1;
                for (int j = jStart; j < 8; j += 2)
                {
                    CheckerPiece newPiece = piece.Clone() as CheckerPiece;
                    newPiece.Position = new Point(j, i);
                    pieces.Add(newPiece);
                    checkerPlayer.Pieces.Add(piece);
                    checkerBoard.Grid[j, i].CheckerPiece = newPiece;
                    checkerPlayer.Pieces.Add(piece);
                }
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            camera = new Camera(World.game.GraphicsDevice, new Vector3(0, 0, 5.0f), Vector3.Zero, new Vector3(0, 1.0f, 0), 0.1f, 500.0f, MathHelper.PiOver2);

            checkerBoard.LoadContent(contentManager);

            SetCheckerPieces(contentManager, ref checkerPlayer[0], PIECE_COLOR.BLACK, @"Pieces\CheckerPieceBlack", 0, 3);
            SetCheckerPieces(contentManager, ref checkerPlayer[1], PIECE_COLOR.RED, @"Pieces\CheckerPieceRed", 5, 8);

            currentPlayerIndex = 0;
        }

        private float SetPiecePosition(float coordinate)
        {
            return coordinate - 3.5f;
        }

        public override void Draw(GameTime gameTime)
        {
            checkerBoard.StaticMesh.SetWVP(Matrix.Identity, camera.ViewMatrix, camera.ProjectionMatrix);
            checkerBoard.Draw(gameTime);

            foreach (CheckerPiece piece in pieces)
            {
                Point position = piece.Position;
                Vector3 translate = new Vector3(SetPiecePosition(position.X), SetPiecePosition(position.Y), 0);
                piece.StaticMesh.SetWVP(Matrix.CreateTranslation(translate), camera.ViewMatrix, camera.ProjectionMatrix);
                piece.Draw(gameTime);
            }
        }

        private Vector3 GetUnproject(float zValue)
        {
            Point mousePoint = currentPlayer.CheckerPlayerController.MouseInput.point;
            return game.GraphicsDevice.Viewport.Unproject(new Vector3(mousePoint.X, mousePoint.Y, zValue), camera.ProjectionMatrix, camera.ViewMatrix, Matrix.Identity);
        }

        private int Floor(float fValue)
        {
            return Convert.ToInt32(Math.Floor(Convert.ToDouble(fValue)));
        }

        public override void Update(GameTime time)
        {
            currentPlayer.CheckerPlayerController.Update(time);
            currentPlayer.CheckerPlayerController.GetInput();
            if (currentPlayer.bDrag == true)
            {
                Vector3 minPoint = GetUnproject(0);
                Vector3 maxPoint = GetUnproject(1.0f);
                Vector3 direction = maxPoint - minPoint;
                direction.Normalize();
                Ray ray = new Ray(minPoint, direction);
                Plane plane = new Plane(new Vector3(0, 0, 1), 0);
                float? distance = ray.Intersects(plane);
                Vector3 piecePoint = minPoint + direction * Convert.ToSingle(distance);
                currentPlayer.bDrag = false;

                // Set up highlight
                piecePoint = new Vector3(Floor(piecePoint.X), Floor(piecePoint.Y), 0) + new Vector3(4, 4, 0);
                checkerBoard.Grid[Convert.ToInt32(piecePoint.X), Convert.ToInt32(piecePoint.Y)].HighLighted = true;
                checkerBoard.GridStaticMesh.SetWVP(Matrix.CreateTranslation(new Vector3(piecePoint.X - 3.5f, piecePoint.Y - 3.5f, 0)), camera.ViewMatrix, camera.ProjectionMatrix);
            }
        }
    }
}
