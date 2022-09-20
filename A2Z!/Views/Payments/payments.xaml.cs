using A2Z_.Models;
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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for payments.xaml
    /// </summary>
    public partial class payments : Window
    {
        public payments()
        {
            InitializeComponent();
            CheckAdmin();
            GridMain.Navigate(new P_Student_Payments());
        }

        private void CheckAdmin()
        {
            Permession permession = new Permession();
            bool CheckAdmin = permession.CheckIfAdminOrUser();
            if (CheckAdmin)
            {
                TeacherPayment.Visibility = Visibility.Visible;
                ShowOutlay.Visibility = Visibility.Visible;
                ShowCourses.Visibility = Visibility.Visible;
                Stochastic.Visibility = Visibility.Visible;
                DailyPayments.Visibility = Visibility.Visible;
                SpecialStochastic.Visibility = Visibility.Visible;
                Terms.Visibility = Visibility.Visible;
            }
            else
            {  }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Permession permession = new Permession();
            bool CheckAdmin = permession.CheckIfAdminOrUser();
            if (CheckAdmin)
            {
                int index = int.Parse(((Button)e.Source).Uid);
                switch (index)
                {
                    case 0:
                        GridMain.Navigate(new P_Student_Payments());
                        break;
                    case 1:
                        GridMain.Navigate(new P_Add_Type_Of_Outlay());
                        break;
                    case 2:
                        GridMain.Navigate(new P_New_Outlay());
                        break;
                    case 3:
                        GridMain.Navigate(new P_Teacher_Payments());
                        break;
                    case 4:
                        GridMain.Navigate(new P_show_Outlay());
                        break;
                    case 5:
                        GridMain.Navigate(new P_Show_Courses_Details_Admin());
                        break;
                    case 6:
                        GridMain.Navigate(new P_Stochastic());
                        break;
                    case 7:
                        GridMain.Navigate(new P_Show_Daily_Payments());
                        break;
                    case 8:
                        GridMain.Navigate(new SpecialStochastic());
                        break;
                    case 9:
                        GridMain.Navigate(new P_Terms());
                        break;
                }

            }
            else
            {
                int index = int.Parse(((Button)e.Source).Uid);
                switch (index)
                {
                    case 0:
                        GridMain.Navigate(new P_Student_Payments());
                        break;
                    case 1:
                        GridMain.Navigate(new P_Add_Type_Of_Outlay());
                        break;
                    case 2:
                        GridMain.Navigate(new P_New_Outlay());
                        break;
                }
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
