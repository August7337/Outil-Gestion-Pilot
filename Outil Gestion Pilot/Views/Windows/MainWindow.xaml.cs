using Outil_Gestion_Pilot.Services;
using Outil_Gestion_Pilot.ViewModels.Windows;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Outil_Gestion_Pilot.Views.Windows
{
    public partial class MainWindow : INavigationWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationViewPageProvider navigationViewPageProvider,
            INavigationService navigationService
        )
        {
            ViewModel = viewModel;
            DataContext = this;

            SystemThemeWatcher.Watch(this);

            ConnectionWindow connectionWindow = new ConnectionWindow();
            bool? result = connectionWindow.ShowDialog();
            if (result != true)
                Environment.Exit(0);

            InitTab();

            InitializeComponent();
            
            SetPageService(navigationViewPageProvider);

            navigationService.SetNavigationControl(RootNavigation);
        }

        /// <summary>
        /// Initializes the tab by removing specific menu items based on the user's role.
        /// </summary>
        private void InitTab()
        {
            if (SessionService.Instance.Role == "Responsable production")
            {
                var itemsToRemove = new List<object>();

                foreach (var item in ViewModel.MenuItems)
                {
                    if (item is NavigationViewItem navigationItem)
                    {
                        string content = navigationItem.Content?.ToString();
                        if (content == "Panier" || content == "Commandes" || content == "Revendeurs")
                        {
                            itemsToRemove.Add(item);
                        }
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    ViewModel.MenuItems.Remove(item);
                }
            }
        }

        #region INavigationWindow methods

        public INavigationView GetNavigation() => RootNavigation;

        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        public void SetPageService(INavigationViewPageProvider navigationViewPageProvider) => RootNavigation.SetPageProviderService(navigationViewPageProvider);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        INavigationView INavigationWindow.GetNavigation()
        {
            throw new NotImplementedException();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
