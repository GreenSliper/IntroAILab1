using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal interface IState<T> where T: IState<T>
	{
		void Print();
		bool StateEquals(T other);
		List<T> GeneratePossibleChildren();
	}
}
