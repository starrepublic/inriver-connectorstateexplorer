using System.Linq;
using System.Windows.Controls;
using StarRepublic.Ipmc.ConnectorStateExplorer.Application.ViewModel;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.View
{
    /// <summary>
    /// Interaction logic for ConnectorStatesView.xaml
    /// </summary>
    public partial class AllConnectorStatesView : UserControl
    {
        public AllConnectorStatesView()
        {
            InitializeComponent();
        }

        // TODO: Find neater way of routing selected items to delete command
        private void ConnectorStateListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;
            var dc = (AllConnectorStatesViewModel)DataContext;

            var si = lb.SelectedItems.Cast<ConnectorStateViewModel>().ToArray();

            dc.SelectedConnectorStates = si;
        }
    }
}
