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
    /// Interaction logic for EngineerWimdow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
       
        //public ObservableCollection<BO.Engineer> EngineerList
        //{
        //    get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerProperty); }
        //    set { SetValue(EngineerProperty, value); }
        //}
        //public static readonly DependencyProperty EngineerProperty =
        //DependencyProperty.Register("Engineer", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }
        public static readonly DependencyProperty EngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow));
        public EngineerWindow(int id=0)
        {
           
            InitializeComponent();
            if (id == 0)
            {
                CurrentEngineer = new BO.Engineer();
            }
            else
            {
                try
                {
                    CurrentEngineer = s_bl.Engineer.Read(id)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    Close();
                }
            }
        }
        public void AddOrUpdateEngineer(object sender, RoutedEventArgs e)
        {
            if(CurrentEngineer.IdEngineer==0)
            {
                s_bl.Engineer.Create(CurrentEngineer);
                MessageBox.Show("The engineer was successfully added");
            }
            else
            {
                s_bl.Engineer.Update(CurrentEngineer);
                MessageBox.Show("The engineer details have been successfully updated");

            }
            Close();
        }
    }
    
}
