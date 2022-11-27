using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class GameState : IState
	{
		int[,] board;
		Vector2Int emptyCoord;

		public GameState(int[,] board)
		{
			this.board = board;
			int emptyCoordValue = -1;
			emptyCoord = new Vector2Int(emptyCoordValue, emptyCoordValue);
			for (int x = 0; x < board.GetLength(0); x++)
			{
				for (int y = 0; y < board.GetLength(1); y++)
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

		GameState Clone()
		{
			GameState clone = (GameState)MemberwiseClone();
			clone.board = (int[,])board.Clone();
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
			for (int i = 1; i < board.GetLength(0) - emptyCoord.x; i++)
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
			for (int i = 1; i < board.GetLength(1) - emptyCoord.y; i++)
			{
				var clone = Clone();
				clone.MoveLeft(i);
				children.Add(clone);
			}
			return children;
		}

		public void Print()
		{
			for (int x = 0; x < board.GetLength(0); x++)
			{
				for (int y = 0; y < board.GetLength(1); y++)
				{
					if (board[x, y] == 0)
						Console.Write("  ");
					else
						Console.Write($"{board[x, y]} ");
				}
				Console.WriteLine();
			}
		}

		List<IState> IState.GeneratePossibleChildren() => GeneratePossibleChildren().Cast<IState>().ToList();

		public bool StateEquals(IState other)
		{
			if (other is GameState gs)
			{ 
				if (gs.board.GetLength(0) == board.GetLength(0)
					&& gs.board.GetLength(1) == board.GetLength(1))
				{
					for (int x = 0; x < board.GetLength(0); x++)
						for (int y = 0; y < board.GetLength(1); y++)
							if (board[x, y] != gs.board[x, y])
								return false;
					return true;
				}
			}
			return false;
		}
	}
}
