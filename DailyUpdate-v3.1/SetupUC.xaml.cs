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
using System.Data;
using System.ComponentModel;



namespace DailyUpdate_v3._1
{
    /// <summary>
    /// Interaction logic for SetupUC.xaml
    /// </summary>
    public partial class SetupUC : UserControl
    {
        private string idEmployee, id_user;
        public SetupUC()
        {
            InitializeComponent();
            MyData data = new MyData();
            //MessageBox.Show(data.Privilege_employee);
            //MessageBox.Show(data.HostNameDb);
            
            if (data.Administrator == "0")
            {
                gridAdministrator1.Visibility = Visibility.Collapsed;
                gridAdministrator2.Visibility = Visibility.Collapsed;
                dataGridViewUser.Visibility = Visibility.Collapsed;
                borderAdministrator.Visibility = Visibility.Collapsed;
            }

            if (data.PreClean == "0") { RdButPreClean.Visibility = Visibility.Collapsed; }
            if (data.Testing == "0") { RdButTesting.Visibility = Visibility.Collapsed; }
            if (data.Repaire == "0") { RdButDebug.Visibility = Visibility.Collapsed; }
            if (data.Debug == "0") { RdButRepaire.Visibility = Visibility.Collapsed; }
            if (data.FinalQC == "0") { RdButPreFinalQc.Visibility = Visibility.Collapsed; }
            if (data.Report == "0") { RdButReport.Visibility = Visibility.Collapsed; }

            id_user = data.Id_employee;
            //MessageBox.Show(id_user);
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {// Hide collums  in DataGrid wpf: AutoGeneratingColumn="OnAutoGeneratingColumn"
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "id_employee")
            {
                e.Cancel = true;
            }


        }
        private void addDeleteUpdateEmployee(string query)
        {
            MyData data = new MyData();

            data.addDeleteUpdateData(query);
        }

        private void selectData(string selectQuery)
        {
            MyData data = new MyData();
            DataTable dt = data.fillMyAdapter(selectQuery);


            dt.Columns[1].ColumnName = "First name";
            dt.Columns[2].ColumnName = "Last name";
            dt.Columns[3].ColumnName = "Initials";
            dt.Columns[4].ColumnName = "Dismissed";
            dt.Columns[5].ColumnName = "Login";
            dt.Columns[6].ColumnName = "Password";
            //dt.Columns[7].ColumnName = "Privilege";
            

            dataGridViewUser.DataContext = dt.DefaultView;
            //dataGridViewUser.Columns[0].Header = "Field1";

            //dataGridViewUser.Columns[0].Visibility = Visibility.Hidden;
            dataGridViewUser.CanUserAddRows = false;
            dataGridViewUser.CanUserDeleteRows = false;
        }

        private void clearTextBox()
        {
            TextBoxFirstName.Clear();
            TextBoxLastName.Clear();
            TextBoxInitials.Clear();
            TextBoxLoginUser.Clear();
            TextBoxPasswordUser.Clear();
            ChkBoxAdministrator.IsChecked = false;
            ChkBoxDismissed.IsChecked = false;
            ChkBoxPreClean.IsChecked = false;
            ChkBoxTesting.IsChecked = false;
            ChkBoxDebug.IsChecked = false;
            ChkBoxRepaire.IsChecked = false;
            ChkBoxFinalQC.IsChecked = false;
            ChkBoxReport.IsChecked = false;

            //ChkBoxOrder.IsChecked = false;
            btAddUser.Visibility = Visibility.Visible;
            btDeleteUser.Visibility = Visibility.Hidden;
            btUpdateUser.Visibility = Visibility.Hidden;
        }
        private void dataGridViewUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            
            if (row_selected != null)
            {
                idEmployee = row_selected["id_employee"].ToString();
                TextBoxFirstName.Text = row_selected["First name"].ToString();
                TextBoxLastName.Text = row_selected["Last name"].ToString();
                TextBoxInitials.Text = row_selected["Initials"].ToString();
                string s = row_selected["employee_privilege"].ToString();

                if (s[0].ToString() == "1")
                {
                    ChkBoxAdministrator.IsChecked = true;
                }
                if (s[1].ToString() == "1")
                {
                    ChkBoxDismissed.IsChecked = true;
                }
                if (s[2].ToString() == "1")
                {
                    ChkBoxPreClean.IsChecked = true;
                }
                if (s[3].ToString() == "1")
                {
                    ChkBoxTesting.IsChecked = true;
                }
                if (s[4].ToString() == "1")
                {
                    ChkBoxDebug.IsChecked = true;
                }
                if (s[5].ToString() == "1")
                {
                    ChkBoxRepaire.IsChecked = true;
                }
                if (s[6].ToString() == "1")
                {
                    ChkBoxFinalQC.IsChecked = true;
                }
                if (s[7].ToString() == "1")
                {
                    ChkBoxReport.IsChecked = true;
                }

                /*
            string s = dt.Rows[0][7].ToString();

            data.Administrator = s[0].ToString();

            data.Dismissed = s[1].ToString();
            data.PreClean = s[2].ToString();
            data.Testing = s[3].ToString();
            data.Repaire = s[4].ToString();
            data.Debug = s[5].ToString();
            data.FinalQC = s[6].ToString();
            */
                btAddUser.Visibility = Visibility.Hidden;
                btDeleteUser.Visibility = Visibility.Visible;
                btUpdateUser.Visibility = Visibility.Visible;
                TextBoxLoginUser.Clear();
                TextBoxPasswordUser.Clear();
            }

        }
        
