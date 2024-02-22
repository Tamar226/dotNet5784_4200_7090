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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow));
        public TaskWindow(int id = 0)
        {

            InitializeComponent();
            if (id == 0)
            {
                CurrentTask = new BO.Task();
            }
            else
            {
                try
                {
                    CurrentTask = s_bl.Task.Read(id)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    Close();
                }
            }
        }
        public void AddOrUpdateTask(object sender, RoutedEventArgs e)
        {
            if (CurrentTask.IdTask == 0)
            {
                s_bl.Task.Create(CurrentTask);
                MessageBox.Show("The Task was successfully added");
            }
            else
            {
                s_bl.Task.Update(CurrentTask);
                MessageBox.Show("The Task details have been successfully updated");
            }
            Close();
        }

       
    }
}

