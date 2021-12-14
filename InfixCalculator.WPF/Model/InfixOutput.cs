using System;
using System.Collections.Generic;
using System.Text;
using static InfixParser.InfixEvaluator;

namespace InfixCalculator.WPF.Model
{
	public class InfixOutput
	{
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

		public string PostFixText
		{
			get;
			set;
		}

	}
}
