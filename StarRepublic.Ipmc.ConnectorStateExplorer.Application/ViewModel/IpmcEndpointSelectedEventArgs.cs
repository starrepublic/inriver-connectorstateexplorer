using System;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class IpmcEndpointSelectedEventArgs : EventArgs
    {
        public IpmcEndpointViewModel IpmcEndpoint { get; }

        public IpmcEndpointSelectedEventArgs(IpmcEndpointViewModel endpoint)
        {
            IpmcEndpoint = endpoint;
        }
    }
}