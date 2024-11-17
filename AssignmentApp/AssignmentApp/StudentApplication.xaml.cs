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
    /// Interaction logic for StudentApplication.xaml
    /// </summary>
    public partial class StudentApplication : Page
    {
        SystemSchoolEntities1 Db  =new SystemSchoolEntities1();
        public StudentApplication()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dpartmenttxt.Text == "" || Agtxt.Text == "" || Adresstxt.Text == "" || Namtxt.Text == "")
            {
                MessageBox.Show("Filed reiqred");
                return;
            }

            var student = Db.Students.Where(stu => stu.Sname == Namtxt.Text).FirstOrDefault();
            if(student != null)
            {

            Department d = new Department();
            d.Dname = Dpartmenttxt.Text;


            student.Department = d;
            student.Saddress = Adresstxt.Text;
            student.Sage =int.Parse(Agtxt.Text);
            student.DepId = d.DId;

            Db.SaveChanges();
            MessageBox.Show("saved successfully");
                this.NavigationService.Navigate(new Login());
            }else
            {
                MessageBox.Show("error");
            }
        }
    }
}
