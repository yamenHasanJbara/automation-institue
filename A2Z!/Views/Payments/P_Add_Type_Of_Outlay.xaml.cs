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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_Add_Type_Of_Outlay.xaml
    /// </summary>
    public partial class P_Add_Type_Of_Outlay : Page
    {
        public P_Add_Type_Of_Outlay()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Name.Text))
                {
                    MessageBox.Show("الرجاء تعبئة الحقل");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        bool CheckIfExist = db.TypyOfOutlayPayments.Include(x => x.Outlays).Any(x => x.type == Name.Text);
                        if (CheckIfExist)
                        {
                            MessageBox.Show("إن النوع مضاف سابقاً");
                        }
                        else
                        {
                            TypyOfOutlayPayment typyOfOutlayPayment = new TypyOfOutlayPayment();
                            typyOfOutlayPayment.type = Name.Text;
                            db.TypyOfOutlayPayments.Add(typyOfOutlayPayment);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الإضافة بنجاح");
                            Name.Text = null;
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
