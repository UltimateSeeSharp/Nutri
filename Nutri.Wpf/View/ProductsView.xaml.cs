using Nutri.Wpf.ViewModel;
using System.Windows.Controls;

namespace Nutri.Wpf.View;

public partial class ProductsView : UserControl
{
    public ProductsView()
    {
        InitializeComponent();
        DataContext = Bootstrapper.Resolve<ProductsViewModel>();
    }
}
