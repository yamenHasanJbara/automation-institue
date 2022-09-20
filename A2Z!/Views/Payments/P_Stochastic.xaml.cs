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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_Stochastic.xaml
    /// </summary>
    public partial class P_Stochastic : Page
    {

        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public List<Material_Study> material_Studies { get; set; }

        public List<Payment> payments { get; set; }

        public List<Course> courses { get; set; }

        public List<StochasticData> stochastics { get; set; }

        public P_Stochastic()
        {
            InitializeComponent();
            Load_Section();
        }

        private void Load_Section()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Sections = db.Sections.ToList();
                    sections = _Sections;
                    Section.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Section_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = Section.SelectedItem as Section;
                using (var db = new DataBaseContext())
                {
                    Section section = new Section();
                    section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                    DisplayStochasticSectionInformation(section);
                    if (section.Faculties.Count > 0)
                    {
                        var _Faculties = db.Faculties.ToList();
                        faculties = _Faculties;
                        Collage.ItemsSource = faculties;
                        Collage.Visibility = Visibility.Visible;
                        YearNumber.Visibility = Visibility.Visible;
                        semester.Visibility = Visibility.Visible;
                        Material.ItemsSource = null;
                    }
                    else
                    {
                        Collage.Visibility = Visibility.Collapsed;
                        YearNumber.Visibility = Visibility.Collapsed;
                        semester.Visibility = Visibility.Collapsed;
                        var materialStuides = db.Material_Studies.Include(x => x.Section).Where(x => x.Section == section).ToList();
                        Material.ItemsSource = materialStuides;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayStochasticSectionInformation(Section section)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => x.section == section).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Collage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedSection = Section.SelectedItem as Section;
            var SelectedCollage = Collage.SelectedItem as Faculty;
            try
            {
                if ((SelectedSection != null) && (SelectedCollage != null))
                {
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.Include(x => x.Section).SingleOrDefault(x => (x.Section == section) && (x.Faculty_Id == SelectedCollage.Faculty_Id));
                        DisplayStochasticSectionFacultyInformation(section, faculty);
                        var _Years = db.Years.Include(x => x.Faculty).Where(x => x.Faculty == faculty).ToList();
                        YearNumber.ItemsSource = _Years;

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        
        private void DisplayStochasticSectionFacultyInformation(Section section, Faculty faculty)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.section == section) && (x.faculty == faculty)).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    stochasticData.faculty = faculty;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void semester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedSection = Section.SelectedItem as Section;
            var SelectedCollage = Collage.SelectedItem as Faculty;
            var SelectedYear = YearNumber.SelectedItem as Year;
            ComboBoxItem typeItem = (ComboBoxItem)semester.SelectedItem;
            string value = typeItem.Content.ToString();
            int Semester = int.Parse(value);
            try
            {
                if ((SelectedSection != null) && (SelectedCollage != null) && (SelectedYear != null) && ((Semester == 1) || (Semester == 2)))
                {
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.Include(x => x.Section).SingleOrDefault(x => (x.Section == section) && (x.Faculty_Id == SelectedCollage.Faculty_Id));
                        Year year = new Year();
                        year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                        DisplayStochasticSectionFacultyYearsemesterInformation(section, faculty, year, Semester);
                        var _materialasStudies = db.Material_Studies.Include(x => x.Faculty).Include(x => x.Section).Include(x => x.Year).Where(x => (x.Section == section) && (x.Faculty == faculty) && (x.Year == year) && (x.Semester == Semester)).ToList();
                        material_Studies = _materialasStudies;
                        Material.ItemsSource = material_Studies;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        
        private void DisplayStochasticSectionFacultyYearsemesterInformation(Section section, Faculty faculty, Year year, int semester)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.section == section) && (x.faculty == faculty) && (x.Year == year) && (x.material_Study.Semester == semester)).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    stochasticData.faculty = faculty;
                    stochasticData.year = year;
                    stochasticData.Semester = semester;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void YearNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedSection = Section.SelectedItem as Section;
            var SelectedCollage = Collage.SelectedItem as Faculty;
            var SelectedYear = YearNumber.SelectedItem as Year;
            try
            {
                if ((SelectedSection != null) && (SelectedCollage != null) && (SelectedYear != null))
                {
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.Include(x => x.Section).SingleOrDefault(x => (x.Section == section) && (x.Faculty_Id == SelectedCollage.Faculty_Id));
                        Year year = new Year();
                        year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                        DisplayStochasticSectionFacultyYearInformation(section, faculty, year);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        
        private void DisplayStochasticSectionFacultyYearInformation(Section section, Faculty faculty, Year year)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.section == section) && (x.faculty == faculty) && (x.Year == year)).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    stochasticData.faculty = faculty;
                    stochasticData.year = year;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedSection = Section.SelectedItem as Section;
            var SelectedCollage = Collage.SelectedItem as Faculty;
            var SelectedYear = YearNumber.SelectedItem as Year;
            ComboBoxItem typeItem = (ComboBoxItem)semester.SelectedItem;
            if (typeItem!=null)
            {
                string value = typeItem.Content.ToString();
                int Semester = int.Parse(value);
                var SelectedMaterial = Material.SelectedItem as Material_Study;
                try
                {
                    if ((SelectedSection != null) && (SelectedCollage != null) && (SelectedYear != null) && ((Semester == 1) || (Semester == 2)) && (SelectedMaterial != null))
                    {
                        using (var db = new DataBaseContext())
                        {
                            Section section = new Section();
                            section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                            Faculty faculty = new Faculty();
                            faculty = db.Faculties.Include(x => x.Section).SingleOrDefault(x => (x.Section == section) && (x.Faculty_Id == SelectedCollage.Faculty_Id));
                            Year year = new Year();
                            year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                            Material_Study material_Study = new Material_Study();
                            material_Study = db.Material_Studies.SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                            DisplayStochasticSectionFacultyYearSemesterMaterialInformation(section, faculty, year, Semester, material_Study);
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                var SelectedMaterial = Material.SelectedItem as Material_Study;
                if (SelectedMaterial!=null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Material_Study material_Study = new Material_Study();
                        material_Study = db.Material_Studies.Include(x => x.Section).SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                        DisplayStochasticSectionMaterialInformation(material_Study, material_Study.Section);
                    }
                }
            }

        }

        private void DisplayStochasticSectionMaterialInformation(Material_Study material_Study, Section section)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.material_Study == material_Study)).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    stochasticData.material_Study = material_Study;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    foreach (var item in courses)
                    {
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void DisplayStochasticSectionFacultyYearSemesterMaterialInformation(Section section, Faculty faculty, Year year, int semester, Material_Study material_Study)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.section == section) && (x.faculty == faculty) && (x.Year == year) && (x.material_Study == material_Study) && (x.material_Study.Semester == semester)).ToList();
                    courses = _SectionCourses;
                    StochasticData stochasticData = new StochasticData();
                    List<StochasticData> stochasticDatas = new List<StochasticData>();
                    stochasticData.section = section;
                    stochasticData.faculty = faculty;
                    stochasticData.year = year;
                    stochasticData.Semester = semester;
                    stochasticData.material_Study = material_Study;
                    long Count1 = 0;
                    long ActualDeserved1 = 0;
                    long InstituteDeserved1 = 0;
                    long TeacherDeserved1 = 0;
                    long StudentTaken1 = 0;
                    long TeacherGiven1 = 0;
                    long Difference1 = 0;
                    long TeacherActualDeserved = 0;
                    foreach (var item in courses)
                    {
                        int coursePayments = 0;
                        Count1 += item.Student_Courses.Count;
                        ActualDeserved1 += item.Student_Courses.Count * item.Price;
                        InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                        TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                        var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                        payments = _StudentPayments;
                        foreach (var item1 in payments)
                        {
                            coursePayments += item1.Amount;
                            StudentTaken1 += item1.Amount;
                        }
                        var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                        payments = _TeacherPayments;
                        foreach (var item1 in payments)
                        {
                            TeacherGiven1 += item1.Amount;
                        }
                        Difference1 = StudentTaken1 - TeacherGiven1;
                        TeacherActualDeserved += coursePayments * item.percent / 100;
                    }
                    stochasticData.Count = Count1;
                    stochasticData.ActualDeserved = ActualDeserved1;
                    stochasticData.InstituteDeserved = InstituteDeserved1;
                    stochasticData.TeacherDeserved = TeacherDeserved1;
                    stochasticData.StudentTaken = StudentTaken1;
                    stochasticData.TeacherGiven = TeacherGiven1;
                    stochasticData.Difference = Difference1;
                    stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                    stochasticDatas.Add(stochasticData);
                    stochastics = stochasticDatas;
                    StochasticDetails.ItemsSource = stochastics;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _SelectedSection = Section.SelectedItem as Section;
                    if ((Start_Date.SelectedDate.Value) != null && (End_Date.SelectedDate.Value) != null && (_SelectedSection != null))
                    {
                        Section section = new Section();
                        section = db.Sections.SingleOrDefault(x => x.Section_Id == _SelectedSection.Section_Id);
                        var _SectionCourses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Include(x => x.Student_Courses).Include(x => x.material_Study).Where(x => (x.section == section) && (x.Start_Date >= Start_Date.SelectedDate.Value) && (x.End_Date <= End_Date.SelectedDate.Value)).ToList();
                        courses = _SectionCourses;
                        StochasticData stochasticData = new StochasticData();
                        List<StochasticData> stochasticDatas = new List<StochasticData>();
                        stochasticData.section = section;
                        long Count1 = 0;
                        long ActualDeserved1 = 0;
                        long InstituteDeserved1 = 0;
                        long TeacherDeserved1 = 0;
                        long StudentTaken1 = 0;
                        long TeacherGiven1 = 0;
                        long Difference1 = 0;
                        long TeacherActualDeserved = 0;
                        foreach (var item in courses)
                        {
                            int coursePayments = 0;
                            Count1 += item.Student_Courses.Count;
                            ActualDeserved1 += item.Student_Courses.Count * item.Price;
                            InstituteDeserved1 += ((100 - item.percent) * item.Student_Courses.Count * item.Price) / 100;
                            TeacherDeserved1 += (item.Student_Courses.Count * item.Price * item.percent) / 100;
                            var _StudentPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 2)).ToList();
                            payments = _StudentPayments;
                            foreach (var item1 in payments)
                            {
                                coursePayments += item1.Amount;
                                StudentTaken1 += item1.Amount;
                            }
                            var _TeacherPayments = db.Payments.Include(x => x.course).Where(x => (x.course == item) && (x.Payment_Type == 1)).ToList();
                            payments = _TeacherPayments;
                            foreach (var item1 in payments)
                            {
                                TeacherGiven1 += item1.Amount;
                            }
                            Difference1 = StudentTaken1 - TeacherGiven1;
                            TeacherActualDeserved += coursePayments * item.percent / 100;
                        }
                        stochasticData.Count = Count1;
                        stochasticData.ActualDeserved = ActualDeserved1;
                        stochasticData.InstituteDeserved = InstituteDeserved1;
                        stochasticData.TeacherDeserved = TeacherDeserved1;
                        stochasticData.StudentTaken = StudentTaken1;
                        stochasticData.TeacherGiven = TeacherGiven1;
                        stochasticData.Difference = Difference1;
                        stochasticData.TeacherActualDeserved = TeacherActualDeserved;
                        stochasticDatas.Add(stochasticData);
                        stochastics = stochasticDatas;
                        StochasticDetails.ItemsSource = stochastics;
                    }
                    else
                    {
                        MessageBox.Show("الرجاء اختيار تاريخ وقسم");
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("الرجاء اختيار تاريخ بداية وانتهاء");

            }
        }

    }
}
