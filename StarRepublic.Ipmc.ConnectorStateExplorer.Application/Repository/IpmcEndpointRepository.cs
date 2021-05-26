using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Repository
{
    class IpmcEndpointRepository
    {
        private readonly FileInfo _file;
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(IpmcEndpoint[]));

        public IpmcEndpointRepository(string filePath)
        {
            _file = new FileInfo(filePath);
            _file.Directory.Create();
        }

        public IEnumerable<IpmcEndpoint> Read()
        {
            _file.Refresh();

            if (!_file.Exists)
                return Enumerable.Empty<IpmcEndpoint>();

            using (var reader = _file.OpenText())
            {
                return (IEnumerable<IpmcEndpoint>)_serializer.Deserialize(reader);
            }
        }

        public void Save(IEnumerable<IpmcEndpoint> ipmcEndpoints)
        {
            using (var writer = _file.CreateText())
            {
                _serializer.Serialize(writer, ipmcEndpoints.ToArray());
            }
        }
    }
}