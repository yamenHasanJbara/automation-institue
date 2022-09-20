using A2Z_.Models;
using A2Z_.Views.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
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
    /// Interaction logic for P_Show_Student.xaml
    /// </summary>
    public partial class P_Show_Student : Page
    {


        public List<IGrouping<Student,Student_course>> student_Courses { get; set; }

        public List<Payment> payments { get; set; }

        public List<Show_Student_details> show_Student_Details11 { get; set; }

        public List<Show_Student_details> show_Student_Details22 { get; set; }


        public P_Show_Student()
        {
            InitializeComponent();
            Load_Student_Info();
        }


        private void Load_Student_Info()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _RegisterStundentCourse = db.Student_Courses.Include(x => x.Student_Id_FK).Include(x => x.Course_Id_FK).Where(x=>x.Withdrawn == false).AsEnumerable().GroupBy(x => x.Student_Id_FK).ToList();
                    student_Courses = _RegisterStundentCourse;
                    List<Show_Student_details> Show_Student = new List<Show_Student_details>();
                    foreach (var item in student_Courses)
                    {
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == item.Key.Student_Id);
                        Show_Student_details show_Student_Details = new Show_Student_details();
                        List<Show_Student_Details_Payments> show_Student_Details_Payments = new List<Show_Student_Details_Payments>();
                        foreach (var Student_course in item)
                        {
                            Course course = new Course();
                            Show_Student_Details_Payments show_Student_Details_Payments1 = new Show_Student_Details_Payments();
                            course = db.Courses.Include(x => x.material_Study).Include(x=>x.term).SingleOrDefault(x => x.Course_Id == Student_course.Course_Id);
                            var _StudentPayments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => (x.course == course) && (x.student == student) && (x.Payment_Type == 2)).ToList();
                            payments = _StudentPayments;
                            int AmountPaied = 0;
                            int AmountStaied = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaied += item1.Amount;
                            }
                            AmountStaied = course.Price - AmountPaied;
                            if (AmountStaied != 0)
                            {
                                show_Student_Details_Payments1.CourseId = course.Course_Id;
                                show_Student_Details_Payments1.CourseName = course.material_Study.Name;
                                show_Student_Details_Payments1.CoursePrice = course.Price;
                                show_Student_Details_Payments1.CourseGroup = course.Group;
                                show_Student_Details_Payments1.IsFinished = course.IsFinished;
                                show_Student_Details_Payments1.Amount = AmountPaied;
                                show_Student_Details_Payments1.Amountstaied = AmountStaied;
                                if (course.term != null)
                                {
                                    show_Student_Details_Payments1.Term = course.term.name;
                                }
                                else
                                {
                                    show_Student_Details_Payments1.Term = "NO TERM";
                                }
                                var studentCoursePivot = db.Student_Courses.FirstOrDefault(x=>x.Course_Id == course.Course_Id && x.Student_Id == student.Student_Id);
                                show_Student_Details_Payments1.Withdrawn = studentCoursePivot.Withdrawn;
                                show_Student_Details_Payments.Add(show_Student_Details_Payments1);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (show_Student_Details_Payments.Count > 0)
                        {
                            show_Student_Details.StudentId = item.Key.Student_Id;
                            show_Student_Details.StudentName = item.Key.Name;
                            show_Student_Details.show_Student_Details_Payments = show_Student_Details_Payments;
                            Show_Student.Add(show_Student_Details);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    show_Student_Details11 = Show_Student;
                    StudentDetails.ItemsSource = show_Student_Details11;
                }

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
                show_Student_Details22 = show_Student_Details11.Where(x => x.StudentName.Contains(Search.Text)).ToList();
                StudentDetails.ItemsSource = show_Student_Details22;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

       

        private void Show(object sender, RoutedEventArgs e)
        {
            var SelectedStudent = StudentDetails.SelectedItem as Show_Student_details;
            List<int> courseId = new List<int>();
            foreach (var item in SelectedStudent.show_Student_Details_Payments)
            {
                courseId.Add(item.CourseId);
            }
            int StudentId = SelectedStudent.StudentId;
            StudentPaymentsForCertainCourse studentPaymentsForCertainCourse = new StudentPaymentsForCertainCourse(StudentId,courseId);
            studentPaymentsForCertainCourse.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStudentInfoWithCheckEvent();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LoadStudentInfoWithCheckEvent()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _RegisterStundentCourse = db.Student_Courses.Include(x => x.Student_Id_FK).Include(x => x.Course_Id_FK).AsEnumerable().GroupBy(x => x.Student_Id_FK).ToList();
                    student_Courses = _RegisterStundentCourse;
                    List<Show_Student_details> Show_Student = new List<Show_Student_details>();
                    foreach (var item in student_Courses)
                    {
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == item.Key.Student_Id);
                        Show_Student_details show_Student_Details = new Show_Student_details();
                        List<Show_Student_Details_Payments> show_Student_Details_Payments = new List<Show_Student_Details_Payments>();
                        foreach (var Student_course in item)
                        {
                            Course course = new Course();
                            Show_Student_Details_Payments show_Student_Details_Payments1 = new Show_Student_Details_Payments();
                            course = db.Courses.Include(x => x.material_Study).Include(x=>x.term).SingleOrDefault(x => x.Course_Id == Student_course.Course_Id);
                            var _StudentPayments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => (x.course == course) && (x.student == student) && (x.Payment_Type == 2)).ToList();
                            payments = _StudentPayments;
                            int AmountPaied = 0;
                            int AmountStaied = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaied += item1.Amount;
                            }
                            AmountStaied = course.Price - AmountPaied;
                           
                            show_Student_Details_Payments1.CourseId = course.Course_Id;
                            show_Student_Details_Payments1.CourseName = course.material_Study.Name;
                            show_Student_Details_Payments1.CoursePrice = course.Price;
                            show_Student_Details_Payments1.CourseGroup = course.Group;
                            show_Student_Details_Payments1.IsFinished = course.IsFinished;
                            show_Student_Details_Payments1.Amount = AmountPaied;
                            if (course.term != null)
                            {
                                show_Student_Details_Payments1.Term = course.term.name;
                            }
                            else
                            {
                                show_Student_Details_Payments1.Term = "NO TERM";
                            }
                            show_Student_Details_Payments1.Amountstaied = AmountStaied;
                            show_Student_Details_Payments1.Withdrawn = Student_course.Withdrawn;
                            show_Student_Details_Payments.Add(show_Student_Details_Payments1);
                            
                        }
                       
                        show_Student_Details.StudentId = item.Key.Student_Id;
                        show_Student_Details.StudentName = item.Key.Name;
                        show_Student_Details.show_Student_Details_Payments = show_Student_Details_Payments;
                        Show_Student.Add(show_Student_Details);
                        
                    }
                    show_Student_Details11 = Show_Student;
                    StudentDetails.ItemsSource = show_Student_Details11;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
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

        private void CheckBoxForWithdrawnStudent_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStudentInfoWithCheckEventForWithdrawnStudent();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LoadStudentInfoWithCheckEventForWithdrawnStudent()
        {

            try
            {
                using (var db = new DataBaseContext())
                {
                    var _RegisterStundentCourse = db.Student_Courses.Include(x => x.Student_Id_FK).Include(x => x.Course_Id_FK).Where(x => x.Withdrawn == true).AsEnumerable().GroupBy(x => x.Student_Id_FK).ToList();
                    student_Courses = _RegisterStundentCourse;
                    List<Show_Student_details> Show_Student = new List<Show_Student_details>();
                    foreach (var item in student_Courses)
                    {
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == item.Key.Student_Id);
                        Show_Student_details show_Student_Details = new Show_Student_details();
                        List<Show_Student_Details_Payments> show_Student_Details_Payments = new List<Show_Student_Details_Payments>();
                        foreach (var Student_course in item)
                        {
                            Course course = new Course();
                            Show_Student_Details_Payments show_Student_Details_Payments1 = new Show_Student_Details_Payments();
                            course = db.Courses.Include(x => x.material_Study).Include(x => x.term).SingleOrDefault(x => x.Course_Id == Student_course.Course_Id);
                            var _StudentPayments = db.Payments.Include(x => x.course).Include(x => x.student).Where(x => (x.course == course) && (x.student == student) && (x.Payment_Type == 2)).ToList();
                            payments = _StudentPayments;
                            int AmountPaied = 0;
                            int AmountStaied = 0;
                            foreach (var item1 in payments)
                            {
                                AmountPaied += item1.Amount;
                            }
                            AmountStaied = course.Price - AmountPaied;

                            show_Student_Details_Payments1.CourseId = course.Course_Id;
                            show_Student_Details_Payments1.CourseName = course.material_Study.Name;
                            show_Student_Details_Payments1.CoursePrice = course.Price;
                            show_Student_Details_Payments1.CourseGroup = course.Group;
                            show_Student_Details_Payments1.IsFinished = course.IsFinished;
                            show_Student_Details_Payments1.Amount = AmountPaied;
                            show_Student_Details_Payments1.Amountstaied = AmountStaied;
                            show_Student_Details_Payments1.Term = course.term.name;
                            show_Student_Details_Payments1.Withdrawn = Student_course.Withdrawn;
                            show_Student_Details_Payments.Add(show_Student_Details_Payments1);

                        }

                        show_Student_Details.StudentId = item.Key.Student_Id;
                        show_Student_Details.StudentName = item.Key.Name;
                        show_Student_Details.show_Student_Details_Payments = show_Student_Details_Payments;
                        Show_Student.Add(show_Student_Details);

                    }
                    show_Student_Details11 = Show_Student;
                    StudentDetails.ItemsSource = show_Student_Details11;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
