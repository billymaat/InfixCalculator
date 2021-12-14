using InfixCalculator.WPF.Model;
using InfixParser;
using System;
using System.Collections.Generic;
using System.Text;
using static InfixParser.InfixEvaluator;

namespace InfixCalculator.WPF.Services
{
	public class InfixService
	{
		public InfixService()
		{

		}

		//public bool Calculate(string infix, out double output, out ResultType result)
		//{
		//	InfixEvaluator evaluator = new InfixEvaluator(infix);
		//	result = evaluator.Result;
		//	output = evaluator.ResultValue;

		//	return evaluator.Result == ResultType.Success;
		//}

		public InfixOutput Calculate(string infix)
		{
			InfixEvaluator evaluator = new InfixEvaluator(infix);

			var infixOutput = new InfixOutput()
			{
				Result = evaluator.Result,
				Output = evaluator.ResultValue
			};

			return infixOutput;
		}
	}
}
