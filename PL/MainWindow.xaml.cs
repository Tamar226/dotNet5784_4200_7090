using PL.Engineer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void showEngineers(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void initDb(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to create Initial data? (Y/N)", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                DalTest.Initialization.Do();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}