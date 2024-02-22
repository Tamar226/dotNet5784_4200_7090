using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            string first = Microsoft.VisualBasic.Interaction.InputBox("Enter a project start date", "First Date");

            // בדיקה אם המשתמש הזין ID

            // הצגת הודעה לאישור

            // איפוס מסד הנתונים רק אם המשתמש אישר

            DateTime StartDateFromUser = DateTime.Parse(first);
            string second = Microsoft.VisualBasic.Interaction.InputBox("Enter a project completion date", "End Date");

            DateTime EndDateFromUser = DateTime.Parse(second); ;

            s_bl.Milestone.CreateProjectSchedule(StartDateFromUser, EndDateFromUser);

            MessageBox.Show("Your project start date: " + StartDateFromUser + '\n' + "Your project end date: " + EndDateFromUser);

        }

    }
}