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
			GameState start = new GameState(solutionBoard);
			Random rnd = new Random();
			for (int i = 0; i < 10; i++)
			{
				var variations = start.GeneratePossibleChildren();
				start = variations[rnd.Next(0, variations.Count)];
			}
			GameState solution = new GameState(solutionBoard);
			Console.WriteLine("Start state:");
			start.Print();
			DepthFirstSearch<GameState> depthSearch = new(start);
			if (depthSearch.TryFindSolution(solution, out var path, 10))
			{
				Console.WriteLine($"Solution has {path.Count} steps:");
				for (int i = 0; i < path.Count; i++)
				{
					Console.WriteLine($"Step {i+1}:");
					path[i].Print();
				}
			}
			else
				Console.WriteLine("No solutions found!");
		}
	}
}