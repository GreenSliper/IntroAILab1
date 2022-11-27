using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class DepthFirstSearch<T> where T : class, IState
	{
		SearchTree<T> tree;
		public DepthFirstSearch(T startState)
		{
			tree = new SearchTree<T>(startState);
		}

		public bool TryFindSolution(T target, out List<T> path)
		{
			path = null;
			SearchTree<T>.SearchTreeNode current = tree.root;
			while (!current.value.StateEquals(target))
			{
				if (!current.marked)
				{
					current.AddChildren(current.value.GeneratePossibleChildren().Cast<T>());
					//mark as not the solution && not last
					current.marked = true;
					//find next child that is not in the current RootPath (that won't cause cycle)
					var nextChild = current.children.FirstOrDefault(x => !current.RootPath.Any(y => y.StateEquals(x.value)));
					//if has any children go to first 
					if (nextChild != null)
						current = nextChild;
					//if no children present, this is a "dead end", need to switch to other branch
					else if (current.parent != null)
						current = current.parent;
					//if we ascended to the root (no parent), there are no solutions
					else
						return false;
				}
				else //this node already has children, at least one of them is a "dead end"
				{
					//next checked child should be not one of the traversed previously (not marked)
					var nextChild = current.children.FirstOrDefault(x => !x.marked);
					if (nextChild == null)
						//all children are traversed, there is no solution in this branch
						if (current.parent != null)
							current = current.parent;
						else //ascended to the root, no solutions at all
							return false;
					else //there are not traversed children
						current = nextChild;
				}
			}
			path = new List<T>();
			while (current != null)
			{
				path.Add(current.value as T);
				current = current.parent;
			}
			path.Reverse();
			return true;
		}
	}
}
