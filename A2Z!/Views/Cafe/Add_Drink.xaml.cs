using A2Z_.Healpers;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
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

namespace A2Z_.Views.Cafe
{
    /// <summary>
    /// Interaction logic for Add_Drink.xaml
    /// </summary>
    public partial class Add_Drink : Page
    {
        public Add_Drink()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(Price.Text) || !IntegerValidation.checkIntValue(Price.Text))
                {
                    MessageBox.Show("الرجاء تعبئة كافة الحقول او التأكد من صحة السعر المدخل");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        bool CheckIfExist = db.Caffes.Include(x => x.CafeSales).Any(x => x.DrinkName == Name.Text);
                        if (CheckIfExist)
                        {
                            MessageBox.Show("إن المشروب موجود مسبقاً");
                        }
                        else
                        {
                            Caffe caffe = new Caffe();
                            caffe.DrinkName = Name.Text;
                            caffe.DrinkPrice = int.Parse(Price.Text);
                            db.Caffes.Add(caffe);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الإضافة بنجاح");
                            Name.Text = null;
                            Price.Text = null;
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
