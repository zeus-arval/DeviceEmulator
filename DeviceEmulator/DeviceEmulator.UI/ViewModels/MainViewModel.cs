using DeviceEmulator.ApplicationLayer;

namespace DeviceEmulator.UserInterface.ViewModels
{
    public class MainViewModel(ApplicationContext context) : ViewModelBase(context)
    {
        public NavigationViewModel NavigationViewModel { get; init; } = new NavigationViewModel(context);

    }
}
