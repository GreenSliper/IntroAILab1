using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal static class Extensions
	{
		public static int MinIndexBy<T>(this IEnumerable<T> collection, Func<T, float> selector)
		{
			float min = float.MaxValue;
			int minIndex = -1;
			int i = 0;
			foreach (T item in collection)
			{
				var val = selector(item);
				if (val < min)
				{
					min = val;
					minIndex = i;
				}
				i++;
			}
			return minIndex;
		}
	}
}
