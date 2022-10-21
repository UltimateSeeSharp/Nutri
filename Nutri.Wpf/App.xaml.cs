using Nutri.Wpf.View;
using System.Globalization;
using System.Windows;

namespace Nutri.Wpf;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");

        Bootstrapper.Start();

        MainWindow = Bootstrapper.Resolve<MainWindow>();
        
        //MainWindow = Bootstrapper.Resolve<RegisterUserView>();
        MainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e) => Bootstrapper.Stop();
}