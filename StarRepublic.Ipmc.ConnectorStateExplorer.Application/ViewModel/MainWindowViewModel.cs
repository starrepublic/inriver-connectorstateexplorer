using System;
using System.Windows;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class MainWindowViewModel
    {
        public AllIpmcEndpointsViewModel AllIpmcEndpointsViewModel { get; }

        public AllConnectorStatesViewModel AllConnectorStatesViewModel { get; }

        public MainWindowViewModel(
            AllIpmcEndpointsViewModel allIpmcEndpointsViewModel,
            AllConnectorStatesViewModel allConnectorStatesViewModel)
        {
            AllIpmcEndpointsViewModel = allIpmcEndpointsViewModel;
            AllConnectorStatesViewModel = allConnectorStatesViewModel;

            AllIpmcEndpointsViewModel.EndpointSelected += IpmcEndpointViewModel_EndpointSelected;
            AllIpmcEndpointsViewModel.SelectFirstEndpoint();
        }

        private async void IpmcEndpointViewModel_EndpointSelected(object sender, IpmcEndpointSelectedEventArgs e)
        {
            try
            {
                await AllConnectorStatesViewModel.SetIpmcEndpoint(e.IpmcEndpoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not establish a connection: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                AllIpmcEndpointsViewModel.SelectedEndpoint = null;
            }
        }
    }
}