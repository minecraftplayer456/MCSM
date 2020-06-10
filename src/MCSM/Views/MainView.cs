using System;
using System.Reflection.Metadata.Ecma335;
using MCSM.Util;
using MCSM.ViewModels;
using Terminal.Gui;

namespace MCSM.Views
{
    /// <summary>
    ///     Main view kind of a dashboard
    /// </summary>
    public class MainView : Window
    {
        private readonly MainViewModel _viewModel;
        
        public MainView() : base($"MCMS - {Constants.MCSMVersion}")
        {
            X = 0;
            Y = 1;
            Height = Dim.Fill() - 1;
            Width = Dim.Fill();
            
            var viewModel = new MainViewModel();

            var input = new TextField(0, 0, 10, "");
            var output = new Label(new Rect(0, 1, 20, 1), "");
            
            viewModel.Name.Subscribe(name =>
            {
                output.Text = name;
            });
            input.TextChanged = _ =>
            {
                viewModel.Name.Value = input.Text.ToString();
            };

            base.Add(input);
            base.Add(output);
        }
    }
}