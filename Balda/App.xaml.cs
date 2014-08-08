using System.Windows;
using Balda.View;
using Balda.ViewModel;

namespace Balda
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StartWindow window = new StartWindow();
            StartViewModel viewModel = new StartViewModel(window);
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
