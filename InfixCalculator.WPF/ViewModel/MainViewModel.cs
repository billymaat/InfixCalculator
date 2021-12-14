using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InfixCalculator.WPF.Model;
using InfixCalculator.WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static InfixParser.InfixEvaluator;

namespace InfixCalculator.WPF.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		private string _infixText;
		private string _postFixText;
		private InfixOutput _infixOutput;

		private readonly InfixService _infixService;

		public MainViewModel(InfixService infixService)
		{
			_infixService = infixService;
			CalculateCommand = new RelayCommand<string>(CalculateInfix, CanCalculateInfix);
		}

		#region commands

		public ICommand CalculateCommand
		{
			get;
		}

		#endregion

		public string InfixText
		{
			get => _infixText;
			set => Set(ref _infixText, value);
		}

		public string InfixResult
		{
			get
			{
				return InfixOutputToString(_infixOutput);
			}
		}

		public string PostFixText
		{
			get
			{
				return InfixOutput?.PostFixText;
			}
		}

		private string InfixOutputToString(InfixOutput infixOutput)
		{
			if (infixOutput is null)
			{
				return "N/A";
			}

			switch (infixOutput.Result)
			{
				case ResultType.Success:
					return infixOutput.Output.ToString();
				case ResultType.InvalidToken:
					return "Invalid token";
				case ResultType.InvalidInfix:
					return "Invalid infix";
				default:
					return "Unknown";
			}
		}

		public InfixOutput InfixOutput
		{
			get => _infixOutput;
			set => Set(ref _infixOutput, value);
		}

		private void CalculateInfix(string infix)
		{
			// do calculation
			InfixOutput = _infixService.Calculate(infix);
			RaisePropertyChanged(nameof(InfixResult));
			RaisePropertyChanged(nameof(PostFixText));
		}

		private bool CanCalculateInfix(string arg)
		{
			return true;
		}
	}
}
