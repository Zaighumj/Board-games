using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using AdamXNALibrary.Miscellaneous;
using AdamXNALibrary.Visual;

using Checkers.Pieces;

namespace Checkers.Boards
{
    public struct GridPiece
    {
        public CheckerPiece CheckerPiece;
        public bool HighLighted;
    }

    public class CheckerBoard:IVisualElement
    {
        public GridPiece[,] Grid = new GridPiece[8, 8];
        public StaticMesh<VertexPositionTexture> StaticMesh;
        private string textureName;
        public StaticMesh<VertexPositionColor> GridStaticMesh;

        public CheckerBoard(string textureName)
        {
            this.textureName = textureName;
        }

        public void LoadContent(ContentManager contentManager)
        {
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    GridPiece gridPiece = new GridPiece();
                    gridPiece.HighLighted = false;
                    gridPiece.CheckerPiece = null;
                    Grid[i, j] = gridPiece; 
                }
            }

            int[] indices = { 0, 1, 3, 0, 3, 2 };

            VertexPositionColor[] gridVertices = new VertexPositionColor[4];
            for (int k = 0; k < 2; ++k)
            {
                for (int m = 0; m < 2; ++m)
                {
                    gridVertices[General.GetIndex(k, m, 2)] = new VertexPositionColor(new Vector3(-0.5f + k, -0.5f + m, 0), new Color(0, 0, 128, 256));
                }
            }
            GridStaticMesh = new StaticMeshColor(gridVertices, indices);

            // Load the static mesh
            VertexPositionTexture[] vertices=new VertexPositionTexture[4];
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    vertices[General.GetIndex(i, j, 2)] = new VertexPositionTexture(new Vector3(8*i-4, 8*j-4, 0), new Vector2(i, 1-j));
                }
            }
            StaticMesh = new StaticMeshTexture(contentManager, textureName, vertices, indices);
        }

        public void Draw(GameTime gameTime)
        {
            StaticMesh.Draw(gameTime);

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (Grid[i, j].HighLighted == true)
                    {
                        GridStaticMesh.Draw(gameTime);
                    }
                }
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            //throw new NotImplementedException();
        }
    }
}
