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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_Existing_Student.xaml
    /// </summary>
    public partial class P_Add_Existing_Student : Page
    {

        public List<Student> students { get; set; }

        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public List<Material_Study> material_Studies { get; set; }

        public List<Course> courses { get; set; }
        public P_Add_Existing_Student()
        {
            InitializeComponent();
            Load_Student();
            Load_Section();
        }

        public void Load_Student()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Student = db.Students.ToList();
                    students = _Student;
                    Student.ItemsSource = students;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Load_Section()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var Sections = db.Sections.ToList();
                    sections = Sections;
                    SectionsName.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void SectionsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = SectionsName.SelectedItem as Section;
                if (SelectedSection != null)
                {
                    YearsNumber.ItemsSource = null;
                    SemesterNumber.SelectedIndex = -1;
                    using (var db = new DataBaseContext())
                    {
                        Section section = new Section();
                        section = db.Sections.Include(x => x.Faculties).SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                        if (section.Faculties.Count > 0)
                        {
                            CollagesName.Visibility = Visibility.Visible;
                            YearsNumber.Visibility = Visibility.Visible;
                            SemesterNumber.Visibility = Visibility.Visible;
                            var _Collage = db.Faculties.Include(x => x.Section).Where(x => x.Section == section).ToList();
                            faculties = _Collage;
                            CollagesName.ItemsSource = faculties;
                        }
                        else
                        {
                            CollagesName.Visibility = Visibility.Collapsed;
                            YearsNumber.Visibility = Visibility.Collapsed;
                            SemesterNumber.Visibility = Visibility.Collapsed;
                            var _courses = db.Courses.Include(x => x.section).Include(x => x.material_Study).Where(x => x.section == section && x.IsFinished == false).ToList();
                            courses = _courses;
                            Coursess.ItemsSource = courses;

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

        private void CollagesName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedCollage = CollagesName.SelectedItem as Faculty;
                if (SelectedCollage != null)
                {
                    YearsNumber.ItemsSource = null;
                    SemesterNumber.SelectedIndex = -1;
                    using (var db = new DataBaseContext())
                    {
                        Faculty faculty = new Faculty();
                        faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedCollage.Faculty_Id);
                        var _Years = db.Years.Include(x => x.Faculty).Where(x => x.Faculty == faculty).ToList();
                        years = _Years;
                        YearsNumber.ItemsSource = years;
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


        private void YearsNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SemesterNumber.SelectedIndex = -1;
                Coursess.SelectedItem = null;
                Coursess.ItemsSource = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void SemesterNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = SectionsName.SelectedItem as Section;
                var SelectedFaculity = CollagesName.SelectedItem as Faculty;
                var SelectedYear = YearsNumber.SelectedItem as Year;
                ComboBoxItem typeItem = (ComboBoxItem)SemesterNumber.SelectedItem;
                if ((SelectedSection != null) && (SelectedFaculity != null) && (SelectedYear != null) && (typeItem != null))
                {
                    string value = typeItem.Content.ToString();
                    int Semester = int.Parse(value);
                    if ((Semester != 1) || (Semester != 2))
                    {
                        Section section = new Section();
                        Faculty faculty = new Faculty();
                        Year year = new Year();
                        using (var db = new DataBaseContext())
                        {
                            section = db.Sections.SingleOrDefault(x => x.Section_Id == SelectedSection.Section_Id);
                            faculty = db.Faculties.SingleOrDefault(x => x.Faculty_Id == SelectedFaculity.Faculty_Id);
                            year = db.Years.Include(x => x.Faculty).SingleOrDefault(x => x.Year_Id == SelectedYear.Year_Id);
                            var _Courses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Where(x => (x.section == section) && (x.faculty == faculty) && (x.Year == year) && (x.material_Study.Semester == Semester) && (x.IsFinished == false)).ToList();
                            courses = _Courses;
                            Coursess.ItemsSource = courses;
                        }
                    }
                    else
                    { }
                }
                else
                { }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        


        public void Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var row = Student.SelectedItem as Student;
                if (row != null)
                {
                    FullName.Text = row.Name;
                    NubmerPhone.Text = row.Number_Phone;
                }
                else
                {}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedStudent = Student.SelectedItem as Student;
                var SelectedCourses = Coursess.SelectedItem as Course;
                if ((SelectedStudent != null) && (SelectedCourses != null))
                {
                    using (var db = new DataBaseContext())
                    {
                        Student student = new Student();
                        Course course = new Course();
                        Payment payment = new Payment();
                        Student_course student_Course = new Student_course();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectedCourses.Course_Id);
                        bool CheckIfStudentIsRegisterInCourseAgain = db.Student_Courses.Any(x => (x.Student_Id == student.Student_Id) && (x.Course_Id == course.Course_Id));
                        if (CheckIfStudentIsRegisterInCourseAgain)
                        {
                            MessageBox.Show("لا يمكن تسجيل طالب في دورة مرتين");
                        }
                        else
                        {
                                student_Course.Course_Id = course.Course_Id;
                                student_Course.Student_Id = student.Student_Id;
                                db.Student_Courses.Add(student_Course);
                                db.SaveChanges();
                                if (String.IsNullOrWhiteSpace(Amount.Text))
                                {
                                    if (String.IsNullOrWhiteSpace(PillNumber.Text))
                                    {
                                        payment.Amount = 0;
                                        payment.date = DateTime.Today;
                                        payment.student = student;
                                        payment.course = course;
                                        payment.Payment_Type = 2;
                                        payment.Stayed = course.Price;
                                        payment.PillNumber = "-1";
                                        db.Payments.Add(payment);
                                        db.SaveChanges();
                                        MessageBox.Show("تمت عملية التسجيل بنجاح");
                                        Amount.Text = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("الرجاء ازالة رقم الفاتورة في حال كان حقل المبلغ المدفوع فارغ");
                                        db.Student_Courses.Remove(student_Course);
                                        db.SaveChanges();
                                        Amount.Text = null;
                                        PillNumber.Text = null;
                                    }
                                }
                                else
                                {
                                    if (!IntegerValidation.checkIntValue(Amount.Text))
                                    {
                                        MessageBox.Show("الرجاء التأكد من صحة المبلغ المدفوع");
                                        db.Student_Courses.Remove(student_Course);
                                        db.SaveChanges();
                                        Amount.Text = null;
                                        PillNumber.Text = null;
                                    }
                                    else
                                    {
                                        if (int.Parse(Amount.Text) > course.Price)
                                        {
                                            MessageBox.Show("إن المبلغ المدفوع أكبر من قيمة الكورس");
                                            db.Student_Courses.Remove(student_Course);
                                            db.SaveChanges();
                                            Amount.Text = null;
                                            PillNumber.Text = null;
                                        }
                                        else
                                        {
                                            if (String.IsNullOrWhiteSpace(Amount.Text) || String.IsNullOrWhiteSpace(PillNumber.Text))
                                            {
                                                MessageBox.Show("الرجاء تعبئة حقل المبلغ ورقم الفاتورة");
                                                db.Student_Courses.Remove(student_Course);
                                                db.SaveChanges();
                                                Amount.Text = null;
                                                PillNumber.Text = null;
                                            }
                                            else
                                            {
                                                payment.Amount = int.Parse(Amount.Text);
                                                payment.date = DateTime.Today;
                                                payment.student = student;
                                                payment.course = course;
                                                payment.Payment_Type = 2;
                                                payment.PillNumber = PillNumber.Text;
                                                payment.Stayed = course.Price - int.Parse(Amount.Text);
                                                db.Payments.Add(payment);
                                                db.SaveChanges();
                                                MessageBox.Show("تمت عملية التسجيل بنجاح");
                                                InsertIntoOutlay(payment);  
                                                Amount.Text = null;
                                                PillNumber.Text = null;
                                            }
                                        }
                                    }
                                }
                            
                        }
                    }
                }
                else
                { MessageBox.Show("الرجاء اختيار طالب ودورة"); }
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

                    var GetStudent = db.Students.Where(x => (x.Number_Phone.Contains(Search.Text)) || (x.Name.Contains(Search.Text))).ToList();
                    students = GetStudent;
                    Student.ItemsSource = students;
                }
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

        private void AddCourse(object sender, RoutedEventArgs e)
        {
            try
            {
                AddCourseFromAddStudentPage addCourseFromAddStudentPage = new AddCourseFromAddStudentPage();
                addCourseFromAddStudentPage.p_Add_Existing_Student = this;
                addCourseFromAddStudentPage.Show();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedStudent = Student.SelectedItem as Student;
                if (SelectedStudent != null)
                {
                    Student student = new Student();
                    using (var db = new DataBaseContext())
                    {
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                    }
                    UpdateExistStudent updateExistStudent = new UpdateExistStudent(student.Student_Id);
                    updateExistStudent.p_Add_Existing_Student = this;
                    updateExistStudent.Show();
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
