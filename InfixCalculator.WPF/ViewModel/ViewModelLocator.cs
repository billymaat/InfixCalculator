using GalaSoft.MvvmLight.Ioc;
using InfixCalculator.WPF.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfixCalculator.WPF.ViewModel
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			SimpleIoc.Default.Register<MainViewModel>();

			SimpleIoc.Default.Register<InfixService>();
		}
		
		public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
	}
}
