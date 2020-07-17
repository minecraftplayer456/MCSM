using System;
using MCSM.Util;
using MCSM.ViewModels;
using Terminal.Gui;

namespace MCSM.Views
{
    public class TestView : View, IViewFor<TestViewModel>
    {
        public TestView(TestViewModel viewModel)
        {
            X = 0;
            Y = 0;
            Width = Dim.Fill();
            Height = Dim.Fill();

            ViewModel = viewModel;

            var label = new Label(new Rect(0, 0, 10, 1), "Hello World!");

            ViewModel.Text.Subscribe(text => label.Text = text);

            base.Add(label);
        }

        public TestViewModel ViewModel { get; }
    }
}