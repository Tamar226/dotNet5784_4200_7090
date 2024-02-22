using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        private void showEngineers(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void showTasks(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }
        public ManegerWindow()
        {
            InitializeComponent();
        }

        private void creatProject(object sender, RoutedEventArgs e)
        {
            DateTime a =new DateTime(2/2/2024);
            DateTime b = new DateTime(2 / 12 / 2024);

            s_bl.Milestone.CreateProjectSchedule(a,b);
            MessageBox.Show("The Task details have been successfully updated");

        }
    }
}
