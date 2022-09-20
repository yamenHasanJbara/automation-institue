using A2Z_.Healpers;
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

namespace A2Z_.Views.Cafe
{
    /// <summary>
    /// Interaction logic for UpdateCafeSales.xaml
    /// </summary>
    public partial class UpdateCafeSales : Window
    {

        public int DrinkSale { get; set; }
        public SalesCafe salesCafe { get; set; }
        public UpdateCafeSales(int cafeSales_Id)
        {
            InitializeComponent();
            loadSaleInformation(cafeSales_Id);
            this.DrinkSale = cafeSales_Id;
        }

        private void loadSaleInformation(int cafeSales_Id)
        {
            try
            {
                CafeSalesForShow cafeSalesForShow = new CafeSalesForShow();
                using (var db = new DataBaseContext())
                {
                    cafeSalesForShow = db.CafeSalesForShows.SingleOrDefault(x => x.CafeSales_Id == cafeSales_Id);
                    Drink.Text = cafeSalesForShow.DrinkName;
                    Quantity.Text = cafeSalesForShow.AmountOfSaleDrink.ToString();

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
                if (String.IsNullOrWhiteSpace(Quantity.Text) || !IntegerValidation.checkIntValue(Quantity.Text))
                {
                    MessageBox.Show("الرجاء ادخال كافة المعلومات المطلوبة والتأكد من صحة الرقم المدخل");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        CafeSalesForShow cafeSalesForShow = new CafeSalesForShow();
                        CafeSales cafeSales = new CafeSales();
                        cafeSalesForShow = db.CafeSalesForShows.SingleOrDefault(x => x.CafeSales_Id == DrinkSale);
                        cafeSales = db.CafeSales.SingleOrDefault(x => x.CafeSales_Id == DrinkSale);
                        cafeSales.AmountOfSaleDrink = int.Parse(Quantity.Text);
                        db.CafeSales.Update(cafeSales);
                        db.SaveChanges();
                        cafeSalesForShow.AmountOfSaleDrink = int.Parse(Quantity.Text);
                        db.CafeSalesForShows.Update(cafeSalesForShow);
                        db.SaveChanges();
                        MessageBox.Show("تمت عمليةالتحديث بنجاح");
                        this.salesCafe.Load_Daily_Cafe_movement();
                        this.Close();
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
