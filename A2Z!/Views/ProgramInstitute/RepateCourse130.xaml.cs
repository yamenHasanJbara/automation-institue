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

namespace A2Z_.Views.ProgramInstitute
{
    /// <summary>
    /// Interaction logic for RepateCourse130.xaml
    /// </summary>
    ///// <ListBoxItem Content="8"/>
    //        <ListBoxItem Content = "930" />
    //        < ListBoxItem Content="11"/>
    //        <ListBoxItem Content = "1230" />
    //        < ListBoxItem Content="2"/>
    //        <ListBoxItem Content = "330" />
    //        < ListBoxItem Content="5"/>
    //        <ListBoxItem Content = "630" />

    public partial class RepateCourse130 : Window
    {
        public int CourseId { get; set; }

        public List<double> vs { get; set; }


        public RepateCourse130(int courseId)
        {
            InitializeComponent();
            this.CourseId = courseId;
            LoadInfo();
        }

        private void LoadInfo()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    SetTimeForCourse setTimeForCourse = new SetTimeForCourse();
                    setTimeForCourse = db.setTimeForCourses.Find(1);
                    List<double> ss = new List<double>();
                    double first = double.Parse(setTimeForCourse.StartTime);
                    if (first <= 12.5)
                    { ss.Add(first); }
                    else
                    {
                        first -= 12;
                        ss.Add(first);
                    }
                    double second = first + double.Parse(setTimeForCourse.Courselength);
                    if (second <= 12.5)
                    { ss.Add(second); }
                    else
                    {
                        second -= 12;
                        ss.Add(second);
                    }
                    
                    double third = second + double.Parse(setTimeForCourse.Courselength);
                    if (third <= 12.5)
                    { ss.Add(third); }
                    else
                    {
                        third -= 12;
                        ss.Add(third);
                    }
                    
                    double fourth = third + double.Parse(setTimeForCourse.Courselength);
                    if (fourth <= 12.5)
                    { ss.Add(fourth); }
                    else
                    {
                        fourth -= 12;
                        ss.Add(fourth);
                    }
                    
                    double five = fourth + double.Parse(setTimeForCourse.Courselength);
                    if (five <= 12.5)
                    { ss.Add(five); }
                    else
                    {
                        five -= 12;
                        ss.Add(five);
                    }
                

                    double six = five + double.Parse(setTimeForCourse.Courselength);
                    if (six <= 12.5)
                    { ss.Add(six); }
                    else
                    {
                        six -= 12;
                        ss.Add(six);
                    }
                

                    double seven = six + double.Parse(setTimeForCourse.Courselength);
                    if (seven <= 12.5)
                    { ss.Add(seven); }
                    else
                    {
                        seven -= 12;
                        ss.Add(seven);
                    }
                


                    double eight = seven + double.Parse(setTimeForCourse.Courselength);
                    if (eight <= 12.5)
                    { ss.Add(eight); }
                    else
                    {
                        eight -= 12;
                        ss.Add(eight);
                    }
                    vs = ss;
                    Hours.ItemsSource = vs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void RepeateOncertainSchedule(object sender, RoutedEventArgs e)
        {
            ListBoxItem comboBoxItemDays = (ListBoxItem)Days.SelectedItem;
            ListBoxItem tycomboBoxItemDayspeItemHalls = (ListBoxItem)Halls.SelectedItem;
            Course course = new Course();
            if ((comboBoxItemDays != null) &&  (tycomboBoxItemDayspeItemHalls != null) && (Hours.SelectedIndex != -1))
            {
                string days = comboBoxItemDays.Content.ToString();
                string hours ="";
                string halls = tycomboBoxItemDayspeItemHalls.Content.ToString();
                if (Hours.SelectedIndex == 0)
                {
                    hours = "8";
                }
                if (Hours.SelectedIndex == 1)
                {
                    hours = "930";
                }
                if (Hours.SelectedIndex == 2)
                {
                    hours = "11";
                }
                if (Hours.SelectedIndex == 3)
                {
                    hours = "1230";
                }
                if (Hours.SelectedIndex == 4)
                {
                    hours = "2";
                }
                if (Hours.SelectedIndex == 5)
                {
                    hours = "330";
                }
                if (Hours.SelectedIndex == 6)
                {
                    hours = "5";
                }
                if (Hours.SelectedIndex == 7)
                {
                    hours = "630";
                }
                using (var db = new DataBaseContext())
                {
                    course = db.Courses.SingleOrDefault(x => x.Course_Id == CourseId);
                    ProgramInstituteOneAndHalfHour programInstituteC = new ProgramInstituteOneAndHalfHour();
                    course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == course.Course_Id);
                    switch (days)
                    {
                        case "السبت":
                            {
                                days = "Saturday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد  لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأحد":
                            {
                                days = "Sunday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأثنين":
                            {
                                days = "Monday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الثلاثاء":
                            {
                                days = "Tuesday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأربعاء":
                            {
                                days = "Wednesday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الخميس":
                            {
                                days = "Thursday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الجمعة":
                            {
                                days = "Friday";
                                bool check = db.programInstituteOneAndHalfHours.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
                                if (check)
                                {
                                    MessageBox.Show("لا يمكن إضافة موعد لانه مشغول");
                                }
                                else
                                {
                                    programInstituteC.course = course;
                                    programInstituteC.day = days;
                                    programInstituteC.hour = hours;
                                    programInstituteC.hall = halls;
                                    db.programInstituteOneAndHalfHours.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("الرجاء اختيار كافة الخيارات");
            }
        }
    }
}
