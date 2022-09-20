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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_New_Outlay.xaml
    /// </summary>
    public partial class P_New_Outlay : Page
    {
        public List<TypyOfOutlayPayment> typyOfOutlayPayments { get; set; }
        public List<Outlay> outlays { get; set; }
        public P_New_Outlay()
        {
            InitializeComponent();
            Load_Types();
            
        }


        private void Load_Types()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _TypesOfPayments = db.TypyOfOutlayPayments.Include(x => x.Outlays).ToList();
                    typyOfOutlayPayments = _TypesOfPayments;
                    Types.ItemsSource = typyOfOutlayPayments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Amount.Text) || Types.SelectedItem == null)
                {
                    MessageBox.Show("الرجاء تعبئة الحقول");
                }
                else
                {
                    
                    if (IntegerValidation.checkIntValue(Amount.Text))
                    {
                        using (var db = new DataBaseContext())
                        {
                            Outlay outlay = new Outlay();
                            TypyOfOutlayPayment typyOfOutlayPayment = new TypyOfOutlayPayment();
                            var SelectedTypeOfOutlay = Types.SelectedItem as TypyOfOutlayPayment;
                            typyOfOutlayPayment = db.TypyOfOutlayPayments.Include(x => x.Outlays).SingleOrDefault(x => x.TypyOfOutlayPayment_Id == SelectedTypeOfOutlay.TypyOfOutlayPayment_Id);
                            outlay.TypyOfOutlayPayment = typyOfOutlayPayment;
                            outlay.Outlay_Type = 1;
                            outlay.Amount = int.Parse(Amount.Text);
                            outlay.date = DateTime.Today;
                            outlay.Note = Note.Text;
                            db.Outlays.Add(outlay);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية التسديد بنجاح");
                            Amount.Text = null;
                            Note.Text = null;
                            Types.SelectedItem = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("الرجاء التأكد من خانة كمية الدفعة");
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
