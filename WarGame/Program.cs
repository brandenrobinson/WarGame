using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Play War");
			Console.ReadKey();

			var player = new Player();
			var cpu = new Player();

			// press key to flip over first card in each deck
			bool end = false;
			
			while(end == false)
			{
				// reveal latest cards
				var pCard = player.GetNextCard();
				var cCard = cpu.GetNextCard();

				if (pCard == null)
				{
					// cpu wins
					Console.WriteLine("Cpu wins.  Player ran out of cards.");
					Console.ReadKey();
					break;
				} 
				else if (cCard == null)
				{
					// player wins
					Console.WriteLine("Player wins.  Cpu ran out of cards.");
					Console.ReadKey();
					break;
				}

				if (pCard.Num > cCard.Num)
				{
					// player gets cards
					player.Cards.Enqueue(pCard);
					player.Cards.Enqueue(cCard);

					Console.WriteLine($"Player wins this round.  Player card was { pCard.Num }, Cpu card was {cCard.Num}");
					Console.ReadKey();
				}
				else if (pCard.Num < cCard.Num)
				{
					// cpu gets cards
					cpu.Cards.Enqueue(pCard);
					cpu.Cards.Enqueue(cCard);

					Console.WriteLine($"Cpu wins this round.  Player card was { pCard.Num }, Cpu card was {cCard.Num}");
					Console.ReadKey();
				}
				else if (pCard.Num == cCard.Num)
				{
					// war
					Console.WriteLine($"Cards are equal; war is commenced.  Player card was { pCard.Num }, Cpu card was {cCard.Num}");
					Console.ReadKey();

					Console.WriteLine("Starting war...");

					var PlayerWarCards = new Queue<Card>();
					var CpuWarCards = new Queue<Card>();

					// possible recursive issue anywhere?
					bool warStop = false;
					// each player returns 3 cards, and a 4th face up
					while (warStop == false)
					{
						for (int i = 0; i < 4; i++)
						{
							var newCard = player.GetNextCard();

							if (newCard == null)
							{
								// cpu wins game
								Console.WriteLine("Cpu wins.  Player ran out of cards.");
								Console.ReadKey();
								return;
							}

							PlayerWarCards.Enqueue(newCard);
						}

						for (int i = 0; i < 4; i++)
						{
							var newCard = cpu.GetNextCard();

							if (newCard == null)
							{
								// player wins game
								Console.WriteLine("Player wins.  Cpu ran out of cards.");
								Console.ReadKey();
								return;
							}

							CpuWarCards.Enqueue(newCard);
						}

						var playerFaceUpCard = PlayerWarCards.First();
						var cpuFaceUpCard = CpuWarCards.First();

						if (playerFaceUpCard.Num > cpuFaceUpCard.Num)
						{
							// player wins all cards
							foreach (var card in PlayerWarCards)
							{
								player.Cards.Enqueue(card);
							}

							foreach (var card in CpuWarCards)
							{
								player.Cards.Enqueue(card);
							}

							player.Cards.Enqueue(pCard);
							player.Cards.Enqueue(cCard);

							Console.WriteLine($"Player wins this war.  Player card was { playerFaceUpCard.Num }, Cpu card was { cpuFaceUpCard.Num }");
							Console.ReadKey();

							warStop = true;
						}
						else if (playerFaceUpCard.Num < cpuFaceUpCard.Num)
						{
							// cpu wins cards
							foreach (var card in PlayerWarCards)
							{
								cpu.Cards.Enqueue(card);
							}

							foreach (var card in CpuWarCards)
							{
								cpu.Cards.Enqueue(card);
							}

							cpu.Cards.Enqueue(pCard);
							cpu.Cards.Enqueue(cCard);

							Console.WriteLine($"Cpu wins this war.  Player card was { playerFaceUpCard.Num }, Cpu card was { cpuFaceUpCard.Num }");
							Console.ReadKey();

							warStop = true;
						}
						else if (playerFaceUpCard == cpuFaceUpCard)
						{
							Console.WriteLine($"Cards are equal again; another war is commenced.  Player card was { playerFaceUpCard.Num }, Cpu card was {cpuFaceUpCard.Num}");
							Console.ReadKey();
							continue;
						}
					}
				}

				if (player.Cards.Count >= 52)
				{
					// player wins
					Console.WriteLine("Player wins.  Player has all of the cards.");
					Console.ReadKey();
					end = true;
				}
				else if (cpu.Cards.Count >= 52)
				{
					// cpu wins
					Console.WriteLine("Cpu wins.  Cpu has all of the cards.");
					Console.ReadKey();
					end = true;
				}
				else
				{
					Console.WriteLine($"Player: {player.Cards.Count}, CPU: {cpu.Cards.Count}");
				}
			}
		}
	}
}
