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
    /// Interaction logic for P_Show_Courses_Details_Admin.xaml
    /// </summary>
    public partial class P_Show_Courses_Details_Admin : Page
    {

        public List<Course> courses { get; set; }

        public List<Payment> payments { get; set; }

        public List<Show_Admin_Course_Details> show_Admin_Course_Details { get; set; }

        public List<Show_Admin_Course_Details> show_Admin_Course_Details1 { get; set; }
        public P_Show_Courses_Details_Admin()
        {
            InitializeComponent();
            Load_General_Informatio();
        }


        private void Load_General_Informatio()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Courses = db.Courses.Include(x => x.teacher).Include(x => x.Student_Courses).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).ToList();
                    courses = _Courses;
                    List<Show_Admin_Course_Details> show_Admin_Course_s = new List<Show_Admin_Course_Details>();
                    foreach (var item in courses)
                    {
                        Show_Admin_Course_Details show_Admin_Course_Details1 = new Show_Admin_Course_Details();
                        show_Admin_Course_Details1.CourseId = item.Course_Id;
                        show_Admin_Course_Details1.CourseName = item.material_Study.Name;
                        show_Admin_Course_Details1.Group = item.Group;
                        show_Admin_Course_Details1.CoursePrice = item.Price;
                        show_Admin_Course_Details1.IsFinshed = item.IsFinished;
                        show_Admin_Course_Details1.Count = item.Student_Courses.Count;
                        if (item.faculty == null || item.Year == null)
                        {
                            show_Admin_Course_Details1.Collage = null;
                            show_Admin_Course_Details1.year = 0;
                            show_Admin_Course_Details1.Semester = 0;
                        }
                        else
                        {
                            show_Admin_Course_Details1.Collage = item.faculty.Name;
                            show_Admin_Course_Details1.year = item.Year.Year_Number;
                            show_Admin_Course_Details1.Semester = item.material_Study.Semester;
                        }
                        show_Admin_Course_Details1.TeacherName = item.teacher.Name;
                        show_Admin_Course_Details1.TeacherPercent = item.percent;
                        show_Admin_Course_Details1.InstituePercent = (100 - item.percent);
                        var _PaymentsForItem = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _PaymentsForItem;
                        int AmountPaid = 0;
                        foreach (var item1 in payments)
                        {
                            AmountPaid += item1.Amount;
                        }
                        int TeacherDeserved = (item.percent * item.Price * item.Student_Courses.Count) / 100;
                        show_Admin_Course_Details1.ActualPaid = AmountPaid - TeacherDeserved;
                        int IstituteDeserved = ((100 - item.percent) * item.Price * item.Student_Courses.Count) / 100;
                        show_Admin_Course_Details1.InstituteAmount = IstituteDeserved;
                        show_Admin_Course_s.Add(show_Admin_Course_Details1);
                    }
                    show_Admin_Course_Details = show_Admin_Course_s;
                    CourseDetails.ItemsSource = show_Admin_Course_Details;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var _ShowResult = show_Admin_Course_Details.Where(x => x.CourseName.Contains(Search.Text)).ToList();
                show_Admin_Course_Details1 = _ShowResult;
                CourseDetails.ItemsSource = show_Admin_Course_Details1;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CourseDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var SelectedCourse = CourseDetails.SelectedItem as Show_Admin_Course_Details;
            try
            {
                if (SelectedCourse != null)
                {
                    ShowAllPaymentsForcourseSectionAdmin showAllPaymentsForcourseSectionAdmin = new ShowAllPaymentsForcourseSectionAdmin(SelectedCourse.CourseId);
                    showAllPaymentsForcourseSectionAdmin.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
