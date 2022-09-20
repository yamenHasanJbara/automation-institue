using A2Z_.Models;
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
    /// Interaction logic for P_Add_Term.xaml
    /// </summary>
    public partial class P_Add_Term : Page
    {
        public P_Add_Term()
        {
            InitializeComponent();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Name.Text) || (start_date.SelectedDate.Value == null) || (end_date.SelectedDate.Value == null))
            {
                MessageBox.Show("الرجاء ادخال كافة الحقول");
            }
            else
            {
                using (var db = new DataBaseContext())
                {
                    Term term = new Term();
                    term.name = Name.Text;
                    term.start_date = start_date.SelectedDate.Value;
                    term.end_date = end_date.SelectedDate.Value;
                    db.terms.Add(term);
                    db.SaveChanges();
                    MessageBox.Show("تمت عملية الاضافة بنجاح");
                    Name.Text = null;
                    start_date.SelectedDate = null;
                    end_date.SelectedDate = null;
                }
                
            }
        }
    }
}
