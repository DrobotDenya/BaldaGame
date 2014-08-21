using System.Windows;

namespace Balda.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindows.xaml
    /// </summary>
    public partial class RegistrationWindows : Window
    {
        public RegistrationWindows()
        {
            InitializeComponent();
        }

        //for the future
        //private void AnimationTextBox(TextBox textBox)
        //{
        //    ColorAnimation colorAnimation = new ColorAnimation();
        //    colorAnimation.From = Colors.White;
        //    colorAnimation.To = Colors.Red;
        //    colorAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.4));
        //    colorAnimation.AutoReverse = true;
        //    colorAnimation.RepeatBehavior = new RepeatBehavior(3);
        //    textBox.Background = new SolidColorBrush(Colors.Red);
        //    textBox.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        //}

    }
}
