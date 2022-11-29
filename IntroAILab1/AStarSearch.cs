using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntroAILab1
{
	internal class AStarSearch<T> where T : class, IAStarState<T>
	{
		SearchTree<T> tree;
		public AStarSearch(T startState)
		{
			tree = new SearchTree<T>(startState);
		}

		bool TryFindSolution(T target, out List<T> path)
		{
			path = null;
			List<SearchTree<T>.SearchTreeNode> open = new(),
				closed = new();
			open.Add(tree.root);
			while (open.Any())
			{
				var qIndex = open.MinIndexBy(x => x.fValue);
				var q = open[qIndex];
				open.RemoveAt(qIndex);
				q.AddChildren(q.value.GeneratePossibleChildren());
				foreach (var child in q.children)
				{
					if (child.value.StateEquals(target))
					{
						path = child.RootPath.Reverse().ToList();
						return true;
					}
					//g is depth, not computing it, it will be auto-computed
					child.fValue = child.depth + child.value.HFunction(target);
					//there is a way to reach child state quicker
					if (open.AsParallel().Any(x => x.value.StateEquals(child.value) && x.fValue < child.fValue))
						continue;
					//if closed does not contain child || closed version has greater fValue
					if (!closed.AsParallel().Any(x => x.value.StateEquals(child.value) && x.fValue < child.fValue))
						open.Add(child);
				}
				closed.Add(q);
			}
			return false;
		}

		bool TryFindSolutionStepwise(T target, out List<T> path)
		{
			path = null;
			List<SearchTree<T>.SearchTreeNode> open = new(),
				closed = new();
			open.Add(tree.root);
			while (open.Any())
			{
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
				Console.WriteLine("Open set:");
				int i = 0;
				foreach (var node in open)
				{
					Console.WriteLine($"#{++i}; F: {node.fValue}");
					node.value.Print();
				}
				Console.WriteLine("Closed set:");
				i = 0;
				foreach (var node in closed)
				{
					Console.WriteLine($"#{++i}; F: {node.fValue}");
					node.value.Print();
				}
				var qIndex = open.MinIndexBy(x => x.fValue);
				var q = open[qIndex];
				open.RemoveAt(qIndex);
				Console.WriteLine("Current state:");
				q.value.Print();
				q.AddChildren(q.value.GeneratePossibleChildren());
				Console.WriteLine("Child states found:");
				foreach (var child in q.children)
				{
					child.value.Print();
					Console.WriteLine();
				}
				foreach (var child in q.children)
				{
					if (child.value.StateEquals(target))
					{
						path = child.RootPath.Reverse().ToList();
						return true;
					}
					//g is depth, not computing it, it will be auto-computed
					child.fValue = child.depth + child.value.HFunction(target);
					//there is a way to reach child state quicker
					if (open.AsParallel().Any(x => x.value.StateEquals(child.value) && x.fValue < child.fValue))
						continue;
					//if closed does not contain child || closed version has greater fValue
					if (!closed.AsParallel().Any(x => x.value.StateEquals(child.value) && x.fValue < child.fValue))
						open.Add(child);
				}
				closed.Add(q);
			}
			return false;
		}

		public bool TryFindSolution(T target, out List<T> path, bool stepwise = false)
		{
			return stepwise ? TryFindSolutionStepwise(target, out path) : TryFindSolution(target, out path);
		}
	}
}
