using System;
using System.Collections.Generic;
using System.Text;
using static InfixParser.InfixEvaluator;

namespace InfixCalculator.WPF.Model
{
	public class InfixOutput
	{
		public InfixOutput()
		{

		}

		public double Output
		{
			get;
			set;
		}

		public ResultType Result
		{
			get;
			set;
		}

	}
}
