using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AdamXNALibrary.Miscellaneous;
using AdamXNALibrary.Visual;

namespace Checkers.Pieces
{
    public enum PIECE_COLOR { RED, BLACK };
    public enum PIECE_TYPE { PAWN, KING };

    public class CheckerPiece : IVisualElement, ICloneable
    {
        private string textureName;
        public StaticMesh<VertexPositionTexture> StaticMesh;

        public Point Position;
        public bool Alive = true;

        public readonly PIECE_COLOR Color;
        public PIECE_TYPE PieceType;

        // Valid moves list
        public List<Point> ValidPieceMoves = new List<Point>() { new Point(-1, -1), new Point(1, -1) };
        public List<Point> ValidPieceJumps = new List<Point>() { new Point(-2, -2), new Point(2, -2) };

        public List<Point> ValidKingMoves = new List<Point>() { new Point(-1, -1), new Point(1, -1), new Point(-1, 1), new Point(1, 1) };
        public List<Point> ValidKingJumps = new List<Point>() { new Point(-2, -2), new Point(2, -2), new Point(-2, 2), new Point(2, 2) };

        public CheckerPiece(PIECE_COLOR color, string textureName)
        {
            Color = color;
            PieceType = PIECE_TYPE.PAWN; 
            this.textureName = textureName;
        }

        public void LoadContent(ContentManager contentManager)
        {
            // Set up static mesh
            VertexPositionTexture[] vertices = new VertexPositionTexture[4];
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    vertices[General.GetIndex(i, j, 2)] = new VertexPositionTexture(new Vector3(i - 0.5f, j - 0.5f, 0), new Vector2(i, 1 - j));
                }
            }
            int[] indices = { 0, 1, 3, 0, 3, 2 };
            StaticMesh = new StaticMeshTexture(contentManager, textureName, vertices, indices);
        }

        public void Draw(GameTime gameTime)
        {
            if (Alive == true)
            {
                StaticMesh.Draw(gameTime);
            }
        }

        public void Update(GameTime time)
        {
            // Update position of piece
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
