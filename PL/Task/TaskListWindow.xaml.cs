using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status TaskStatus { get; set; } = BO.Status.Unscheduled;
        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        private void TaskExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = TaskStatus == BO.Status.Unscheduled?
            s_bl?.Task.ReadAll():
            s_bl?.Task.ReadAll(item => (BO.Status?)item?.Status == (TaskStatus));
            TaskList = temp == null ? new() : new(temp);
        }
        private void showTask(object sender, RoutedEventArgs e)
        {
            BO.Task? TaskInList = (sender as ListView)?.SelectedItem as BO.Task;
            if (TaskInList is not null)
            {
                new TaskWindow(TaskInList!.IdTask).ShowDialog();
            }
            else
            {
                new TaskWindow().ShowDialog();
            }
        }
        public TaskListWindow()
        {
            InitializeComponent();
            Activated += OnWindowActivated!;
        }
        private void OnWindowActivated(object sender, EventArgs e)
        {
            // קבלת רשימת המהנדסים מהשכבה העסקית
            var Tasks = s_bl?.Task.ReadAll();

            // עדכון רשימת המהנדסים בממשק המשתמש
            TaskList = Tasks == null ? new() : new(Tasks);
        }


    }
}
