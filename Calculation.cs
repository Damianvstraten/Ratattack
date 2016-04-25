using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack
{
	class Calculation
	{

		private string _question;
        private int _answer, _difficulty, firstNumber, secondNumber;
		private Vector2 _location;
		private SpriteFont _spriteFont;
		private Rat _mouse;

		private Random random = new Random ();

		/// <summary>
		/// this is the constructor of the Calculation
		/// </summary>
		/// <param name="question"></param>
		/// <param name="answer"></param>
		/// <param name="difficulty"></param>
		public Calculation(Rat mouse,string question , int answer, SpriteFont spriteFont)
		{
			// Create the instance of a calculation.
			_question = question;
			_answer = answer;
			_difficulty = 0;
			_location = new Vector2(mouse.GetLocation().X, mouse.GetLocation().Y - 20);
			_spriteFont = spriteFont;
			_mouse = mouse;
			GenerateSum ();
		}

		public void GenerateSum() {

            firstNumber = random.Next(1, 10);
            secondNumber = random.Next(1, 15);

			_question = firstNumber + " x " + secondNumber + "  = ";
			_answer = firstNumber * secondNumber;
		}

		public void Draw(SpriteBatch spriteBatch,String _string)
		{
			String ToDraw = _question + _string;
			//Draw a String in the color white with the given font through the mouse class as parameter.
			spriteBatch.DrawString(
				_spriteFont,
				ToDraw,
				_location,
				Color.White);
		}
		public void Update()
		{
			_location = new Vector2(_mouse.GetLocation().X, _mouse.GetLocation().Y - 20);
		}

		public Boolean Verify(string Answer)
		{
			int GivenAnswer;

			int.TryParse(Answer, out GivenAnswer);
			if (GivenAnswer == _answer)
			{
				return true;

			}
			return false;
		}
	}
}
