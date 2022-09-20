using A2Z_.Healpers;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Interaction logic for SalesCafe.xaml
    /// </summary>
    public partial class SalesCafe : Page
    {

        public List<Caffe> caffes { get; set; }

        public List<CafeSales> cafeSales { get; set; }

        public List<CafeSalesForShow> salesForShows { get; set; }

        
        public SalesCafe()
        {
            InitializeComponent();
            Load_Drinks();
            Load_Daily_Cafe_movement();
        }

        public void Load_Daily_Cafe_movement()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    DateTime dateTime = DateTime.Today;
                    var _cafeSales = db.CafeSalesForShows.Where(x=>x.date == dateTime).ToList();
                    salesForShows = _cafeSales;
                    DailyMovment.ItemsSource = salesForShows;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Drinks()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Dirnks = db.Caffes.Include(x => x.CafeSales).ToList();
                    caffes = _Dirnks;
                    NameOfDrink.ItemsSource = caffes;
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
                if (NameOfDrink.SelectedItem == null)
                {
                    MessageBox.Show("الرجاء اختيار مشروب");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        var SelectedDrink = NameOfDrink.SelectedItem as Caffe;
                        CafeSales cafeSales = new CafeSales();
                        Caffe caffe = new Caffe();
                        CafeSalesForShow cafeSalesForShow = new CafeSalesForShow();
                        caffe = db.Caffes.SingleOrDefault(x => x.Drink_Id == SelectedDrink.Drink_Id);
                        if (String.IsNullOrWhiteSpace(Amount.Text))
                        {
                            cafeSales.Caffe = caffe;
                            cafeSales.date = DateTime.Today;
                            db.CafeSales.Add(cafeSales);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية البيع بنجاح");
                            cafeSalesForShow.AmountOfSaleDrink = cafeSales.AmountOfSaleDrink;
                            cafeSalesForShow.date = DateTime.Today;
                            cafeSalesForShow.DrinkName = caffe.DrinkName;
                            cafeSalesForShow.DrinkPrice = caffe.DrinkPrice;
                            db.CafeSalesForShows.Add(cafeSalesForShow);
                            db.SaveChanges();
                            Load_Daily_Cafe_movement();
                            NameOfDrink.SelectedItem = null;
                            Amount.Text = null;
                        }
                        else
                        {
                            if (!IntegerValidation.checkIntValue(Amount.Text))
                            {
                                MessageBox.Show("الرجاء التأكد من صحة الكمية المدخلة");
                            }
                            else
                            {
                                cafeSales.Caffe = caffe;
                                cafeSales.date = DateTime.Today;
                                cafeSales.AmountOfSaleDrink = int.Parse(Amount.Text);
                                db.CafeSales.Add(cafeSales);
                                db.SaveChanges();
                                cafeSalesForShow.AmountOfSaleDrink = int.Parse(Amount.Text);
                                cafeSalesForShow.date = DateTime.Today;
                                cafeSalesForShow.DrinkName = caffe.DrinkName;
                                cafeSalesForShow.DrinkPrice = caffe.DrinkPrice;
                                db.CafeSalesForShows.Add(cafeSalesForShow);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية البيع بنجاح");
                                Load_Daily_Cafe_movement();
                                NameOfDrink.SelectedItem = null;
                                Amount.Text = null;
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

        private void Show(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Start_Date.SelectedDate != null && End_Date.SelectedDate != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        var _CafeSalesBetweenTwoDate = db.CafeSalesForShows.Where(x => (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).ToList();
                        int sumOfSales = db.CafeSalesForShows.Where(x => (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).Sum(x=> (x.DrinkPrice) * (x.AmountOfSaleDrink));
                        salesForShows = _CafeSalesBetweenTwoDate;
                        DailyMovment.ItemsSource = salesForShows;
                        Sumofsales.Text = sumOfSales.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار تاريخ");
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
                var SelectedDrinkSale = DailyMovment.SelectedItem as CafeSalesForShow;
                if (SelectedDrinkSale != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        CafeSalesForShow cafeSalesForShow = new CafeSalesForShow();
                        cafeSalesForShow = db.CafeSalesForShows.SingleOrDefault(x => x.CafeSales_Id == SelectedDrinkSale.CafeSales_Id);
                        CafeSales cafeSales = new CafeSales();
                        cafeSales = db.CafeSales.SingleOrDefault(x => x.CafeSales_Id == cafeSalesForShow.CafeSales_Id);
                        db.CafeSales.Remove(cafeSales);
                        db.SaveChanges();
                        db.CafeSalesForShows.Remove(cafeSalesForShow);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        Load_Daily_Cafe_movement();
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار احد المبيعات");
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
                var SelectedDrinkSale = DailyMovment.SelectedItem as CafeSalesForShow;
                if (SelectedDrinkSale != null)
                {
                    UpdateCafeSales updateCafeSales = new UpdateCafeSales(SelectedDrinkSale.CafeSales_Id);
                    updateCafeSales.salesCafe = this;
                    updateCafeSales.Show();
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار احد المبيعات");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
