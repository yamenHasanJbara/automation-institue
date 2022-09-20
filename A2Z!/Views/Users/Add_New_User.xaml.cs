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
    /// Interaction logic for Add_New_User.xaml
    /// </summary>
    public partial class Add_New_User : Page
    {
        public Add_New_User()
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
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("الرجاء المحاولة لاحقاً");
                }
            }
        }
    }
}
