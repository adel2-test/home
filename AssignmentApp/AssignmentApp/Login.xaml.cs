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

namespace AssignmentApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        SystemSchoolEntities1 Db = new SystemSchoolEntities1();
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Signup());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Emaltxt.Text == "" || Psswordtxt.Text == "")
            {
                MessageBox.Show("unfilled fields");
                return;
            }

            if(Psswordtxt.Text == "1" && Emaltxt.Text == "1")
            {
             this.NavigationService.Navigate(new Admin());
             MessageBox.Show("Logged in as admin scussgully");
             return;
            }


            var student = Db.Students.Where(stu => stu.email == Emaltxt.Text && stu.Spassword == Psswordtxt.Text).FirstOrDefault();
            if(student != null)
            {
                MessageBox.Show("Logged in Succefully");
                this.NavigationService.Navigate(new StudentApplication());
            }
            else
            {
                MessageBox.Show("Incorrect username or passsword");
            }
        }
    }
}
