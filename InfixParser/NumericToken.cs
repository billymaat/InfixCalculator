using System;
using System.Collections.Generic;
using System.Text;

namespace InfixParser
{
	public class NumericToken : Token
	{
		/// <summary>
		/// Gets or sets the numeric value
		/// </summary>
		public double NumericValue { get; set; }

		public NumericToken(double val) : base()
		{
			NumericValue = val;
		}

		public override TokenType TokenType
		{
			get { return TokenType.Numeric; }
		}
	}
}
