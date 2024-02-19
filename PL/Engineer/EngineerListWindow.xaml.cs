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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.EngineerExperience engineerExperience { get; set; } = BO.EngineerExperience.None;
        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        private void engineerExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = engineerExperience == BO.EngineerExperience.None ?
            s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => (BO.EngineerExperience?)item?.Level == (engineerExperience));
            EngineerList = temp == null ? new() : new(temp);
        }
        private void showEngineer(object sender, RoutedEventArgs e)
        {
            BO.Engineer? EngineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
           if(EngineerInList is not null)
            {
                new EngineerWindow(EngineerInList!.IdEngineer).ShowDialog();
            }
            else
            {
                new EngineerWindow().ShowDialog();
            }
        }
        public EngineerListWindow()
        {
            InitializeComponent();
            Activated += OnWindowActivated!;
        }
        private void OnWindowActivated(object sender, EventArgs e)
        {
            // קבלת רשימת המהנדסים מהשכבה העסקית
            var engineers = s_bl?.Engineer.ReadAll();

            // עדכון רשימת המהנדסים בממשק המשתמש
            EngineerList = engineers == null ? new() : new(engineers);
        }

      
    }
}
