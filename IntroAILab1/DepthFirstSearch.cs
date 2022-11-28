using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class DepthFirstSearch<T> where T : class, IState<T>
	{
		SearchTree<T> tree;
		public DepthFirstSearch(T startState)
		{
			tree = new SearchTree<T>(startState);
		}

		SearchTree<T>.SearchTreeNode FindSolution(SearchTree<T>.SearchTreeNode node, T target, int maxSteps = 0)
		{
			//node.marked = true;
			if(node.value.StateEquals(target))
				return node;
			if ((maxSteps != 0 && node.depth == maxSteps) || 
				(node.parent != null && node.parent.RootPath.AsParallel().Any(x => x.StateEquals(node.value))))
				return null;
			node.AddChildren(node.value.GeneratePossibleChildren());
			foreach (var child in node.children)
			{
				//if (child.marked)
				//	continue;
				var dfs = FindSolution(child, target, maxSteps);
				if (dfs != null)
					return dfs;
			}
			return null;
		}
		public bool TryFindSolution(T target, out List<T> path, int maxSteps = 0)
		{
			path = null;
			SearchTree<T>.SearchTreeNode current = tree.root;
			var soulution = FindSolution(current, target, maxSteps);
			if (soulution != null)
				path = soulution.RootPath.Reverse().ToList();
			return soulution != null;
		}
	}
}
