using System;
using System.Collections.Generic;
using System.Text;

namespace WarGame
{
	public class Player
	{
		public Queue<Card> Cards;

		//public int index = -1;

		public Player()
		{
			Cards = new Queue<Card>();

			// get list of 26 cards
			for (int i = 0; i < 26; i++)
			{
				Cards.Enqueue(new Card());
			}
		}

		public Card GetNextCard()
		{
			// if getting the next card results in an error
			if (Cards.Count == 0)
			{
				// no more new cards
				return null;
			}

			//index++;
			return Cards.Dequeue();
		}
	}
}
