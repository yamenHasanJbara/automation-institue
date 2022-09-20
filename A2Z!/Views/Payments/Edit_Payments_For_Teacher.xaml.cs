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
    /// Interaction logic for Edit_Payments_For_Teacher.xaml
    /// </summary>
    public partial class Edit_Payments_For_Teacher : Window
    {

        public List<Payment> payments { get; set; }

        public P_Teacher_Payments p_Teacher_Payments { get; set; }
        public Edit_Payments_For_Teacher(int teacherId,int courseId)
        {
            InitializeComponent();
            Laod_Last_Payment(teacherId, courseId);
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

        private void Laod_Last_Payment(int teacherId, int courseId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Teacher teacher = new Teacher();
                    teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == teacherId);
                    Course course = new Course();
                    course = db.Courses.Include(x => x.material_Study).Include(x => x.Student_Courses).SingleOrDefault(x => x.Course_Id == courseId);
                    var _payments = db.Payments.Include(x => x.course).Include(x => x.teacher).Where(x => (x.course == course) && (x.teacher == teacher) && (x.Payment_Type == 1)).ToList();
                    if (_payments.Count > 0)
                    {
                        List<Payment> payments1 = new List<Payment>();
                        payments1.Add(_payments.Last());
                        payments = payments1;
                        TheTeacherDetails.ItemsSource = payments;
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد شيء للتعديل لأن المدرس لم يدفع له أي قسط بعد");
                    }
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
                var SelectedPayment = TheTeacherDetails.SelectedItem as Payment;
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
                            payment = db.Payments.Include(x => x.course).Include(x => x.teacher).SingleOrDefault(x => x.Payment_Id == SelectedPayment.Payment_Id);
                            course = db.Courses.Include(x => x.teacher).Include(x => x.Student_Courses).SingleOrDefault(x => x.Course_Id == payment.course.Course_Id);
                            int OldAmount = payment.Amount;
                            int NewAmount = int.Parse(Amount.Text);
                            int DifferencePositive = 0;
                            int DifferenceNegative = 0;
                            if (OldAmount > NewAmount)
                            {
                                DifferenceNegative = OldAmount - NewAmount;
                                payment.Amount = NewAmount;
                                int TeacherDeserved = (db.Payments.Include(x => x.course).Where(x => x.course == course && x.Payment_Type == 2).Sum(x => x.Amount) * course.percent) / 100;
                                if (payment.Stayed <= TeacherDeserved)
                                {
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    var teacherPaymentsForUpdatedCourse = db.Payments.Include(x => x.course).Where(x => (x.course == course) && (x.Payment_Type == 1)).Sum(x => x.Amount);
                                    payment.Stayed = TeacherDeserved - teacherPaymentsForUpdatedCourse;
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التعديل بنجاح");
                                    this.p_Teacher_Payments.Load_Teacher_Info1(payment.teacher.Teacher_Id);
                                    this.p_Teacher_Payments.TeacherDetailsPayments_SelectionChanged(this, null);
                                    Amount.Text = null;
                                    Outlay outlay = new Outlay();
                                    bool CheckIfExist = db.Outlays.Any(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date == DateTime.Today));
                                    if (CheckIfExist)
                                    {
                                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date == DateTime.Today));
                                        outlay.Amount -= DifferenceNegative;
                                        db.Outlays.Update(outlay);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        outlay.Amount = -DifferenceNegative;
                                        outlay.date = DateTime.Today;
                                        outlay.Outlay_Type = 2;
                                        outlay.Note = "دفوعات اليوم للاساتذة";
                                        db.Outlays.Add(outlay);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لقد أصبح المبلغ المدفوع أكثر من  مستحق الاستاذ الرجاء التأكد من عملية التعديل");
                                }
                            }
                            else
                            {
                                DifferencePositive = NewAmount - OldAmount;
                                payment.Amount = NewAmount;
                                int TeacherDeserved = (db.Payments.Include(x => x.course).Where(x => x.course == course && x.Payment_Type == 2).Sum(x => x.Amount) * course.percent) / 100;
                                if (payment.Stayed <= TeacherDeserved)
                                {
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    var teacherPaymentsForUpdatedCourse = db.Payments.Include(x => x.course).Where(x => (x.course == course) && (x.Payment_Type == 1)).Sum(x => x.Amount);
                                    payment.Stayed = TeacherDeserved - teacherPaymentsForUpdatedCourse;
                                    db.Payments.Update(payment);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التعديل بنجاح");

                                    this.p_Teacher_Payments.Load_Teacher_Info1(payment.teacher.Teacher_Id);
                                    this.p_Teacher_Payments.TeacherDetailsPayments_SelectionChanged(this, null);
                                    Amount.Text = null;
                                    Outlay outlay = new Outlay();
                                    bool CheckIfExist = db.Outlays.Any(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date == DateTime.Today));
                                    if (CheckIfExist)
                                    {
                                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "دفوعات اليوم للاساتذة") && (x.date == DateTime.Today));
                                        outlay.Amount += DifferencePositive;
                                        db.Outlays.Update(outlay);
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        outlay.Amount = DifferenceNegative;
                                        outlay.date = DateTime.Today;
                                        outlay.Outlay_Type = 1;
                                        outlay.Note = "دفوعات اليوم للاساتذة";
                                        db.Outlays.Add(outlay);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لقد أصبح المبلغ المدفوع أكثر من مستحق الاستاذ الرجاء التأكد من عملية التعديل");
                                }
                            }
                        }
                    }
                }
                else
                { }
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void TheTeacherDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedPayment = TheTeacherDetails.SelectedItem as Payment;
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
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
