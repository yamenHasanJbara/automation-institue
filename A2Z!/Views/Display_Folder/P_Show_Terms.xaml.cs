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

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for P_Show_Terms.xaml
    /// </summary>
    public partial class P_Show_Terms : Page
    {

        public List<Term> terms { get; set; }

        public P_Show_Terms()
        {
            InitializeComponent();
            loadInformation();
        }

        private void loadInformation()
        {
            using (var db = new DataBaseContext())
            {
                var _trems = db.terms.ToList();
                terms = _trems;
                Tearms.ItemsSource = terms;
            }    
        }

        private void Tearms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTerm = Tearms.SelectedItem as Term;
            try
            {
                if (selectedTerm != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        var term = db.terms.Where(x => x.Terms_id == selectedTerm.Terms_id).FirstOrDefault();
                        Name.Text = term.name;
                        start_date.SelectedDate = term.start_date;
                        end_date.SelectedDate = term.end_date;
                    }
                    IsSelection.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            var selectedTerm = Tearms.SelectedItem as Term;
            if (selectedTerm != null)
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        var term = db.terms.SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                        term.name = Name.Text;
                        term.start_date = start_date.SelectedDate.Value;
                        term.end_date = end_date.SelectedDate.Value;
                        db.terms.Update(term);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية التحديث بنجاح");
                        Name.Text = null;
                        start_date.SelectedDate = null;
                        end_date.SelectedDate = null;
                        loadInformation();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _terms = db.terms.Where(x => x.name.Contains(Search.Text)).ToList();
                    terms = _terms;
                    Tearms.ItemsSource = terms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
