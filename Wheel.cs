using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ratattack
{
	class Wheel
	{
		private Texture2D _texture;
		private Vector2 _location, _origin;
		private Rat _mouse;

		private float _rotation;

		public Wheel (Rat mouse, Texture2D texture, Vector2 location) 
		{
			_texture = texture;
			_location = location;
			_mouse = mouse;
			_origin = new Vector2 (_texture.Width / 2, _texture.Height / 2);
		}

		public void Rotate(Vector2 position, int rotationSide)
		{
			_location = new Vector2(_mouse.GetLocation().X + position.X, _mouse.GetLocation().Y + position.Y);
			_rotation += MathHelper.ToRadians(rotationSide);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				_texture,
				_location,
				null,
				Color.White,
				_rotation,
				_origin,
				1,
				SpriteEffects.None,
				0);
		}
	}
}

