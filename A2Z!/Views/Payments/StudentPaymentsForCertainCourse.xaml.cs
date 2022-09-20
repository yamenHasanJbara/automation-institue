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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for StudentPaymentsForCertainCourse.xaml
    /// </summary>
    public partial class StudentPaymentsForCertainCourse : Window
    {

        public List<Payment> payments { get; set; }

        public List<IGrouping<Course,Payment>> payments11 { get; set; }
        public StudentPaymentsForCertainCourse(int studentId, List<int> courseId)
        {
            InitializeComponent();
            LoadInformation(studentId, courseId);
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

        private void LoadInformation(int studentId, List<int> courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Student student = new Student();
                    student = db.Students.SingleOrDefault(x => x.Student_Id == studentId);
                    List<Payment> payments1 = new List<Payment>();
                    foreach (var item in courseId)
                    {
                        Course course = new Course();
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == item);
                        var _payments = db.Payments.Include(x => x.student).Include(x => x.course).Include(x=>x.course.material_Study).Where(x => (x.student == student) && (x.course == course)).ToList();
                        foreach (var item1 in _payments)
                        {
                            payments1.Add(item1);
                        }
                    }
                    var paymetsgroup = payments1.GroupBy(x => x.course).ToList();
                    payments11 = paymetsgroup;
                    CoursePaymentsDetails.ItemsSource = payments11;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
