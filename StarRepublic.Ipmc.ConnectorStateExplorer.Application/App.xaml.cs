using System;
using System.IO;
using System.Windows;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            var ipmcEndpointViewModel = GetIpmcEndpointViewModel();

            var allConnectorStatesViewModel = GetAllConnectorStatesViewModel();

            var window = new MainWindow
            {
                DataContext = new MainWindowViewModel(
                    ipmcEndpointViewModel, allConnectorStatesViewModel)
            };

            window.Show();
        }

        private static AllIpmcEndpointsViewModel GetIpmcEndpointViewModel()
        {
            var repository = GetIpmcEndpointRepository();
            return new AllIpmcEndpointsViewModel(repository);
        }

        private static IpmcEndpointRepository GetIpmcEndpointRepository()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var endpointsFilePath = Path.Combine(appDataFolder, "ConnectorStateExplorer", "endpoints.xml");
            return new IpmcEndpointRepository(endpointsFilePath);
        }

        private static AllConnectorStatesViewModel GetAllConnectorStatesViewModel()
        {
            return new AllConnectorStatesViewModel(new ConnectorStateRepository());
        }
    }
}
