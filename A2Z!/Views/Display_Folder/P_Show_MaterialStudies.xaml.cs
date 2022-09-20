using A2Z_.Models;
using A2Z_.Views.Add_Folder;
using Microsoft.EntityFrameworkCore;
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

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for P_Show_MaterialStudies.xaml
    /// </summary>
    public partial class P_Show_MaterialStudies : Page
    {
        public List<Material_Study> material_Studies { get; set; }

        public List<Year> years { get; set; }
        public P_Show_MaterialStudies()
        {
            InitializeComponent();
            Load_MaterialStudy();
        }

        private void Load_MaterialStudy()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Material_Study = db.Material_Studies.Include(x=>x.Faculty).Include(x=>x.Year).Include(x=>x.Section).ToList();
                    material_Studies = _Material_Study;
                    materialStudies.ItemsSource = material_Studies;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void materialStudies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedMaterial = materialStudies.SelectedItem as Material_Study;
            try
            {
                if (SelectedMaterial != null)
                {
                    IsSelection.IsEnabled = true;
                    IsSelection1.IsEnabled = true;
                    Material_Study material_Study = new Material_Study();
                    using (var db = new DataBaseContext())
                    {
                        material_Study = db.Material_Studies.Include(x => x.Faculty).Include(x => x.Year).Include(x => x.Section).SingleOrDefault(x=>x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                        MaterialName.Text = material_Study.Name;
                        if (material_Study.Faculty == null || material_Study.Year == null)
                        {
                            YearNumber.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            YearNumber.Visibility = Visibility.Visible;
                            YearNumber.Text = material_Study.Year.Year_Number.ToString();
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

        private void Update(object sender, RoutedEventArgs e)
        {
            var SelectedMaterial = materialStudies.SelectedItem as Material_Study;
            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)SemesterNumber.SelectedItem;
                if (typeItem!= null)
                {
                    string value = typeItem.Content.ToString();
                    int Semester = int.Parse(value);
                    if (SelectedMaterial != null)
                    {
                        using (var db = new DataBaseContext())
                        {
                            Material_Study material_Study = new Material_Study();
                            Year year = new Year();
                            material_Study = db.Material_Studies.Include(x => x.Year).Include(x => x.Faculty).Include(x => x.Section).SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                            // the material is دورات جامعية here i should check if i want to update the value is not exist in database
                            if (material_Study.Faculty != null || material_Study.Year != null || (SemesterNumber.SelectedIndex == -1))
                            {
                                if (String.IsNullOrWhiteSpace(MaterialName.Text))
                                {
                                    MessageBox.Show("الرجاء تعبئة حقل الاسم");
                                }
                                else
                                {
                                    year = db.Years.Include(x => x.Faculty).SingleOrDefault(x => x.Faculty.Faculty_Id == SelectedMaterial.Faculty.Faculty_Id && x.Year_Number == int.Parse(YearNumber.Text));
                                    bool Check = db.Material_Studies.Include(x => x.Year).Include(x => x.Faculty).Any(x => (x.Faculty.Faculty_Id == material_Study.Faculty.Faculty_Id) && (x.Year.Year_Id == year.Year_Id) && (x.Name == MaterialName.Text) && (x.Semester == Semester));
                                    if (Check)
                                    {
                                        MessageBox.Show("المعلومات المدخلة موجودة مسبقاً ");
                                    }
                                    else
                                    {
                                        material_Study.Name = MaterialName.Text;
                                        material_Study.Year = year;
                                        material_Study.Semester = Semester;
                                        db.Update(material_Study);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية التحديث بنجاح ");
                                        MaterialName.Text = null;
                                        YearNumber.SelectedItem = null;
                                        SemesterNumber.SelectedIndex = -1;
                                        Load_MaterialStudy();

                                    }
                                }
                            }
                            else
                            {
                                bool Check = db.Material_Studies.Any(x => x.Name == MaterialName.Text);
                                if (Check)
                                {
                                    MessageBox.Show("الرجاء عدم ادخال نفس الاسم");
                                }
                                else
                                {
                                    material_Study.Name = MaterialName.Text;
                                    db.Update(material_Study);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التحديث بنجاح ");
                                    MaterialName.Text = null;
                                    YearNumber.SelectedItem = null;
                                    SemesterNumber.SelectedIndex = -1;
                                    Load_MaterialStudy();
                                }
                            }
                        }
                    }
                    else
                    { }
                }
                else
                { MessageBox.Show("اختيار فصل المادة"); }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var SelectedMaterial = materialStudies.SelectedItem as Material_Study;
            try
            {
                if (SelectedMaterial != null)
                {
                    Material_Study material_Study = new Material_Study();
                    using (var db = new DataBaseContext())
                    {
                        material_Study = db.Material_Studies.SingleOrDefault(x => x.Material_Study_Id == SelectedMaterial.Material_Study_Id);
                        db.Remove(material_Study);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        MaterialName.Text = null;
                        Load_MaterialStudy();
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show("لا يمكن حذف مادة لها دورات");
            }
        }

        private void MaterialName_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                var SelectedMaterial = materialStudies.SelectedItem as Material_Study;
                if (SelectedMaterial != null)
                {
                    if (SelectedMaterial.Faculty != null)
                    {
                        using (var db = new DataBaseContext())
                        {
                            var _Years = db.Years.Include(x => x.Faculty).Where(x => x.Faculty.Faculty_Id == SelectedMaterial.Faculty.Faculty_Id).ToList();
                            years = _Years;
                            YearNumber.ItemsSource = years;
                        }
                    }
                    else
                    { }
                    
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _material = db.Material_Studies.Include(x => x.Faculty).Include(x => x.Year).Include(x => x.Section).Where(x => x.Name.Contains(Search.Text)).ToList();
                    material_Studies = _material;
                    materialStudies.ItemsSource = material_Studies;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
