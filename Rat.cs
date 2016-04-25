using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack
{
	class Rat
	{

		//fields
		private  int _health;
		private Texture2D _texture;
		private Vector2 _location, _speed;
		private int _rotationSide = -13;
		public bool isDead = false;
		public bool _HasLife = false;
		Boolean Done = false;
		Calculation _calculation;
		Wheel _wheel1, _wheel2;
		Game1 _game;
		private SpriteFont _spriteFont;
		SpriteEffects _spriteEffect = SpriteEffects.None;
		private int TextureNumber = 0;
		Controls _controls;



		public Rat (int health ,Vector2 location,SpriteFont spriteFont, Game1 game, Controls controls) 
		{
			_health = health;
			_location = location;
			_controls = controls;
			_game = game;
			_calculation = new Calculation(this, "10 x 10 = ", 100,spriteFont);
			_wheel1 = new Wheel(this, _game.Content.Load<Texture2D>("wheel"), new Vector2(_location.X, _location.Y));
			_wheel2 = new Wheel(this, _game.Content.Load<Texture2D>("wheel"), new Vector2(_location.X , _location.Y));
			_speed = new Vector2(-2, 0);
			GenerateRandomTexture ();
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

		public void ClearAnswer() {
			_controls._string = "";
		}

		private void GenerateRandomTexture() {
			
			if (TextureNumber > 4) {
				TextureNumber = 0;
			}

			if (TextureNumber == 0) {
				_texture = _game.Content.Load<Texture2D> ("mouse-redd");
			} 
			if (TextureNumber == 1) {
				_texture = _game.Content.Load<Texture2D> ("mouse-purple");
			} 
			if (TextureNumber == 2) {
				_texture = _game.Content.Load<Texture2D> ("mouse-green");
			} 
			if (TextureNumber == 3) {
				_texture = _game.Content.Load<Texture2D> ("mouse-darkblue");
			} 
			if (TextureNumber == 4) {
				_texture = _game.Content.Load<Texture2D> ("mouse-blue");
			} 
			TextureNumber++;
		}

		private string GenerateSum()
		{
			return "true";
		}


		public void GenerateNewSum() {
			_calculation.GenerateSum ();
			GenerateRandomTexture ();
			ClearAnswer ();
		}

		public void Update()
		{
			_location += _speed;
			_calculation.Update();

			if (_speed.X != 0) {
				if (_speed.X > 0) {
					_speed.X = 6;
				} else {
					_speed.X = -2;
				}
			}


			_wheel1.Rotate(new Vector2(61, 159), _rotationSide);
			_wheel2.Rotate(new Vector2(113, 159), _rotationSide);


			if (isDead && this.GetLocation ().X > _game.GraphicsDevice.Viewport.Width) {
				GenerateNewSum ();

				isDead = false;
			} 
			if (_HasLife && this.GetLocation ().X > _game.GraphicsDevice.Viewport.Width) {
				ReverseMouse ();
			}
		}

		public Vector2 GetLocation()
		{
			return _location;
		}

		public void TookLife(Boolean HasLife) {
			_HasLife = HasLife;
		}

		public void  MouseHit() {
			ReverseMouse ();
			isDead = true;
		}
		public void Stop() {
			_speed.X = 0;
		}


		public void ReverseMouse() {
			_speed.X *= -1;	

			if (_spriteEffect == SpriteEffects.FlipHorizontally)
				_spriteEffect = SpriteEffects.None;
			else 
				_spriteEffect = SpriteEffects.FlipHorizontally;
			_rotationSide *= -1;
		}


		public Boolean VerifyAnswer(String Answer)

		{
			return _calculation.Verify(Answer);
		}

		public void Draw(SpriteBatch spriteBatch,String _string)
		{
			spriteBatch.Draw (
				_texture, 
				_location, 
				null,
				Microsoft.Xna.Framework.Color.White,
				0,
				new Vector2(0,0),
				1,
				_spriteEffect,
				0
			);
			_calculation.Draw(spriteBatch,_string);
			_wheel1.Draw(spriteBatch);
			_wheel2.Draw(spriteBatch);
		}
	}
}
