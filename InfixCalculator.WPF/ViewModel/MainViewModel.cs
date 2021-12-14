using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
		private string _infixResult;

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
			get => _infixResult;
			set => Set(ref _infixResult, value);
		}

		private void CalculateInfix(string infix)
		{
			// do calculation
			var infixOutput = _infixService.Calculate(infix);
			InfixResult = infixOutput.Output.ToString();
		}

		private bool CanCalculateInfix(string arg)
		{
			return true;
		}
	}
}
