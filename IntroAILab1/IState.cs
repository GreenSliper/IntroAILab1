using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAILab1
{
	internal interface IState
	{
		void Print();
		List<IState> GeneratePossibleChildren();
	}
}
