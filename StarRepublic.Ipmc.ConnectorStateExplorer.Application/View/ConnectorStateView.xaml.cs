using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.View
{
    /// <summary>
    /// Interaction logic for ConnectorStateView.xaml
    /// </summary>
    public partial class ConnectorStateView : UserControl
    {
        public static readonly DependencyProperty ConnectorIdsProperty =
            DependencyProperty.Register(
                "ConnectorIds",
                typeof(IEnumerable<string>),
                typeof(ConnectorStateView),
                new PropertyMetadata(Enumerable.Empty<string>()));

        public IEnumerable<string> ConnectorIds
        {
            get => (IEnumerable<string>)GetValue(ConnectorIdsProperty);
            set => SetValue(ConnectorIdsProperty, value);
        }

        public ConnectorStateView()
        {
            InitializeComponent();
        }
    }
}
