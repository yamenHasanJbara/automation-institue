using A2Z_.Models;
using A2Z_.Views.Display_Folder;
using Microsoft.EntityFrameworkCore.Storage;
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
    /// Interaction logic for P_Add_Section.xaml
    /// </summary>
    public partial class P_Add_Section : Page
    {
        public P_Add_Section()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Name.Text))
                {
                    MessageBox.Show("الرجاء تعبئة الحقول");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        String section_name = Name.Text;
                        bool CheckIfExist = db.Sections.Any(x => x.Section_Name == section_name);
                        if (CheckIfExist)
                        {
                            MessageBox.Show("القسم موجود سابقأ");
                            Name.Text = null;
                        }
                        else
                        {
                            section.Section_Name = section_name;
                            db.Sections.Add(section);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الإضافة بنجاح");
                            Name.Text = null;
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
