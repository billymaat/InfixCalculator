using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixParser
{
	/// <summary>
	/// Token type
	/// </summary>
	public enum TokenType
	{
		Operator,
		Numeric,
		Bracket,
		Other
	}

	/// <summary>
	/// Class for token in Shunting Yard algorithm
	/// </summary>
	public abstract class Token
	{
		#region separators

		private const char LeftBracket = '(';
		private const char RightBracket = ')';

		private const char Colon = ':';
		private const char Comma = ',';

		private const char Add = '+';
		private const char Subtract = '-';
		private const char Multiply = '*';
		private const char Divide = '/';

		#endregion

		protected Token()
		{
		}

		/// <summary>
		/// Gets token type
		/// </summary>
		public abstract TokenType TokenType
		{
			get;
		}

		/// <summary>
		/// Operate on two tokens (operands)
		/// </summary>
		/// <param name="token1">first token</param>
		/// <param name="token2">second token</param>
		/// <param name="op">the operator</param>
		/// <returns>output of operation</returns>
		public static double Operate(NumericToken token1, NumericToken token2, OperatorToken op)
		{
			double value1 = token1.NumericValue;
			double value2 = token2.NumericValue;

			switch (op.Type)
			{
				case OperatorToken.OperatorType.Add:
					return value1 + value2;
				case OperatorToken.OperatorType.Subtract:
					return value1 - value2;
				case OperatorToken.OperatorType.Multiply:
					return value1 * value2;
				case OperatorToken.OperatorType.Divide:
					return value1 / value2;
				default:
					throw new ArgumentException("Undefined operation");
			}
		}

		/// <summary>
		/// Get precedence of operators
		/// </summary>
		/// <param name="token">token to check precedence</param>
		/// <returns>int representing precedence (high int, higher precedence)</returns>
		public static int GetPrecedence(Token token)
		{
			var operatorToken = token as OperatorToken;

			if (operatorToken is null)
			{
				return 0;
			}

			switch (operatorToken.Type)
			{
				case OperatorToken.OperatorType.Add:
				case OperatorToken.OperatorType.Subtract:
					return 1;
				case OperatorToken.OperatorType.Multiply:
				case OperatorToken.OperatorType.Divide:
					return 2;
				default:
					throw new Exception("Should never get here");
			}
		}

		public static bool CompareTokenPrecedence(Token token1, Token token2)
		{
			return GetPrecedence(token1) >= GetPrecedence(token2);
		}
	}
}
