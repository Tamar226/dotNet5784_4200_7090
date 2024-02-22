using BO;
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
    /// Interaction logic for EngineerTask.xaml
    /// </summary>
    public partial class EngineerTask : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        int idEng = 0;
        public static readonly DependencyProperty EngineerProperty =
           DependencyProperty.Register("CurrentEngineerTask", typeof(BO.Engineer), typeof(EngineerTask));
        public EngineerTask(int id)
        {
            InitializeComponent();
            idEng = id;
        }

        private void ShowTask(object sender, RoutedEventArgs e)
        {

            new TaskWindow(3931).ShowDialog();
        }

        private void ShowTaskToDo(object sender, RoutedEventArgs e)
        {
            new EngineerTasksListWindow(idEng).Show();
        }
    }
}
