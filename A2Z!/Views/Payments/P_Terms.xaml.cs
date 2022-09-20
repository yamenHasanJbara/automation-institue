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
    /// Interaction logic for P_Terms.xaml
    /// </summary>
    public partial class P_Terms : Page
    {

        public List<Term> terms { get; set; }

        public List<Course> courses { get; set; }

        public List<Payment> payments { get; set; }

        public List<StochasticData> stochastics { get; set; }

        public P_Terms()
        {
            InitializeComponent();
            loadTerms();
        }

        private void loadTerms()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _terms = db.terms.ToList();
                    terms = _terms;
                    Terms.ItemsSource = terms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Terms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTerm = Terms.SelectedItem as Term;
            try
            {
                using (var db = new DataBaseContext())
                {
                    Term term = new Term();
                    term = db.terms.Include(x => x.courses).SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                    displayStochasticTermInformation(term);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void displayStochasticTermInformation(Term term)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _courses = db.Courses.Include(x => x.section).Include(x => x.faculty)
                                            .Include(x => x.Year).Include(x => x.material_Study)
                                            .Include(x => x.Student_Courses).Include(x => x.material_Study)
                                            .Include(x => x.term)
                                            .Where(x =>
                                            (x.Start_Date == term.start_date)
                                         && (x.End_Date == term.end_date)
                                         && (x.faculty != null)).ToList();
                    courses = _courses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.term = term;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    int TeacherActualDeserved = 0;
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
                            coursePayments+= item1.Amount;
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
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            
    }
}
