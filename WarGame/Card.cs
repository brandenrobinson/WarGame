using System;
using System.Collections.Generic;
using System.Text;

namespace WarGame
{
	public class Card
	{
		// 2 - 14 (J = 11, Q = 12, K = 13, A = 14)
		public int Num;

		public Card()
		{
			var rand = new Random();
			Num = rand.Next(2, 14);
		}
	}
}
