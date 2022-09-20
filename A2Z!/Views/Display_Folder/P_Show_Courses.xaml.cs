using A2Z_.Healpers;
using A2Z_.Models;
using MaterialDesignThemes.Wpf.Converters.CircularProgressBar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Win32;
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
    /// Interaction logic for P_Show_Courses.xaml
    /// </summary>
    /// 
    public partial class P_Show_Courses : Page
    {

        public List<Course> courses { get; set; }

        public List<Teacher> teachers { get; set; }

        public List<Student_course> student_Courses { get; set; }

        public List<Payment> paymentsStudent { get; set; }

        public List<Payment> paymentsTeacher { get; set; }

        public List<IGrouping<Student,Payment>> studentPayments { get; set; }

        public List<Term> terms { get; set; }

        public P_Show_Courses()
        {
            InitializeComponent();
            Load_Courses();
        }

        private bool BuildAmessageBoxWithYesAndNo(string CouresName)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("هل انت متأكد من حذف الدورة؟" + " " + CouresName, "تأكيد حذف دورة", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Load_Courses()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Courses = db.Courses.Include(x=>x.section).Include(x=>x.faculty).Include(x=>x.Year).Include(x=>x.teacher).Include(x=>x.material_Study).Include(x=>x.Student_Courses).Where(x=>x.IsFinished == false).ToList();
                    courses = _Courses;
                    CourseDetails.ItemsSource = courses;
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CourseDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedCourse = CourseDetails.SelectedItem as Course;
            if (SelectedCourse!=null)
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        var _Teachers = db.Teachers.ToList();
                        teachers = _Teachers;
                        TeachersName.ItemsSource = teachers;
                        if (course.term != null)
                        {
                            var _terms = db.terms.ToList();
                            terms = _terms;
                            Terms.ItemsSource = terms;
                            Start_Date.Visibility = Visibility.Collapsed;
                            End_Date.Visibility = Visibility.Collapsed;
                            Terms.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Terms.Visibility = Visibility.Collapsed;
                            Start_Date.Visibility = Visibility.Visible;
                            End_Date.Visibility = Visibility.Visible;
                        }
                        Permession permession = new Permession();
                        bool Check = permession.CheckIfAdminOrUser();
                        if (Check)
                        {
                            IsSelection1.IsEnabled = true;
                            Percent.Visibility = Visibility.Visible;
                            Price.Visibility = Visibility.Visible;
                            Percent.Text = course.percent.ToString();
                            Group.Text = course.Group;
                            Price.Text = course.Price.ToString();
                            Start_Date.SelectedDate = course.Start_Date;
                            End_Date.SelectedDate = course.End_Date;
                            TeachersName.SelectedItem = course.teacher;
                            Terms.SelectedItem = course.term;
                            IsSelection.IsEnabled = true;
                            IsSelection1.IsEnabled = true;
                            var SelectedTeacher = TeachersName.SelectedItem as Teacher;
                        }
                        else
                        {
                            Group.Text = course.Group;
                            Terms.SelectedItem = course.term;
                            Start_Date.SelectedDate = course.Start_Date;
                            End_Date.SelectedDate = course.End_Date;
                            TeachersName.SelectedItem = course.teacher;
                            IsSelection.IsEnabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            { }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var SelectedCourse = CourseDetails.SelectedItem as Course;
            var SelectedTeacher = TeachersName.SelectedItem as Teacher;
            var selectedTerm = Terms.SelectedItem as Term;
            try
            {
                if (SelectedCourse != null)
                {
                    Course course = new Course();
                    Teacher teacher = new Teacher();
                    Term term = new Term();
                    using (var db = new DataBaseContext())
                    {
                        course = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term).SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        //term = db.terms.SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                        Permession permession = new Permession();
                        bool CheckIfAdmin = permession.CheckIfAdminOrUser();
                        if (CheckIfAdmin)
                        {
                            // here is a course for university
                            if (course.faculty != null)
                            {
                                term = db.terms.SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                                if (String.IsNullOrWhiteSpace(Price.Text) || String.IsNullOrWhiteSpace(Group.Text) || String.IsNullOrWhiteSpace(Percent.Text) || TeachersName.SelectedItem == null || Terms.SelectedItem == null || !IntegerValidation.checkIntValue(Price.Text) || !IntegerValidation.checkIntValue(Percent.Text))
                                {
                                    MessageBox.Show("الرجاء تعبئة كافة الحقول او التأكد من صحة حقل السعر والنسبة");
                                }
                                else
                                {
                                    string CourseGroup = Group.Text;
                                    bool CheckIfUpdatedValueIsExist = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term)
                                    .Any(x =>
                                    (x.teacher == teacher)
                                    && (x.Group.Contains(Group.Text))
                                    && (x.Course_Id == SelectedCourse.Course_Id)
                                    && (x.Price == int.Parse(Price.Text))
                                    && (x.percent == int.Parse(Percent.Text))
                                    && (x.term == selectedTerm)
                                    && (x.IsFinished == false));

                                    if (CheckIfUpdatedValueIsExist)
                                    {
                                        MessageBox.Show("إن الدورة موجودة مسبقا");
                                    }
                                    else
                                    {
                                        course.teacher = teacher;
                                        course.percent = int.Parse(Percent.Text);
                                        course.Group = CourseGroup;
                                        course.term = term;
                                        course.Start_Date = term.start_date;
                                        course.End_Date = term.end_date;
                                        course.Price = int.Parse(Price.Text);
                                        db.Courses.Update(course);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية التحديث بنجاح");
                                        UpdateCoursePaymentsForStudentAndTeacher(course.Course_Id);
                                        TeachersName.SelectedItem = null;
                                        Group.Text = null;
                                        Price.Text = null;
                                        Percent.Text = null;
                                        Terms.SelectedItem = null;
                                        Load_Courses();
                                    }
                                }
                            }
                            else
                            {
                                // here is language course or program course for admin update course
                                string CourseGroup = Group.Text;
                                bool CheckIfUpdatedValueIsExist = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term)
                                .Any(x =>
                                (x.teacher == teacher)
                                && (x.Group.Contains(Group.Text))
                                && (x.Course_Id == SelectedCourse.Course_Id)
                                && (x.Price == int.Parse(Price.Text))
                                && (x.percent == int.Parse(Percent.Text))
                                && (x.Start_Date == Start_Date.SelectedDate.Value)
                                && (x.End_Date == End_Date.SelectedDate.Value)
                                && (x.IsFinished == false));

                                if (CheckIfUpdatedValueIsExist)
                                {
                                    MessageBox.Show("إن الدورة موجودة مسبقا");
                                }
                                else
                                {
                                    course.teacher = teacher;
                                    course.percent = int.Parse(Percent.Text);
                                    course.Group = CourseGroup;
                                    course.Start_Date = Start_Date.SelectedDate.Value;
                                    course.End_Date = End_Date.SelectedDate.Value;
                                    course.Price = int.Parse(Price.Text);
                                    db.Courses.Update(course);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية التحديث بنجاح");
                                    UpdateCoursePaymentsForStudentAndTeacher(course.Course_Id);
                                    TeachersName.SelectedItem = null;
                                    Group.Text = null;
                                    Price.Text = null;
                                    Percent.Text = null;
                                    Start_Date.SelectedDate = null;
                                    End_Date.SelectedDate = null;
                                    Load_Courses();
                                }
                            }

                        }
                        // here the update make by employee not admin
                        else
                        {
                            if (String.IsNullOrWhiteSpace(Group.Text) || (Start_Date.SelectedDate == null) || (End_Date.SelectedDate == null) || (TeachersName.SelectedItem == null))
                            {
                                MessageBox.Show("الرجاء تعبئة الحقول");
                            }
                            else
                            {
                                if (course.term != null)
                                {
                                    term = db.terms.SingleOrDefault(x => x.Terms_id == selectedTerm.Terms_id);
                                    string CourseGroup = Group.Text;
                                    bool CheckIfUpdatedValueIsExist = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term)
                                        .Any(x =>
                                        (x.teacher == teacher)
                                        && (x.Group.Contains(Group.Text))
                                        && (x.Course_Id == SelectedCourse.Course_Id)
                                        && (x.term == term)
                                        && (x.IsFinished == false));
                                    if (CheckIfUpdatedValueIsExist)
                                    {
                                        MessageBox.Show("ان الدورة موجودة مسبقاً");
                                    }
                                    else
                                    {
                                        course.teacher = teacher;
                                        course.Group = CourseGroup;
                                        course.term = term;
                                        course.Start_Date = term.start_date;
                                        course.End_Date = term.end_date;
                                        db.Courses.Update(course);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية التحديث بنجاح");
                                        Terms.SelectedItem = null;
                                        TeachersName.SelectedItem = null;
                                        Group.Text = null;
                                        Load_Courses();
                                    }
                                }
                                else
                                {
                                    // here for update non university course
                                    string CourseGroup = Group.Text;
                                    bool CheckIfUpdatedValueIsExist = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.term)
                                        .Any(x =>
                                        (x.teacher == teacher)
                                        && (x.Group.Contains(Group.Text))
                                        && (x.Course_Id == SelectedCourse.Course_Id)
                                        && (x.Start_Date == Start_Date.SelectedDate.Value)
                                        && (x.End_Date == End_Date.SelectedDate.Value)
                                        && (x.IsFinished == false));
                                    if (CheckIfUpdatedValueIsExist)
                                    {
                                        MessageBox.Show("ان الدورة موجودة مسبقاً");
                                    }
                                    else
                                    {
                                        course.teacher = teacher;
                                        course.Group = CourseGroup;
                                        course.Start_Date = Start_Date.SelectedDate.Value;
                                        course.End_Date = End_Date.SelectedDate.Value;
                                        db.Courses.Update(course);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية التحديث بنجاح");
                                        Start_Date.SelectedDate = null;
                                        End_Date.SelectedDate = null;
                                        TeachersName.SelectedItem = null;
                                        Group.Text = null;
                                        Load_Courses();
                                    }
                                }
                            }
                        }
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

        // when update course price it will update the student payments and the teacher payments
        private void UpdateCoursePaymentsForStudentAndTeacher(int course_Id)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    course = db.Courses.Include(x => x.Student_Courses).SingleOrDefault(x=>x.Course_Id == course_Id);
                    var _studentPayments = db.Payments.Include(x => x.student).Include(x => x.course).Include(x=>x.course.Student_Courses).Where(x => (x.course == course) && (x.Payment_Type == 2)).ToList();
                    var _groupintStudentPaymnets = _studentPayments.GroupBy(x => x.student).ToList();
                    studentPayments = _groupintStudentPaymnets;
                    foreach (var item in studentPayments)
                    {
                        int AmountPaid = 0;
                        foreach (var Payment in item)
                        {
                            AmountPaid += Payment.Amount;
                            Payment.Stayed = (Payment.course.Price - AmountPaid);
                            db.Payments.Update(Payment);
                            db.SaveChanges();
                        }
                    }
                    var _teacherPayments = db.Payments.Include(x => x.teacher).Include(x => x.course).Include(x=>x.course.Student_Courses).Where(x => (x.course == course) && (x.Payment_Type ==1)).ToList();
                    paymentsTeacher = _teacherPayments;
                    int AmountTeacherPayment = 0;
                    foreach (var item in paymentsTeacher)
                    {
                        AmountTeacherPayment += item.Amount;
                        item.Stayed = (item.course.percent * item.course.Price * item.course.Student_Courses.Count / 100) - AmountTeacherPayment;
                        db.Payments.Update(item);
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
            var SelectedCourseForDelete = CourseDetails.SelectedItem as Course;
            try
            {
                if (SelectedCourseForDelete != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        course = db.Courses.Include(x => x.Student_Courses).Include(x => x.payments).Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == SelectedCourseForDelete.Course_Id);
                        bool ResultFromMessageBox = BuildAmessageBoxWithYesAndNo(course.material_Study.Name);
                        if (ResultFromMessageBox)
                        {
                            db.Courses.Remove(course);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            TeachersName.SelectedItem = null;
                            Group.Text = null;
                            Price.Text = null;
                            Percent.Text = null;
                            Start_Date.SelectedDate = null;
                            End_Date.SelectedDate = null;
                            Load_Courses();
                        }
                        else
                        {
                            TeachersName.SelectedItem = null;
                            Group.Text = null;
                            Price.Text = null;
                            Percent.Text = null;
                            Start_Date.SelectedDate = null;
                            End_Date.SelectedDate = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void CourseDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var SelectedCourse = CourseDetails.SelectedItem as Course;
                if (SelectedCourse != null)
                {
                    Show_Student_Details show_Student_Details = new Show_Student_Details(SelectedCourse.Course_Id);
                    show_Student_Details.Show();

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
                using (var db = new DataBaseContext())
                {
                    var _Courses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x=> (x.material_Study.Name.Contains(Search.Text)) &&(x.IsFinished == false)).ToList();
                    courses = _Courses;
                    CourseDetails.ItemsSource = courses;
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
                var SelectedCourse = CourseDetails.SelectedItem as Course;
                if (SelectedCourse != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Course course = new Course();
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectedCourse.Course_Id);
                        ShowCourseDetails showCourseDetails = new ShowCourseDetails(course.Course_Id);
                        showCourseDetails.Show();
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

    }
}
