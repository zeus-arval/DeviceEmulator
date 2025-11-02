using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DeviceEmulator.App;
using DeviceEmulator.ApplicationLayer;
using DeviceEmulator.UI.Views;
using DeviceEmulator.UserInterface.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceEmulator.UserInterface
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            _serviceProvider = CreateProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(_serviceProvider.GetRequiredService<ApplicationContext>()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private IServiceProvider CreateProvider() => DIInitializer
            .InitializeContainer()
            .BuildServiceProvider();
    }
}