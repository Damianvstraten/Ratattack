using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack
{
	class Weapon
	{
		//All fields listed below are used for the weapon animation
		private const int WIDTH 			= 183;
		private const int HEIGHT 			= 205;
		private const int NUMBER_OF_FRAMES 	= 2;
		public int deaths;
		private int _currentFrame;
		private int _animationCounter;
		private int _animationSpeed = 60;

		private Texture2D _texture;
		private Vector2 _location;
		Boolean animationDone = false;
		Bullet _bullet; 
		Game1 _game;
		Boolean hit = false;



		private Rectangle _currentRectangle 
		{
			get 
			{
				return new Microsoft.Xna.Framework.Rectangle(
					WIDTH * _currentFrame,
					0,
					WIDTH,
					HEIGHT
				);
			}
		}

		public Weapon (Game1 game, Texture2D texture, Vector2 location) 
		{
			_texture = texture;
			_location = location;
			_game = game;
			_bullet = new Bullet (game,
				new Vector2(_location.X + 135,_location.Y + 10),
				game.Content.Load<Texture2D>("cheesbullet"));



		}

		public void ShootAnimation()
		{
			_animationCounter++;

			if (!animationDone) {
				for (int i = 0;  i < 120; i++) {
					if (i % 40 == 0) {
						_currentFrame++;
					}

				}

				if(_currentFrame > NUMBER_OF_FRAMES) {
					_currentFrame = 0;
					animationDone = true;
				}
			}

		}


		public void Shoot(Rat mouse, Controls _controls) {
			if (_bullet != null) {
				_bullet.ShowBullet (1);
			}
			if (hit && mouse.GetLocation().X > _game.GraphicsDevice.Viewport.Width) {
				mouse.MouseHit ();

				_game._answer = false;

			

				_bullet = new Bullet (_game,
					new Vector2(_location.X + 135,_location.Y + 10),
					_game.Content.Load<Texture2D>("cheesbullet"));
				
				
				_controls._string = "";
			}
			if (_bullet == null)
				return;
			
			_bullet.Shoot (mouse.GetLocation());
			if (mouse.GetBoundingBox().Intersects(_bullet.GetBoundingBox()))
			{
				mouse.MouseHit ();

				deaths++;

				_bullet = null;
				hit = true;
			}
		}
			

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (
				_texture, 
				_location, 
				_currentRectangle,
				Microsoft.Xna.Framework.Color.White,
				0,
				new Vector2(0,0),
				1,
				SpriteEffects.None,
				0
			);
			if(_bullet != null)
				_bullet.Draw (spriteBatch);
		}


	}
}

