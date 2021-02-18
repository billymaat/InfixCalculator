using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixParser
{
	public static class TokenParser
	{
		/// <summary>
		/// Separate all massses (operands can be numbers or chemical formula) 
		/// and operators into Tokens for Shunting Yard algorithm.
		/// </summary>
		/// <param name="str">String object containing expression</param>
		/// <returns>A list of Tokens if all text in expression has been matched,
		/// otherwise will return null if there is text that is not a valid token</returns>
		public static List<Token> ParseTokens(string str)
		{
			string regex_number = @"(?<number>(\d+)\.?(\d*)) + ### match number
									(?!.*\])";
			str = Regex.Replace(str, @"\s+", string.Empty);

			// regex to match numbers (as decimals or not), operators, brackets, and formulas
			// if there is anything left in the string unmatched then there is problem with the tokens
			Regex regex = new Regex(@"(?x)
									(?:(?<!\[[^\]\[]*)(  ### make sure number does not start wth [
										" + regex_number + @"
										|
										(?<operator>[\+\-\*\/]{1})  ### match operator
										|
										(?<bracket>\(|\))   ### match bracket
										)(?!.*\])
										)");

			MatchCollection matchCollection = regex.Matches(str);

			List<Token> tokens = new List<Token>();
			foreach (Match match in matchCollection)
			{
				Token token = TokenFactory.CreateToken(match.Value);
				tokens.Add(token);
			}

			return tokens;
		}
	}
}
