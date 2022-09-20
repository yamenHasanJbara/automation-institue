using A2Z_.Healpers;
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
using System.Windows.Shapes;

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for Edit_Payments_For_Student.xaml
    /// </summary>
    public partial class Edit_Payments_For_Student : Window
    {

        public List<Payment> payments1 { get; set; }

        public P_Student_Payments p_Student_Payments { get; set; }

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

        public Edit_Payments_For_Student(int CourseId, int StudentId)
        {
            InitializeComponent();
            Load_Payments_Information(CourseId, StudentId);
        }

        private void Load_Payments_Information(int CourseId, int StudentId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    Student student = new Student();
                    Payment payment = new Payment();
                    course = db.Courses.SingleOrDefault(x => x.Course_Id == CourseId);
                    student = db.Students.SingleOrDefault(x => x.Student_Id == StudentId);
                    List<Payment> payments = new List<Payment>();
                    var _testing = db.Payments.Include(x => x.course).Include(x => x.course.material_Study).Include(x => x.student).Where(x => (x.student == student) & (x.course == course) & (x.Payment_Type == 2)).ToList();
                    payment = _testing.Last();
                    payments.Add(payment);
                    payments1 = payments;
                    TheStudentDetails.ItemsSource = payments;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void TheStudentDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedPayment = TheStudentDetails.SelectedItem as Payment;
                if (SelectedPayment != null)
                {
                    IsSelection.IsEnabled = true;
                    Payment payment = new Payment();
                    using (var db = new DataBaseContext())
                    {
                        payment = db.Payments.Include(x => x.course).Include(x => x.student).SingleOrDefault(x => x.Payment_Id == SelectedPayment.Payment_Id);
                        Amount.Text = payment.Amount.ToString();
                    }
                }
                else
                {  }
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
                var SelectedPayment = TheStudentDetails.SelectedItem as Payment;
                if (SelectedPayment != null)
                {
                    Payment payment = new Payment();
                    Course course = new Course();
                    using (var db = new DataBaseContext())
                    {
                        if (!IntegerValidation.checkIntValue(Amount.Text))
                        {
                            MessageBox.Show("الرجاء التأكد من صحة الرقم المدخل");
                        }
                        else
                        {
                            payment = db.Payments.Include(x => x.course).Include(x => x.student).SingleOrDefault(x => x.Payment_Id == SelectedPayment.Payment_Id);
                            course = db.Courses.SingleOrDefault(x => x.Course_Id == payment.course.Course_Id);
                            int OldAmount = payment.Amount;
                            int NewAmount = int.Parse(Amount.Text);
                            int DifferencePositive = 0;
                            int DifferenceNegative = 0;
                            if (OldAmount > NewAmount)
                            {
                                DifferenceNegative = OldAmount - NewAmount;
                                payment.Amount = NewAmount;
                                payment.Stayed += DifferenceNegative;
                                if (payment.Stayed <= course.Price && payment.Stayed >= 0)
                                {
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التعديل بنجاح");
                                    Amount.Text = null;
                                    Outlay outlay = new Outlay();
                                    bool CheckIfExist = db.Outlays.Any(x => (x.Note == "واردات اليوم من الطلاب") && (x.date == DateTime.Today));
                                    if (CheckIfExist)
                                    {
                                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "واردات اليوم من الطلاب") && (x.date == DateTime.Today));
                                        outlay.Amount -= DifferenceNegative;
                                        db.Outlays.Update(outlay);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        outlay.Amount = -DifferenceNegative;
                                        outlay.date = DateTime.Today;
                                        outlay.Outlay_Type = 1;
                                        outlay.Note = "واردات اليوم من الطلاب";
                                        db.Outlays.Add(outlay);
                                        db.SaveChanges();
                                    }

                                    p_Student_Payments.Load_Student_Info1(payment.student.Student_Id);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("لقد أصبح المبلغ المدفوع أكبر من سعر الكورس الرجاء التأكد من عملية التعديل");
                                }
                            }
                            else
                            {
                                DifferencePositive = NewAmount - OldAmount;
                                payment.Amount = NewAmount;
                                payment.Stayed -= DifferencePositive;
                                if (payment.Stayed <= course.Price && payment.Stayed >= 0)
                                {
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التعديل بنجاح");
                                    Amount.Text = null;
                                    Outlay outlay = new Outlay();
                                    bool CheckIfExist = db.Outlays.Any(x => (x.Note == "واردات اليوم من الطلاب") && (x.date == DateTime.Today));
                                    if (CheckIfExist)
                                    {
                                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "واردات اليوم من الطلاب") && (x.date == DateTime.Today));
                                        outlay.Amount += DifferencePositive;
                                        db.Outlays.Update(outlay);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        outlay.Amount = DifferenceNegative;
                                        outlay.date = DateTime.Today;
                                        outlay.Outlay_Type = 2;
                                        outlay.Note = "واردات اليوم من الطلاب";
                                        db.Outlays.Add(outlay);
                                        db.SaveChanges();
                                    }
                                    p_Student_Payments.Load_Student_Info1(payment.student.Student_Id);
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("لقد أصبح المبلغ المدفوع أكبر من سعر الكورس الرجاء التأكد من عملية التعديل");
                                }
                            }
                        }
                    }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
