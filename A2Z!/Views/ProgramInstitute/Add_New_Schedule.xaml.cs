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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A2Z_.Views.ProgramInstitute
{
    /// <summary>
    /// Interaction logic for Add_New_Schedule.xaml
    /// </summary>
    public partial class Add_New_Schedule : Window
    {

        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public List<Course> courses { get; set; }

        public string NameOfDataGrid { get; set; }

        public Add_New_Schedule(DataGridCell dataGridCell)
        {
            InitializeComponent();
            this.NameOfDataGrid = dataGridCell.Name;
            LoadSections();
        }

        private void LoadSections()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _section = db.Sections.ToList();
                    sections = _section;
                    SectionName.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void SectionName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = SectionName.SelectedItem as Section;
                CollageName.ItemsSource = null;
                YearNumber.ItemsSource = null;
                using (var db = new DataBaseContext())
                {
                    Section section = new Section();
                    section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                    var _Section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == section.Section_Id);
                    if (_Section.Faculties.Count >0)
                    {
                        Coursess.ItemsSource = null;
                        CollageName.Visibility = Visibility.Visible;
                        YearNumber.Visibility = Visibility.Visible;
                        var _Faculties = db.Faculties.Include(x => x.Section).Where(x => x.Section == section).ToList();
                        faculties = _Faculties;
                        CollageName.ItemsSource = faculties;
                    }
                    else
                    {
                        CollageName.Visibility = Visibility.Collapsed;
                        YearNumber.Visibility = Visibility.Collapsed;
                        var _Courses = db.Courses.Include(x => x.section).Include(x=>x.material_Study).Where(x => x.section == section).ToList();
                        courses = _Courses;
                        Coursess.ItemsSource = courses;
                    }
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CollageName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedCollage = CollageName.SelectedItem as Faculty;
                Faculty faculty = new Faculty();
                if (SelectedCollage != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedCollage.Faculty_Id);
                        var YearsNumber = db.Years.Include(x => x.Faculty).Where(x => x.Faculty == faculty).ToList();
                        years = YearsNumber;
                        YearNumber.ItemsSource = years;
                    }
                }
                else
                {  }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void YearNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedCollage = CollageName.SelectedItem as Faculty;
            var SelectedYear = YearNumber.SelectedItem as Year;
            try
            {
                if (SelectedCollage != null && SelectedYear != null)
                {                    
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedCollage.Faculty_Id);
                        Year year = new Year();
                        year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                        var _Courses = db.Courses.Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Where(x => (x.faculty == faculty) & (x.Year == year) && (x.IsFinished == false)).ToList();
                        courses = _Courses;
                        Coursess.ItemsSource = courses;
                    }
                }
                else
                {  }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Coursess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedCourse = Coursess.SelectedItem as Course;
                if (SelectedCourse != null)
                {
                    using (var db =  new DataBaseContext())
                    {
                        var _CoursesGroups = db.Courses.Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.Course_Id == SelectedCourse.Course_Id).ToList();
                        courses = _CoursesGroups;
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AddCourseSchedule(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedCourseAsGroup = Coursess.SelectedItem as Course;
                if (SelectedCourseAsGroup != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == SelectedCourseAsGroup.Course_Id);
                        string[] s = NameOfDataGrid.Split('_');
                        string Day = s[0];
                        string Hall = s[1];
                        string Time = s[2];
                        ProgramInstituteC program = new ProgramInstituteC();
                        program = db.programInstitutes.Include(x => x.course).SingleOrDefault(x => (x.day == Day) && (x.hall == Hall) && (x.hour == Time));
                        if (program != null)
                        {
                            program.course = course;
                            db.programInstitutes.Update(program);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية التحديث بنجاح");
                            ProgramStatus programStatus = new ProgramStatus();
                            programStatus.Show();
                            this.Close();
                        }
                        else

                        {
                            ProgramInstituteC program1 = new ProgramInstituteC();
                            program1.course = course;
                            program1.day = Day;
                            program1.hall = Hall;
                            program1.hour = Time;
                            db.programInstitutes.Add(program1);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الاضافة بنجاح");
                            ProgramStatus programStatus = new ProgramStatus();
                            programStatus.Show();
                            this.Close();
                        }

                    }
                }
                else
                { MessageBox.Show("الرجاء اختيار مجموعة"); }

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
                ProgramStatus programStatus = new ProgramStatus();
                programStatus.Show();
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

        private void RepeateOncertainSchedule(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedCourse = Coursess.SelectedItem as Course;
                if (SelectedCourse != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        RepeatCourse repeatCourse = new RepeatCourse(course.Course_Id);
                        repeatCourse.Show();
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار كورس");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
