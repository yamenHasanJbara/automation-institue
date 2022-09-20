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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {

        public Add()
        {
            InitializeComponent();
            GridMain.Navigate(new P_Add_Student());
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(15 + (140 * index), 40, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Navigate(new P_Add_Student());
                    break;
                case 1:
                    GridMain.Navigate(new P_Add_Existing_Student());
                    break;
                case 2:
                    GridMain.Navigate(new P_Add_Course_());
                    break;
                case 3:
                    GridMain.Navigate(new P_Add_Teacher());
                    break;
                case 4:
                    GridMain.Navigate(new P_Add_MaterialStudy());
                    break;
                case 5:
                    GridMain.Navigate(new P_Add_Section());
                    break;
                case 6:
                    GridMain.Navigate(new Add_Collage());
                    break;
                case 7:
                    GridMain.Navigate(new P_Add_Year());
                    break;
                case 8:
                    GridMain.Navigate(new P_Add_Term());
                    break;

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
