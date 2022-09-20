using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
    /// Interaction logic for ShowCourseDetails.xaml
    /// </summary>
    public partial class ShowCourseDetails : Window
    {

        public List<IGrouping<Course, ProgramInstituteC>> programInstituteCs { get; set; }

        public List<ProgramInstituteC> programInstitutes { get; set; }

        public List<ShowCourseDetailsWhenClickOnShow> showCourseDetailsWhenClickOns { get; set; }


        public int courseId { get; set; }
        public ShowCourseDetails(int CourseId)
        {
            InitializeComponent();
            this.courseId = CourseId;
            LoadCourseInfoOneAndHalfHour(courseId);



        }

        private void LoadCourseInfoOneAndHalfHour(int courseId)
        {
            try
            {
                
                using (var db = new DataBaseContext())
                {
                    Course course = new Course();
                    course = db.Courses.Include(x => x.material_Study).SingleOrDefault(x => x.Course_Id == courseId);
                    var _CourseDetails = db.programInstituteOneAndHalfHours.Include(x => x.course).Where(x => x.course == course).AsEnumerable().GroupBy(x => x.course).ToList();
                    ShowCourseDetailsWhenClickOnShow showCourseDetailsWhenClickOnShow = new ShowCourseDetailsWhenClickOnShow();
                    List<ProgramInstituteOneAndHalfHour> program = new List<ProgramInstituteOneAndHalfHour>();
                    SetTimeForCourse setTimeForCourse = new SetTimeForCourse();
                    setTimeForCourse = db.setTimeForCourses.Find(1);
                    if (_CourseDetails != null)
                    {
                        showCourseDetailsWhenClickOnShow.course = course;
                        showCourseDetailsWhenClickOnShow.Start_Date = course.Start_Date;
                        showCourseDetailsWhenClickOnShow.End_Date = course.End_Date;
                        foreach (var item in _CourseDetails)
                        {
                            foreach (var ProgramInstituteC in item)
                            {
                                ProgramInstituteOneAndHalfHour programInstituteC = new ProgramInstituteOneAndHalfHour();
                                programInstituteC.course = item.Key;
                                switch (ProgramInstituteC.day)
                                {
                                    case "Sunday":
                                        {
                                            programInstituteC.day = "الأحد";
                                            break;
                                        }
                                    case "Monday":
                                        {
                                            programInstituteC.day = "الأثنين";
                                            break;
                                        }
                                    case "Tuesday":
                                        {
                                            programInstituteC.day = "الثلاثاء";
                                            break;
                                        }
                                    case "Wednesday":
                                        {
                                            programInstituteC.day = "الأربعاء";
                                            break;
                                        }
                                    case "Thursday":
                                        {
                                            programInstituteC.day = "الخميس";
                                            break;
                                        }
                                    case "Friday":
                                        {
                                            programInstituteC.day = "الجمعة";
                                            break;
                                        }
                                    case "Saturday":
                                        {
                                            programInstituteC.day = "السبت";
                                            break;
                                        }

                                    default:
                                        break;
                                }
                                programInstituteC.hall = ProgramInstituteC.hall;
                                if (setTimeForCourse.Courselength == "1.5"  && setTimeForCourse.StartTime =="8")
                                {
                                    if (ProgramInstituteC.hour =="930")
                                    {
                                        programInstituteC.hour = "9.5";
                                    }
                                    else if (ProgramInstituteC.hour == "1230")
                                    {
                                        programInstituteC.hour = "12.5";
                                    }
                                    else if (ProgramInstituteC.hour == "330")
                                    {
                                        programInstituteC.hour = "3.5";
                                    }
                                    else if (ProgramInstituteC.hour == "630")
                                    {
                                        programInstituteC.hour = "6.5";
                                    }
                                    else
                                    {
                                        programInstituteC.hour = ProgramInstituteC.hour;
                                    }
                                }
                                else
                                {
                                    string newHour = ConvertToRealTime(ProgramInstituteC.hour);
                                    programInstituteC.hour = newHour;
                                }
                                program.Add(programInstituteC);
                            }
                        }
                        showCourseDetailsWhenClickOnShow.programInstituteOneAndHalfHours = program;
                        List<ShowCourseDetailsWhenClickOnShow> courseDetailsWhenClickOnShows = new List<ShowCourseDetailsWhenClickOnShow>();
                        courseDetailsWhenClickOnShows.Add(showCourseDetailsWhenClickOnShow);
                        List<ProgramInstituteC> programInstituteOneAndHalfHours = new List<ProgramInstituteC>();
                        foreach (var item in courseDetailsWhenClickOnShows)
                        {
                            ProgramInstituteC program1 = new ProgramInstituteC();
                            program1.course = item.course;
                            foreach (var item2 in item.programInstituteOneAndHalfHours)
                            {
                                program1.day = item2.day;
                                program1.hall = item2.hall;
                                program1.hour = item2.hour;
                            }
                            programInstituteOneAndHalfHours.Add(program1);
                        }
                        showCourseDetailsWhenClickOnShow.programInstituteCs= programInstituteOneAndHalfHours;
                        showCourseDetailsWhenClickOns = courseDetailsWhenClickOnShows;
                        CourseDetails.ItemsSource = showCourseDetailsWhenClickOns;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string ConvertToRealTime(string hour)
        {
            try
            {
                string Start = "";
                string jump = "";
                using (var db = new DataBaseContext())
                {
                    SetTimeForCourse setTimeForCourse = new SetTimeForCourse();
                    setTimeForCourse = db.setTimeForCourses.Find(1);
                    Start = setTimeForCourse.StartTime;
                    jump = setTimeForCourse.Courselength;
                }
                switch (hour)
                {
                    case "8":
                        {
                            
                            return Start;
                        }

                    case "930":
                        {
                            float hours = float.Parse(Start) + (float.Parse(jump));
                            if (hours >12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                    case "11":
                        {

                            float hours = float.Parse(Start) + (2 * float.Parse(jump));
                            if (hours > 12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                        
                    case "1230":
                        {
                            float hours = float.Parse(Start) + (3 * float.Parse(jump));
                            if (hours > 12.5) 
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                        
                    case "2":
                        {
                            float hours = float.Parse(Start) + (4 * float.Parse(jump));
                            if (hours > 12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                        
                    case "330":
                        {
                            float hours = float.Parse(Start) + (5 * float.Parse(jump));
                            if (hours > 12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                        
                    case "5":
                        {
                            float hours = float.Parse(Start) + (6 * float.Parse(jump));
                            if (hours > 12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                        
                    case "630":
                        {
                            float hours = float.Parse(Start) + (7 * float.Parse(jump));
                            if (hours > 12.5)
                            {
                                hours -= 12;
                                return hours.ToString();
                            }
                            else
                            {
                                return hours.ToString();
                            }
                        }
                    default:
                        return hour;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return hour;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
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

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
