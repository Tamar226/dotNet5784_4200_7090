using PL.Engineer;
using PL.Task;
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
using System.Windows.Diagnostics;
using System.Windows.Threading;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;


        private void InitDb(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to create Initial data? (Y/N)", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
            }

        }
        private void ResetDb(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reset the database?(Y/N)", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                DalTest.Initialization.ResetDB();
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();

        }
        private void InitializeTimer()
        {
            // יצירת טיימר
            timer = new DispatcherTimer();

            // קביעת מהירות עדכון (בדוגמה, כל שנייה)
            timer.Interval = TimeSpan.FromSeconds(1);

            // הוספת אירוע לעדכון השעה
            timer.Tick += Timer_Tick;

            // התחלת הטיימר
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // קבלת השעה הנוכחית
            DateTime currentTime = DateTime.Now;

            // הצגת השעה בתיבת הטקסט של השעה
            timeText.Text = currentTime.ToString("HH:mm:ss");

            // הצגת התאריך בתיבת הטקסט של התאריך
            dateText.Text = currentTime.ToString("dddd, MMMM dd, yyyy");
        }

        private void ShowMangerMenu(object sender, RoutedEventArgs e)
        {
            new ManegerWindow().Show();
        }

        private void GetIdEngineer(object sender, RoutedEventArgs e)
        {

            // הצגת תיבת טקסט בתוך MessageBox
            string id = Microsoft.VisualBasic.Interaction.InputBox("Enter Engineer ID", "ID");

            // בדיקה אם המשתמש הזין ID
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            // הצגת הודעה לאישור

            // איפוס מסד הנתונים רק אם המשתמש אישר
        
             new EngineerTask(int.Parse(id)).Show();
        }
    }
}