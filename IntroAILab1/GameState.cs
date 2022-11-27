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

		void MoveUp()
		{
			for (int x = emptyCoord.x + 1; x < board.GetLength(0); x++)
			{
				board[x - 1, emptyCoord.y] = board[x, emptyCoord.y];
			}
			emptyCoord.x = board.GetLength(0) - 1;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveDown()
		{
			for (int x = emptyCoord.x; x > 0; x--)
			{
				board[x, emptyCoord.y] = board[x-1, emptyCoord.y];
			}
			emptyCoord.x = 0;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveRight()
		{
			for (int y = emptyCoord.y; y > 0; y--)
			{
				board[emptyCoord.x, y] = board[emptyCoord.x, y - 1];
			}
			emptyCoord.y = 0;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}
		void MoveLeft()
		{
			for (int y = emptyCoord.y + 1; y < board.GetLength(1); y++)
			{
				board[emptyCoord.x, y - 1] = board[emptyCoord.x, y];
			}
			emptyCoord.y = board.GetLength(1) - 1;
			board[emptyCoord.x, emptyCoord.y] = 0;
		}

		public List<GameState> GeneratePossibleChildren()
		{
			List<GameState> children = new();
			if (emptyCoord.x > 0)
			{
				var clone = Clone();
				clone.MoveDown();
				children.Add(clone);
			}
			if (emptyCoord.x < board.GetLength(0))
			{
				var clone = Clone();
				clone.MoveUp();
				children.Add(clone);
			}
			if (emptyCoord.y > 0)
			{
				var clone = Clone();
				clone.MoveRight();
				children.Add(clone);
			}
			if (emptyCoord.y < board.GetLength(1))
			{
				var clone = Clone();
				clone.MoveLeft();
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
	}
}
