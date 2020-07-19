using System;
using MCSM.Ui.Util.Ui;
using MCSM.Ui.ViewModel;
using Terminal.Gui;

namespace MCSM.Ui.Views
{
    public class TestView : View, IViewFor<TestViewModel>
    {
        public TestView(TestViewModel viewModel)
        {
            Width = Dim.Fill();
            Height = Dim.Fill();

            ViewModel = viewModel;

            var label = new Label("");
            ViewModel.Text.Subscribe(text => label.Text = text);
            base.Add(label);
        }

        public TestViewModel ViewModel { get; }
    }
}