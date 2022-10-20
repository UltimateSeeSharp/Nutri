using Nutri.Wpf.ViewModel;
using System.Windows;

namespace Nutri.Wpf.View;

public partial class RegisterUserView : Window
{
    public RegisterUserView()
    {
        InitializeComponent();
        DataContext = Bootstrapper.Resolve<RegisterUserViewModel>();
    }
}
