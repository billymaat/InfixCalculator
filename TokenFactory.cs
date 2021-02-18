using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfixParser
{
	public static class TokenFactory
	{
		public static Token CreateToken(string value)
		{
			if (double.TryParse(value, out double num))
			{
				return CreateToken(num);
			}

			if (value.Length != 1)
			{
				throw new ArgumentException("Invalid operator or bracket length");
			}

			char c = value.First();
			switch (c)
			{
				case '(':
				case ')':
					return new BracketToken(c);
				case '+':
				case '-':
				case '*':
				case '/':
					return new OperatorToken(c);
				default:
					throw new ArgumentException("Unknown token");
			}
		}

		public static Token CreateToken(double value)
		{
			return new NumericToken(value);
		}
	}
}
