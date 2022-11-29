namespace IntroAILab1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[,] startBoard = new int[,]
				{
					{6, 2, 8 },
					{4, 1, 7 },
					{5, 3, 0 }
				};
			int[,] solutionBoard = new int[,]
				{
					{0, 1, 2 },
					{3, 4, 5 },
					{6, 7, 8 }
				};
			GameState start = new GameState(startBoard);
			/*Random rnd = new Random();
			for (int i = 0; i < 15; i++)
			{
				var variations = start.GeneratePossibleChildren();
				start = variations[rnd.Next(0, variations.Count)];
			}*/
			GameState solution = new GameState(solutionBoard);
			Console.WriteLine("Start state:");
			start.Print();
			UniformCostSearch<GameState> ucs = new(start);
			AStarSearch<GameState> astar = new(start);
			if (astar.TryFindSolution(solution, out var aStarPath))
			{
				Console.WriteLine($"A-star solution has {aStarPath.Count - 1} steps:");
				for (int i = 1; i < aStarPath.Count; i++)
				{
					Console.WriteLine($"Step {i}:");
					aStarPath[i].Print();
				}
			}
			else
				Console.WriteLine("No solutions found!");
			/*if (ucs.TryFindSolution(solution, out var path, 15))
			{
				Console.WriteLine($"UCS solution has {path.Count-1} steps:");
				for (int i = 1; i < path.Count; i++)
				{
					Console.WriteLine($"Step {i}:");
					path[i].Print();
				}

				DepthFirstSearch<GameState> dfs = new(start);
				if (dfs.TryFindSolution(solution, out path, 15))
				{
					Console.WriteLine($"DFS solution has {path.Count - 1} steps:");
					for (int i = 1; i < path.Count; i++)
					{
						Console.WriteLine($"Step {i}:");
						path[i].Print();
					}
				}
			}
			else
				Console.WriteLine("No solutions found!");
			*/
		}
	}
}