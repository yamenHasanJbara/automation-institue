using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
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
using System.Windows.Shapes;

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for UpdateOutlay.xaml
    /// </summary>
    public partial class UpdateOutlay : Window
    {
        public P_show_Outlay p_Show_Outlay { get; set; }
        public int OutLay_id { get; set; }
        public UpdateOutlay(int outlay_Id)
        {
            InitializeComponent();
            this.OutLay_id = outlay_Id;
            loadOutlay();
        }

        private void loadOutlay()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Outlay outlay = new Outlay();
                    outlay = db.Outlays.Include(x => x.TypyOfOutlayPayment).SingleOrDefault(x => x.Outlay_Id == OutLay_id);
                    List<Outlay> outlays = new List<Outlay>();
                    outlays.Add(outlay);
                    OutLayss.ItemsSource = outlays;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Ellipse_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OutLayss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Outlay outlay = new Outlay();
                    outlay = db.Outlays.Include(x => x.TypyOfOutlayPayment).SingleOrDefault(x => x.Outlay_Id == OutLay_id);
                    Amount.Text = outlay.Amount.ToString();
                    Note.Text = outlay.Note;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Amount.Text))
                {
                    MessageBox.Show("الرجاء ادخال المبلغ الجديد");
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Outlay outlay = new Outlay();
                        outlay = db.Outlays.Include(x => x.TypyOfOutlayPayment).SingleOrDefault(x => x.Outlay_Id == OutLay_id);
                        outlay.Amount = int.Parse(Amount.Text);
                        outlay.Note = Note.Text;
                        db.Outlays.Update(outlay);
                        db.SaveChanges();
                        p_Show_Outlay.Load_Today_Payments();
                        this.Close();
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
