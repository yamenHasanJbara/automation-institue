using A2Z_.Models;
using A2Z_.Views.Add_Folder;
using A2Z_.Views.Cafe;
using A2Z_.Views.Display_Folder;
using A2Z_.Views.Payments;
using A2Z_.Views.ProgramInstitute;
using A2Z_.Views.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
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

namespace A2Z_.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        public List<Course> courses { get; set; }



        public Home()
        {
            InitializeComponent();
            MoveCourseToFinishedCourse();
            DeleteFinshedCourseFromProgramInstitute();
            
        }

        

        private void DeleteFinshedCourseFromProgramInstitute()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    DateTime dateTime = DateTime.Today;
                    var _CourseEndedToDelete = db.programInstitutes.Include(x=>x.course).Where(x=>x.course.End_Date <= dateTime).ToList();
                    foreach (var item in _CourseEndedToDelete)
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

        private bool BuildAmessageBoxWithYesAndNo(string CourseName)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("هل انت متأكد من تعديل تاريخ الكورس؟" + " " + CourseName, "تأكيد التعديل", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void MoveCourseToFinishedCourse()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    DateTime dateTime = DateTime.Today;
                    var _finished_courses = db.Courses.Include(x => x.section).Include(x => x.faculty).Include(x => x.Year).Include(x => x.teacher).Include(x => x.material_Study).Include(x => x.Student_Courses).Where(x => x.End_Date < dateTime && x.IsFinished == false).ToList();
                    courses = _finished_courses;
                    foreach (var item in courses)
                    {
                        item.IsFinished = true;
                        db.Courses.Update(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }



        

        private void Show_Add_Page(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;

                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Add)
                    {
                        isWindowOpen = true;
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            w.Activate();
                        }
                    }
                }

                if (!isWindowOpen)
                {
                    Add add = new Add();
                    add.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Display)
                    {
                        isWindowOpen = true;
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            w.Activate();
                        }
                    }
                }

                if (!isWindowOpen)
                {
                    Display display = new Display();
                    display.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void Payments_Page(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is payments)
                    {
                        isWindowOpen = true;
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            w.Activate();
                        }
                    }
                }

                if (!isWindowOpen)
                {
                    payments payments = new payments();
                    payments.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Cafeteria)
                    {
                        isWindowOpen = true;
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            w.Activate();
                            
                        }
                    }
                }

                if (!isWindowOpen)
                {
                    Cafeteria cafeteria = new Cafeteria();
                    cafeteria.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                User user = new User();
                using (var db = new DataBaseContext())
                {
                    user = db.Users.SingleOrDefault(x => x.Status == 2);
                    user.Status = 1;
                    db.Users.Update(user);
                    db.SaveChanges();
                    Login login = new Login();
                    login.Show();
                    this.Close();
                    foreach (Window w in Application.Current.Windows)
                    {
                        if (w is Login )
                        {
                            continue;
                        }
                        else
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void ListViewItem_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Permession permession = new Permession();
                bool check = permession.CheckIfAdminOrUser();
                if (check)
                {
                    AddUsers addUser = new AddUsers();
                    addUser.Show();
                }
                else
                {
                    MessageBox.Show("لا يمكن الوصول إلى هنا");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool isWindowOpen = false;
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is ProgramStatus)
                    {
                        isWindowOpen = true;
                        if (w.WindowState == WindowState.Minimized)
                        {
                            w.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            w.Activate();
                        }
                    }
                }

                if (!isWindowOpen)
                {
                    ProgramStatus programStatus = new ProgramStatus();
                    programStatus.Show();
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
                Login login = new Login();
                login.Show();
                this.Close();
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Login)
                    {
                        continue;
                    }
                    else
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

    }
}
