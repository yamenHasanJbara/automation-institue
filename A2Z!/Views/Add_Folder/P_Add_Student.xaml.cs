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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for P_Add_Student.xaml
    /// </summary>
    public partial class P_Add_Student : Page
    {

        public List<Section> sections { get; set; }

        public List<Faculty> faculties { get; set; }

        public List<Year> years { get; set; }

        public List<Material_Study> material_Studies { get; set; }

        public List<Course> courses { get; set; }

        public P_Add_Student()
        {
            InitializeComponent();
            Load_Section();
        }



        // هنا سوف نقوم بالاتصال بقاعدة البيانات لنجلب كافة الاقسام الموجودة لدينا
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

        /*هنا يتم إطلاق حدث عندما يتم اختيار قسم ففي حال 
         * 
         * هذا القسم تابع لدورات جامعية ويحوي اكثر من كلية
         * نقوم بجلب الكليات من قاعدة البيانات وعرضها على المستخدم وفي حال عدم 
         * وجود كليات لدى القسم نقوم بإالغاء عرض الكليات والسنوات والفصل*/
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
                        if (section.Faculties.Count>0)
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
                            var _courses = db.Courses.Include(x => x.section).Include(x=>x.material_Study).Where(x => x.section == section && x.IsFinished == false).ToList();
                            courses = _courses;
                            Coursess.ItemsSource = courses;

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


        /*هنا يتم اطلاق حدث عنما يتم اختيار كلية
         ليقوم بعدها بجلب كافة الكليات الموجودة ضمن قاعدة المعطيات وعرضها ضمن الكومبوكس الخاص بالكليات*/
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
                {   }
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
        /*هنا يتم اختيار رقم الفصل ومن بعدها يقوم بجلب كافة المواد حسب الفصل المختار*/
        public void SemesterNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedSection = SectionsName.SelectedItem as Section;
                var SelectedFaculity = CollagesName.SelectedItem as Faculty;
                var SelectedYear = YearsNumber.SelectedItem as Year;
                ComboBoxItem typeItem = (ComboBoxItem)SemesterNumber.SelectedItem;
                if ( (SelectedSection != null) && ( SelectedFaculity != null ) && ( SelectedYear != null ) && ( typeItem != null ) )
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
                            var _Courses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.material_Study).Where(x=>(x.section == section)&&(x.faculty == faculty)&&(x.Year == year)&&(x.material_Study.Semester == Semester) && (x.IsFinished == false)).ToList();
                            courses = _Courses;
                            Coursess.ItemsSource = courses;
                        }
                    }
                    else
                    { }
                }
                else
                {  }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        /*بداية لدينا حالتين
         الاولى في حال التسجيل في دورات جامعية او لغات
         ثم يتم التأكد من أن الدورة موجودة ضمن قاعدة البيانات
         ومن ثم يتم يتم تسجيل الطالب ويتم ربط الطالب بالكورس وفي حال حدوث اي خطأ اثناء التسجيل يتم التراجع عن عمليات الاضافة التي تمت
         ومن ثم حسب المبلغ المدفوع يتم التعامل */
        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                string NameStudent = StudentName.Text;
                string NmberPhone = NumberPhone.Text.Trim();
                if (String.IsNullOrWhiteSpace(StudentName.Text) || (String.IsNullOrWhiteSpace(NumberPhone.Text)) || Coursess.SelectedItem == null)
                {
                    MessageBox.Show("الرجاء تعبئة حقل الاسم والرقم واختيار دورة");
                    
                }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        if (NmberPhone.Length != 10)
                        {
                            MessageBox.Show("الرجاء التأكد من الرقم المدخل");
                        }
                        else
                        {
                            bool CheckIfStudentIsExistWhithTheSameINformation = db.Students.Any(x => (x.Name == NameStudent) && (x.Number_Phone == NmberPhone));
                            if (CheckIfStudentIsExistWhithTheSameINformation)
                            {
                                Student student = new Student();
                                student = db.Students.FirstOrDefault(x => (x.Name == NameStudent) && (x.Number_Phone == NmberPhone));
                                Student_course student_Course = new Student_course();
                                Payment payment = new Payment();
                                Course course = new Course();
                                var SelectecetCourse = Coursess.SelectedItem as Course;
                                course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectecetCourse.Course_Id);
                                var checkIfExistInCourse = db.Student_Courses.Any(x => x.Course_Id_FK == course && x.Student_Id_FK == student);
                                if (!checkIfExistInCourse)
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
                                            StudentName.Text = null;
                                            NumberPhone.Text = null;
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
                                                    StudentName.Text = null;
                                                    NumberPhone.Text = null;
                                                    Amount.Text = null;
                                                    PillNumber.Text = null;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                     MessageBox.Show("ان الطالب مسجل مسبقاً في هذه الدورة");
                                    
                                }
                            }
                            else
                            {
                                bool CheckIfStudentIsExistWhithTheSameInformation = db.Students.Any(x => (x.Name == NameStudent) || (x.Number_Phone == NmberPhone));
                                if (CheckIfStudentIsExistWhithTheSameInformation)
                                {
                                    MessageBox.Show("ان اسم الطالب او الرقم موجود مسبقا");
                                }
                                else
                                { 
                                    Student student = new Student();
                                    Student_course student_Course = new Student_course();
                                    Payment payment = new Payment();
                                    Course course = new Course();
                                    var SelectecetCourse = Coursess.SelectedItem as Course;
                                    student.Name = NameStudent;
                                    student.Number_Phone = NmberPhone;
                                    db.Students.Add(student);
                                    db.SaveChanges();
                                    course = db.Courses.SingleOrDefault(x => x.Course_Id == SelectecetCourse.Course_Id);
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
                                            StudentName.Text = null;
                                            NumberPhone.Text = null;
                                            Amount.Text = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("الرجاء ازالة رقم الفاتورة في حال كان حقل المبلغ المدفوع فارغ");
                                            db.Students.Remove(student);
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
                                            db.Students.Remove(student);
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
                                                db.Students.Remove(student);
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
                                                    db.Students.Remove(student);
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
                                                    StudentName.Text = null;
                                                    NumberPhone.Text = null;
                                                    Amount.Text = null;
                                                    PillNumber.Text = null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        /*هنا يتم استدعائه عندما يتم تسجيل طالب
         يتم التأكد من جدول المصاريف ازا موجود فيه واردات اليوم من الطلاب بتاريخ اليوم
         في حال وجوده يتم تحديثه بالمبلغ المدفوع للطالب
         وفي حال عدم وجوده يتم إنشاءه */
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
                addCourseFromAddStudentPage.p_Add_Student = this;
                addCourseFromAddStudentPage.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
