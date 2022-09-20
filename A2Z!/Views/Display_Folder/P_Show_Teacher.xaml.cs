using A2Z_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

namespace A2Z_.Views.Display_Folder
{
    /// <summary>
    /// Interaction logic for P_Show_Teacher.xaml
    /// </summary>
    public partial class P_Show_Teacher : Page
    {
        public List<Teacher> teachers { get; set; }
        public IGrouping<Teacher,Course> teacher_course { get; set; }
        public P_Show_Teacher()
        {
            InitializeComponent();
            Load_Teachers();
        }

        private void Load_Teachers()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Teachers = db.Teachers.ToList();
                    teachers = _Teachers;
                    Teacher.ItemsSource = teachers;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Teacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedTeacher = Teacher.SelectedItem as Teacher;
            if (SelectedTeacher != null)
            {
                try
                {
                    using (var db = new DataBaseContext())
                    {
                        Teacher teacher = new Teacher();
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        Name.Text = teacher.Name;
                        NumberPhone.Text = teacher.Number_Phone.ToString();
                        IsSelection.IsEnabled = true;
                        IsSelection1.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var _Teachers = db.Teachers.Where(x => x.Name.Contains(Search.Text)).ToList();
                    teachers = _Teachers;
                    Teacher.ItemsSource = teachers;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var SelectedTeacher = Teacher.SelectedItem as Teacher;
            if (SelectedTeacher != null)
            {
                try
                {
                    Teacher teacher = new Teacher();
                    using (var db = new DataBaseContext())
                    {
                        teacher = db.Teachers.SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(NumberPhone.Text))
                        {
                            MessageBox.Show("الرجاء تعبئة الحقول");
                        }
                        else
                        {
                            string TeacherName = Name.Text.TrimEnd();
                            bool CheckIfUpdatedNumberIsExist = db.Teachers.Any(x => (x.Number_Phone == NumberPhone.Text) || (x.Name == TeacherName));
                            if (CheckIfUpdatedNumberIsExist)
                            {
                                MessageBox.Show("إن المدرس موجود سابقاً");
                            }
                            else
                            {
                                teacher.Name = Name.Text;
                                teacher.Number_Phone = NumberPhone.Text;
                                db.Update(teacher);
                                db.SaveChanges();
                                MessageBox.Show("تمت عملية التحديث بنجاح");
                                Load_Teachers();
                                Name.Text = null;
                                NumberPhone.Text = null;
                                IsSelection.IsEnabled = false;
                                IsSelection1.IsEnabled = false;
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            { }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedTeacher = Teacher.SelectedItem as Teacher;
                if (SelectedTeacher != null)
                {
                    Teacher teacher = new Teacher();
                    using (var db = new DataBaseContext())
                    {
                        teacher = db.Teachers.Include(x => x.courses).SingleOrDefault(x => x.Teacher_Id == SelectedTeacher.Teacher_Id);
                        if (String.IsNullOrWhiteSpace(Name.Text) || String.IsNullOrWhiteSpace(NumberPhone.Text))
                        {
                            MessageBox.Show("الرجاء تعبئة الحقول");
                        }
                        else
                        {
                            db.Remove(teacher);
                            db.SaveChanges();
                            MessageBox.Show("تمت عملية الحذف بنجاح");
                            Load_Teachers();
                            Name.Text = null;
                            NumberPhone.Text = null;
                            IsSelection.IsEnabled = false;
                            IsSelection1.IsEnabled = false;
                        }
                    }

                }
                else
                { }
            }
            catch (Exception)
            {

                MessageBox.Show("لا يمكن حذف استاذ لديه دورات");
            }
           
        }

        /*this event to show the courses for the teacher and display the payments for him and all the information*/
        private void Teacher_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var SelectedTeacher = Teacher.SelectedItem as Teacher;
                Permession permession = new Permession();
                bool check = permession.CheckIfAdminOrUser();
                if (check)
                {
                    if (SelectedTeacher != null)
                    {
                        Show_All_Teacher_Details show_All_Teacher_Details = new Show_All_Teacher_Details(SelectedTeacher.Teacher_Id);
                        show_All_Teacher_Details.Show();
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
    }
}
