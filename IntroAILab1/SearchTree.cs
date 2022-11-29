using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal class SearchTree<T>
	{
		internal class SearchTreeNode
		{
			public T value;
			public SearchTreeNode parent;
			public List<SearchTreeNode> children;
			public int depth = 0;
			public float fValue = 0;
			//public bool marked = false;
			public SearchTreeNode(T value, SearchTreeNode parent, List<SearchTreeNode> children = null)
			{
				this.value = value;
				this.parent = parent;
				if (parent != null)
					depth = parent.depth + 1;
				if(children != null)
					this.children = children;
				else
					this.children = new List<SearchTreeNode>();
			}

			public void AddChildren(IEnumerable<T> children)
			{
				//foreach child create SearchTreeNode and insert as child
				this.children.AddRange(children.Select(x => new SearchTreeNode(x, this)));
			}

			public IEnumerable<T> RootPath {
				get
				{
					SearchTreeNode current = this;
					while (current != null)
					{
						yield return current.value;
						current = current.parent;
					}
				}
			}

			public int Count()
			{
				int cnt = 1;
				foreach (var child in children)
					cnt += child.Count();
				return cnt;
			}
		}

		public SearchTreeNode root;

		public SearchTree(T root)
		{
			this.root = new SearchTreeNode(root, null);
		}

		public int Count() => root.Count();
	}
}
