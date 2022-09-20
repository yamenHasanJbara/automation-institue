using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for ShowAllPaymentsForcourseSectionAdmin.xaml
    /// </summary>
    public partial class ShowAllPaymentsForcourseSectionAdmin : Window
    {

        public List<IGrouping<Teacher, Payment>> teacherPayments { get; set; }

        public List<IGrouping<Student,Payment>> studentsPayments { get; set; }

        public List<Payment> payments { get; set; }
        public ShowAllPaymentsForcourseSectionAdmin(int courseId)
        {
            InitializeComponent();
            LoadTeacherStudentPayemts(courseId);
        }


        //private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void Ellipse_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        this.WindowState = WindowState.Minimized;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ChangedButton == MouseButton.Left)
        //            this.DragMove();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void LoadTeacherStudentPayemts(int courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    course = db.Courses.Include(x=>x.material_Study).Include(x=>x.Student_Courses).Include(x=>x.teacher).SingleOrDefault(x => x.Course_Id == courseId);
                    var _StudentPayments = db.Payments.Include(x=>x.student).Include(x => x.course).Where(x => (x.course == course) && (x.Payment_Type == 2)).ToList();
                    studentsPayments = _StudentPayments.GroupBy(x => x.student).ToList();
                    StudentDetails.ItemsSource = studentsPayments;
                    var _TeacherPayments = db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => (x.course == course) && (x.Payment_Type == 1)).ToList();
                    if (_TeacherPayments.Count == 0)
                    {
                        List<Payment> payments1 = new List<Payment>();
                        Payment payment = new Payment();
                        Teacher teacher = new Teacher();
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == course.teacher.Teacher_Id);
                        payment.Amount = 0;
                        payment.course = course;
                        payment.date = DateTime.Today;
                        payment.Payment_Type = 1;
                        payment.Stayed = course.percent * course.Student_Courses.Count * course.Price / 100;
                        payment.teacher = teacher;
                        payments1.Add(payment);
                        teacherPayments =payments1.GroupBy(x => x.teacher).ToList();
                        TeacherDetails.ItemsSource = teacherPayments;
                    }
                    else
                    {
                        teacherPayments = _TeacherPayments.GroupBy(x => x.teacher).ToList();
                        TeacherDetails.ItemsSource = teacherPayments;
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
