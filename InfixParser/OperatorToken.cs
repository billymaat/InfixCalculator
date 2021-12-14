using System;
using System.Collections.Generic;
using System.Text;

namespace InfixParser
{
	public class OperatorToken : Token
	{
		/// <summary>
		/// Operator type
		/// </summary>
		public enum OperatorType
		{
			Add,
			Subtract,
			Multiply,
			Divide
		}

		/// <summary>
		/// Gets operator type
		/// </summary>
		public OperatorType Type
		{
			get;
			set;
		}

		public char Operator
		{
			get;
			private set;
		}

		public OperatorToken(char op)
		{
			Operator = op;
			switch (op)
			{
				case '+':
					Type = OperatorType.Add;
					break;
				case '-':
					Type = OperatorType.Subtract;
					break;
				case '*':
					Type = OperatorType.Multiply;
					break;
				case '/':
					Type = OperatorType.Divide;
					break;
				default:
					throw new ArgumentException("Invalid operator");
			}
		}

		public override TokenType TokenType
		{
			get { return TokenType.Operator; }
		}

		public override string ToString()
		{
			return Operator.ToString();
		}
	}
}
