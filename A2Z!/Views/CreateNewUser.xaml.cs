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
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void New_User(object sender, RoutedEventArgs e)
        {
            using (var db = new DataBaseContext())
            {
                try
                {
                    User user = new User();
                    if (String.IsNullOrWhiteSpace(UserName.Text) || String.IsNullOrWhiteSpace(Password.Password))
                    {
                        MessageBox.Show("الرجاء تعبئة المعلومات ولا يجوز إدخال فراغات");
                    }
                    else
                    {
                        user.UserName = UserName.Text;
                        user.Password = Password.Password;
                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية إنشاء مستخدم بنجاح");
                        this.Close();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("الرجاء المحاولة لاحقاً");
                }
            }
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
