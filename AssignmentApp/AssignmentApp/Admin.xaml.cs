using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public partial class Admin : Page
    {
        SystemSchoolEntities1 Db = new SystemSchoolEntities1();   
        public Admin()
        {
            InitializeComponent();
            Refresh();
            comboBox.ItemsSource = Db.Departments.Select(dep => dep.Dname).Distinct().ToList();
        }

        public void Refresh()
        {
            students.ItemsSource = Db.Students.Select(e => new {e.ID, e.Sname, e.Saddress, e.Department.Dname }).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentId.Text == "")
            {
                return;
            }
            int id = int.Parse(studentId.Text);
            var stu = Db.Students.Where(s => s.ID == id).FirstOrDefault();
            if(stu != null) { 
            Db.Students.Remove(stu);
            Db.SaveChanges();
            MessageBox.Show("Deleted Successfully");
            }
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (studentId.Text == "" || DepName.Text == "")
            {
                MessageBox.Show("Fileds Are reqiured");
                return;
            }

            int id = int.Parse(studentId.Text);
            var student = Db.Students.Where(s => s.ID == id).First();
            if (student != null)
            {
                if (student.Department?.Dname == "" || student.Department?.Dname == null)
                {
                    Department d = new Department();
                    d.Dname = DepName.Text;
                    student.Department = d;
                    student.Department.DId = d.DId;
                }
                else
                {
                   student.Department.Dname = DepName.Text;
                }
              Db.SaveChanges();
              MessageBox.Show("Updated Successfully");
            }
            Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(search.Text == "")
            {
                Refresh();
            }
            else
            {
                var selectedItwem = comboBox.SelectedItem.ToString();
                if (selectedItwem != "")
                {
                var students = Db.Students.Where(s => s.Sname.Contains(search.Text) && s.Department.Dname == selectedItwem).Select(stu => new { stu.ID, stu.Sname, stu.Saddress, stu.Department.Dname });
                this.students.ItemsSource = students.ToList();
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedITtem = comboBox.SelectedItem.ToString();
            var Students = Db.Students.Where(s => s.Department.Dname ==  selectedITtem).Select(stu => new { stu.Department.DId, stu.Sname , stu.Saddress , stu.Department.Dname }).Distinct();
            students.ItemsSource = Students.ToList();
        }
    }
}