        private void borderAdministrator_Loaded(object sender, RoutedEventArgs e)
        {
            btDeleteUser.Visibility = Visibility.Hidden;
            btUpdateUser.Visibility = Visibility.Hidden;


            string selectQuery = "SELECT id_employee, employee_first_name, employee_last_name, employee_initials, employee_dismissed, employee_login, employee_password, employee_privilege  FROM tb_employee WHERE id_employee != '" + id_user + "' order by  employee_last_name";
            //Clipboard.SetDataObject(selectQuery);
            selectData(selectQuery);


            // Clear current sort descriptions

            //(dataGridViewUser.ItemsSource as DataView).Sort = "employeelast_name";
            //Get property name to apply sort based on desired column 

            //dataGridViewUser.SortDescriptions.Add(new SortDescription("ID", direction));

            //ICollectionView dataView = CollectionViewSource.GetDefaultView(myDataGrid.ItemsSource);

        }
 
       
        private void btAddUser_Click(object sender, RoutedEventArgs e)
        {
            //string selectQuery = "SELECT id_employee, employee_first_name, employee_last_name, employee_initials, employee_dismissed, employee_login, employee_password, employee_privilege  FROM tb_employee order by  employee_last_name DESC";
            int flag = 0;

            if (TextBoxFirstName.Text == string.Empty )
            {
                TextBoxFirstName.Background = new SolidColorBrush(Colors.Red);
                flag = 1;
            }
            if (TextBoxLastName.Text == string.Empty)
            {
                TextBoxLastName.Background = new SolidColorBrush(Colors.Red);
                flag = 1;
            }
            if (TextBoxInitials.Text == string.Empty)
            {
                TextBoxInitials.Background = new SolidColorBrush(Colors.Red);
                flag = 1;
            }
            if (TextBoxLoginUser.Text == string.Empty)
            {
                TextBoxLoginUser.Background = new SolidColorBrush(Colors.Red);
                flag = 1;
            }
            if (TextBoxPasswordUser.Password.ToString() == string.Empty)
            {
                TextBoxPasswordUser.Background = new SolidColorBrush(Colors.Red);
                flag = 1;
            }
            if (flag != 1)
            {
                string query = "INSERT INTO `tb_employee`(`employee_first_name`, `employee_last_name`, `employee_initials`, `employee_login`, `employee_password`, `employee_privilege`) VALUES('" + TextBoxFirstName.Text + "', '" + TextBoxLastName.Text + "', '" + TextBoxInitials.Text + "', '" + TextBoxLoginUser.Text + "', '"+ TextBoxPasswordUser.Password.ToString() + "', '";

                /*
                if (ChkBoxAdministrator.IsChecked ?? true) {query = query +  "'1";}
                else { query += "'0";}
                if (ChkBoxDismissed.IsChecked ?? true) { query = query + "1"; }
                else { query += "0";}
                if (ChkBoxPreClean.IsChecked ?? true) { query = query + "1"; }
                else { query +=  "0"; }
                if (ChkBoxTesting.IsChecked ?? true) { query = query + "1"; }
                else { query += "0";}
                if (ChkBoxDebug.IsChecked ?? true) { query = query +  "1"; }
                else { query += "0";}
                if (ChkBoxRepaire.IsChecked ?? true) { query = query + "1"; }
                else { query += "0"; }
                if (ChkBoxFinalQC.IsChecked ?? true) { query = query + "1)"; }
                else { query += "0"; }
                if (ChkBoxReport.IsChecked ?? true) { query = query + "1')"; }
                else { query += "0')"; }
                */
                //Clipboard.SetDataObject(query.ToString());

                query = Checked_box(query) + ")";
                addDeleteUpdateEmployee(query);
                //Clipboard.SetDataObject(query.ToString());
                string selectQuery = "SELECT id_employee, employee_first_name, employee_last_name, employee_initials, employee_dismissed, employee_login, employee_password, employee_privilege  FROM tb_employee WHERE id_employee != '" + id_user + "' order by  employee_last_name";

                selectData(selectQuery);
                clearTextBox();
            }
           
        }
        private void btDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //dt.Columns[0].ColumnName;
            //MessageBox.Show(idEmployee);
            //Clipboard.SetDataObject(id);
            string query = "DELETE FROM `tb_employee` WHERE id_employee = " + idEmployee;
            addDeleteUpdateEmployee(query);
            string selectQuery = "SELECT id_employee, employee_first_name, employee_last_name, employee_initials, employee_dismissed, employee_login, employee_password, employee_privilege  FROM tb_employee WHERE id_employee != '" + id_user + "' order by  employee_last_name";
            selectData(selectQuery);
            clearTextBox();
        }

        private void btUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE `tb_employee` SET employee_first_name = '" + TextBoxFirstName.Text + "', employee_last_name = '" + TextBoxLastName.Text + "', employee_initials =  '" + TextBoxInitials.Text + "'";

            if (TextBoxLoginUser.Text != string.Empty)
            {
                query += ", employee_login ='" + TextBoxLoginUser.Text + "'";
            }

            if (TextBoxPasswordUser.Password.ToString() != string.Empty)
            {
                query += ", employee_password ='" + TextBoxPasswordUser.Password.ToString() + "'";
            }


            query += ", employee_privilege = '";

            /*
            if (ChkBoxAdministrator.IsChecked ?? true) { query += "1"; }
            else { query += "0"; }
            if (ChkBoxDismissed.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxPreClean.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxTesting.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxDebug.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxRepaire.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxFinalQC.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxReport.IsChecked ?? true) { query = query + "1'"; }
            else { query = query + "0'"; }
            */

            //query = Check_box(query);

            query = Checked_box(query) + " WHERE id_employee = " + idEmployee;

            //query = "UPDATE `tb_employee` SET employee_first_name = '" + TextBoxFirstName.Text + "', employee_last_name = '" + TextBoxLastName.Text + "', employee_initials =  '" + TextBoxInitials.Text + "', employee_login ='" + TextBoxLoginUser.Text + "', employee_password = '" + TextBoxPasswordUser.Password.ToString() + "' WHERE id_employee = " + idEmployee;
            //Clipboard.SetDataObject(query);
            addDeleteUpdateEmployee(query);
            string selectQuery = "SELECT id_employee, employee_first_name, employee_last_name, employee_initials, employee_dismissed, employee_login, employee_password, employee_privilege  FROM tb_employee WHERE id_employee != '" + id_user + "' order by  employee_last_name";
            selectData(selectQuery);
            clearTextBox();
        }
        private string Checked_box(string query)
        {
            if (ChkBoxAdministrator.IsChecked ?? true) { query += "1"; }
            else { query += "0"; }
            if (ChkBoxDismissed.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxPreClean.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxTesting.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxDebug.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxRepaire.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxFinalQC.IsChecked ?? true) { query = query + "1"; }
            else { query += "0"; }
            if (ChkBoxReport.IsChecked ?? true) { query = query + "1'"; }
            else { query = query + "0'"; }

            return (query);

        }
       

        private void TextBoxFirstName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxFirstName.Background = new SolidColorBrush(Colors.White);
        }

        private void TextBoxLastName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxLastName.Background = new SolidColorBrush(Colors.White);
        }

        private void TextBoxInitials_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxInitials.Background = new SolidColorBrush(Colors.White);
        }

        private void TextBoxLoginUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxLoginUser.Background = new SolidColorBrush(Colors.White);
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            clearTextBox();
        }

        private void ChkBoxDismissed_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxAdministrator.IsChecked = false;
            ChkBoxPreClean.IsChecked = false;
            ChkBoxTesting.IsChecked = false;
            ChkBoxDebug.IsChecked = false;
            ChkBoxRepaire.IsChecked = false;
            ChkBoxFinalQC.IsChecked = false;
            ChkBoxReport.IsChecked = false;
        }

        private void ChkBoxAdministrator_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxPreClean_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxTesting_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxDebug_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxRepaire_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxFinalQC_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void ChkBoxReport_Checked(object sender, RoutedEventArgs e)
        {
            ChkBoxDismissed.IsChecked = false;
        }

        private void TextBoxPasswordUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxPasswordUser.Background = new SolidColorBrush(Colors.White);
        }

        
    }
}
