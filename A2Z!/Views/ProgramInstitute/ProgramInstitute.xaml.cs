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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2Z_.Views.ProgramInstitute
{
    /// <summary>
    /// Interaction logic for ProgramInstitute.xaml
    /// </summary>
    public partial class ProgramInstitute : Page
    {
        public ProgramInstitute()
        {
            InitializeComponent();
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
                    StartTime.Text = setTimeForCourse.StartTime;
                    CourseLength.Text = setTimeForCourse.Courselength;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    SetTimeForCourse setTimeForCourse = new SetTimeForCourse();
                    setTimeForCourse = db.setTimeForCourses.Find(1);
                    if (String.IsNullOrWhiteSpace(StartTime.Text) || String.IsNullOrWhiteSpace(CourseLength.Text))
                    {
                        MessageBox.Show("تعبئة الحقول");
                    }
                    else
                    {
                        setTimeForCourse.StartTime = StartTime.Text;
                        setTimeForCourse.Courselength = CourseLength.Text;
                        db.setTimeForCourses.Update(setTimeForCourse);
                        db.SaveChanges();
                        MessageBox.Show("تمت عملية التحديث بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteProgramInstituteFor130()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var all2CourseHour = db.programInstituteOneAndHalfHours.ToList();
                    foreach (var item in all2CourseHour)
                    {
                        db.programInstituteOneAndHalfHours.Remove(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteProgramInstituteFor2()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var all2CourseHour = db.programInstitutes.ToList();
                    foreach (var item in all2CourseHour)
                    {
                        db.programInstitutes.Remove(item);
                        db.SaveChanges();
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
