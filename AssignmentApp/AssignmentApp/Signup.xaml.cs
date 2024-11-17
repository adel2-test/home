using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        SystemSchoolEntities1 Db = new SystemSchoolEntities1();
        public Signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Passwordtxt.Text == "" || Nametxt.Text == ""  || Emailtxt.Text == "" || ConfirmPasswordtxt.Text == "")
            {
                MessageBox.Show("Requir field");
                return;
            }

            if (Passwordtxt.Text != ConfirmPasswordtxt.Text)
            {
                MessageBox.Show("Password dont match");
                return;
            }

            if( !(Regex.IsMatch(Passwordtxt.Text , "[a-z]") && Regex.IsMatch(Passwordtxt.Text, "[A-Z]") && Regex.IsMatch(Passwordtxt.Text, "[& - ! - % - ~ - #]") && Regex.IsMatch(Passwordtxt.Text , "[\\d]")) )
            {
                MessageBox.Show("Weak Password");
                return;
            }

            //var student = Db.Students.Where(stu => stu.Sname == Nametxt.Text || stu.Spassword == Passwordtxt.Text).First();
            //if(student.Sname != null || student.Sname != "")
            //{
            //    MessageBox.Show("Taken UserName");
            //    return;
            //}

            var student = Db.Students.Where(stu => stu.Sname == Nametxt.Text).FirstOrDefault();

            if(student != null)
            {
                MessageBox.Show("Name already ecxits");
                return;
            }


            Student s = new Student();
            s.Spassword = Passwordtxt.Text;
            s.Sname = Nametxt.Text;
            s.email = Emailtxt.Text;
            Db.Students.Add(s);
            Db.SaveChanges();
            MessageBox.Show("Logged in Succeffully");
            this.NavigationService.Navigate(new Login());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }
    }
}
