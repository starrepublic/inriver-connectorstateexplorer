using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class ConnectorStateViewModel : INotifyPropertyChanged
    {
        private readonly ConnectorStateRepository _connectorStateRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        private ConnectorState _connectorState;

        public ConnectorState ConnectorState
        {
            get => _connectorState;
            set
            {
                _connectorState = value;

                Id = ConnectorState.Id;
                Created = ConnectorState.Created;
                Modified = ConnectorState.Modified;
                ConnectorId = ConnectorState.ConnectorId;
                Data = ConnectorState.Data;

                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(Created));
                OnPropertyChanged(nameof(Modified));
                OnPropertyChanged(nameof(ConnectorId));
                OnPropertyChanged(nameof(Data));
            }
        }

        public int Id { get; private set; }

        public DateTime Created { get; private set; }

        public DateTime Modified { get; private set; }

        public string ConnectorId { get; set; }

        public string Data { get; set; }

        public ConnectorStateViewModel(
            ConnectorState connectorState,
            ConnectorStateRepository connectorStateRepository)
        {
            _connectorStateRepository = connectorStateRepository;
            ConnectorState = connectorState;
        }

        public void Save()
        {
            // TODO: look over this again.
            _connectorState.Data = Data;
            _connectorState.ConnectorId = ConnectorId;
            ConnectorState = _connectorStateRepository.Save(ConnectorState);
        }

        public void Delete()
        {
            _connectorStateRepository.Delete(ConnectorState);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}