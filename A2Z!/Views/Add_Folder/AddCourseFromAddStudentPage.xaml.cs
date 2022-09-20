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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for AddCourseFromAddStudentPage.xaml
    /// </summary>
    public partial class AddCourseFromAddStudentPage : Window
    {
        public List<Teacher> teachers { get; set; }
        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public P_Add_Existing_Student p_Add_Existing_Student { get; set; }

        public P_Add_Student p_Add_Student { get; set; }

        public List<Material_Study> material_Studies { get; set; }

        
        public AddCourseFromAddStudentPage()
        {
            InitializeComponent();
            Load_Section();
            Load_Teacher();
            
        }

        public void Load_Teacher()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var Teachers = db.Teachers.ToList();
                    teachers = Teachers;
                    TeachersName.ItemsSource = teachers;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Load_Section()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var Sections = db.Sections.ToList();
                    sections = Sections;
                    SectionsName.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void SectionsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = SectionsName.SelectedItem as Section;
                if (SelectedSection != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        if (section.Faculties.Count > 0)
                        {
                            var Collage = db.Faculties.Include(x => x.Section).Where(x => x.Section.Section_Id == section.Section_Id).ToList();
                            faculties = Collage;
                            CollagesName.ItemsSource = faculties;
                            CollagesName.SelectedItem = null;
                            YearsNumber.SelectedItem = null;
                            SemesterNumber.SelectedIndex = -1;
                            MaterialsName.ItemsSource = null;
                            CollagesName.Visibility = Visibility.Visible;
                            YearsNumber.Visibility = Visibility.Visible;
                            SemesterNumber.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            CollagesName.Visibility = Visibility.Collapsed;
                            YearsNumber.Visibility = Visibility.Collapsed;
                            SemesterNumber.Visibility = Visibility.Collapsed;
                            var Materials = db.Material_Studies.Include(x => x.Section).Where(x => x.Section.Section_Id == section.Section_Id).ToList();
                            material_Studies = Materials;
                            MaterialsName.ItemsSource = material_Studies;
                        }
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

        public void CollagesName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedCollage = CollagesName.SelectedItem as Faculty;
                if (SelectedCollage != null)
                {
                    MaterialsName.ItemsSource = null;
                    SemesterNumber.SelectedIndex = -1;
                    using (var db = new DataBaseContext())
                    {
                        var Years = db.Years.Include(x => x.Faculty).Where(x => x.Faculty.Faculty_Id == SelectedCollage.Faculty_Id).ToList();
                        years = Years;
                        YearsNumber.ItemsSource = years;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        public void SemesterNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedYear = YearsNumber.SelectedItem as Year;
                ComboBoxItem typeItem = (ComboBoxItem)SemesterNumber.SelectedItem;
                if (typeItem != null && YearsNumber.SelectedItem != null)
                {
                    string value = typeItem.Content.ToString();
                    int Semester = int.Parse(value);
                    if ((Semester != 1) || (Semester != 2))
                    {
                        Year year = new Year();
                        using (var db = new DataBaseContext())
                        {
                            year = db.Years.Include(x => x.Faculty).SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                            var _MaterialStudies = db.Material_Studies.Include(x => x.Year).Include(x => x.Section).Include(x => x.Faculty).Where(x => (x.Year == year) && (x.Semester == Semester)).ToList();
                            material_Studies = _MaterialStudies;
                            MaterialsName.ItemsSource = material_Studies;
                        }
                    }
                    else
                    { }
                }
                else
                { MaterialsName.ItemsSource = null; }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        
        public void Add(object sender, RoutedEventArgs e)
        {
            var SelectedSection = SectionsName.SelectedItem as Section;
            try
            {
                if (SelectedSection != null)
                {
                    Section section = new Section();
                    using (var db = new DataBaseContext())
                    {
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        // here to check if i want to create course for faclty or not
                        if (section.Faculties.Count > 0)
                        {
                            if ((CollagesName.SelectedItem == null) || (YearsNumber.SelectedItem == null) || (SemesterNumber.SelectedIndex == -1) || (MaterialsName.SelectedItem == null) || (TeachersName.SelectedItem == null) || (String.IsNullOrWhiteSpace(Percent.Text)) || (String.IsNullOrWhiteSpace(Group.Text)) || (Start_Date.SelectedDate.Value == null) || (End_Date.SelectedDate.Value == null) || (String.IsNullOrWhiteSpace(Price.Text)))
                            {
                                MessageBox.Show("الرجاء تعبئة الحقول المطلوبة");
                                SectionsName.SelectedItem = null;
                                CollagesName.SelectedItem = null;
                                YearsNumber.SelectedItem = null;
                                SemesterNumber.SelectedIndex = -1;
                                MaterialsName.SelectedItem = null;
                                TeachersName.SelectedItem = null;
                                Percent.Text = null;
                                Group.Text = null;
                                Start_Date.SelectedDate = null;
                                End_Date.SelectedDate = null;
                                Price.Text = null;
                            }
                            // here we will save course details
                            else
                            {
                                if (IntegerValidation.checkIntValue(Price.Text) || IntegerValidation.checkIntValue(Percent.Text))
                                {
                                    MessageBox.Show("ادخل النسبة والسعر بصيغ رقمية صحيحة");
                                }
                                else
                                {
                                    var SelectedFaculty = CollagesName.SelectedItem as Faculty;
                                    Faculty faculty = new Faculty();
                                    faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedFaculty.Faculty_Id);
                                    var SelectedYear = YearsNumber.SelectedItem as Year;
                                    Year year = new Year();
                                    year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                                    var SelectedMaterial = MaterialsName.SelectedItem as Material_Study;
                                    Material_Study material_Study = new Material_Study();
                                    material_Study = db.Material_Studies.SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                                    var SeltectedTeacher = TeachersName.SelectedItem as Teacher;
                                    Teacher teacher = new Teacher();
                                    teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SeltectedTeacher.Teacher_Id);
                                    Course course = new Course();
                                    course.section = section;
                                    course.faculty = faculty;
                                    course.Year = year;
                                    course.material_Study = material_Study;
                                    course.teacher = teacher;
                                    course.percent = int.Parse(Percent.Text);
                                    course.Group = Group.Text;
                                    course.Start_Date = Start_Date.SelectedDate.Value;
                                    course.End_Date = End_Date.SelectedDate.Value;
                                    course.Price = int.Parse(Price.Text);
                                    bool Check = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Include(x => x.teacher).Any(x => (x.section == section) && (x.faculty == faculty) && (x.Year == year) && (x.teacher == teacher) && (x.material_Study == material_Study) && (x.Group.Contains(Group.Text)) && (x.IsFinished == false));
                                    if (Check)
                                    {
                                        MessageBox.Show("إن الدورة موجودة بالفعل");
                                        SectionsName.SelectedItem = null;
                                        CollagesName.SelectedItem = null;
                                        YearsNumber.SelectedItem = null;
                                        SemesterNumber.SelectedIndex = -1;
                                        MaterialsName.SelectedItem = null;
                                        TeachersName.SelectedItem = null;
                                        Percent.Text = null;
                                        Group.Text = null;
                                        Start_Date.SelectedDate = null;
                                        End_Date.SelectedDate = null;
                                        Price.Text = null;
                                    }
                                    else
                                    {
                                        db.Courses.Add(course);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية الاضافة بنجاح");
                                        if (p_Add_Existing_Student != null)
                                        {

                                            From_P_Add_ExistingStudent(course);
                                        }
                                        else
                                        { }
                                        if (p_Add_Student != null)
                                        {
                                            From_P_Add_Student(course);
                                        }
                                        else
                                        { }
                                        this.Close();

                                    }
                                }
                            }
                        }
                        // here language course or program course
                        else
                        {
                            if (IntegerValidation.checkIntValue(Price.Text) || IntegerValidation.checkIntValue(Percent.Text))
                            {
                                MessageBox.Show("ادخل النسبة والسعر بصيغ رقمية صحيحة");
                            }
                            else
                            {
                                Course course = new Course();
                                var SelectedMaterial = MaterialsName.SelectedItem as Material_Study;
                                Material_Study material_Study = new Material_Study();
                                material_Study = db.Material_Studies.SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                                var SeltectedTeacher = TeachersName.SelectedItem as Teacher;
                                Teacher teacher = new Teacher();
                                teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SeltectedTeacher.Teacher_Id);
                                course.section = section;
                                course.faculty = null;
                                course.Year = null;
                                course.material_Study = material_Study;
                                course.teacher = teacher;
                                course.percent = int.Parse(Percent.Text);
                                course.Group = Group.Text;
                                course.Start_Date = Start_Date.SelectedDate.Value;
                                course.End_Date = End_Date.SelectedDate.Value;
                                course.Price = int.Parse(Price.Text);
                                bool Check = db.Courses.Include(x => x.section).Include(x => x.material_Study).Include(x => x.teacher).Any(x => (x.section == section) && (x.material_Study == material_Study) && (x.teacher == teacher) && (x.Group.Contains(Group.Text)) && (x.IsFinished == false));
                                if (Check)
                                {
                                    MessageBox.Show("إن الدورة موجودة بالفعل");
                                    SectionsName.SelectedItem = null;
                                    CollagesName.SelectedItem = null;
                                    YearsNumber.SelectedItem = null;
                                    SemesterNumber.SelectedIndex = -1;
                                    MaterialsName.SelectedItem = null;
                                    TeachersName.SelectedItem = null;
                                    Percent.Text = null;
                                    Group.Text = null;
                                    Start_Date.SelectedDate = null;
                                    End_Date.SelectedDate = null;
                                    Price.Text = null;
                                }
                                else
                                {
                                    db.Courses.Add(course);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الاضافة بنجاح");
                                    if (p_Add_Existing_Student != null)
                                    {

                                        From_P_Add_ExistingStudent(course);
                                    }
                                    else
                                    { }
                                    if (p_Add_Student != null)
                                    {
                                        From_P_Add_Student(course);
                                    }
                                    else
                                    { }
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار حقل القسم");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void From_P_Add_Student(Course course)
        {
            try
            {
                if (p_Add_Student.faculties != null)
                {
                    p_Add_Student.courses.Add(course);
                    p_Add_Student.SemesterNumber_SelectionChanged(p_Add_Student.Coursess, null);
                }
                else
                {
                    p_Add_Student.courses.Add(course);
                    p_Add_Student.SectionsName_SelectionChanged(p_Add_Student.Coursess, null);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void From_P_Add_ExistingStudent(Course course)
        {
            try
            {
                if (p_Add_Existing_Student.faculties!= null)
                {
                    p_Add_Existing_Student.courses.Add(course);
                    p_Add_Existing_Student.SemesterNumber_SelectionChanged(p_Add_Existing_Student.Coursess, null);
                }
                else
                {
                    p_Add_Existing_Student.courses.Add(course);
                    p_Add_Existing_Student.SectionsName_SelectionChanged(p_Add_Existing_Student.Coursess, null);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void YearsNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SemesterNumber.SelectedIndex = -1;
                MaterialsName.SelectedItem = null;
                MaterialsName.ItemsSource = null;
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
    }
}
