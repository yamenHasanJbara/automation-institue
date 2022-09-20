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
using System.Windows.Shapes;

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for Update_Finished_Course.xaml
    /// </summary>
    public partial class Update_Finished_Course : Window
    {
        public List<Teacher> teachers { get; set; }
        public List<Course> courses { get; set; }

        public int courseId { get; set; }

        public P_Show_FinishedCourses p_Show_FinishedCourses { get; set; }
        public Update_Finished_Course(P_Show_FinishedCourses p_Show_FinishedCourses, Models.Course selectedCourse)
        {
            InitializeComponent();
            courseId = selectedCourse.Course_Id;
            this.p_Show_FinishedCourses = p_Show_FinishedCourses;
            loadCourse(courseId);
        }

        private void loadCourse(int courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.Course_Id == courseId).FirstOrDefault();
                    List<Course> coursesList = new List<Course>();
                    coursesList.Add(course);
                    courses = coursesList;
                    CourseDetails.ItemsSource = courses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CourseDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedCourse = CourseDetails.SelectedItem as Course;
            if (SelectedCourse != null)
            {

                using (var db = new DataBaseContext())
                {
                    Course course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                    var _Teachers = db.Teachers.ToList();
                    teachers = _Teachers;
                    TeachersName.ItemsSource = teachers;
                    TeachersName.SelectedItem = course.teacher;
                    Percent.Text = course.percent.ToString();
                    Price.Text = course.Price.ToString();
                }
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            try
            {
                Course course = new Course();
                Teacher teacher = new Teacher();
                var selectedCourse = CourseDetails.SelectedItem as Course;
                var selectedTeacher = TeachersName.SelectedItem as Teacher;
                using (var db = new DataBaseContext())
                {
                    if (!IntegerValidation.checkIntValue(Price.Text) || !IntegerValidation.checkIntValue(Percent.Text) || selectedCourse == null || selectedTeacher == null || String.IsNullOrWhiteSpace(Percent.Text) || String.IsNullOrWhiteSpace(Price.Text))
                    {
                        MessageBox.Show("الرجاء تعبة كافة الحقول او التأكد من السعر والنسبة");
                    }
                    else
                    {
                        course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.Course_Id == courseId).FirstOrDefault();
                        teacher = db.Teachers.FirstOrDefault(x => x.Teacher_Id == selectedTeacher.Teacher_Id);
                        course.Price = int.Parse(Price.Text);
                        course.percent = int.Parse(Percent.Text);
                        course.teacher = teacher;
                        db.Courses.Update(course);
                        db.SaveChanges();
                        p_Show_FinishedCourses.Load_Finished_Courses();
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
