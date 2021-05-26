using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel
{
    class IpmcEndpointViewModel
    {
        public IpmcEndpoint Endpoint { get; }

        public IpmcEndpointViewModel(IpmcEndpoint endpoint)
        {
            Endpoint = endpoint;
        }

        public string Name
        {
            get => Endpoint.Name;
            set => Endpoint.Name = value;
        }

        public string Uri
        {
            get => Endpoint.Uri;
            set => Endpoint.Uri = value;
        }

        public string ApiKey
        {
            get => Endpoint.ApiKey;
            set => Endpoint.ApiKey = value;
        }
    }
}