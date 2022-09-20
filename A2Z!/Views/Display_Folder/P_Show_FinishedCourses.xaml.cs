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

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for P_Show_FinishedCourses.xaml
    /// </summary>
    public partial class P_Show_FinishedCourses : Page
    {

        public List<Course> courses { get; set; }

        public P_Show_FinishedCourses()
        {
            InitializeComponent();
            Load_Finished_Courses();
        }

        public void Load_Finished_Courses()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Courses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Include(x=>x.term).Where(x => x.IsFinished == true).ToList();
                    courses = _Courses;
                    CourseDetails.ItemsSource = courses;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CourseDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var SelectedCourse = CourseDetails.SelectedItem as Course;
                if (SelectedCourse != null)
                {
                    Show_Student_Details show_Student_Details = new Show_Student_Details(SelectedCourse.Course_Id);
                    show_Student_Details.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Permession permession = new Permession(); // 1 is admin for permission if 1 is pass
                bool check = permession.CheckIfAdminOrUser();
                if (check)
                {
                    var selectedCourse = CourseDetails.SelectedItem as Course;
                    Update_Finished_Course update_Finished_Course = new Update_Finished_Course(this, selectedCourse);
                    update_Finished_Course.Show();
                }
                else
                {
                    MessageBox.Show("لا يمكن تعديل الدورة");
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
                Course course = new Course();
                var selectedCourse = CourseDetails.SelectedItem as Course;
                Permession permession = new Permession(); // 1 is admin for permission if 1 is pass
                bool check = permession.CheckIfAdminOrUser();
                if (check)
                {
                    using (var db = new DataBaseContext())
                    {
                        course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.Course_Id == selectedCourse.Course_Id).FirstOrDefault();
                        db.Courses.Remove(course);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        Load_Finished_Courses();
                    }
                }
                else
                {
                    MessageBox.Show("لا يمكن حذف الدورة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
