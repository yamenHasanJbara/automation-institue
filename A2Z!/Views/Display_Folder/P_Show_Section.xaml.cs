using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for P_Show_Section.xaml
    /// </summary>
    public partial class P_Show_Section : Page
    {
        public List<Section> sections { get; set; }
        public P_Show_Section()
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
                var SelectedRow = Section.SelectedItem as Section;
                if (SelectedRow != null)
                {
                    Name.Text = SelectedRow.Section_Name;
                    IsSelection.IsEnabled = true;
                    IsSelection1.IsEnabled = true;
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
                    var _Section = db.Sections.Where(x => x.Section_Name.Contains(Search.Text)).ToList();
                    sections = _Section;
                    Section.ItemsSource = sections;
                }
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
                var SelectedSection = Section.SelectedItem as Section;
                if (String.IsNullOrWhiteSpace(Name.Text))
                {
                    MessageBox.Show("الرجاء اختيار قسم للتحديث");
                }
                else
                {
                    if (SelectedSection != null)
                    {
                        using (var db = new DataBaseContext())
                        {
                            Section section = new Section();
                            section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                            if (!String.IsNullOrWhiteSpace(Name.Text))
                            {

                                bool CheckIfTheNewNameIsExistBefor = db.Sections.Any(x => x.Section_Name == Name.Text);
                                if (CheckIfTheNewNameIsExistBefor)
                                {
                                    MessageBox.Show("إن القسم موجود مسبقاً");
                                }
                                else
                                {
                                    section.Section_Name = Name.Text;
                                    db.Update(section);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التحديث بنجاح");
                                    Name.Text = null;
                                    Load_Section();
                                }
                            }
                            else
                            {
                                MessageBox.Show("الرجاء ادخال اسم القسم الجديد");
                            }

                        }
                    }
                    else
                    { }
                } 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedSection = Section.SelectedItem as Section;
                if (SelectedSection != null)
                {
                    if (!String.IsNullOrWhiteSpace(Name.Text))
                    {
                        Section section = new Section();
                        using (var db = new DataBaseContext())
                        {
                            section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                            db.Remove(section);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            Name.Text = null;
                            Load_Section();
                        }
                    }
                    else
                    { MessageBox.Show("الرجاء اختيار القسم الذي ترغب بحذفه"); }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show("لا يمكن حذف قسم له دورات");
            }
        }
    }
}
