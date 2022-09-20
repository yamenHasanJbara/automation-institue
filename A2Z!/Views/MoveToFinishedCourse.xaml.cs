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

namespace A2Z_.Views
{
    /// <summary>
    /// Interaction logic for MoveToFinishedCourse.xaml
    /// </summary>
    public partial class MoveToFinishedCourse : Window
    {

        public List<Course> courses { get; set; }

        public int CourseIDD { get; set; }
        public MoveToFinishedCourse(int CourseId)
        {
            InitializeComponent();
            this.CourseIDD = CourseId;
            LoadCourseInfo(CourseId);
        }

        private void LoadCourseInfo(int courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Course> courses1 = new List<Course>();
                    Course course = new Course();
                    course = db.Courses.Include(x => x.material_Study).Include(x => x.section).Include(x => x.Year).Include(x => x.faculty).Include(x=>x.teacher).SingleOrDefault(x => x.Course_Id == courseId);
                    courses1.Add(course);
                    courses = courses1;
                    CourseDetails.ItemsSource = courses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    bool check = db.Courses.Any(x => x.Course_Id == CourseIDD);
                    if (check)
                    {
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == CourseIDD);
                        if (End_Date.SelectedDate.Value != null && End_Date.SelectedDate.Value > dateTime)
                        {
                            course.End_Date = End_Date.SelectedDate.Value;
                            db.Courses.Update(course);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية التحديث بنجاح");
                        }
                        else
                        {
                            MessageBox.Show("الرجاء اختيار تاريخ وتعيين تاريخ أكبر من تاريخ اليوم");
                        }
                    }
                    else
                    {
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
