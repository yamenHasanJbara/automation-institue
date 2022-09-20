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

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for Show_All_Teacher_Details.xaml
    /// </summary>
    public partial class Show_All_Teacher_Details : Window
    {
        public List<Course> courses { get; set; }

        public List<ShowAllTeacherDetails> ShowAllTeacherDetails { get; set; }

        public List<Payment> payments { get; set; }
        public Show_All_Teacher_Details(int TeacherId)
        {
            InitializeComponent();
            Load_Teacher_Course(TeacherId);
        }

        //private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void Ellipse_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        this.WindowState = WindowState.Minimized;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ChangedButton == MouseButton.Left)
        //            this.DragMove();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void Load_Teacher_Course(int teacherId)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Teacher teacher = new Teacher();
                    teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == teacherId);
                    List<ShowAllTeacherDetails> teacherAmountDeserveds1 = new List<ShowAllTeacherDetails>();
                    var _Courses = db.Courses.Include(x => x.material_Study).Include(x => x.teacher).Include(x => x.payments).Include(x => x.Student_Courses).Where(x=>x.teacher == teacher).ToList();
                    courses = _Courses;
                    foreach (var item in courses)
                    {
                        ShowAllTeacherDetails teacherAmountDeserved = new ShowAllTeacherDetails();
                        var _Payments = db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => x.course == item && x.teacher == teacher && x.Payment_Type == 1).ToList();
                        var teacherDeserved = (db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => x.course == item && x.Payment_Type==2).Sum(x=>x.Amount) * item.percent) /100;
                        var teacherPayments = db.Payments.Include(x => x.teacher).Include(x => x.course).Where(x => x.course == item && x.Payment_Type == 1).Sum(x => x.Amount);
                        payments = _Payments;
                        if (payments.Count >0)
                        {
                            if (teacherDeserved >= teacherPayments)
                            {
                                teacherAmountDeserved.payments = payments;
                                teacherAmountDeserved.Group = item.Group;
                                teacherAmountDeserved.Price = item.Price;
                                teacherAmountDeserved.materialStudy = item.material_Study.Name;
                                teacherAmountDeserved.Isfinished = item.IsFinished;
                                teacherAmountDeserved.teacher = item.teacher.Name;
                                teacherAmountDeserved.Course_Id = item.Course_Id;
                                teacherAmountDeserved.TeacherAmount = item.Price * item.Student_Courses.Count * item.percent / 100;
                                teacherAmountDeserved.TeacherDeserved = teacherDeserved;
                                teacherAmountDeserved.coursePercent = item.percent;
                                teacherAmountDeserved.studentCount = item.Student_Courses.Count;
                                teacherAmountDeserveds1.Add(teacherAmountDeserved);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            teacherAmountDeserved.payments = null;
                            teacherAmountDeserved.Group = item.Group;
                            teacherAmountDeserved.Price = item.Price;
                            teacherAmountDeserved.materialStudy = item.material_Study.Name;
                            teacherAmountDeserved.teacher = item.teacher.Name;
                            teacherAmountDeserved.Isfinished = item.IsFinished;
                            teacherAmountDeserved.Course_Id = item.Course_Id;
                            teacherAmountDeserved.TeacherAmount = item.Price * item.Student_Courses.Count * item.percent / 100;
                            teacherAmountDeserved.TeacherDeserved = teacherDeserved;
                            teacherAmountDeserved.coursePercent = item.percent;
                            teacherAmountDeserved.studentCount = item.Student_Courses.Count;
                            teacherAmountDeserveds1.Add(teacherAmountDeserved);
                        }
                    }
                    ShowAllTeacherDetails = teacherAmountDeserveds1;
                    TeacherDetails.ItemsSource = ShowAllTeacherDetails;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
