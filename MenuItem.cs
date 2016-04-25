using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack_V3
{
    class MenuItem
    {
        private Texture2D _texture;
		private Vector2 _location;

        public MenuItem (Texture2D texture, Vector2 location) 
		{
			_texture = texture;
			_location = location;
		}

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(
                (int)_location.X,
                (int)_location.Y,
                _texture.Width,
                _texture.Height
            );
        }

        public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (
				_texture, 
				_location, 
				null,
				Microsoft.Xna.Framework.Color.White,
				0,
				new Vector2(0,0),
				1,
				SpriteEffects.None,
				0
			);
		}
    }
}
