using System.Threading.Tasks;
using System.Windows.Input;

namespace Nutri.Wpf.Infrastructure.Interfaces;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object? parameter);
}