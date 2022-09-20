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

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_Teacher.xaml
    /// </summary>
    public partial class P_Add_Teacher : Page
    {
        public P_Add_Teacher()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(FullName.Text) || String.IsNullOrWhiteSpace(NubmerPhone.Text))
                {
                    MessageBox.Show("الرجاء تبعئة كافة الحقول");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Teacher teacher = new Teacher();
                        teacher.Name = FullName.Text.TrimEnd();
                        string number = NubmerPhone.Text;
                        if (number.Length != 10)
                        {
                            MessageBox.Show("الرجاء التأكد من صحة الرقم");
                        }
                        else
                        {
                            teacher.Number_Phone = number;
                            bool check = db.Teachers.Any(x => x.Name == FullName.Text && x.Number_Phone == number);
                            if (check)
                            {
                                MessageBox.Show("إن المدرس موجود مسبقاً");
                                FullName.Text = null;
                                NubmerPhone.Text = null;
                            }
                            else
                            {
                                db.Teachers.Add(teacher);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية الإضافة بنجاح");
                                FullName.Text = null;
                                NubmerPhone.Text = null;
                            }
                            
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
