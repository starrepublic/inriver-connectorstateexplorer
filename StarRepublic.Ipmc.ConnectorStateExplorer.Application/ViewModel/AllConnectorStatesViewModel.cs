using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Command;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.View;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class AllConnectorStatesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ConnectorStateRepository _connectorStateRepository;

        public ICommand DeleteCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand SaveCommand { get; }

        public bool IsConnected { get; private set; }

        private ObservableCollection<ConnectorStateViewModel> _connectorStates;
        public ObservableCollection<ConnectorStateViewModel> ConnectorStates
        {
            get => _connectorStates;
            private set
            {
                _connectorStates = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<ConnectorStateViewModel> _selectedConnectorStates;
        public IEnumerable<ConnectorStateViewModel> SelectedConnectorStates
        {
            get => _selectedConnectorStates;
            set
            {
                _selectedConnectorStates = value;
                OnPropertyChanged();
            }
        }

        private ConnectorStateViewModel _selectedConnectorState;
        public ConnectorStateViewModel SelectedConnectorState
        {
            get => _selectedConnectorState;
            set
            {
                _selectedConnectorState = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _connectorIds;
        public ObservableCollection<string> ConnectorIds
        {
            get => _connectorIds;
            set
            {
                _connectorIds = value;
                OnPropertyChanged();
            }
        }

        public async Task SetIpmcEndpoint(IpmcEndpointViewModel ipmcEndpoint)
        {
            if (ipmcEndpoint == null)
            {
                ConnectorStates = null;
                return;
            }

            await _connectorStateRepository.Connect(ipmcEndpoint.Endpoint);
        }

        public AllConnectorStatesViewModel(ConnectorStateRepository connectorStateRepository)
        {
            _connectorStateRepository = connectorStateRepository;
            _connectorStateRepository.Connected += _connectorStateRepository_Connected;

            ReloadCommand = new RelayCommand(async
                p => await Load(),
                p => IsConnected);

            DeleteCommand = new RelayCommand(
                p => Delete(SelectedConnectorStates),
                p => SelectedConnectorStates != null && SelectedConnectorStates.Count() != 0);

            CreateCommand = new RelayCommand(
                p => Create((ConnectorStateView)p));

            SaveCommand = new RelayCommand(
                p => Save((ConnectorStateViewModel)p));
        }

        private void Save(ConnectorStateViewModel connectorStateViewModel)
        {
            var isCreated = connectorStateViewModel.Id == 0;

            connectorStateViewModel.Save();

            //_connectorStateRepository.Save(connectorStateViewModel.ConnectorState); // TODO: consider this alternative?

            if (isCreated)
            {
                ConnectorStates.Add(connectorStateViewModel);
                SelectedConnectorState = connectorStateViewModel;
            }
        }

        private void Create(ConnectorStateView connectorStateView)
        {
            var temp = new ConnectorStateViewModel(new ConnectorState(), _connectorStateRepository);
            SelectedConnectorState = temp;
            //connectorStateView.DataContext = temp; // TODO: Destroys data binding for future events but is a prettier solution
        }

        private async void _connectorStateRepository_Connected(object sender, ConnectedEventArgs e)
        {
            await Load();
            IsConnected = true;
        }

        public async Task Load()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                ConnectorStates = new ObservableCollection<ConnectorStateViewModel>(
                    (await _connectorStateRepository.Read())
                    .Select(cs => new ConnectorStateViewModel(cs, _connectorStateRepository)));

                ConnectorIds = new ObservableCollection<string>(ConnectorStates
                    .GroupBy(cs => cs.ConnectorId)
                    .OrderBy(csg => csg.Key)
                    .Select(csg => csg.Key));
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        public void Delete(IEnumerable<ConnectorStateViewModel> connectorStates)
        {
            var statesEnumerated = connectorStates.ToArray();

            var result = MessageBox.Show($"Delete {connectorStates.Count()} connector state(s)?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (result != MessageBoxResult.OK)
                return;

            foreach (var connectorState in statesEnumerated)
            {
                connectorState.Delete();
                ConnectorStates.Remove(connectorState);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
