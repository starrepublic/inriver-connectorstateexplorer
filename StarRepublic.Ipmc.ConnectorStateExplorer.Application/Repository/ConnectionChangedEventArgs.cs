using System;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model
{
    public class ConnectedEventArgs : EventArgs
    {
        public IpmcEndpoint IpmcEndpoint { get; }

        public ConnectedEventArgs(IpmcEndpoint ipmcEndpoint)
        {
            IpmcEndpoint = ipmcEndpoint;
        }
    }
}