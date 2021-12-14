using System;
using System.Collections.Generic;
using System.Text;

namespace InfixParser
{
	public class BracketToken : Token
	{
		public override TokenType TokenType => TokenType.Bracket;

		public enum BracketType
		{
			LeftBracket,
			RightBracket
		}

		/// <summary>
		/// Gets bracket type
		/// </summary>
		public BracketType Type
		{
			get;
			private set;
		}
		
		public Char Bracket
		{
			get;
			private set;
		}

		public BracketToken(char bracket) : base()
		{
			Bracket = bracket;
			switch (bracket)
			{
				case '(':
					Type = BracketType.LeftBracket;
					break;
				case ')':
					Type = BracketType.RightBracket;
					break;
				default:
					throw new ArgumentException("not a bracket");
			}
		}

		public bool IsBracket(char c)
		{
			switch (c)
			{
				case '(':
				case ')':
					return true;
				default:
					return false;
			}
		}

		public override string ToString()
		{
			return Bracket.ToString();
		}

	}
}
