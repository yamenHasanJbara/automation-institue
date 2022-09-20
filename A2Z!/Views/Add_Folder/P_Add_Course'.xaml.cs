using A2Z_.Healpers;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_Course_.xaml
    /// </summary>
    public partial class P_Add_Course_ : Page
    {
        public List<Section> sections{ get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public List<Material_Study> material_Studies { get; set; }

        public List<Teacher> teachers { get; set; }

        public List<Term> terms { get; set; }
        public P_Add_Course_()
        {
            InitializeComponent();
            Load_Section();
            Load_Teacher();
            load_Terms();
            Start_Date.SelectedDate = DateTime.Today;
        }

        private void load_Terms()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _trems = db.terms.ToList();
                    terms = _trems;
                    Terms.ItemsSource = terms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                        section  = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        if (section.Faculties.Count >0)
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
                            Terms.Visibility = Visibility.Visible;
                            Start_Date.Visibility = Visibility.Collapsed;
                            End_Date.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            CollagesName.Visibility = Visibility.Collapsed;
                            YearsNumber.Visibility = Visibility.Collapsed;
                            SemesterNumber.Visibility = Visibility.Collapsed;
                            Start_Date.Visibility = Visibility.Visible;
                            End_Date.Visibility = Visibility.Visible;
                            Terms.Visibility = Visibility.Collapsed;
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
                { MaterialsName.ItemsSource = null;  }
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
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x=>x.Section_Id == SelectedSection.Section_Id);
                        // here to check if i want to create course for faclty or not
                        if (section.Faculties.Count > 0)
                        {
                            if ((CollagesName.SelectedItem == null) || (YearsNumber.SelectedItem == null) || (SemesterNumber.SelectedIndex == -1) || (MaterialsName.SelectedItem == null) || (TeachersName.SelectedItem == null) || (String.IsNullOrWhiteSpace(Percent.Text)) || (String.IsNullOrWhiteSpace(Group.Text)) || (String.IsNullOrWhiteSpace(Price.Text)))
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
                                if (!IntegerValidation.checkIntValue(Price.Text) || !IntegerValidation.checkIntValue(Percent.Text))
                                {
                                    
                                    MessageBox.Show("الرجاء ادخال النسبة والسعر بصيغ صحيحة");
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
                                    var selectedTerm = Terms.SelectedItem as Term;
                                    Term term = new Term();
                                    term = db.terms.SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                                    Course course = new Course();
                                    course.section = section;
                                    course.faculty = faculty;
                                    course.Year = year;
                                    course.material_Study = material_Study;
                                    course.teacher = teacher;
                                    course.percent = int.Parse(Percent.Text);
                                    course.Group = Group.Text;
                                    course.term = term;
                                    course.Start_Date = term.start_date;
                                    course.End_Date = term.end_date;
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
                                        Terms.SelectedItem = null;
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
                                        SectionsName.SelectedItem = null;
                                        CollagesName.SelectedItem = null;
                                        YearsNumber.SelectedItem = null;
                                        SemesterNumber.SelectedIndex = -1;
                                        MaterialsName.SelectedItem = null;
                                        TeachersName.SelectedItem = null;
                                        Terms.SelectedItem = null;
                                        Percent.Text = null;
                                        Group.Text = null;
                                        Start_Date.SelectedDate = null;
                                        End_Date.SelectedDate = null;
                                        Price.Text = null;
                                    }
                                }
                            }
                        }
                        // here language course or program course
                        else
                        {
                            if (!IntegerValidation.checkIntValue(Price.Text) || !IntegerValidation.checkIntValue(Percent.Text))
                            {
                                MessageBox.Show("الرجاء ادخال النسبة والسعر بصيغ صحيحة");
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
                            }
                        }
                    }
                }else
                {
                    MessageBox.Show("الرجاء اختيار حقل القسم");
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
    }
}
