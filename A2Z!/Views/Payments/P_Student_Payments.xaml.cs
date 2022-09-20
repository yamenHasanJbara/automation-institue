using A2Z_.Healpers;
using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for P_Student_Payments.xaml
    /// </summary>
    public partial class P_Student_Payments : Page
    {

        public List<Student_course> student_Courses { get; set; }

        public List<Student_course> student_Courses_HaveAmountStayedPayments { get; set; }

        public List<Student> students { get; set; }

        public List<Student> studentsForBinding { get; set; }

        public List<Payment> payments { get; set; }

        public List<ShowAllStudentDetails> ShowAllStudentDetails { get; set; }

        public List<IGrouping<int, Student_course>> student_Courses1 { get; set; }

        public List<Payment> payments1 { get; set; }


        public P_Student_Payments()
        {
            InitializeComponent();
            Load_Student_Info();
            CheckForAdminForUpdateStudentPayment();
        }

        private void CheckForAdminForUpdateStudentPayment()
        {
            try
            {
                Permession permession = new Permession();
                var check = permession.CheckIfAdminOrUser();
                if (check)
                {
                    IsSelection2.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool BuildAmessageBoxWithYesAndNo(int sumAmount)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("هل انت متأكد من المبلغ؟" + " " + sumAmount, "تأكيد الدفع", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Load_Student_Info()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Student> students1 = new List<Student>();
                    var _RegisterdStudent = db.Students.ToList();
                    students = _RegisterdStudent;
                    foreach (var item in students)
                    {
                        // first change to add withdraw button add where condition on _RegisterCourses
                        var _RegisterCourses = db.Student_Courses.Include(x => x.Course_Id_FK).Include(x => x.Student_Id_FK).Where(x => (x.Student_Id == item.Student_Id) && (x.Withdrawn == false)).ToList();
                        student_Courses = _RegisterCourses;
                        foreach (var item1 in student_Courses)
                        {
                            var _payments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => x.course == item1.Course_Id_FK && x.student == item && x.Payment_Type == 2).ToList();
                            payments = _payments;
                            int amountPaid = 0;
                            foreach (var item2 in payments)
                            {
                                amountPaid += item2.Amount;
                            }
                            int amountStayed = item1.Course_Id_FK.Price - amountPaid;
                            if (amountStayed != 0)
                            {
                                if (students1.Contains(item))
                                {
                                    continue;
                                }
                                else
                                {
                                    students1.Add(item);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    studentsForBinding = students1;
                    StudentDetailsPayments.ItemsSource = studentsForBinding;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        public void Load_Student_Info1(int student_Id)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _StudentCourses = db.Student_Courses.Include(x => x.Student_Id_FK).Include(x => x.Course_Id_FK).Where(x => x.Student_Id == student_Id && x.Withdrawn == false).AsEnumerable().GroupBy(x => x.Student_Id).ToList();
                    student_Courses1 = _StudentCourses;
                    List<ShowAllStudentDetails> ShowAllStudentDetails1 = new List<ShowAllStudentDetails>();
                    Course course = new Course();
                    foreach (var item in student_Courses1)
                    {
                        ShowAllStudentDetails showAllStudentDetails = new ShowAllStudentDetails();
                        List<AllStudentCourse> allStudentCourses1 = new List<AllStudentCourse>();
                        foreach (var Student_course in item)
                        {

                            showAllStudentDetails.Student_Id = Student_course.Student_Id;
                            showAllStudentDetails.StudnetName = Student_course.Student_Id_FK.Name;
                            AllStudentCourse allStudentCourse = new AllStudentCourse();
                            course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == Student_course.Course_Id);
                            var _Payments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => x.course == course && x.student == Student_course.Student_Id_FK).ToList();
                            payments1 = _Payments;
                            int paymentspaid = 0;
                            int paymentsStyaed = 0;
                            foreach (var amount in payments1)
                            {
                                paymentspaid += amount.Amount;
                            }
                            paymentsStyaed = course.Price - paymentspaid;
                            if (paymentsStyaed != 0)
                            {
                                allStudentCourse.CourseName = course.material_Study.Name;
                                allStudentCourse.CoursePrice = course.Price;
                                allStudentCourse.CourseGroup = course.Group;
                                allStudentCourse.IsFinished = course.IsFinished;
                                allStudentCourse.AmountPaid = paymentspaid;
                                allStudentCourse.AmountStayed = paymentsStyaed;
                                allStudentCourses1.Add(allStudentCourse);
                            }
                            else
                            { continue; }

                            showAllStudentDetails.allStudentCourses = allStudentCourses1;
                        }
                        ShowAllStudentDetails1.Add(showAllStudentDetails);
                    }

                    ShowAllStudentDetails = ShowAllStudentDetails1;
                    StudentPaymentsAmount.ItemsSource = ShowAllStudentDetails;

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        private void StudentDetailsPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
            if (SelectedStudent != null && CheckBoxForFilter.IsChecked == false)
            {

                RemainingAmountWithStudentName.Text = SelectedStudent.Name;
                IsSelection.IsEnabled = true;
                IsSelection1.IsEnabled = true;
                IsSelection2.IsEnabled = true;
                Load_Student_Info1(SelectedStudent.Student_Id);
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        var _RegisterdCourse = db.Student_Courses.Include(x => x.Course_Id_FK).Include(x => x.Course_Id_FK.material_Study).Where(x => x.Student_Id == SelectedStudent.Student_Id).ToList();
                        student_Courses = _RegisterdCourse;
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        List<Student_course> student_Courses11 = new List<Student_course>();
                        foreach (var item in student_Courses)
                        {
                            var _payments = db.Payments.Include(x => x.student).Include(x => x.course).Where(x => (x.student == student) && (x.course == item.Course_Id_FK) && (x.Payment_Type == 2)).ToList();
                            payments = _payments;
                            int AmountPaid = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaid += item1.Amount;
                            }
                            int Stayed = item.Course_Id_FK.Price - AmountPaid;
                            if (Stayed != 0)
                            {
                                student_Courses11.Add(item);
                            }
                        }
                        student_Courses_HaveAmountStayedPayments = student_Courses11;
                        RegisterCourse.ItemsSource = student_Courses_HaveAmountStayedPayments;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                LoadStuedntWithCheckBox();
            }
        }

        private void LoadStuedntWithCheckBox()
        {
            try
            {
                var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
                if (SelectedStudent != null && CheckBoxForFilter.IsChecked == true)
                {
                    using (var db = new DataBaseContext())
                    {
                        RemainingAmountWithStudentName.Text = SelectedStudent.Name;
                        IsSelection.IsEnabled = true;
                        IsSelection1.IsEnabled = true;
                        IsSelection2.IsEnabled = true;
                        Load_Student_Info1WithCheckBox(SelectedStudent.Student_Id);
                        var _RegisterdCourse = db.Student_Courses.Include(x => x.Course_Id_FK).Include(x => x.Course_Id_FK.material_Study).Where(x => x.Student_Id == SelectedStudent.Student_Id).ToList();
                        student_Courses = _RegisterdCourse;
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        List<Student_course> student_Courses11 = new List<Student_course>();
                        foreach (var item in student_Courses)
                        {
                            var _payments = db.Payments.Include(x => x.student).Include(x => x.course).Where(x => (x.student == student) && (x.course == item.Course_Id_FK) && (x.Payment_Type == 2)).ToList();
                            payments = _payments;
                            int AmountPaid = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaid += item1.Amount;
                            }
                            int Stayed = item.Course_Id_FK.Price - AmountPaid;
                            if (Stayed >= 0)
                            {
                                student_Courses11.Add(item);
                            }
                        }
                        student_Courses_HaveAmountStayedPayments = student_Courses11;
                        RegisterCourse.ItemsSource = student_Courses_HaveAmountStayedPayments;
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

        private void Load_Student_Info1WithCheckBox(int student_Id)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _StudentCourses = db.Student_Courses.Include(x => x.Student_Id_FK).Include(x => x.Course_Id_FK).Where(x => x.Student_Id == student_Id).AsEnumerable().GroupBy(x => x.Student_Id).ToList();
                    student_Courses1 = _StudentCourses;
                    List<ShowAllStudentDetails> ShowAllStudentDetails1 = new List<ShowAllStudentDetails>();
                    Course course = new Course();
                    foreach (var item in student_Courses1)
                    {
                        ShowAllStudentDetails showAllStudentDetails = new ShowAllStudentDetails();
                        List<AllStudentCourse> allStudentCourses1 = new List<AllStudentCourse>();
                        foreach (var Student_course in item)
                        {

                            showAllStudentDetails.Student_Id = Student_course.Student_Id;
                            showAllStudentDetails.StudnetName = Student_course.Student_Id_FK.Name;
                            AllStudentCourse allStudentCourse = new AllStudentCourse();
                            course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == Student_course.Course_Id);
                            var _Payments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => x.course == course && x.student == Student_course.Student_Id_FK).ToList();
                            payments1 = _Payments;
                            int paymentspaid = 0;
                            int paymentsStyaed = 0;
                            foreach (var amount in payments1)
                            {
                                paymentspaid += amount.Amount;
                            }
                            paymentsStyaed = course.Price - paymentspaid;
                            if (paymentsStyaed >= 0)
                            {
                                allStudentCourse.CourseName = course.material_Study.Name;
                                allStudentCourse.CoursePrice = course.Price;
                                allStudentCourse.CourseGroup = course.Group;
                                allStudentCourse.IsFinished = course.IsFinished;
                                allStudentCourse.AmountPaid = paymentspaid;
                                allStudentCourse.AmountStayed = paymentsStyaed;
                                allStudentCourses1.Add(allStudentCourse);
                            }
                            else
                            { continue; }

                            showAllStudentDetails.allStudentCourses = allStudentCourses1;
                        }
                        ShowAllStudentDetails1.Add(showAllStudentDetails);
                    }

                    ShowAllStudentDetails = ShowAllStudentDetails1;
                    StudentPaymentsAmount.ItemsSource = ShowAllStudentDetails;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewPayment(object sender, RoutedEventArgs e)
        {

            var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
            var SelectedCourse = RegisterCourse.SelectedItem as Student_course;
            if (SelectedCourse != null && SelectedStudent != null)
            {
                Course course = new Course();
                Student student = new Student();
                Payment payment = new Payment();
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);

                        student = db.Students.Include(x => x.payments).Include(x => x.Student_Courses).SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);

                        if (String.IsNullOrWhiteSpace(AmountPaied.Text) || String.IsNullOrWhiteSpace(PillNumber.Text) || !IntegerValidation.checkIntValue(AmountPaied.Text))
                        {
                            MessageBox.Show(" الرجاء تعبئة كافة الحقول او التأكد من الرقم المدخل");
                        }
                        else
                        {
                            Permession permession = new Permession();
                            if ((int.Parse(AmountPaied.Text) < 0) && (permession.CheckIfAdminOrUser() == false))
                            {
                                MessageBox.Show("لا يمكن القيام بهذه العملية");
                            }
                            else
                            {
                                payment.Amount = int.Parse(AmountPaied.Text);
                                payment.course = course;
                                payment.student = student;
                                payment.date = DateTime.Today;
                                payment.Payment_Type = 2;
                                payment.PillNumber = PillNumber.Text;
                                int SumAmount = 0;
                                var _Payments = db.Payments.Include(x => x.student).Include(x => x.course).Where(x => x.course.Course_Id == course.Course_Id && x.student.Student_Id == student.Student_Id).ToList();
                                payments = _Payments;
                                foreach (var item in payments)
                                {
                                    SumAmount += item.Amount;
                                }
                                int checkIfThePaidIsBigger = SumAmount + int.Parse(AmountPaied.Text);
                                if (checkIfThePaidIsBigger > course.Price)
                                {
                                    MessageBox.Show("إن هذه القسط أكبر من حجم المبلغ المتبقي الرجاء التأكد من صحة المعلومات");
                                }
                                else
                                {
                                    SumAmount += int.Parse(AmountPaied.Text);
                                    payment.Stayed = course.Price - SumAmount;
                                    bool Check = BuildAmessageBoxWithYesAndNo(int.Parse(AmountPaied.Text));
                                    if (Check)
                                    {
                                        db.Payments.Add(payment);
                                        db.SaveChanges();
                                        InsertIntoOutlay(payment);
                                        RegisterCourse.SelectedItem = null;
                                        AmountPaied.Text = null;
                                        PillNumber.Text = null;
                                        MessageBox.Show("تمت عملية التسديد بنجاح");
                                    }
                                    else
                                    {
                                        RegisterCourse.SelectedItem = null;
                                        AmountPaied.Text = null;
                                        PillNumber.Text = null;
                                    }
                                }
                            }
                        }
                    }
                    Load_Student_Info1(student.Student_Id);
                    Load_Student_Info();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
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
                    bool CheckIfTheStudentDailypaymentsIsExist = db.Outlays.Any(x => (x.Note == "واردات اليوم من الطلاب") & (x.date == dateTime));
                    if (CheckIfTheStudentDailypaymentsIsExist)
                    {
                        outlay = db.Outlays.SingleOrDefault(x => (x.Note == "واردات اليوم من الطلاب") & (x.date == dateTime));
                        outlay.Amount += payment.Amount;
                        db.Outlays.Update(outlay);
                        db.SaveChanges();
                    }
                    else
                    {
                        outlay.Amount = payment.Amount;
                        outlay.date = dateTime;
                        outlay.Outlay_Type = 2;
                        outlay.Note = "واردات اليوم من الطلاب";
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
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
                var selectedCourse = RegisterCourse.SelectedItem as Student_course;
                if (SelectedStudent != null && selectedCourse != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        Student student = new Student();
                        Student_course student_Course = new Student_course();
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == selectedCourse.Course_Id);
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        student_Course = db.Student_Courses.Include(x => x.Student_Id_FK.payments).SingleOrDefault(x => (x.Student_Id == student.Student_Id) && (x.Course_Id == course.Course_Id));
                        var _payments = db.Payments.Include(x => x.student).Include(x => x.course).Where(x => (x.student == student) && (x.course == course) && (x.Payment_Type == 2)).ToList();
                        payments = _payments;
                        int AmountPaid = 0;
                        foreach (var item in payments)
                        {
                            AmountPaid += item.Amount;
                        }
                        if (AmountPaid > 0)
                        {
                            MessageBox.Show("يجب ارجاع المبلغ المدفوع للطالب");
                        }
                        else
                        {
                            db.Student_Courses.Remove(student_Course);
                            db.SaveChanges();
                            foreach (var item in payments)
                            {
                                db.Payments.Remove(item);
                                db.SaveChanges();
                            }
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            course = db.Courses.Include(x => x.teacher).Include(x => x.Student_Courses).SingleOrDefault(x => x.Course_Id == selectedCourse.Course_Id);
                            var teacherTakenAmount = db.Payments.Include(x => x.course).Where(x => x.course == course && x.Payment_Type == 1).Sum(x => x.Amount);
                            var teacherPaymentUpdateList = db.Payments.Include(x => x.course).Where(x => (x.course == course) && (x.Payment_Type == 1)).ToList();
                            if (teacherPaymentUpdateList.Count > 0)
                            {
                                var teacherPaymentUpdate = teacherPaymentUpdateList.Last();
                                teacherPaymentUpdate.Stayed = ((course.Price * course.percent * course.Student_Courses.Count) / 100) - (teacherTakenAmount);
                                db.Payments.Update(teacherPaymentUpdate);
                                db.SaveChanges();
                                Load_Student_Info1(student.Student_Id);
                                Load_Student_Info();
                                RegisterCourse.SelectedItem = null;
                                AmountPaied.Text = null;
                            }
                            else
                            {
                                Load_Student_Info1(student.Student_Id);
                                Load_Student_Info();
                                RegisterCourse.SelectedItem = null;
                                AmountPaied.Text = null;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار الكورس والطالب المراد حذفه من الكورس");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void UpdatePayment(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
                var selectedCourse = RegisterCourse.SelectedItem as Student_course;
                if (SelectedStudent != null && selectedCourse != null)
                {
                    Course course = new Course();
                    Student student = new Student();
                    using (var db = new DataBaseContext())
                    {
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == selectedCourse.Course_Id);
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        Edit_Payments_For_Student edit_Payments_For_Student = new Edit_Payments_For_Student(course.Course_Id, student.Student_Id);
                        edit_Payments_For_Student.p_Student_Payments = this;
                        edit_Payments_For_Student.Show();
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

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                var _SerachrResult = studentsForBinding.Where(x => x.Name.Contains(Search.Text)).ToList();
                students = _SerachrResult;
                StudentDetailsPayments.ItemsSource = students;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBoxForFilter_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Student> students1 = new List<Student>();
                    var _RegisterdStudent = db.Students.ToList();
                    students = _RegisterdStudent;
                    foreach (var item in students)
                    {
                        var _RegisterCourses = db.Student_Courses.Include(x => x.Course_Id_FK).Include(x => x.Student_Id_FK).Where(x => x.Student_Id == item.Student_Id).ToList();
                        student_Courses = _RegisterCourses;
                        foreach (var item1 in student_Courses)
                        {
                            var _payments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => x.course == item1.Course_Id_FK && x.student == item && x.Payment_Type == 2).ToList();
                            payments = _payments;
                            int amountPaid = 0;
                            foreach (var item2 in payments)
                            {
                                amountPaid += item2.Amount;
                            }
                            int amountStayed = item1.Course_Id_FK.Price - amountPaid;
                            if (amountStayed >= 0)
                            {
                                if (students1.Contains(item))
                                {
                                    continue;
                                }
                                else
                                {
                                    students1.Add(item);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    studentsForBinding = students1;
                    StudentDetailsPayments.ItemsSource = studentsForBinding;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBoxForFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Load_Student_Info(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void withdrwan_Click(object sender, RoutedEventArgs e)
        {
            var SelectedStudent = StudentDetailsPayments.SelectedItem as Student;
            var SelectedCourse = RegisterCourse.SelectedItem as Student_course;
            if (SelectedCourse != null && SelectedStudent != null)
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        var _regigsterCourse = db.Student_Courses.SingleOrDefault(x => x.Student_Course_Id == SelectedCourse.Student_Course_Id);
                        _regigsterCourse.Withdrawn = true;
                        db.Student_Courses.Update(_regigsterCourse);
                        db.SaveChanges();
                        MessageBox.Show("تم تغيير وضع الطالب إلى منسحب");
                        RegisterCourse.SelectedItem = null;
                        Load_Student_Info();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("الرجاء اختيار الكورس التي تود سحب الطالب منه");
            }
        }
    }
}
