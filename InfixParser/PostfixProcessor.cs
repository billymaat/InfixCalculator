using System;
using System.Collections.Generic;
using System.Text;

namespace InfixParser
{
	public class PostfixProcessor
	{
		/// <summary>
		/// process the postfix queue
		/// http://btechsmartclass.com/DS/U2_T6.html
		/// </summary>
		/// <param name="queue">Postfix queue</param>
		/// <returns>Result after processing queue</returns>
		public static double ProcessPostfix(Queue<Token> queue)
		{
			Stack<Token> processingStack = new Stack<Token>();
			while (queue.Count > 0)
			{
				Token token = queue.Peek();
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Numeric)
				{
					processingStack.Push(queue.Dequeue());
				}
				else if (tokenType == TokenType.Operator)
				{
					if (processingStack.Count < 2)
					{
						throw new ArgumentException("Invalid postfix: Stack too small");
					}

					// expect numeric tokens
					NumericToken token1 = processingStack.Pop() as NumericToken;
					NumericToken token2 = processingStack.Pop() as NumericToken;

					if (token1 is null || token2 is null)
					{
						throw new ArgumentException("Invalid postfix: Trying to operate on tokens that are not operands");
					}

					OperatorToken operatorToken = queue.Dequeue() as OperatorToken;

					double operationResult = Token.Operate(token2, token1, operatorToken);

					processingStack.Push(TokenFactory.CreateToken(operationResult));
				}
			}

			if (processingStack.Count != 1)
			{
				throw new ArgumentException("Invalid postfix: Stack doesn't have right count");
			}

			var numericToken = processingStack.Pop() as NumericToken;
			if (numericToken is null)
			{
				throw new Exception("Something when wrong with the tokens!");
			}

			if (processingStack.Count > 0)
			{
				throw new Exception("Invalid postfix: Stack hasn't been emptied");
			}

			return numericToken.NumericValue; ;
		}
	}
}
