using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class GameState : IAStarState<GameState>
	{
		int[,] board;
		Vector2Int emptyCoord;
		Vector2Int size;

		public GameState(int[,] board)
		{
			this.board = board;
			int emptyCoordValue = -1;
			emptyCoord = new Vector2Int(emptyCoordValue, emptyCoordValue);
			size = new Vector2Int(board.GetLength(0), board.GetLength(1));
			for (int x = 0; x < size.x; x++)
			{
				for (int y = 0; y < size.y; y++)
				{
					if (board[x, y] == 0)
					{
						emptyCoord = new Vector2Int(x, y);
						break;
					}
				}
				if (emptyCoord.x != emptyCoordValue)
					break;
			}
		}

		private GameState()
		{

		}

		GameState Clone()
		{
			GameState clone = new GameState();//(GameState)MemberwiseClone();
			clone.board = (int[,])board.Clone();
			clone.emptyCoord = emptyCoord;
			clone.size = size;
			return clone;
		}

		void MoveUp(int steps)
		{
			for (int x = emptyCoord.x + 1; x <= emptyCoord.x + steps; x++)
			{
				board[x - 1, emptyCoord.y] = board[x, emptyCoord.y];
			}
			emptyCoord.x = emptyCoord.x + steps;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveDown(int steps)
		{
			for (int x = emptyCoord.x; x >= Math.Max(emptyCoord.x - steps, 1); x--)
			{
				board[x, emptyCoord.y] = board[x-1, emptyCoord.y];
			}
			emptyCoord.x = emptyCoord.x - steps;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveRight(int steps)
		{
			for (int y = emptyCoord.y; y >= Math.Max(emptyCoord.y - steps, 1); y--)
			{
				board[emptyCoord.x, y] = board[emptyCoord.x, y - 1];
			}
			emptyCoord.y = emptyCoord.y - steps;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveLeft(int steps)
		{
			for (int y = emptyCoord.y + 1; y <= emptyCoord.y + steps; y++)
			{
				board[emptyCoord.x, y - 1] = board[emptyCoord.x, y];
			}
			emptyCoord.y = emptyCoord.y + steps;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}

		public List<GameState> GeneratePossibleChildren()
		{
			List<GameState> children = new();
			for (int i = 1; i <= emptyCoord.x; i++)
			{
				var clone = Clone();
				clone.MoveDown(i);
				children.Add(clone);
			}
			for (int i = 1; i < size.x - emptyCoord.x; i++)
			{
				var clone = Clone();
				clone.MoveUp(i);
				children.Add(clone);
			}
			for (int i = 1; i <= emptyCoord.y; i++)
			{
				var clone = Clone();
				clone.MoveRight(i);
				children.Add(clone);
			}
			for (int i = 1; i < size.y - emptyCoord.y; i++)
			{
				var clone = Clone();
				clone.MoveLeft(i);
				children.Add(clone);
			}
			return children;
		}

		public void Print()
		{
			for (int x = 0; x < size.x; x++)
			{
				for (int y = 0; y < size.y; y++)
				{
					if (board[x, y] == 0)
						Console.Write("  ");
					else
						Console.Write($"{board[x, y]} ");
				}
				Console.WriteLine();
			}
		}

		public bool StateEquals(GameState other)
		{
			if (other.size.x == size.x
					&& other.size.y == size.y)
			{
				for (int x = 0; x < size.x; x++)
					for (int y = 0; y < size.y; y++)
						if (board[x, y] != other.board[x, y])
							return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Based on the number of cells not on their positions
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public float HFunction(GameState other)
		{
			int cellNotOnPlaceCount = 0;
			for (int x = 0; x < size.x; x++)
				for (int y = 0; y < size.y; y++)
					if (board[x, y] != other.board[x, y])
						cellNotOnPlaceCount++;
			return cellNotOnPlaceCount;
		}
	}
}
