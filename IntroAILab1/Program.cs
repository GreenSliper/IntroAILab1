namespace IntroAILab1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[,] board = new int[,]
				{
					{0, 1, 2 },
					{3, 4, 5 }//,
					//{6, 7, 8 }
				};
			GameState gs = new GameState(board);
			gs.Print();
			Console.ReadLine();
			foreach (var child in gs.GeneratePossibleChildren())
			{
				child.Print();
				Console.ReadLine();
			}
		}
	}
}