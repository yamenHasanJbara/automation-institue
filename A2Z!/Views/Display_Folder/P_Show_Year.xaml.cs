using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
    /// Interaction logic for P_Show_Year.xaml
    /// </summary>
    public partial class P_Show_Year : Page
    {
        public IEnumerable<IGrouping<int, Year>> years { get; set; }
        public List<Material_Study> Material_Studies { get; set; }
        public P_Show_Year()
            {
                InitializeComponent();
                Load_Years();
            }

        private void Load_Years()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Years = db.Years.Include(x => x.Faculty).AsEnumerable().GroupBy(x => x.Faculty.Faculty_Id).ToList();
                    years = _Years;
                    Year.ItemsSource = years;

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
                    var _Years = db.Years.Include(x => x.Faculty).Where(x => x.Faculty.Name.Contains(Search.Text)).AsEnumerable().GroupBy(x=>x.Faculty.Faculty_Id).ToList();
                    years = _Years;
                    Year.ItemsSource = years;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedYear = Year.SelectedItem;
            try
            {
                using (var db = new DataBaseContext())
                {
                    if (SelectedYear != null)
                    {
                        IsSelection1.IsEnabled = true;
                        int Id = Convert.ToInt32((Year.SelectedCells[0].Column.GetCellContent(SelectedYear) as TextBlock).Text);
                        Year year = new Year();
                        year = db.Years.Include(x => x.Faculty).SingleOrDefault(x=>x.Year_Id == Id);
                        CollageName.Text = year.Faculty.Name;
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
                var SelectedYear = Year.SelectedItem;

                using (var db = new DataBaseContext())
                {
                    if (SelectedYear != null)
                    {
                        IsSelection1.IsEnabled = true;
                        int Id = Convert.ToInt32((Year.SelectedCells[0].Column.GetCellContent(SelectedYear) as TextBlock).Text);
                        Year year = new Year();
                        Year year1 = new Year();
                        year = db.Years.Include(x => x.Faculty).SingleOrDefault(x => x.Year_Id == Id);

                        if (CollageName.Text == null || Year_Number.Text == null)
                        {
                            MessageBox.Show("الرجاء تعبئة كافة الحقول");
                        }
                        else
                        {
                            year1 = db.Years.Include(x => x.Faculty).Include(x => x.Material_Studies).SingleOrDefault(x => x.Faculty.Name == CollageName.Text && x.Year_Number == int.Parse(Year_Number.Text));
                            if (year1 != null)
                            {
                                db.Remove(year1);
                                var DeletedMaterialForThisYear = db.Material_Studies.Include(x => x.Faculty).Include(x => x.Section).Include(x => x.Year).Where(x => x.Year == year1).ToList();
                                Material_Studies = DeletedMaterialForThisYear;
                                foreach (var item in Material_Studies)
                                {
                                    db.Material_Studies.Remove(item);

                                }
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية الحذف بنجاح");
                                CollageName.Text = null;
                                Year_Number.Text = null;
                                Load_Years();
                            }
                            else
                            {
                                MessageBox.Show("لا يمكن حذف سنة غير موجودة لهذه الكلية");
                            }
                        }
                    }
                    else
                    { }

                }

            }
            catch (Exception)
            {

                MessageBox.Show("هذه السنة غير موجودة");
            }
            
        }
    }
}
