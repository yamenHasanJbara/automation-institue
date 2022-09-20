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

namespace A2Z_.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public List<User> users { get; set; }
        public Login()
        {
            InitializeComponent();
            TurnAllUserToStatusOff();
        }
        private void TurnAllUserToStatusOff()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _AllUsers = db.Users.ToList();
                    users = _AllUsers;
                    foreach (var item in users)
                    {
                        item.Status = 1;
                        db.Users.Update(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {

            string UserName = nameUserName.Text;
            string Password = namePassword.Password;
            if (String.IsNullOrWhiteSpace(nameUserName.Text) || String.IsNullOrWhiteSpace(namePassword.Password))
            {
                MessageBox.Show("الرجاء تعبئة الحقول");
            }
            else
            {
                Permession permession = new Permession();
                bool CheckFromCredintal = permession.LogInProcess(UserName, Password);
                if (CheckFromCredintal)
                {
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else
                { }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void Ellipse_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
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
