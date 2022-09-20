using A2Z_.Views;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace A2Z_.Models
{
    public class Permession
    {

        public  bool CheckIfAdminOrUser()
        {
            try
            {
                User user = new User();
                using (var db = new DataBaseContext())
                {
                    user = db.Users.SingleOrDefault(x => x.Status == 2);
                }
                if (user.permission == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool LogInProcess(string UserName, string Password)
        {
            try
            {
                User user = new User();
                using (var db = new DataBaseContext())
                {
                    bool Check = db.Users.Any(x => (x.UserName == UserName) && (x.Password == Password));
                    if (Check)
                    {
                        user = db.Users.SingleOrDefault(x => (x.UserName == UserName) && (x.Password == Password));
                        user.Status = 2;
                        db.Users.Update(user);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("الرجاء التأكد من صحة المعلومات");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
