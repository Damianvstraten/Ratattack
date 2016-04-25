using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ratattack
{
	public class Life
	{
		private Texture2D _texture;
		private Vector2 _location;


		public Life (Vector2 location, Texture2D texture) 
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

		public void SetPosition(Vector2 position) {
			_location = position;
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

