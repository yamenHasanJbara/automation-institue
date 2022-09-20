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
    /// Interaction logic for P_Show_Faculty.xaml
    /// </summary>
    public partial class P_Show_Faculty : Page
    {
        public List<Faculty> faculties { get; set; }
        
        public P_Show_Faculty()
        {
            InitializeComponent();
            LoadFaculties();
        }

        private void LoadFaculties()
        {
            try
            {
                using (var db = new DataBaseContext())
                {

                    var _Faculites = db.Faculties.ToList();
                    faculties = _Faculites;
                    Faculty.ItemsSource = faculties;
                    //var _Years = db.Years.Include(x => x.Faculty).AsEnumerable().GroupBy(x => x.Faculty.Faculty_Id).ToList();
                    //years = _Years;
                    //Faculty.ItemsSource = years;
                }
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
                    var _Faclties = db.Faculties.Where(x=>x.Name.Contains(Search.Text)).ToList();
                    faculties = _Faclties;
                    Faculty.ItemsSource = faculties;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {

            var SelecteFaculty = Faculty.SelectedItem as Faculty;
            try
            {
                if (SelecteFaculty != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelecteFaculty.Faculty_Id);
                        if (String.IsNullOrWhiteSpace(CollageName.Text))
                        {
                            MessageBox.Show("الرجاء ادخال الاسم الجديد للكلية");
                        }
                        else
                        {
                            bool CheckIfUpdateValueIsExist = db.Faculties.Any(x => x.Name == CollageName.Text);
                            if (CheckIfUpdateValueIsExist)
                            {
                                MessageBox.Show("إن الاسم موجود مسبقاً");
                            }
                            else
                            {
                                faculty.Name = CollageName.Text;
                                db.Update(faculty);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية التحديث بنجاح");
                                CollageName.Text = null;
                                LoadFaculties();
                            }
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

        private void Delete(object sender, RoutedEventArgs e)
        {
            var SelecteFaculty = Faculty.SelectedItem as Faculty;
            try
            {
                if (SelecteFaculty != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelecteFaculty.Faculty_Id);
                        if (String.IsNullOrWhiteSpace(CollageName.Text))
                        {
                            MessageBox.Show("الرجاء اختيار الكلية التي ترغب بحذفها");
                        }
                        else
                        {
                            db.Remove(faculty);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            LoadFaculties();
                            CollageName.Text = null;
                        }
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show("لا يمكن حذف كلية لها سنوات");
            }
        }

        private void Faculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelecteFaculty = Faculty.SelectedItem as Faculty;
            try
            {
                if (SelecteFaculty != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x=>x.Faculty_Id == SelecteFaculty.Faculty_Id);
                        CollageName.Text = faculty.Name;
                        IsSelection.IsEnabled = true;
                        IsSelection1.IsEnabled = true;
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

    }
}
