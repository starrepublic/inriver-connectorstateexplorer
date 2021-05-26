using System.Windows;

namespace StarRepublic.Ipmc.ConnectorStateExplorer.Application.View
{
    /// <summary>
    /// Interaction logic for IpmcEndpointView.xaml
    /// </summary>
    public partial class IpmcEndpointView : Window
    {
        public IpmcEndpointView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
