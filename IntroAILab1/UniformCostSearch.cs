using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class UniformCostSearch<T> where T : class, IState<T>
	{
		SearchTree<T> tree;
		public UniformCostSearch(T startState)
		{
			tree = new SearchTree<T>(startState);
		}

		public bool TryFindSolution(T target, out List<T> path, int maxDepth = 0)
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
				if (current.depth == maxDepth)
					return false;
				current.AddChildren(current.value.GeneratePossibleChildren());
				queue.EnqueueRange(current.children, current.depth + 1);
			}
			return false;
		}
	}
}
