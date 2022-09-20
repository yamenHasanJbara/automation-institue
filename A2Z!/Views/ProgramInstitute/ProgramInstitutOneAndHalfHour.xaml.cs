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
using System.Xml;

namespace A2Z_.Views.ProgramInstitute
{
    /// <summary>
    /// Interaction logic for ProgramInstitutOneAndHalfHour.xaml
    /// </summary>
    public partial class ProgramInstitutOneAndHalfHour : Page
    {

        private List<IGrouping<string, ProgramInstituteC>> programInstituteCs;

        public List<IGrouping<string, ProgramInstituteOneAndHalfHour>> programInstituteOneAndHalfHours { get; set; }

        public string NameOFCell { get; set; }

        public ProgramInstitutOneAndHalfHour()
        {
            InitializeComponent();
            LoadInfo();
            //LoadInterfaceAccordingToCourseTime();
        }

        private void LoadInfo()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    SetTimeForCourse setTimeForCourse = new SetTimeForCourse();
                    setTimeForCourse = db.setTimeForCourses.Find(1);
                    FirstTime.Text = setTimeForCourse.StartTime;
                    double first = double.Parse(setTimeForCourse.StartTime);
                    if (first <= 12.5)
                    { }
                    else
                    {
                        first -= 12;
                    }
                    double second = first + double.Parse(setTimeForCourse.Courselength);
                    if (second <= 12.5)
                    { }
                    else
                    {
                        second -= 12;
                    }
                    SecondTime.Text = second.ToString();
                    double third = second + double.Parse(setTimeForCourse.Courselength);
                    if (third <= 12.5)
                    { }
                    else
                    {
                        third -= 12;
                    }
                    ThirdTime.Text = third.ToString();
                    double fourth = third + double.Parse(setTimeForCourse.Courselength);
                    if (fourth <= 12.5)
                    { }
                    else
                    {
                        fourth -= 12;
                    }
                    FourthTime.Text = fourth.ToString();
                    double five = fourth + double.Parse(setTimeForCourse.Courselength);
                    if (five <= 12.5)
                    { }
                    else
                    {
                        five -= 12;
                    }
                    FifthTime.Text = five.ToString();

                    double six = five + double.Parse(setTimeForCourse.Courselength);
                    if (six <= 12.5)
                    { }
                    else
                    {
                        six -= 12;
                    }
                    SixTime.Text = six.ToString();

                    double seven = six + double.Parse(setTimeForCourse.Courselength);
                    if (seven <= 12.5)
                    { }
                    else
                    {
                        seven -= 12;
                    }
                    SevenTime.Text = seven.ToString();


                    double eight = seven + double.Parse(setTimeForCourse.Courselength);
                    if (eight <= 12.5)
                    { }
                    else
                    {
                        eight -= 12;
                    }
                    EightTime.Text = eight.ToString();
                }
                loadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadCourses()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _instituteHall = db.programInstituteOneAndHalfHours.Include(x => x.course).Include(x => x.course.material_Study).AsEnumerable().GroupBy(x => x.day).ToList();
                    programInstituteOneAndHalfHours = _instituteHall;
                    foreach (var item in programInstituteOneAndHalfHours)
                    {
                        foreach (var ProgramInstituteOneAndHalfHour in item)
                        {
                            switch (ProgramInstituteOneAndHalfHour.hall)
                            {
                                case "T1":
                                    {
                                        switch (ProgramInstituteOneAndHalfHour.hour)
                                        {
                                            case "8":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "930":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "11":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "1230":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "2":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "330":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "5":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "630":
                                                {
                                                    if (item.Key == "Friday") Friday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T1_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                        break;
                                    }

                                case "T2":
                                    {
                                        switch (ProgramInstituteOneAndHalfHour.hour)
                                        {
                                            case "8":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "930":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "11":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "1230":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "2":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "330":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "5":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "630":
                                                {
                                                    if (item.Key == "Friday") Friday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T2_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                        break;
                                    }

                                case "T3":
                                    {
                                        switch (ProgramInstituteOneAndHalfHour.hour)
                                        {
                                            case "8":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_8.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "930":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_930.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "11":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_11.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "1230":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_1230.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "2":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_2.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "330":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_330.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }

                                            case "5":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_5.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            case "630":
                                                {
                                                    if (item.Key == "Friday") Friday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Thursday") Thursday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Wednesday") Wednesday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Tuesday") Tuesday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Monday") Monday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Sunday") Sunday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    if (item.Key == "Saturday") Saturday_T3_630.Content = ProgramInstituteOneAndHalfHour.course.material_Study.Name + "  " + ProgramInstituteOneAndHalfHour.hall + "   " + ProgramInstituteOneAndHalfHour.course.Group;
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                default:
                                    break;
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

        




        // this event for 1.30 course time
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridCell dataGridCell = sender as DataGridCell;
                Add_NewScheduleForOneAndHalfHour add_NewScheduleForOneAndHalfHour = new Add_NewScheduleForOneAndHalfHour(dataGridCell);
                add_NewScheduleForOneAndHalfHour.Show();
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is ProgramStatus)
                    {
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.Close();
                        }
                        else
                        {
                            w.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        //this is for delete course from 1.30 hour courseTime
        private void Friday_T1_8_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridCell dataGridCell = sender as DataGridCell;
            this.NameOFCell = dataGridCell.Name;
            if (e.Key == Key.Delete)
            {
                try
                {
                    string[] s = NameOFCell.Split('_');
                    string Day = s[0];
                    string Hall = s[1];
                    string Time = s[2];
                    using (var db = new DataBaseContext())
                    {
                        ProgramInstituteOneAndHalfHour programInstituteC = new ProgramInstituteOneAndHalfHour();
                        programInstituteC = db.programInstituteOneAndHalfHours.Include(x => x.course).SingleOrDefault(x => (x.day == Day) && (x.hall == Hall) && (x.hour == Time));
                        if (programInstituteC != null)
                        {
                            db.programInstituteOneAndHalfHours.Remove(programInstituteC);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            foreach (Window w in Application.Current.Windows)
                            {
                                if (w is ProgramStatus)
                                {
                                    if (w.WindowState == WindowState.Minimized)
                                    {
                                        w.Close();
                                    }
                                    else
                                    {
                                        w.Close();
                                    }
                                }
                            }
                            ProgramStatus programStatus = new ProgramStatus();
                            programStatus.Show();
                        }
                        else
                        {
                            MessageBox.Show("لا يوجد شيء للحذف");
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
}
