using System;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model
{
    [Serializable]
    public class IpmcEndpoint
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ApiKey { get; set; }
    }
}