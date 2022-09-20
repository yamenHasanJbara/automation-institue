using A2Z_.Healpers;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_Year.xaml
    /// </summary>
    public partial class P_Add_Year : Page
    {
        public List<Faculty> faculties { get; set; }
        public P_Add_Year()
        {
            InitializeComponent();
            Load_Faculties();
        }

        private void Load_Faculties()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var Collage = db.Faculties.Where(x=>x.Section.Faculties != null).ToList();
                    faculties = Collage;
                    Faculty.ItemsSource = faculties;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(NumberOfYear.Text) || Faculty.SelectedItem == null || !IntegerValidation.checkIntValue(NumberOfYear.Text))
                {
                    MessageBox.Show("الرجاء تعبئة الحقول");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Year year = new Year();
                        year.Year_Number = int.Parse(NumberOfYear.Text);
                        var SelectedFaculty = Faculty.SelectedItem as Faculty;
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedFaculty.Faculty_Id);
                        year.Faculty = faculty;
                        bool CheckIfYearIsAddedBefor = db.Years.Include(x => x.Faculty).Any(x=> (x.Faculty.Faculty_Id == faculty.Faculty_Id) && (x.Year_Number == int.Parse(NumberOfYear.Text)));
                        if (CheckIfYearIsAddedBefor)
                        {
                            MessageBox.Show("هذه السنة مضافة من قبل لهذه الكلية");
                        }
                        else
                        {
                            db.Years.Add(year);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الإضافة بنجاح");
                            NumberOfYear.Text = null;
                            Faculty.SelectedItem = null;
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
