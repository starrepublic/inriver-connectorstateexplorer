using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inRiver.Remoting;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository
{
    class ConnectorStateRepository
    {
        public event EventHandler<ConnectedEventArgs> Connected;

        private RemoteManager _remoteManager;

        public async Task Connect(IpmcEndpoint ipmcEndpoint)
        {
            _remoteManager = await Task.Run(() => RemoteManager.CreateInstance(ipmcEndpoint.Uri, ipmcEndpoint.ApiKey));

            OnConnected(ipmcEndpoint);
        }

        public async Task<IEnumerable<ConnectorState>> Read()
        {
            if (_remoteManager == null)
                throw new InvalidOperationException("Can't read repository, no connection established.");

            var connectorStates = await Task.Run(() =>
                _remoteManager.UtilityService
                    .GetAllConnectorStates()
                    .Select(cs => new ConnectorState(cs))
            );

            return connectorStates;
        }

        public ConnectorState Save(ConnectorState connectorState)
        {
            var temp = new inRiver.Remoting.Objects.ConnectorState
            {
                ConnectorId = connectorState.ConnectorId,
                Created = connectorState.Created,
                Data = connectorState.Data,
                Id = connectorState.Id,
                Modified = connectorState.Modified
            };

            if (connectorState.Id == 0)
            {
                return new ConnectorState(
                    _remoteManager.UtilityService.AddConnectorState(temp));
            }

            return new ConnectorState(
                    _remoteManager.UtilityService.UpdateConnectorState(temp));
        }

        public void Delete(ConnectorState connectorState)
        {
            _remoteManager.UtilityService.DeleteConnectorState(connectorState.Id);
        }

        private void OnConnected(IpmcEndpoint ipmcEndpoint)
        {
            var e = new ConnectedEventArgs(ipmcEndpoint);
            Connected?.Invoke(this, e);
        }
    }
}
