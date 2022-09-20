using A2Z_.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xaml.Permissions;

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_show_Outlay.xaml
    /// </summary>
    public partial class P_show_Outlay : Page
    {

        public List<Outlay> outlays { get; set; }

        public List<Outlay> TeacherCheck { get; set; }

        public List<Outlay> StudentCheck { get; set; }

        public List<Outlay> Others { get; set; }

        public List<TypyOfOutlayPayment> typyOfOutlayPayments { get; set; }
        public P_show_Outlay()
        {
            InitializeComponent();
            Load_Today_Payments();
        }

        public void Load_Today_Payments()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    DateTime dateTime = DateTime.Today;
                    var _ListOfOutlay = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.date == dateTime).ToList();
                    outlays = _ListOfOutlay;
                    Outlays.ItemsSource = outlays;
                    summing.Text = null;
                    var typesOfOutlay = db.TypyOfOutlayPayments.ToList();
                    typyOfOutlayPayments = typesOfOutlay;
                    OutLayTypes.ItemsSource = typyOfOutlayPayments;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Start_Date.SelectedDate != null && End_Date.SelectedDate != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        var _OutlayBetweenTwodates = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).ToList();
                        outlays = _OutlayBetweenTwodates;
                        Outlays.ItemsSource = outlays;
                        int Payments = 0;
                        int kabed = 0;
                        foreach (var item in outlays)
                        {
                            //دفع
                            if (item.Outlay_Type == 1)
                            {
                                Payments += item.Amount;
                            }
                            else
                            {
                                kabed += item.Amount;
                            }
                        }
                        long theWinnerWinner = kabed - Payments;
                        Thewin.Text = theWinnerWinner.ToString();
                    }
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        var outlies = db.Outlays.ToList();
                        outlays = outlies;
                        Outlays.ItemsSource = outlays;

                        long kabed = db.Payments.Where(x=>x.Payment_Type == 2).Sum(x=>x.Amount);
                        long outliessum = db.Outlays.Where(x => x.Outlay_Type == 1).Sum(x => x.Amount);
                        long theWinnerWinner = kabed - outliessum;
                        Thewin.Text = theWinnerWinner.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BackupDataBase(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = @"Data Source = E:\Special\SecretSanta\BackUp\backupDb.sqlite";
                string SourceData = @"Data Source = E:\Special\SecretSanta\LastOne\A2Z.sqlite";
                using (var con = new SqliteConnection(SourceData))
                {
                    SqliteConnection backup = new SqliteConnection(sql);
                    con.Open();
                    backup.Open();
                    con.BackupDatabase(backup, "main", "main");
                    backup.Close();
                    con.Close();
                    MessageBox.Show("تمت عملية النسخ بنجاح");
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
                var SelectedRow = Outlays.SelectedItem as Outlay;
                if (SelectedRow != null)
                {
                    Outlay outlay = new Outlay();
                    using (var db = new DataBaseContext())
                    {
                        outlay = db.Outlays.SingleOrDefault(x => x.Outlay_Id == SelectedRow.Outlay_Id);
                        if (outlay.Note == "واردات اليوم من الطلاب" || outlay.Note == "دفوعات اليوم للاساتذة")
                        {
                            MessageBox.Show("عملية التعديل غير ممكنة");
                        }
                        else
                        {
                            UpdateOutlay updateOutlay = new UpdateOutlay(outlay.Outlay_Id);
                            updateOutlay.p_Show_Outlay = this;
                            updateOutlay.Show();
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار دفعة من اجل عملية التحديث");
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
                var SelectedRow = Outlays.SelectedItem as Outlay;
                if (SelectedRow != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Outlay outlay = new Outlay();
                        outlay = db.Outlays.SingleOrDefault(x => x.Outlay_Id == SelectedRow.Outlay_Id);
                        if (outlay.Note == "واردات اليوم من الطلاب" || outlay.Note == "دفوعات اليوم للاساتذة")
                        {
                            MessageBox.Show("عملية الحذف غير ممكنة");
                        }
                        else
                        {
                            db.Outlays.Remove(outlay);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            Load_Today_Payments();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار مصروف لعملية الحذف");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Teachers_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue)
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Where(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).ToList();
                        TeacherCheck = Outlayes;
                        Outlays.ItemsSource = TeacherCheck;
                        long sum = db.Outlays.Where(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Where(x => x.Note == "دفوعات اليوم للاساتذة").ToList();
                        TeacherCheck = Outlayes;
                        Outlays.ItemsSource = TeacherCheck;
                        long sum = db.Outlays.Where(x => x.Note == "دفوعات اليوم للاساتذة").Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        
        public void students_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue)
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Where(x => (x.Note == "واردات اليوم من الطلاب")&& (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).ToList();
                        StudentCheck = Outlayes;
                        Outlays.ItemsSource = StudentCheck;
                        long sum = db.Outlays.Where(x => (x.Note == "واردات اليوم من الطلاب") && (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).Sum(x=>x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Where(x => x.Note == "واردات اليوم من الطلاب").ToList();
                        StudentCheck = Outlayes;
                        Outlays.ItemsSource = StudentCheck;
                        long sum = db.Outlays.Where(x => x.Note == "واردات اليوم من الطلاب").Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Outlayies_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Start_Date.SelectedDate.HasValue && End_Date.SelectedDate.HasValue)
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => (x.TypyOfOutlayPayment != null) && (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).ToList();
                        Others = Outlayes;
                        Outlays.ItemsSource = Others;
                        long sum = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => (x.TypyOfOutlayPayment != null) && (x.date >= Start_Date.SelectedDate.Value) && (x.date <= End_Date.SelectedDate.Value)).Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        var Outlayes = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null).ToList();
                        Others = Outlayes;
                        Outlays.ItemsSource = Others;
                        long sum = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null).Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Teachers_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                using (var db = new DataBaseContext())
                {
                    if (students.IsChecked == true && Outlayies.IsChecked == true)
                    {
                        var Outlayes = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null || x.Note == "واردات اليوم من الطلاب").ToList();
                        outlays = Outlayes;
                        Outlays.ItemsSource = outlays;
                        long sum = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null || x.Note == "واردات اليوم من الطلاب").Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                    else if (students.IsChecked == true)
                    {
                        students_Checked(this, null);
                    }
                    else if (Outlayies.IsChecked == true)
                    {
                        Outlayies_Checked(this, null);
                    }
                    else
                    {
                        Load_Today_Payments();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void students_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    if (Teachers.IsChecked == true && Outlayies.IsChecked == true)
                    {
                        var Outlayes = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null || x.Note == "دفوعات اليوم للاساتذة").ToList();
                        outlays = Outlayes;
                        Outlays.ItemsSource = outlays;
                        long sum = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment != null || x.Note == "دفوعات اليوم للاساتذة").Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                    else if (Teachers.IsChecked == true)
                    {
                        Teachers_Checked(this, null);

                    }
                    else if (Outlayies.IsChecked == true)
                    {
                        Outlayies_Checked(this, null);

                    }
                    else
                    {
                        Load_Today_Payments();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Outlayies_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    if (Teachers.IsChecked == true && students.IsChecked == true)
                    {
                        var Outlayes = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.Note == "واردات اليوم من الطلاب" || x.Note == "دفوعات اليوم للاساتذة").ToList();
                        outlays = Outlayes;
                        Outlays.ItemsSource = outlays;
                        long sum = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.Note == "واردات اليوم من الطلاب" || x.Note == "دفوعات اليوم للاساتذة").Sum(x => x.Amount);
                        summing.Text = sum.ToString();
                    }
                    else if (Teachers.IsChecked == true)
                    {
                        Teachers_Checked(this, null);

                    }
                    else if (students.IsChecked == true)
                    {
                        students_Checked(this, null);

                    }
                    else
                    {
                        Load_Today_Payments();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OutLayTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedType = OutLayTypes.SelectedItem as TypyOfOutlayPayment;
                using (var db = new DataBaseContext())
                {
                    TypyOfOutlayPayment typyOfOutlayPayment = new TypyOfOutlayPayment();
                    typyOfOutlayPayment = db.TypyOfOutlayPayments.First(x=>x.TypyOfOutlayPayment_Id == selectedType.TypyOfOutlayPayment_Id);
                    var outlies = db.Outlays.Include(x => x.TypyOfOutlayPayment).Where(x => x.TypyOfOutlayPayment == typyOfOutlayPayment).ToList();
                    outlays = outlies;
                    Outlays.ItemsSource = outlays;
                    long sum = outlies.Sum(x => x.Amount);
                    Thewin.Text = sum.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
