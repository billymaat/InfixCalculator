using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InfixParser
{
	public class InfixEvaluator
	{
		/// <summary>
		/// Evaluate infix expression (given as string)
		/// </summary>
		/// <param name="infix">infix expression</param>
		/// <returns>result of infix (null if failed)</returns>
		internal static double? Evaluate(string infix)
		{
			// split infix into tokens
			List<Token> tokens = TokenParser.ParseTokens(infix);
			if (tokens == null)
			{
				Debug.WriteLine("Invalid token found in infix");
				return null;
			}

			try
			{
				Queue<Token> queue = InfixToPostfixConverter.ConvertInfixToPostfix(tokens);
				return PostfixProcessor.ProcessPostfix(queue);
			}
			catch (ArgumentException e)
			{
				Debug.WriteLine("Invalid postfix: " + e.Message);
				return null;
			}
		}
	}
}
