namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.Model
{
    /// <summary>
    /// Wrapper for the inRiver internal DTO
    /// </summary>
    public class ConnectorState : inRiver.Remoting.Objects.ConnectorState
    {
        public ConnectorState()
        {

        }

        public ConnectorState(inRiver.Remoting.Objects.ConnectorState connectorState)
        {
            Id = connectorState.Id;
            Created = connectorState.Created;
            Modified = connectorState.Modified;
            ConnectorId = connectorState.ConnectorId;
            Data = connectorState.Data;
        }
    }
}