using A2Z_.Models;
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

namespace A2Z_.Views.Add_Folder
{
    /// <summary>
    /// Interaction logic for UpdateExistStudent.xaml
    /// </summary>
    public partial class UpdateExistStudent : Window
    {

        public int StudentId { get; set; }

        public P_Add_Existing_Student p_Add_Existing_Student { get; set; }
        public UpdateExistStudent(int studentId)
        {
            InitializeComponent();
            this.StudentId = studentId;
            LoadStudentInfo(StudentId);
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
        private void LoadStudentInfo(int studentId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Student student = new Student();
                    List<Student> students = new List<Student>();
                    student = db.Students.SingleOrDefault(x => x.Student_Id == studentId);
                    students.Add(student);
                    studentDetails.ItemsSource = students;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void studentDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var SelectedStudent = studentDetails.SelectedItem as Student;
                if (SelectedStudent != null)
                {
                    using (var db = new DataBaseContext())
                    {
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x=>x.Student_Id == SelectedStudent.Student_Id);
                        FullName.Text = student.Name;
                        NubmerPhone.Text = student.Number_Phone;
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

        private void UpdateStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedStudent = studentDetails.SelectedItem as Student;
                if (SelectedStudent == null || String.IsNullOrWhiteSpace(FullName.Text) || String.IsNullOrWhiteSpace(NubmerPhone.Text))
                { MessageBox.Show("الرجاء تحديد الطالب وتعبئة الحقول المطلوبة"); }
                else
                {
                    using (var db = new DataBaseContext())
                    {
                        Student student = new Student();
                        student = db.Students.SingleOrDefault(x => x.Student_Id == SelectedStudent.Student_Id);
                        string StudentName = FullName.Text;
                        string Number = NubmerPhone.Text;
                        if (Number.Length!=10)
                        {
                            MessageBox.Show("الرجاء التأكد من الرقم");
                        }
                        else
                        {
                            bool check = db.Students.Any(x => (x.Name == StudentName) && (x.Number_Phone == Number));
                            if (check)
                            {
                                MessageBox.Show("إن المعلومات المدخلة موجودة سابقاً");
                            }
                            else
                            {
                                student.Name = StudentName;
                                student.Number_Phone = Number;
                                db.Students.Update(student);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية التعديل بنجاح");
                                p_Add_Existing_Student.Load_Student();
                                this.Close();
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
    }
}
