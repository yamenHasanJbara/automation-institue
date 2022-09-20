using A2Z_.Healpers;
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

namespace A2Z_.Views.Payments
{
    /// <summary>
    /// Interaction logic for P_Teacher_Payments.xaml
    /// </summary>
    public partial class P_Teacher_Payments : Page
    {
        public List<Teacher> teachers { get; set; }

        public List<Teacher> teachersForBinding { get; set; }

        public List<Course> courses { get; set; }

        public List<Course> coursesNotPaied { get; set; }

        public List<Payment> payments { get; set; }

        public List<ShowAllStudentDetails> ShowAllStudentDetails { get; set; }

        public List<IGrouping<Teacher, Course>> TeacherCourses { get; set; }

        public P_Teacher_Payments()
        {
            InitializeComponent();
            Load_Teacher();
        }

        private bool BuilbAnewMessageBox(int AmountPaid)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("هل انت متأكد من المبلغ؟" + " " + AmountPaid, "تأكيد الدفع", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Load_Teacher()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Teacher> teachers1 = new List<Teacher>();
                    var _RegisterdStudent = db.Teachers.ToList();
                    teachers = _RegisterdStudent;
                    foreach (var item in teachers)
                    {
                        var _TeacherCourses = db.Courses.Include(x => x.teacher).Include(x => x.Student_Courses).Where(x => x.teacher == item).ToList();
                        courses = _TeacherCourses;
                        foreach (var item1 in courses)
                        {
                            var _payments = db.Payments.Include(x => x.course).Include(x => x.teacher).Where(x => x.course == item1 && x.teacher == item && x.Payment_Type == 1).ToList();
                            payments = _payments;
                            int amountPaid = 0;
                            foreach (var item2 in payments)
                            {
                                amountPaid += item2.Amount;
                            }
                            int studentPayments = db.Payments.Include(x => x.course).Where(x => x.course == item1 && x.Payment_Type == 2).Sum(x => x.Amount);
                            int amountStayed = ( studentPayments * item1.percent  / 100) - amountPaid;
                            if (amountStayed != 0)
                            {
                                if (teachers1.Contains(item))
                                {
                                    continue;
                                }
                                else
                                {
                                    teachers1.Add(item);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    teachersForBinding = teachers1;
                    TeacherDetailsPayments.ItemsSource = teachersForBinding;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void TeacherDetailsPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedTeacher = TeacherDetailsPayments.SelectedItem as Teacher;
                if (SelectedTeacher != null)
                {
                    RemainingAmountWithTeacherName.Text = SelectedTeacher.Name;
                    IsSelection.IsEnabled = true;
                    IsSelection1.IsEnabled = true;
                    Load_Teacher_Info1(SelectedTeacher.Teacher_Id);
                    using (var db = new DataBaseContext())
                    {
                        List<Course> courses1 = new List<Course>();
                        Teacher teacher = new Teacher();
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        var _TeacherCourses = db.Courses.Include(x => x.teacher).Include(x => x.material_Study).Include(x=>x.Student_Courses).Where(x => (x.teacher == teacher ) ).ToList();
                        courses = _TeacherCourses;
                        long SumCourse = 0;
                        long SumPayment = 0;
                        long SumStayed = 0;
                        foreach (var item in courses)
                        {
                            int studentPayments = db.Payments.Include(x => x.course).Where(x => x.course == item && x.Payment_Type == 2).Sum(x => x.Amount);
                            int teacherPayment = db.Payments.Include(x => x.course).Where(x => x.course == item && x.Payment_Type == 1).Sum(x => x.Amount);
                            int teacherDeserved = (studentPayments * item.percent) / 100;
                            int teacherStayed = teacherDeserved - teacherPayment;
                            if (teacherStayed > 0)
                            {
                                SumCourse += teacherDeserved;
                                SumPayment += teacherPayment;
                                SumStayed += teacherStayed;
                                courses1.Add(item);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        coursesNotPaied = courses1;
                        DeservedAmount.Content = SumCourse;
                        SumStayedAmount.Content = SumPayment;
                        SumPayedAmount.Content = SumStayed;
                        Teacher_Courses.ItemsSource = coursesNotPaied;
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

        public void Load_Teacher_Info1(int TeacherId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Teacher teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == TeacherId);
                    var _Course = db.Courses.Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.teacher == teacher).AsEnumerable().GroupBy(x => x.teacher).ToList();
                    TeacherCourses = _Course;
                    List<ShowAllStudentDetails> ShowAllStudentDetails1 = new List<ShowAllStudentDetails>();
                    foreach (var item in TeacherCourses)
                    {
                        ShowAllStudentDetails showAllStudent = new ShowAllStudentDetails();
                        showAllStudent.Student_Id = item.Key.Teacher_Id;
                        showAllStudent.StudnetName = item.Key.Name;
                        List<AllStudentCourse> allStudentCourses = new List<AllStudentCourse>();
                        foreach (var Course in item)
                        {
                            AllStudentCourse allStudentCourse = new AllStudentCourse();
                            var _paymentsForThisCourse = db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => (x.teacher == teacher) && (x.course == Course) && (x.Payment_Type == 1)).ToList();
                            payments = _paymentsForThisCourse;
                            int AmountPaid = 0;
                            int AmountStaid = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaid += item1.Amount;
                            }
                            int ActualDeservedPayment = (db.Payments.Include(x => x.course).Where(x => x.course == Course && x.Payment_Type == 2).Sum(x => x.Amount) * Course.percent) /100;
                            //int studentPayment = db.Payments.Include(x => x.course).Where(x=> x.course==Course && x.Payment_Type==2).Sum(x=>x.Amount);
                            AmountStaid = ActualDeservedPayment - AmountPaid;
                            if (AmountStaid != 0)
                            {
                                allStudentCourse.ActualDeserved = ActualDeservedPayment ;
                                allStudentCourse.AmountPaid = AmountPaid;
                                allStudentCourse.AmountStayed = AmountStaid;
                                allStudentCourse.CourseName = Course.material_Study.Name;
                                allStudentCourse.CourseGroup = Course.Group;
                                allStudentCourse.IsFinished = Course.IsFinished;
                                allStudentCourse.CoursePrice = (Course.Price * Course.Student_Courses.Count * Course.percent) / 100;
                                allStudentCourses.Add(allStudentCourse);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        showAllStudent.allStudentCourses = allStudentCourses;
                        ShowAllStudentDetails1.Add(showAllStudent);
                    }
                    ShowAllStudentDetails = ShowAllStudentDetails1;
                    TeacherPaymentsAmount.ItemsSource = ShowAllStudentDetails;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void NewPayment(object sender, RoutedEventArgs e)
        {

            var SelectedTeacher = TeacherDetailsPayments.SelectedItem as Teacher;
            var SelectedCourse = Teacher_Courses.SelectedItem as Course;
            try
            {
                if ((SelectedTeacher != null) && (SelectedCourse != null))
                {
                    Course course = new Course();
                    Teacher teacher = new Teacher();
                    Payment payment = new Payment();
                    using (var db = new DataBaseContext())
                    {
                        course = db.Courses.Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        teacher = db.Teachers.Include(x => x.courses).SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        if (course.Student_Courses.Count == 0)
                        {
                            MessageBox.Show("لايوجد مبلغ مستحق بسبب عدم وجود طلاب مسجلة");
                            AmountPaied.Text = null;
                            RemainingAmountWithTeacherName.Text = null;
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(AmountPaied.Text) || String.IsNullOrWhiteSpace(PillNumber.Text) || (!IntegerValidation.checkIntValue(AmountPaied.Text)))
                            {
                                MessageBox.Show("الرجاء ادخال كافة الحقول او التأكد من صحة الرقم المدخل");
                            }
                            else
                            {
                                var _TeacherPayments = db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => (x.course == course) && (x.teacher == teacher) && (x.Payment_Type == 1)).ToList();
                                payments = _TeacherPayments;
                                int AmountPaid = 0;
                                foreach (var item in payments)
                                {
                                    AmountPaid += item.Amount;
                                }
                                int DeservedAmountToTeacher = (db.Payments.Include(x => x.course).Where(x => x.course == course && x.Payment_Type == 2).Sum(x => x.Amount) * course.percent) / 100;
                                int NewPaymentAmount = int.Parse(AmountPaied.Text);
                                int AmountPaidPlusNewPaymentAmount = NewPaymentAmount + AmountPaid;
                                if (AmountPaidPlusNewPaymentAmount > DeservedAmountToTeacher)
                                {
                                    MessageBox.Show("إن القمية المدخلة حاليا مع الدفعات السابقة اصبحت اكبر من مستحقات الاستاذ");
                                }
                                else
                                {
                                    payment.Amount = int.Parse(AmountPaied.Text);
                                    payment.date = DateTime.Today;
                                    payment.Payment_Type = 1;
                                    payment.teacher = teacher;
                                    payment.Stayed = DeservedAmountToTeacher - AmountPaidPlusNewPaymentAmount;
                                    payment.course = course;
                                    payment.PillNumber = PillNumber.Text;
                                    bool Check = BuilbAnewMessageBox(int.Parse(AmountPaied.Text));
                                    if (Check)
                                    {
                                        db.Payments.Add(payment);
                                        db.SaveChanges();
                                        Load_Teacher_Info1(teacher.Teacher_Id);
                                        InsertIntoOutlay(payment);
                                        Load_Teacher();
                                        Teacher_Courses.SelectedItem = null;
                                        AmountPaied.Text = null;
                                        RemainingAmountWithTeacherName.Text = null;
                                        PillNumber.Text = null;
                                        MessageBox.Show("تمت عملية التسديد بنجاح");
                                        //test 
                                        List<Course> courses1 = new List<Course>();
                                        var _TeacherCourses = db.Courses.Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => (x.teacher == teacher)).ToList();
                                        courses = _TeacherCourses;
                                        long SumCourse = 0;
                                        long SumPayment = 0;
                                        long SumStayed = 0;
                                        foreach (var item in courses)
                                        {
                                            int studentPayments = db.Payments.Include(x => x.course).Where(x => x.course == item && x.Payment_Type == 2).Sum(x => x.Amount);
                                            int teacherPayment = db.Payments.Include(x => x.course).Where(x => x.course == item && x.Payment_Type == 1).Sum(x => x.Amount);
                                            int teacherDeserved = (studentPayments * item.percent) / 100;
                                            int teacherStayed = teacherDeserved - teacherPayment;
                                            if (teacherStayed > 0)
                                            {
                                                SumCourse += teacherDeserved;
                                                SumPayment += teacherPayment;
                                                SumStayed += teacherStayed;
                                                courses1.Add(item);
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        coursesNotPaied = courses1;
                                        DeservedAmount.Content = SumCourse;
                                        SumStayedAmount.Content = SumPayment;
                                        SumPayedAmount.Content = SumStayed;
                                        Teacher_Courses.ItemsSource = coursesNotPaied;
                                    }
                                    else
                                    {
                                        RemainingAmountWithTeacherName.Text = null;
                                        PillNumber.Text = null;
                                    }
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

        private void InsertIntoOutlay(Payment payment)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Outlay outlay = new Outlay();
                    DateTime dateTime = DateTime.Today;
                    bool CheckIfTheStudentDailypaymentsIsExist = db.Outlays.Any(x => (x.Note == "دفوعات اليوم للاساتذة") & (x.date == dateTime));
                    if (CheckIfTheStudentDailypaymentsIsExist)
                    {
                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "دفوعات اليوم للاساتذة") & (x.date == dateTime));
                        outlay.Amount += payment.Amount;
                        db.Outlays.Update(outlay);
                        db.SaveChanges();
                    }
                    else
                    {
                        outlay.Amount = payment.Amount;
                        outlay.date = dateTime;
                        outlay.Outlay_Type = 1;
                        outlay.Note = "دفوعات اليوم للاساتذة";
                        db.Outlays.Add(outlay);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Teacher_Courses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedCourse = Teacher_Courses.SelectedItem as Course;
            var SelectedTeacher = TeacherDetailsPayments.SelectedItem as Teacher;
            try
            {
                if ((SelectedCourse != null)&&(SelectedTeacher != null))
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        Teacher teacher = new Teacher();
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        RemainingAmountWithTeacherName.Text = teacher.Name + "   " + course.Group;
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

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var _teacher = teachersForBinding.Where(x => x.Name.Contains(Search.Text)).ToList();
                teachers = _teacher;
                TeacherDetailsPayments.ItemsSource = teachers;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void EditPayment(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedTeacher = TeacherDetailsPayments.SelectedItem as Teacher;
                var SelectedCourse = Teacher_Courses.SelectedItem as Course;
                if (SelectedCourse != null &&  SelectedTeacher != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Teacher teacher = new Teacher();
                        Course course = new Course();
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        course = db.Courses.Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        Edit_Payments_For_Teacher edit_Payments_For_Teacher = new Edit_Payments_For_Teacher(teacher.Teacher_Id, course.Course_Id);
                        edit_Payments_For_Teacher.p_Teacher_Payments = this;
                        
                        if (edit_Payments_For_Teacher.payments != null)
                        {
                            edit_Payments_For_Teacher.Show();
                        }
                        else
                        {  }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار مدرس وكورس");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
