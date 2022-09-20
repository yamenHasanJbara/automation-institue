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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_Show_Daily_Payments.xaml
    /// </summary>
    public partial class P_Show_Daily_Payments : Page
    {

        public List<Payment> payments { get; set; }

        public List<PaymentsForShowInAdmin> paymentsForShowInAdmins { get; set; }

        public P_Show_Daily_Payments()
        {
            InitializeComponent();
            LoadDatilyPayments();
        }

        public void LoadDatilyPayments()
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                using (var db = new DataBaseContext())
                {
                    var _payments = db.Payments.Include(x => x.student).Include(x => x.teacher).Include(x => x.course).Include(x => x.course.material_Study).Where(x => x.date == dateTime).ToList();
                    payments = _payments;
                    List<PaymentsForShowInAdmin> PaymentsForShow = new List<PaymentsForShowInAdmin>();
                    foreach (var item in payments)
                    {
                        if (item.Amount > 0)
                        {
                            PaymentsForShowInAdmin payment = new PaymentsForShowInAdmin();
                            payment.Payment_Id = item.Payment_Id;
                            payment.teacher = item.teacher;
                            payment.student = item.student;
                            payment.date = item.date;
                            payment.course = item.course;
                            payment.Amount = item.Amount;
                            payment.Stayed = item.Stayed;
                            payment.PillNumber = item.PillNumber;
                            if (item.Payment_Type == 1)
                            {
                                payment.Payment_Type = "دفع";
                            }
                            else
                            {
                                payment.Payment_Type = "قبض";
                            }
                            PaymentsForShow.Add(payment);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    paymentsForShowInAdmins = PaymentsForShow;
                    PaymentsDetail.ItemsSource = paymentsForShowInAdmins;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void show(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Start_Date.SelectedDate != null && End_Date.SelectedDate != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        var paymentsFilter = db.Payments.Include(x=>x.teacher).Include(x=>x.student).Include(x=>x.course).Include(x => x.course.material_Study).Where(x => x.date >= Start_Date.SelectedDate.Value && x.date <= End_Date.SelectedDate.Value).ToList();
                        payments = paymentsFilter;
                        List<PaymentsForShowInAdmin> PaymentsForShow = new List<PaymentsForShowInAdmin>();
                        foreach (var item in payments)
                        {
                            if (item.Amount > 0)
                            {
                                PaymentsForShowInAdmin payment = new PaymentsForShowInAdmin();
                                payment.Payment_Id = item.Payment_Id;
                                payment.teacher = item.teacher;
                                payment.student = item.student;
                                payment.date = item.date;
                                payment.course = item.course;
                                payment.Amount = item.Amount;
                                payment.Stayed = item.Stayed;
                                payment.PillNumber = item.PillNumber;
                                if (item.Payment_Type == 1)
                                {
                                    payment.Payment_Type = "دفع";
                                }
                                else
                                {
                                    payment.Payment_Type = "قبض";
                                }
                                PaymentsForShow.Add(payment);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        paymentsForShowInAdmins = PaymentsForShow;
                        PaymentsDetail.ItemsSource = paymentsForShowInAdmins;
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار تاريخ بداية ونهاية");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPayment = PaymentsDetail.SelectedItem as PaymentsForShowInAdmin;
                if (selectedPayment != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Payment payment = new Payment();
                        payment = db.Payments.FirstOrDefault(x => x.Payment_Id == selectedPayment.Payment_Id);
                        DateTime dateTime = new DateTime();
                        dateTime = payment.date;
                        int paymentAmount = payment.Amount;
                        if (payment.Payment_Type == 1)
                        {
                            db.Payments.Remove(payment);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            Outlay outlay = new Outlay();
                            outlay = db.Outlays.FirstOrDefault(x => x.date == dateTime && x.Note == "دفوعات اليوم للاساتذة");
                            if (outlay != null)
                            {
                                outlay.Amount -= paymentAmount;
                                db.Outlays.Update(outlay);
                                db.SaveChanges();
                            }
                            LoadDatilyPayments();
                        }
                        else
                        {
                            db.Payments.Remove(payment);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            Outlay outlay = new Outlay();
                            outlay = db.Outlays.FirstOrDefault(x => x.date == dateTime && x.Note == "واردات اليوم من الطلاب");
                            if (outlay != null)
                            {
                                outlay.Amount -= paymentAmount;
                                db.Outlays.Update(outlay);
                                db.SaveChanges();
                            }
                            LoadDatilyPayments();
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
