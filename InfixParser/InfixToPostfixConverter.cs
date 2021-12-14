using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InfixParser
{
	/// <summary>
	/// This class converts infix tokens and orders them into postfix
	/// </summary>
	public class InfixToPostfixConverter
	{
		/// <summary>
		/// Convert an infix expression, given as list of tokens, to a postfix expression
		/// </summary>
		/// <param name="tokens">List of tokens ordered as in infix</param>
		/// <returns>queue in postfix</returns>
		public static Queue<Token> ConvertInfixToPostfix(IEnumerable<Token> tokens)
		{
			// it's neater to enumerate twice for code readability, but I think these two method could be joined into one
			if (!CheckParenthesisAreMatched(tokens)) 
			{
				throw new ArgumentException("Unmatched parenthesis");
			}

			Queue<Token> queue = GetOutputQueue(tokens);
			return queue;
		}

		/// <summary>
		/// Check to see whether string has mismatched parenthesis
		/// </summary>
		/// <param name="tokens">tokens to check</param>
		/// <returns>True if parenthesis matched, otherwise false</returns>
		private static bool CheckParenthesisAreMatched(IEnumerable<Token> tokens)
		{
			int counter = 0;
			int index = -1;

			foreach (var token in tokens)
			{
				var bracketToken = token as BracketToken;

				if (bracketToken is null)
				{
					continue;
				}
				
				if (bracketToken.Type == BracketToken.BracketType.LeftBracket)
				{
					counter++;
				}
				else if (bracketToken.Type == BracketToken.BracketType.RightBracket)
				{
					// if we found a closed bracket without an opening bracket, error
					if (counter == 0)
					{
						// error at index
						return false;
					}
					counter--;
				}
			}

			if (counter == 0)
			{
				return true;
			}

			// error at index
			Debug.WriteLine("Error at index " + index);
			return false;
		}

		/// <summary>
		/// Shunting Yard algorithm
		/// https://simpledevcode.wordpress.com/2015/01/03/convert-infix-to-reverse-polish-notation-c-implementation/
		/// </summary>
		/// <param name="tokens">list of tokens</param>
		/// <returns>queue to process</returns>
		private static Queue<Token> GetOutputQueue(IEnumerable<Token> tokens)
		{
			Queue<Token> outputQueue = new Queue<Token>();
			Stack<Token> stack = new Stack<Token>();

			foreach (var token in tokens)
			{
				switch (token.TokenType)
				{
					case TokenType.Numeric:
						outputQueue.Enqueue(token);
						break;
					case TokenType.Operator:
						while (stack.Count > 0 && Token.CompareTokenPrecedence(stack.Peek(), token))
						{
							outputQueue.Enqueue(stack.Pop());
						}
						stack.Push(token);
						break;
					case TokenType.Bracket:
						BracketToken bracketToken = token as BracketToken;

						switch (bracketToken.Type)
						{
							case BracketToken.BracketType.LeftBracket:
								stack.Push(bracketToken);
								break;
							case BracketToken.BracketType.RightBracket:
								// if its a right bracket
								while (stack.Count > 0)
								{
									var tok = stack.Peek() as BracketToken;
									if (tok != null && tok.Type == BracketToken.BracketType.LeftBracket)
									{
										break;
									}
									
									outputQueue.Enqueue(stack.Pop());
								}
								stack.Pop(); // pop left bracket, hopefully, and chuck it away
								break;
							default:
								break;
						}
						break;

					case TokenType.Other:
						throw new ArgumentException("Token not recognised");
				}
			}

			while (stack.Count > 0)
			{
				outputQueue.Enqueue(stack.Pop());
			}
			return outputQueue;
		}
	}
}
