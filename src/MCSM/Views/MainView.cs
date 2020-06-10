using System;
using MCSM.ViewModels;
using Terminal.Gui;

namespace MCSM.Views
{
    /// <summary>
    ///     Main view kind of a dashboard
    /// </summary>
    public class MainView : View
    {
        public MainView()
        {
            X = 0;
            Y = 1;
            Height = Dim.Fill() - 1;
            Width = Dim.Fill();

            var viewModel = new MainViewModel();

            var input = new TextField(0, 0, 10, "");
            var output = new Label(new Rect(0, 1, 20, 1), "");

            input.TextChanged = _ => viewModel.Input.Value = input.Text.ToString();
            viewModel.Output.Subscribe(text => output.Text = text);

            base.Add(input);
            base.Add(output);
        }
    }
}