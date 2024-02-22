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

namespace PL
{
    /// <summary>
    /// Interaction logic for EngineerTasksListWindow.xaml
    /// </summary>
    public partial class EngineerTasksListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public ObservableCollection<BO.Task> EngineerTaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(EngineerTaskListProp); }
            set { SetValue(EngineerTaskListProp, value); }
        }

        public static readonly DependencyProperty EngineerTaskListProp =
DependencyProperty.Register("EngineerTaskList", typeof(ObservableCollection<BO.Task>), typeof(EngineerTasksListWindow), new PropertyMetadata(null));

        public EngineerTasksListWindow(int id=0)
        {
            InitializeComponent();
            BO.Engineer currentEnguneer = s_bl.Engineer.Read(id)!;
            var exEng = currentEnguneer!.Level;
            BO.Status TaskStatus = BO.Status.Unscheduled;
            var temp =s_bl?.Task.ReadAll(item=>((item.CopmlexityLevel==exEng)&&(BO.Status?)item?.Status == (TaskStatus)));
            EngineerTaskList = temp == null ? new() : new(temp);
        }
    }
}
