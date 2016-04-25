using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratattack
{
	class Controls
	{
		private Keys[] _oldKeybState;
		public String _string = "";

		/// <summary>
		/// this removes 1 character from our string, thus functions as backspace
		/// </summary>
		protected void RemoveCharacter()
		{
			//check if the string has atleast 1 character
			if (_string.Length > 0)
			{
				//cut the last number from the string
				_string = _string.Substring(0, _string.Length - 1);
			}
		}

		protected void ClearString()
		{
			_string = "";
		}



		/// <summary>
		/// this adds one character to the _string
		/// </summary>
		/// <param name="character"></param>
		protected void AddCharacter(string character)
		{
			int n;
			bool isNumeric = int.TryParse(character, out n);

			if (isNumeric)
			{
				_string += character;
			}
		}



		public void update()
		{
			//we loop through all the keys being currently pressed
			foreach (Keys curKey in Keyboard.GetState().GetPressedKeys())
			{
				// we set the counter to 0
				int counter = 0;

				//we loop through all the keys in the previous keyboard state
				foreach (Keys oldKey in _oldKeybState)
				{
					//if the key was already present in our previous state we up the counter
					if (oldKey.Equals(curKey))
					{
						++counter;
					}

				}
				//if the counter is still 0, the key has just been pressed, and we should
				// exert the key once and log it accordingly
				if (counter == 0)
				{
					//make all strings uppercase for easier checking
					string keyValue = curKey.ToString().ToUpper();
					//split on the D ( D1 D2 D3 , [NUMPA ,1] 2 NUMPAD 3 => 1 2 3 , 1 2 3)  
					string[] split = keyValue.Split('D');
					if (split.Length > 1)
					{
						AddCharacter(split[1]);
					}

					//check for backspace
					if (keyValue == "BACK")
					{
						RemoveCharacter();
					}


				}

			}
			_oldKeybState = Keyboard.GetState().GetPressedKeys();
		}
	}


}
