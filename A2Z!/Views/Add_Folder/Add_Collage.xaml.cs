using A2Z_.Models;
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
    /// Interaction logic for Add_Collage.xaml
    /// </summary>
    public partial class Add_Collage : Page
    {
        public List<Section> sections { get; set; }
        public Add_Collage()
        {
            InitializeComponent();
            Load_Sections();
        }

        private void Load_Sections()
        {
            try
            {
                using (var db  = new DataBaseContext())
                {
                    var section = db.Sections.ToList();
                    sections = section;
                    Sections.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            
                if (String.IsNullOrWhiteSpace(Name.Text))
                {
                    MessageBox.Show("الرجاء تعبئة حقل الاسم");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        String NameOfFaculty = Name.Text;
                        bool CheckIfExist = db.Faculties.Any(x => x.Name == NameOfFaculty);
                        if (CheckIfExist)
                        {
                            MessageBox.Show("لا يمكن إضافة كلية موجودة");
                            Name.Text = null;
                            Sections.SelectedItem = null;
                        }
                        else
                        {
                            faculty.Name = NameOfFaculty;
                            var SelectedSection = Sections.SelectedItem as Section;
                            if (SelectedSection == null)
                            {
                            MessageBox.Show("الرجاء اختيار قسم ");
                            }
                            else
                            {
                                Section section = new Section();
                                section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                                faculty.Section = section;
                                db.Faculties.Add(faculty);
                                db.SaveChanges();
                                MessageBox.Show("تمت عمليةالإضافة بنجاح");
                                Name.Text = null;
                                Sections.SelectedItem = null;
                        }
                        }

                    }
                }
            
        }
    }
}
