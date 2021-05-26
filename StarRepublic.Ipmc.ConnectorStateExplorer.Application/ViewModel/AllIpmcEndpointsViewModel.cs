using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Command;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.View;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class AllIpmcEndpointsViewModel
    {
        public event EventHandler<IpmcEndpointSelectedEventArgs> EndpointSelected;

        public ICommand DeleteCommand { get; }
        public ICommand CreateCommand { get; }

        private readonly IpmcEndpointRepository _ipmcEndpointRepository;

        private IpmcEndpointViewModel _selectedEndpoint;

        public ObservableCollection<IpmcEndpointViewModel> IpmcEndpoints { get; }

        public IpmcEndpointViewModel SelectedEndpoint
        {
            get => _selectedEndpoint;
            set
            {
                _selectedEndpoint = value;
                OnEndpointSelected(SelectedEndpoint);
            }
        }

        public AllIpmcEndpointsViewModel(IpmcEndpointRepository ipmcEndpointRepository)
        {
            _ipmcEndpointRepository = ipmcEndpointRepository;

            IpmcEndpoints = new ObservableCollection<IpmcEndpointViewModel>(
                _ipmcEndpointRepository.Read()
                    .Select(e => new IpmcEndpointViewModel(e)));

            DeleteCommand = new RelayCommand(
                p => Delete((IpmcEndpointViewModel)p),
                p => p != null);

            CreateCommand = new RelayCommand(
                p => Create());
        }

        private void Delete(IpmcEndpointViewModel ipmcEndpoint)
        {
            IpmcEndpoints.Remove(ipmcEndpoint);
            _ipmcEndpointRepository.Save(IpmcEndpoints.Select(e => e.Endpoint));
        }

        private void Create()
        {
            var newIpmcEndpoint = new IpmcEndpointViewModel(new IpmcEndpoint());

            var ipmcEndpointView = new IpmcEndpointView
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = newIpmcEndpoint
            };

            var result = ipmcEndpointView.ShowDialog();
            if (result == true)
            {
                IpmcEndpoints.Add(newIpmcEndpoint);
                _ipmcEndpointRepository.Save(IpmcEndpoints.Select(e => e.Endpoint));
            }
        }

        public void SelectFirstEndpoint()
        {
            SelectedEndpoint = IpmcEndpoints.FirstOrDefault();
        }

        protected virtual void OnEndpointSelected(IpmcEndpointViewModel ipmcEndpoint)
        {
            var e = new IpmcEndpointSelectedEventArgs(ipmcEndpoint);
            EndpointSelected?.Invoke(this, e);
        }
    }
}