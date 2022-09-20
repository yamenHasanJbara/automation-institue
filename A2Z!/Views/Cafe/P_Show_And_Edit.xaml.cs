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
    /// Interaction logic for P_Show_And_Edit.xaml
    /// </summary>
    public partial class P_Show_And_Edit : Page
    {

        public List<Caffe> caffes { get; set; }
        public P_Show_And_Edit()
        {
            InitializeComponent();
            Load_Drinks();
        }

        private void Load_Drinks()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _cafeDrinks = db.Caffes.Include(x => x.CafeSales).ToList();
                    caffes = _cafeDrinks;
                    CafeDrink.ItemsSource = caffes;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedDrink = CafeDrink.SelectedItem as Caffe;
                if (SelectedDrink != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        if (!IntegerValidation.checkIntValue(DrinkPrice.Text))
                        {
                            MessageBox.Show("الرجاء التأكد من السعر المدخل");
                        }
                        else
                        {
                            Caffe caffe = new Caffe();
                            caffe = db.Caffes.Include(x => x.CafeSales).SingleOrDefault(x => x.Drink_Id == SelectedDrink.Drink_Id);
                            caffe.DrinkName = DrinkName.Text;
                            caffe.DrinkPrice = int.Parse(DrinkPrice.Text);
                            db.Caffes.Update(caffe);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية التحديث بنجاح");
                            DrinkPrice.Text = null;
                            DrinkName.Text = null;
                            SelectedDrink = null;
                            Load_Drinks();
                        }
                    }
                }
                else
                {    }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DailyMovment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedDrink = CafeDrink.SelectedItem as Caffe;
                if (SelectedDrink != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Caffe caffe = new Caffe();
                        caffe = db.Caffes.Include(x => x.CafeSales).SingleOrDefault(x => x.Drink_Id == SelectedDrink.Drink_Id);
                        DrinkName.Text = caffe.DrinkName;
                        DrinkPrice.Text = caffe.DrinkPrice.ToString();
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
