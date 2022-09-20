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
    /// Interaction logic for RepeatCourse.xaml
    /// </summary>
    public partial class RepeatCourse : Window
    {
        public int CourseId { get; set; }
        public RepeatCourse(int courseId)
        {
            InitializeComponent();
            this.CourseId = courseId;
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
            ListBoxItem tycomboBoxItemDayspeItemHours = (ListBoxItem)Hours.SelectedItem;
            ListBoxItem tycomboBoxItemDayspeItemHalls = (ListBoxItem)Halls.SelectedItem;
            Course course = new Course();
            if ((comboBoxItemDays != null) && (tycomboBoxItemDayspeItemHours != null) && (tycomboBoxItemDayspeItemHalls != null))
            {
                string days = comboBoxItemDays.Content.ToString();
                string hours = tycomboBoxItemDayspeItemHours.Content.ToString();
                string halls = tycomboBoxItemDayspeItemHalls.Content.ToString();
                using (var db = new DataBaseContext())
                {
                    course = db.Courses.SingleOrDefault(x => x.Course_Id == CourseId);
                    ProgramInstituteC programInstituteC = new ProgramInstituteC();
                    course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == course.Course_Id);
                    switch (days)
                    {
                        case "السبت":
                            {
                                days = "Saturday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأحد":
                            {
                                days = "Sunday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأثنين":
                            {
                                days = "Monday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الثلاثاء":
                            {
                                days = "Tuesday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الأربعاء":
                            {
                                days = "Wednesday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الخميس":
                            {
                                days = "Thursday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
                                    db.SaveChanges();
                                    MessageBox.Show("تمت عملية الإضافة بنجاح");
                                }
                                break;
                            }

                        case "الجمعة":
                            {
                                days = "Friday";
                                bool check = db.programInstitutes.Include(x => x.course).Any(x => (x.day == days) && (x.hour == hours) && (x.hall == halls));
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
                                    db.programInstitutes.Add(programInstituteC);
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
