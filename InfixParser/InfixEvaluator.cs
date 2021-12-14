using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InfixParser
{
	public class InfixEvaluator
	{
		public InfixEvaluator(string infix)
		{
			Result = Evaluate(infix);
		}

		public enum ResultType
		{
			None,
			Success,
			InvalidToken,
			InvalidInfix
		}

		public ResultType Result
		{
			get;
			private set;
		}

		public List<Token> InfixTokens
		{
			get;
			private set;
		}

		public IEnumerable<Token> PostFixTokens
		{
			get;
			private set;
		}

		public double ResultValue
		{
			get;
			private set;
		}

		/// <summary>
		/// Evaluate infix expression (given as string)
		/// </summary>
		/// <param name="infix">infix expression</param>
		/// <returns>result of infix (null if failed)</returns>
		private ResultType Evaluate(string infix)
		{
			// split infix into tokens
			if (!TokenParser.TryParseTokens(infix, out var tokens))
			{
				Debug.WriteLine("Invalid token found in infix");
				return ResultType.InvalidToken;
			}

			InfixTokens = tokens;

			try
			{
				Queue<Token> queue = InfixToPostfixConverter.ConvertInfixToPostfix(tokens);
				PostFixTokens = new List<Token>(queue);
				ResultValue = PostfixProcessor.ProcessPostfix(queue);

				return ResultType.Success;
			}
			catch (ArgumentException e)
			{
				Debug.WriteLine("Invalid postfix: " + e.Message);
				return ResultType.InvalidInfix;
			}
		}
	}
}
