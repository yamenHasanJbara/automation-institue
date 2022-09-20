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
    /// Interaction logic for SpecialStochastic.xaml
    /// </summary>
    public partial class SpecialStochastic : Page
    {

        public List<Course> courses { get; set; }
        public List<Payment> payments { get; set; }
        public List<StochasticData> stochastics { get; set; }
        public SpecialStochastic()
        {
            InitializeComponent();
            LoadStochasticInformation();
        }

        private void LoadStochasticInformation()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.Student_Courses).Include(x => x.material_Study).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    long cafeProfit = db.CafeSalesForShows.ToList().Sum(x => x.AmountOfSaleDrink * x.DrinkPrice);
                    long theProfit = StudentTaken1 + cafeProfit -( db.Outlays.Where(x => x.Outlay_Type == 1).Sum(x => x.Amount)) ;
                    TheProfit.Text = theProfit.ToString();
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void show(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    if ((Start_Date.SelectedDate.Value) != null && (End_Date.SelectedDate.Value) != null)
                    {
                        var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x =>  (x.Start_Date >= Start_Date.SelectedDate.Value) && (x.End_Date <= End_Date.SelectedDate.Value)).ToList();
                        courses = _SectionCourses;
                        StochasticData stochasticData = new StochasticData();
                        List<StochasticData> stochasticDatas = new List<StochasticData>();
                        long Count1 = 0;
                        long ActualDeserved1 = 0;
                        long InstituteDeserved1 = 0;
                        long TeacherDeserved1 = 0;
                        long StudentTaken1 = 0;
                        long TeacherGiven1 = 0;
                        long Difference1 = 0;
                        long TeacherActualDeserved = 0;
                        foreach (var item in courses)
                        {
                            int coursePayments = 0;
                            Count1 += item.Student_Courses.Count;
                            ActualDeserved1 += item.Student_Courses.Count * item.Price;
                            InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                            TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                            var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                            payments = _StudentPayments;
                            foreach (var item1 in payments)
                            {
                                coursePayments += item1.Amount;
                                StudentTaken1 += item1.Amount;
                            }
                            var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                            payments = _TeacherPayments;
                            foreach (var item1 in payments)
                            {
                                TeacherGiven1 += item1.Amount;
                            }
                            Difference1 = StudentTaken1 - TeacherGiven1;
                            TeacherActualDeserved += coursePayments * item.percent / 100;
                        }
                        stochasticData.Count = Count1;
                        stochasticData.ActualDeserved = ActualDeserved1;
                        stochasticData.InstituteDeserved = InstituteDeserved1;
                        stochasticData.TeacherDeserved = TeacherDeserved1;
                        stochasticData.StudentTaken = StudentTaken1;
                        stochasticData.TeacherGiven = TeacherGiven1;
                        stochasticData.Difference = Difference1;
                        stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                        stochasticDatas.Add(stochasticData);
                        stochastics = stochasticDatas;
                        long cafeProfit = db.CafeSalesForShows.ToList().Sum(x=>x.AmountOfSaleDrink* x.DrinkPrice);
                        long theProfit = StudentTaken1 + cafeProfit - (db.Outlays.Where(x=>(x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value) && (x.Outlay_Type == 1)).Sum(x=>x.Amount));
                        TheProfit.Text = theProfit.ToString();
                        StochasticDetails.ItemsSource = stochastics;
                    }
                    else
                    {
                        MessageBox.Show("الرجاء اختيار تاريخ بداية وانتهاء");
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("الرجاء اختيار تاريخ بداية وانتهاء");

            }
        }
    }
}
