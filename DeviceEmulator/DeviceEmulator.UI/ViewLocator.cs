using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace DeviceEmulator.UserInterface
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? data)
        {
            return data switch
            {
                NavigationViewModel vm => new NavigationUserControl { DataContext = vm },
                _ => new TextBlock { Text = $"View not found for {data.GetType().Name}" }
            };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
