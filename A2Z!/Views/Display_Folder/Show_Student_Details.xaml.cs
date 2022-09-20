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
using System.Windows.Shapes;

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for Show_Student_Details.xaml
    /// </summary>
    public partial class Show_Student_Details : Window
    {
        public IEnumerable<IGrouping<int,Payment>> student_Courses { get; set; }

        public List<Show_Student_details_When_DoubleClickOnCourse> show_Student_Details_When_DoubleClickOnCourses { get; set; }

        public Show_Student_Details(int CourseId)
        {
            InitializeComponent();
            Load_Student_Details(CourseId);
        }


        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Ellipse_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Load_Student_Details(int courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    course = db.Courses.Include(x => x.Student_Courses).Include(x => x.material_Study).Include(x => x.payments).SingleOrDefault(x => x.Course_Id == courseId);
                    List<Show_Student_details_When_DoubleClickOnCourse> show_Student_Details_When_DoubleClickOnCourses1 = new List<Show_Student_details_When_DoubleClickOnCourse>();
                    var _StudentCourses = db.Payments.Include(x => x.course).Include(x => x.course.material_Study).Include(x => x.student).Where(x => x.course.Course_Id == courseId && x.Payment_Type ==2).AsEnumerable().GroupBy(x => x.student.Student_Id).ToList();
                    student_Courses = _StudentCourses;
                    int counter = 1;
                    foreach (var item in student_Courses)
                    {
                        Show_Student_details_When_DoubleClickOnCourse show_Student_Details_When_DoubleClickOnCourse2 = new Show_Student_details_When_DoubleClickOnCourse();
                        int AmountPaid = 0;
                        int AmountStayed = 0;
                        int id = 0;
                        foreach (var Payment in item)
                        {
                            show_Student_Details_When_DoubleClickOnCourse2.Student_Id = counter;
                            show_Student_Details_When_DoubleClickOnCourse2.Student_Name = Payment.student.Name;
                            show_Student_Details_When_DoubleClickOnCourse2.Student_Phone = Payment.student.Number_Phone;
                            show_Student_Details_When_DoubleClickOnCourse2.CourseName = Payment.course.material_Study.Name + "  " +  Payment.course.Group;
                            AmountPaid += Payment.Amount;
                            id = Payment.student.Student_Id;
                        }
                        var studentCoursePivot = db.Student_Courses.Where(x => x.Student_Id == id && x.Course_Id == course.Course_Id).FirstOrDefault();
                        show_Student_Details_When_DoubleClickOnCourse2.AmountPaid = AmountPaid;
                        show_Student_Details_When_DoubleClickOnCourse2.Withdrawn = studentCoursePivot.Withdrawn;
                        show_Student_Details_When_DoubleClickOnCourse2.CoursePrice = course.Price;
                        AmountStayed = course.Price - AmountPaid;
                        show_Student_Details_When_DoubleClickOnCourse2.AmountStayed = AmountStayed;
                        
                        show_Student_Details_When_DoubleClickOnCourses1.Add(show_Student_Details_When_DoubleClickOnCourse2);
                        counter += 1;
                    }
                    show_Student_Details_When_DoubleClickOnCourses = show_Student_Details_When_DoubleClickOnCourses1;
                    StudentDetails.ItemsSource = show_Student_Details_When_DoubleClickOnCourses;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
