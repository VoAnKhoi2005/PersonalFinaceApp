﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceApp.Database;
using PersonalFinanceApp.etc;
using PersonalFinanceApp.View;
using PersonalFinanceApp.ViewModel.LoginMenu;
using PersonalFinanceApp.ViewModel.MainMenu;
using PersonalFinanceApp.ViewModel.Stores;

namespace PersonalFinanceApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //Default login window
            NavigationStore navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
            navigationStore.CurrentViewModel = _serviceProvider.GetRequiredService<LoginNewAccountViewModel>();
            MainWindow = _serviceProvider.GetRequiredService<LoginWindow>();

            //Default main window
            //NavigationStore navigationStore = _serviceProvider.GetRequiredService<NavigationStore>();
            //navigationStore.CurrentViewModel = _serviceProvider.GetRequiredService<DashboardViewModel>();
            //MainWindow = _serviceProvider.GetRequiredService<MainWindow>();


            MainWindow.Show();

            //Create database
            using (var context = new AppDbContext())
            {
                context.EnsureDatabaseCreated();
            }

            DBManager.AutoDelete();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<NavigationStore>();

            //Login window
            services.AddSingleton<LoginMainViewModel>();
            services.AddSingleton<LoginWindow>(s => new LoginWindow
            {
                DataContext = s.GetRequiredService<LoginMainViewModel>()
            });
            services.AddTransient<LoginNewAccountViewModel>(s => new LoginNewAccountViewModel(s));
            services.AddTransient<ResetPasswordViewModel>(s => new ResetPasswordViewModel(s));
            services.AddTransient<CodeVerificationViewModel>(s => new CodeVerificationViewModel(s));
            services.AddTransient<CreateNewPasswordViewModel>(s => new CreateNewPasswordViewModel(s));

            //Main window
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IMainWindowFactory>(s =>
            {
                var dataContext = s.GetRequiredService<MainViewModel>();
                return new MainWindowFactory(dataContext, s);
            });
            //services.AddSingleton<MainWindow>(s => new MainWindow
            //{
            //    DataContext = s.GetRequiredService<MainViewModel>()
            //});
            services.AddTransient<DashboardViewModel>(s => new DashboardViewModel(s));
            services.AddTransient<GoalplanViewModel>(s => new GoalplanViewModel(s));
            services.AddTransient<SummaryViewModel>(s => new SummaryViewModel(s));
        }
    }
}