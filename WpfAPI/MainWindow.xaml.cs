using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfAPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Employee  employee = new Employee();
        IEnumerable<Employee> mVCEmployeeModels;

        public MainWindow()
        {
            InitializeComponent();          
            
        }

        public void GetEmployeeDetailsAPI()
        {
            try
            {
                HttpResponseMessage httpResponseMessage = GlobalClient.httpClient.GetAsync("Employees").Result;
                mVCEmployeeModels = httpResponseMessage.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                dataEmployeeDetails.ItemsSource = mVCEmployeeModels.ToList();
                this.DataContext = employee;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString(), ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }          

        }

        private void dataGridStudent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            FrameworkElement stud_ID = dataEmployeeDetails.Columns[0].GetCellContent(e.Row);
            if (stud_ID.GetType() == typeof(TextBox))
            {
                employee.EmployeeID = Convert.ToInt32(((TextBox)stud_ID).Text);
            }

            FrameworkElement stud_Name = dataEmployeeDetails.Columns[1].GetCellContent(e.Row);
            if (stud_Name.GetType() == typeof(TextBox))
            {
                employee.Name = ((TextBox)stud_Name).Text;
            }

            FrameworkElement stud_Email = dataEmployeeDetails.Columns[2].GetCellContent(e.Row);
            if (stud_Email.GetType() == typeof(TextBox))
            {
                employee.Position = ((TextBox)stud_Email).Text;
            }

            FrameworkElement stud_Class = dataEmployeeDetails.Columns[3].GetCellContent(e.Row);
            if (stud_Class.GetType() == typeof(TextBox))
            {
                employee.Age = Convert.ToInt32(((TextBox)stud_Class).Text);
            }

            FrameworkElement stud_Address = dataEmployeeDetails.Columns[4].GetCellContent(e.Row);
            if (stud_Address.GetType() == typeof(TextBox))
            {
                employee.Salary = Convert.ToInt32(((TextBox)stud_Address).Text);
            }

        }

        private void dataGridStudent_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if(employee.EmployeeID == 0)
            {
                HttpResponseMessage httpResponseMessage = GlobalClient.httpClient.PostAsJsonAsync("Employees", employee).Result;
                GetEmployeeDetailsAPI();
                MessageBox.Show("Record Inserted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                HttpResponseMessage httpResponseMessage = GlobalClient.httpClient.PutAsJsonAsync("Employees/" + employee.EmployeeID, employee).Result;
                GetEmployeeDetailsAPI();
                MessageBox.Show("Record Updated Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
        }

        private void dataGridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRefreshRecord_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeDetailsAPI();
        }
    }
}
