using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntroAILab1
{
	internal class UniformCostSearch<T> where T : class, IState<T>
	{
		public SearchTree<T> tree;
		public UniformCostSearch(T startState)
		{
			tree = new SearchTree<T>(startState);
		}
		bool TryFindSolution(T target, out List<T> path, int maxDepth = 0)
		{
			path = null;
			PriorityQueue<SearchTree<T>.SearchTreeNode, int> queue = new();
			queue.Enqueue(tree.root, 0);
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (current.parent != null && current.parent.RootPath.AsParallel().Any(x => x.StateEquals(current.value)))
					continue;
				if (current.value.StateEquals(target))
				{
					path = current.RootPath.Reverse().ToList();
					return true;
				}
				if (maxDepth > 0 && current.depth == maxDepth)
					return false;
				current.AddChildren(current.value.GeneratePossibleChildren());
				queue.EnqueueRange(current.children, current.depth + 1);
			}
			return false;
		}

		bool TryFindSolutionStepwise(T target, out List<T> path, int maxDepth = 0)
		{
			path = null;
			PriorityQueue<SearchTree<T>.SearchTreeNode, int> queue = new();
			queue.Enqueue(tree.root, 0);
			while (queue.Count > 0)
			{
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
				Console.WriteLine("Current state queue:");
				foreach (var node in queue.UnorderedItems.OrderBy(x => x.Priority))
				{
					node.Element.value.Print();
					Console.WriteLine();
				}
				var current = queue.Dequeue();
				Console.WriteLine("Current state:");
				current.value.Print();
				if (current.parent != null && current.parent.RootPath.AsParallel().Any(x => x.StateEquals(current.value)))
				{
					Console.WriteLine("Cycle detected, skip state");
					continue;
				}
				if (current.value.StateEquals(target))
				{
					Console.WriteLine("Target state reached");
					path = current.RootPath.Reverse().ToList();
					return true;
				}
				if (maxDepth > 0 && current.depth == maxDepth)
					return false;
				current.AddChildren(current.value.GeneratePossibleChildren());
				Console.WriteLine("Child states found:");
				foreach (var child in current.children)
				{
					child.value.Print();
					Console.WriteLine();
				}
				queue.EnqueueRange(current.children, current.depth + 1);
			}
			return false;
		}

		public bool TryFindSolution(T target, out List<T> path, int maxDepth = 0, bool stepwise = false)
		{
			return stepwise ? TryFindSolutionStepwise(target, out path, maxDepth) : TryFindSolution(target, out path, maxDepth);
		}
	}
}
