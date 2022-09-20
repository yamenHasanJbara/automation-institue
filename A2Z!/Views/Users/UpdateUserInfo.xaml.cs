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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.Users
{
    /// <summary>
    /// Interaction logic for UpdateUserInfo.xaml
    /// </summary>
    public partial class UpdateUserInfo : Page
    {

        public List<User> users { get; set; }
        public UpdateUserInfo()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _users = db.Users.ToList();
                    users = _users;
                    Users.ItemsSource = users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedUser = Users.SelectedItem as User;
                if (selectedUser != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        User user = new User();
                        user = db.Users.SingleOrDefault(x => x.User_Id == selectedUser.User_Id);
                        if (String.IsNullOrWhiteSpace(UserName.Text) || String.IsNullOrWhiteSpace(Password.Password))
                        {
                            MessageBox.Show("الرجاء تعبشة حقل اسم المستخدم وكلمة السر");
                        }
                        else
                        {
                            if (user.Status == 1)
                            {
                                user.UserName = UserName.Text;
                                user.Password = Password.Password;
                                db.Users.Update(user);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية التحديث بنجاح");
                                this.LoadUsers();
                            }
                            else
                            {
                                user.UserName = UserName.Text;
                                user.Password = Password.Password;
                                db.Users.Update(user);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية التحديث بنجاح، الرجاء اعادة تسجيل الدخول مرة اخرى");
                                this.goToLogInWindow();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار مستخدم");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedUser = Users.SelectedItem as User;
                if (selectedUser != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        User user = new User();
                        user = db.Users.SingleOrDefault(x => x.User_Id == selectedUser.User_Id);
                        if (user.permission == 1)
                        {
                            MessageBox.Show("لا يمكن حذف المدير");
                        }
                        else
                        {
                            db.Users.Remove(user);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            this.LoadUsers();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار مستخدم");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedUser = Users.SelectedItem as User;
                if (selectedUser != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        User user = new User();
                        user = db.Users.SingleOrDefault(x=>x.User_Id == selectedUser.User_Id);
                        UserName.Text = user.UserName;
                        Password.Password = user.Password;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goToLogInWindow()
        {
            try
            {
                Login login = new Login();
                login.Show();
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Login)
                    {
                        continue;
                    }
                    else
                    {
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.Close();
                        }
                        else
                        {
                            w.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
