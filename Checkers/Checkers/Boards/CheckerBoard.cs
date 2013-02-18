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
    public class CheckerBoard:IVisualElement
    {
        public CheckerPiece[,] Grid = new CheckerPiece[8, 8];
        public StaticMesh<VertexPositionTexture> StaticMesh;
        private string textureName;

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
                    Grid[i, j] = null;
                }
            }

            // Load the static mesh
            VertexPositionTexture[] vertices=new VertexPositionTexture[4];
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    vertices[General.GetIndex(i, j, 2)] = new VertexPositionTexture(new Vector3(8*i-4, 8*j-4, 0), new Vector2(i, 1-j));
                }
            }
            int[] indices={0, 1, 3, 0, 3, 2};
            StaticMesh<VertexPositionTexture>.VertexDeclaration = VertexPositionTexture.VertexDeclaration;
            StaticMesh = new StaticMesh<VertexPositionTexture>(contentManager, textureName, vertices, indices);
        }

        public void Draw(GameTime gameTime)
        {
            StaticMesh.EnableTexture();
            StaticMesh.Draw(gameTime);
            StaticMesh.DisableTexture();
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            //throw new NotImplementedException();
        }
    }
}
