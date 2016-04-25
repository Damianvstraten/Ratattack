using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack
{
	public class Bullet
	{
		private Texture2D _texture;
		private Vector2 _location, _speed;
		private int BulletShow = 0;


		public Bullet (Game1 _game, Vector2 location, Texture2D texture) 
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

		public void ResetBullet() {
			_location = new Vector2(_location.X + 135,_location.Y + 10);
		}

		public void ShowBullet (int visible) {
			BulletShow = visible;
		}

		public void Shoot(Vector2 mouseLocation) {
			_speed = new Vector2 (4, 1);
			if (mouseLocation.X > _location.X) {
				_location.X += _speed.X;
			}
			if (mouseLocation.Y > _location.Y) {
				_location.Y += _speed.Y;
			}

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
				BulletShow,
				SpriteEffects.None,
				0
			);
		}
	}
}

