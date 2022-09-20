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

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_MaterialStudy.xaml
    /// </summary>

    public partial class P_Add_MaterialStudy : Page
    {

        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public P_Add_MaterialStudy()
        {
            InitializeComponent();
            Load_Information();
        }

        private void Load_Information()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var section = db.Sections.ToList();
                    sections = section;
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
                Section section = new Section();
                var SelectedSection = SectionName.SelectedItem as Section;
                if (SelectedSection != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        if (section.Faculties.Count > 0)
                        {
                            var Collage = db.Faculties.ToList();
                            faculties = Collage;
                            CollageName.ItemsSource = faculties;
                            CollageName.Visibility = Visibility.Visible;
                            YearNumber.Visibility = Visibility.Visible;
                            SemesterNumber.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            CollageName.Visibility = Visibility.Collapsed;
                            YearNumber.Visibility = Visibility.Collapsed;
                            SemesterNumber.Visibility = Visibility.Collapsed;
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

        private void CollageName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                var SelectedCollage = CollageName.SelectedItem as Faculty;
                if (SelectedCollage != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        var CollageYear = db.Years.Where(x => x.Faculty.Faculty_Id == SelectedCollage.Faculty_Id).ToList();
                        years = CollageYear;
                        YearNumber.ItemsSource = years;
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

        private void Add(object sender, RoutedEventArgs e)
        {
            var SelectedSection = SectionName.SelectedItem as Section;
            var SelectedFaculty = CollageName.SelectedItem as Faculty;
            var SelectedYear = YearNumber.SelectedItem as Year;
            try
            {
                if ( (SelectedSection == null) || (String.IsNullOrWhiteSpace(Name.Text)))
                {
                    MessageBox.Show("الرجاء تعبئة الحقول المطلوبة");
                }
                else
                {
                    Section section = new Section();
                    Faculty faculty = new Faculty();
                    Year year = new Year();
                    Material_Study material_Study = new Material_Study();
                    using (var db = new DataBaseContext())
                    {
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        if (section.Faculties.Count > 0)
                        {
                            material_Study.Name = Name.Text;
                            material_Study.Section = section;
                            ComboBoxItem typeItem = (ComboBoxItem)SemesterNumber.SelectedItem;
                            if (typeItem != null)
                            {
                                string value = typeItem.Content.ToString();
                                int Semester = int.Parse(value);
                                if ((SelectedFaculty == null) || (SelectedYear == null) )
                                {
                                    MessageBox.Show("الرجاء تعبئة الحقول المطلوبة");
                                }
                                // here if not null
                                else
                                {
                                    faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedFaculty.Faculty_Id);
                                    year = db.Years.SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                                    material_Study.Faculty = faculty;
                                    material_Study.Year = year;
                                    material_Study.Semester = Semester;
                                    bool CheckIfExists = db.Material_Studies.Include(x => x.Section).Include(x => x.Faculty).Include(x => x.Year).Any(x => (x.Name == Name.Text)&& (x.Section == section) && (x.Faculty == faculty) && (x.Year == year) && (x.Semester == Semester));
                                    if (CheckIfExists)
                                    {
                                        MessageBox.Show("إن المادة مضافة مسبقاً");
                                        Name.Text = null;
                                        SectionName.SelectedItem = null;
                                        CollageName.SelectedItem = null;
                                        YearNumber.SelectedItem = null;
                                        SemesterNumber.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        db.Material_Studies.Add(material_Study);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية إضافة مادة بنجاح");
                                        Name.Text = null;
                                        SectionName.SelectedItem = null;
                                        CollageName.SelectedItem = null;
                                        YearNumber.SelectedItem = null;
                                        SemesterNumber.SelectedIndex = -1;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("الرجاء تعبئة الحقول المطلوبة");
                            }
                        }
                        // here if section.faculty.count == 0
                        else
                        {
                            material_Study.Name = Name.Text;
                            material_Study.Section = section;
                            bool check = db.Material_Studies.Include(x => x.Section).Any(x => (x.Section == section) && (x.Name == Name.Text));
                            if (check)
                            {
                                MessageBox.Show("إن المادة مضافة مسبقاً");
                            }
                            else
                            {
                                db.Material_Studies.Add(material_Study);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية إضافة مادة بنجاح");
                                Name.Text = null;
                                SectionName.SelectedItem = null;
                            }
                        }
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
