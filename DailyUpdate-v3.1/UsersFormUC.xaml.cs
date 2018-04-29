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
using MySql.Data.MySqlClient;



//recavery
namespace DailyUpdate_v3._1
{

    public partial class UsersFormUC : UserControl
    {
        //String id_table;
        MyData data;
        static string id_main;
        int rowCountSubDataGrid;
        string id_worksheet;
        int firstRowIndexMainDataGridView;
        int secondRowIndexMainDataGridView;
        string activeDataGridView;
        string id_user_main;
        string id_user_sub;
        string id_employee;
        string wo_number;
        string datatimeWO;
        bool finalazeCheckedFlag;





        public UsersFormUC()
        {
            InitializeComponent();
            MyData userId = new MyData();
            id_employee = userId.Id_employee;
            datePickerFrom.SelectedDate = DateTime.Today;
            datePickerTo.SelectedDate = DateTime.Today;
        }
        public void preCleanFinal(string WorkSpace)
        {
            // Main form use it
            lbNameWorkSpace.Content = WorkSpace;
            andForDataTime.Visibility = Visibility.Hidden;
            datePickerTo.Visibility = Visibility.Hidden;
            ChkBoxAllDays.Visibility = Visibility.Hidden;
            //BtDelete.IsEnabled = false;
            //BtUpdate.IsEnabled = false;
            BtFail.Visibility = Visibility.Hidden;
            BtPass.Visibility = Visibility.Hidden;
            ChkFinalize.Visibility = Visibility.Hidden;
            //RdBtAll.Visibility = Visibility.Hidden;
            //RdBtPass.Visibility = Visibility.Hidden;
            //RdBtFail.Visibility = Visibility.Hidden;
        }

        public void finalQc(string WorkSpace)
        {
            
            // Main form use it
            lbNameWorkSpace.Content = WorkSpace;
            //BtDelete.IsEnabled = false;
            //BtUpdate.IsEnabled = false;
            andForDataTime.Visibility = Visibility.Hidden;
            datePickerTo.Visibility = Visibility.Hidden;
            ChkBoxAllDays.Visibility = Visibility.Hidden;
            BtFail.Visibility = Visibility.Hidden;
            BtPass.Visibility = Visibility.Hidden;
            BtSave.Visibility = Visibility.Hidden;
            //RdBtPass.Visibility = Visibility.Hidden;
            //RdBtFail.Visibility = Visibility.Hidden;
        }

        public void debugRepair(string WorkSpace)
        {
            lbNameWorkSpace.Content = WorkSpace;
            andForDataTime.Visibility = Visibility.Hidden;
            datePickerTo.Visibility = Visibility.Hidden;
            ChkBoxAllDays.Visibility = Visibility.Hidden;
            BtSave.Visibility = Visibility.Hidden;
            BtFail.Visibility = Visibility.Hidden;
            BtPass.Visibility = Visibility.Hidden;
            ChkFinalize.Visibility = Visibility.Hidden;
            //RdBtAll.Visibility = Visibility.Visible;
            //RdBtPass.Visibility = Visibility.Visible;
            //RdBtFail.Visibility = Visibility.Visible;
        }

        public void test(string WorkSpace)
        {
            lbNameWorkSpace.Content = WorkSpace;
            BtSave.Visibility = Visibility.Hidden;
            ChkFinalize.Visibility = Visibility.Hidden;
            andForDataTime.Visibility = Visibility.Hidden;
            datePickerTo.Visibility = Visibility.Hidden;
            ChkBoxAllDays.Visibility = Visibility.Hidden;
            //BtFail.Visibility = Visibility.Hidden;
            //BtPass.Visibility = Visibility.Hidden;
            //RdBtAll.Visibility = Visibility.Visible;
            //RdBtPass.Visibility = Visibility.Visible;
            //RdBtFail.Visibility = Visibility.Visible;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // ---------- Populate comboBoxses ---------------



            data = new MyData();
            ComboBox comboBoxName = cmbox_Stage;
            string selectQuery = "SELECT * FROM `tb_stage` ORDER BY stage_type";
            string column_name = "stage_type";
            string id_stage = "id_stage";
            populate_combobox(selectQuery, comboBoxName, column_name, id_stage);
            column_name = "failure_code_type";
            comboBoxName = cmbox_FailureCode;
            id_stage = "id_failure_code";
            selectQuery = "SELECT * FROM `tb_failure_code` ORDER BY failure_code_type";
            populate_combobox(selectQuery, comboBoxName, column_name, id_stage);

            //cmbox_FailureCode.SelectedValue = 1;
            cmbox_Stage.SelectedIndex = 0;
            cmbox_FailureCode.SelectedIndex = 1;
            /*
            DateTime dateValue = DateTime.Now;
            datePickerFrom.SelectedDate = dateValue;
            datePickerTo.SelectedDate = dateValue;
            string dateMysql = dateValue.ToString("yyyy-MM-dd");
            queryForMainDataGridView(dateMysql, dateMysql);
            */



        }

        private void queryForMainDataGridView(string dateFrom, string dateTo)
        {


            string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                "FROM tb_main " +
                "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                "WHERE main_date_create between '" + dateFrom + "' and '" + dateTo + "' and id_tb_employee = '" + data.Id_employee + "'" +
                " order by  id_main desc";

            //Clipboard.SetDataObject(selectQuery);

            populateDataGridViewMain(selectQuery);
        }

        private void populateDataGridViewMain(string selectQuery)
        {

            data = new MyData();
            DataTable dt = data.fillMyAdapter(selectQuery);


            dt.Columns[1].ColumnName = "Create Date";
            dt.Columns[2].ColumnName = "WO";
            dt.Columns[3].ColumnName = "SKU";
            dt.Columns[4].ColumnName = "SN";
            dt.Columns[5].ColumnName = "REF";
            dt.Columns[6].ColumnName = "Finalized";
            dt.Columns[9].ColumnName = "Stage";
            dt.Columns[10].ColumnName = "Name";


            dataGridViewTbMain.DataContext = dt.DefaultView;
            dataGridViewTbMain.CanUserAddRows = false;
            dataGridViewTbMain.CanUserDeleteRows = false;


            //dataGridViewSubTbMain.Items.Refresh();


        }
        private void populate_combobox(string selectQuery, ComboBox comboBoxName, string column_name, string id)
        {
            //comboBoxName.Items.Clear(); // Clear combobox


            MyData myAdapter = new MyData();

            MySqlDataReader reader = myAdapter.MySqlDataReader(selectQuery);
            while (reader.Read())
            {
                comboBoxName.Items.Add(new { ColumnName = reader[column_name].ToString(), ColumnId = reader[id].ToString() });

            }

            myAdapter.connectionClose();

        }
        private void dataGridViewTbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            TextNotesWorkSheet.Text = string.Empty;

            //------------Populate text boxes---------------
            if (row_selected != null)
            {
                datatimeWO = row_selected["Create Date"].ToString();
                textBox_WO.Text = row_selected["WO"].ToString();
                textBox_SKU.Text = row_selected["SKU"].ToString();
                textBox_SN.Text = row_selected["SN"].ToString();
                textBox_REF.Text = row_selected["REF"].ToString();

                wo_number = row_selected["WO"].ToString();
                id_main = row_selected["id_main"].ToString();
                id_user_main = row_selected["id_tb_employee"].ToString();
                string dataFinalized = row_selected["Finalized"].ToString();

                //Clipboard.SetDataObject(selectQuery);
                populateDataGridViewSubTbMain(id_main);
                //MessageBox.Show(id_main.ToString());
                if (dataFinalized != string.Empty)
                {
                    textBox_WO.IsEnabled = false;
                    textBox_SKU.IsEnabled = false;
                    textBox_SN.IsEnabled = false;
                    textBox_REF.IsEnabled = false;

                    BtFail.IsEnabled = false;
                    BtPass.IsEnabled = false;
                    BtAdd.IsEnabled = false;
                    BtDelete.IsEnabled = false;
                    BtUpdate.IsEnabled = false;
                    BtSave.IsEnabled = false;
                    textBoxNotes.IsEnabled = false;
                    finalazeCheckedFlag = true;
                    //dataGridViewSubTbMain.IsEnabled = false;

                    ChkFinalize.IsChecked = true;
                }
            }
            firstRowIndexMainDataGridView = dataGridViewTbMain.Items.IndexOf(dataGridViewTbMain.CurrentItem);
            //MessageBox.Show(currentRowIndex.ToString());

        }

        private void populateDataGridViewSubTbMain(string id_main)
        {

            string selectQuery = "SELECT tb_worksheet .*, tb_employee.employee_first_name, tb_failure_code.failure_code_type FROM tb_worksheet left Join tb_employee ON id_tb_employee = tb_employee.id_employee left join tb_failure_code on id_tb_failure_code = tb_failure_code.id_failure_code WHERE id_tb_main = '" + id_main + "'order by worksheet_date  ";

            data = new MyData();
            DataTable dt = data.fillMyAdapter(selectQuery);

            if (dt != null)
            {
                dt.Columns[1].ColumnName = "Date";
                dt.Columns[2].ColumnName = "Notes";
                dt.Columns[3].ColumnName = "Result";
                dt.Columns[7].ColumnName = "Name";
                dt.Columns[8].ColumnName = "Failure Code";
            }
            //else
            //{
            //dataGridViewSubTbMain.Items.Refresh();
            //MessageBox.Show("dataGridViewSubTbMain Refresh");
            //}


            //Clipboard.SetDataObject(selectQuery);

            dataGridViewSubTbMain.DataContext = dt.DefaultView;
            dataGridViewSubTbMain.CanUserAddRows = false;
            dataGridViewSubTbMain.CanUserDeleteRows = false;
            rowCountSubDataGrid = dataGridViewSubTbMain.Items.Count;
            if (rowCountSubDataGrid == 0)
            {
                dataGridViewTbMain.Focus();
            }

            //MessageBox.Show(rowCountSubDataGrid.ToString());

            //dataGridViewTbMain.Focus.



        }

        private void dataGridViewSubTbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BtDelete.IsEnabled = true;
            //BtUpdate.IsEnabled = true;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                id_worksheet = row_selected["id_worksheet"].ToString();
                id_user_sub = row_selected["id_tb_employee"].ToString();

                //MessageBox.Show(id_worksheet);
                string selectQuery = "SELECT * FROM tb_worksheet WHERE id_worksheet ='" + id_worksheet + "'";
                //Clipboard.SetDataObject(selectQuery);
                MyData data = new MyData();
                DataTable dt = data.fillMyAdapter(selectQuery);
                TextNotesWorkSheet.Text = row_selected["Notes"].ToString();
                textBoxNotes.Text = row_selected["Notes"].ToString();
                //dataGridName.DataContext = dt.DefaultView;



            }


            //tableName = "tb_worksheet";

        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            //DateTime dateValue = DateTime.Parse(datePickerFrom.ToString());

            //dataGridViewSubTbMain.Items.Clear();
            //dataGridViewSubTbMain.Items.Refresh();
            //dataGridViewSubTbMain.ItemsSource = null;
            MyData error = new MyData();
 

            if (cmbox_Stage.SelectedValue == null)
            {
                MessageBox.Show("Fill field stage");
            }
            else
            {
       
                MyData data = new MyData();
                string dateMysql = Convert.ToDateTime(datePickerFrom.Text).ToString("yyyy-MM-dd");
             
                string idStage = cmbox_Stage.SelectedValue.ToString();
                string query = "insert INTO tb_main(`main_date_create`, `main_wo`, `main_sku`, `main_sn`, `main_ref`, `id_tb_stage`, `id_tb_employee`) VALUES('" + dateMysql + "','" + textBox_WO.Text + "', '" + textBox_SKU.Text + "', '" + textBox_SN.Text + "', '" + textBox_REF.Text + "', '" + idStage + "','" + data.Id_employee + "')";
                //Clipboard.SetDataObject(query);
                data.fillMyAdapter(query);
                string selectQuery = "SELECT id_main FROM tb_main WHERE main_wo ='" + textBox_WO.Text + "'"; //Id for SUB DataGridWiev


                if (error.Error.Contains("Duplicate entry"))
                {
                    MessageBox.Show("The WO exist already ");
                    DataTable dt = data.fillMyAdapter(selectQuery);
                    string id_main = dt.Rows[0][0].ToString(); //for Updete SUB DataGridView
                    selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                   "FROM tb_main " +
                   "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                   "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                   "WHERE main_wo ='" + textBox_WO.Text + "'" +
                   " order by  id_main desc";

                    populateDataGridViewMain(selectQuery);
                    //queryForMainDataGridView(dateMysql, dateMysql);
                    populateDataGridViewSubTbMain(id_main);

                  


                }
                else
                {
                    
                    DataTable dt = data.fillMyAdapter(selectQuery);
                    string id_main = dt.Rows[0][0].ToString(); //for Updete SUB DataGridView

                    DateTime dateValue = DateTime.Now;
                
                    string dateTimeMysql = dateMysql + dateValue.ToString(" HH:mm:ss");

               
                    string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();

                    query = "insert INTO tb_worksheet(`worksheet_date`, `worksheet_notes`, `id_tb_main`, `id_tb_employee" +
                     "`, `id_tb_failure_code`) VALUES('" + dateTimeMysql + "','" + textBoxNotes.Text + "','" + dt.Rows[0][0].ToString() + "', '" + data.Id_employee + "', '" + idFailureCode + "')";
                    //Clipboard.SetDataObject(query);
                    data.fillMyAdapter(query);
                    selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                    "FROM tb_main " +
                    "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                    "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                    "WHERE main_wo ='" + textBox_WO.Text + "'" +
                    " order by  id_main desc";

                    populateDataGridViewMain(selectQuery);
                    //queryForMainDataGridView(dateMysql, dateMysql);
                    populateDataGridViewSubTbMain(id_main);
                    clearControls();
                }
                
               


               

                

                /*
                selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                "FROM tb_main " +
                "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                "WHERE main_wo ='" + textBox_WO.Text + "'" +
                " order by  id_main desc";

                populateDataGridViewMain(selectQuery);
                //queryForMainDataGridView(dateMysql, dateMysql);
                populateDataGridViewSubTbMain(id_main);
                clearControls();
                */
            }

           
            
        }

        private void populate_ComboBox(string comboboxName, string selectQuery)
        {
            MySqlDataReader reader;


            //string selectQuery = "SELECT * FROM `tb_stage` ORDER BY stages_type";

            MyData myAdapter = new MyData();

            //MessageBox.Show(MyData.conn.State.ToString());
            reader = myAdapter.MySqlDataReader(selectQuery);
            while (reader.Read())
            {
                if (comboboxName == "cmbox_Stage")
                {
                    //cmbox_Stages.Items.Clear(); // Clear combobox
                    cmbox_Stage.Items.Add(reader.GetString("stage_type"));
                }
                else
                {
                    //cmbox_FailureCode.Items.Clear();
                    cmbox_FailureCode.Items.Add(reader.GetString("failure_code_type"));
                }


                //cmbox_Stages.Items.Items.Add(reader.GetString("Users"));
            }
            reader.Close();

            /*        

                     selectQuery = "SELECT * FROM `tb_failure_code` ORDER BY failure_code_type";
                     cmbox_FailureCode.Items.Clear();
                     reader = myAdapter.MySqlDataReader(selectQuery);
                     while (reader.Read())
                     {
                         cmbox_FailureCode.Items.Add(reader.GetString("failure_code_type"));
                         //cmbox_Stages.Items.Items.Add(reader.GetString("Users"));
                     }
         */
        }

        private void BtFail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {

            MyData workSpace = new MyData();
            //MessageBox.Show(workSpace.ActiveWorkSpace);

            if (workSpace.ActiveWorkSpace == "PreClean")
            {
                buttonDeleteForPreClean();
            }
            else
            {
                buttonDeleteForAll();
            }

            //M(id_worksheet);
        }
        private void buttonDeleteForAll()
        {
            if (activeDataGridView == "dataGridViewSubTbMain")
            {

                string query = "DELETE FROM `tb_worksheet` WHERE id_worksheet = " + id_worksheet;
                data.fillMyAdapter(query);
                populateDataGridViewSubTbMain(id_main);

            }
            else
            {
                MessageBox.Show("Choose row for delete");
            }
        }
        //string idFailureCode;
        private void cmbox_Stage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //idStage = cmbox_Stage.SelectedValue.ToString();
        }
        private void BtCleare_Click(object sender, RoutedEventArgs e)
        {
            clearControls();
        }
        private void cmbox_FailureCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            //dataGridViewTbMain.DataContext = dt.DefaultView;
            //dataGridViewSubTbMain.DataContext = null; 
            //dataGridViewSubTbMain.UnselectAllCells(); // Unselect datagridview
            if (textBox_WO.Text == string.Empty & textBox_SN.Text == string.Empty)
            {
                MessageBox.Show("Fill field WO and(or) S/N");
            }
            else
            {


                if (textBox_WO.Text != string.Empty & textBox_SN.Text != string.Empty)
                {
                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                    "FROM tb_main " +
                    "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                    "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                    "WHERE   main_wo REGEXP  '" + textBox_WO.Text + "' and main_sn REGEXP  '" + textBox_SN.Text + "'" +
                    " order by  id_main desc";
                    //Clipboard.SetDataObject(selectQuery);
                    populateDataGridViewMain(selectQuery);
                }
                if (textBox_WO.Text != string.Empty & textBox_SN.Text == string.Empty)
                {
                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                    "FROM tb_main " +
                    "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                    "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                    "WHERE   main_wo REGEXP  '" + textBox_WO.Text + "'" +
                    " order by  id_main desc";
                    //Clipboard.SetDataObject(selectQuery);

                    populateDataGridViewMain(selectQuery);
                }
                if (textBox_WO.Text == string.Empty & textBox_SN.Text != string.Empty)
                {
                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                    "FROM tb_main " +
                    "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                    "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                    "WHERE   main_sn REGEXP  '" + textBox_SN.Text + "'" +
                    " order by  id_main desc";
                    //Clipboard.SetDataObject(selectQuery);
                    populateDataGridViewMain(selectQuery);
                }
                dataGridViewSubTbMain.DataContext = null;
            }


            //queryForMainDataGridView(dataMysqlFrom, dataMysqlTo);
            //dataGridViewSubTbMain.ItemsSource = null;
            //dataGridViewSubTbMain.Items.Refresh();// ??????????
            //dataGridViewSubTbMain.Items.Clear();

        }

        void clearControls()
        {
            textBox_WO.Clear();
            textBox_SKU.Clear();
            textBox_SN.Clear();
            textBox_REF.Clear();
            textBoxNotes.Clear();

            textBox_WO.IsEnabled = true;
            textBox_SKU.IsEnabled = true;
            textBox_SN.IsEnabled = true;
            textBox_REF.IsEnabled = true;
        }

        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {

            MyData workSpace = new MyData();
            MessageBox.Show(workSpace.ActiveWorkSpace);

            if (workSpace.ActiveWorkSpace == "PreClean")
            {
                butonUpdateForPreClean();
            }
            else if (workSpace.ActiveWorkSpace == "FinalQc")
            {
                butonUpdateForFinalQc();
            }

            else
            {
                butonUpdateForAll();
            }

            //int currentRowIndex = dataGridViewTbMain.Items.IndexOf(dataGridViewTbMain.CurrentItem);
            //MessageBox.Show(currentRowIndex.ToString());
            // DataGridViewRow.DataBoundItem;
            //IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            //MessageBox.Show(focusedControl.ToString());
        }

        private void butonUpdateForAll() // Update for Tester, Debug, Reapire
        {
            if (activeDataGridView == "dataGridViewSubTbMain")
            {
                if (id_employee == id_user_sub)
                {

                    if (cmbox_Stage.SelectedValue != null)
                    {

                        MessageBox.Show("The row will be update");
                        string idStage = cmbox_Stage.SelectedValue.ToString();
                        string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();

                        string query = "update `tb_main` set id_tb_stage = '" + idStage + "'  WHERE main_wo = '" + wo_number + "'";
                        data.fillMyAdapter(query);

                        query = "update `tb_worksheet` set worksheet_notes = '" + textBoxNotes.Text + "', id_tb_failure_code = '" + idFailureCode + "' WHERE id_tb_main = '" + id_main + "'";
                        data.fillMyAdapter(query);

                        DateTime dateFrom = datePickerFrom.SelectedDate.Value.Date;
                        string dataMysqlFrom = dateFrom.ToString("yyyy-MM-dd");
                        DateTime dateTo = datePickerTo.SelectedDate.Value.Date;
                        string dataMysqlTo = dateTo.ToString("yyyy-MM-dd");
                        queryForMainDataGridView(dataMysqlFrom, dataMysqlTo);
                        clearControls();
                        dataGridViewTbMain.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Fill field stage");
                    }
                }
                else
                {
                    MessageBox.Show("You cannot update, because you are not owner the row.");
                }
            }
        }
        private void dataGridViewTbMain_GotFocus(object sender, RoutedEventArgs e)
        {
            secondRowIndexMainDataGridView = dataGridViewTbMain.Items.IndexOf(dataGridViewTbMain.CurrentItem);
            activeDataGridView = "dataGridViewTbMain";
            //MessageBox.Show(currentRowIndex.ToString());
            //sender x = System.Windows.Controls.DataGrid Items.Count: 6
            //SelectionChangedEventArgs ex = new SelectionChangedEventArgs();
            //MessageBox.Show(firstRowIndexMainDataGridView.ToString());
            //MessageBox.Show(secondRowIndexMainDataGridView.ToString());

        }

        private void dataGridViewSubTbMain_GotFocus(object sender, RoutedEventArgs e)
        {
            activeDataGridView = "dataGridViewSubTbMain";
        }

        private void butonUpdateForPreClean() // Button Update Click for pre clean
        {
           
            if (id_employee == id_user_main)
            {
                
                if (cmbox_Stage.SelectedValue != null)
                {

                    MessageBox.Show("WO will be update");
                    string idStage = cmbox_Stage.SelectedValue.ToString();
                    string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
                    string query = "update `tb_main` set main_wo = '" + textBox_WO.Text + "', main_sku= '" + textBox_SKU.Text + "', main_sn = '" + textBox_SN.Text + "', main_ref = '" + textBox_REF.Text + "', id_tb_stage = '" + idStage + "'  WHERE main_wo = '" + wo_number + "'";
                    data.fillMyAdapter(query);
                    query = "update `tb_worksheet` set worksheet_notes = '" + textBoxNotes.Text + "', id_tb_failure_code = '" + idFailureCode + "' WHERE id_tb_main = '" + id_main + "'";
                    data.fillMyAdapter(query);

                    /*
                    DateTime dateFrom = datePickerFrom.SelectedDate.Value.Date;
                    string dataMysqlFrom = dateFrom.ToString("yyyy-MM-dd");

                    DateTime dateTo = datePickerTo.SelectedDate.Value.Date;
                    string dataMysqlTo = dateTo.ToString("yyyy-MM-dd");
                    */

                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                   "FROM tb_main " +
                   "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                   "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                   "WHERE main_wo = '" + wo_number + "'" +
                   " order by  id_main desc";
                    populateDataGridViewMain(selectQuery);
                    populateDataGridViewSubTbMain(id_main);



                    //queryForMainDataGridView(dataMysqlFrom, dataMysqlFrom);
                    //clearControls();
                    dataGridViewTbMain.Focus();
                }
                else
                {
                    MessageBox.Show("Fill field stage");
                }
            }
            else
            {
                MessageBox.Show("You cannot update, because you are not owner the row.");
            }


            /*
            if (activeDataGridView == "dataGridViewTbMain")
            {

                if (id_employee == id_user_main)
                {
                    MessageBox.Show("WO will be update");
                    string idStage = cmbox_Stage.SelectedValue.ToString();
                    string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
                    string query = "update `tb_main` set main_wo = '" + textBox_WO.Text + "', main_sku= '" + textBox_SKU.Text + "', main_sn = '" + textBox_SN.Text + "', main_ref = '" + textBox_REF.Text + "', id_tb_stage = '" + idStage + "'  WHERE main_wo = '" + wo_number + "'";
                    data.fillMyAdapter(query);
                    query = "update `tb_worksheet` set worksheet_notes = '" + textBoxNotes.Text + "', id_tb_failure_code = '" + idFailureCode + "' WHERE id_tb_main = '" + id_main + "'";
                    data.fillMyAdapter(query);

                    DateTime dateFrom = datePickerFrom.SelectedDate.Value.Date;
                    string dataMysqlFrom = dateFrom.ToString("yyyy-MM-dd");
                    
                    DateTime dateTo = datePickerTo.SelectedDate.Value.Date;
                    string dataMysqlTo = dateTo.ToString("yyyy-MM-dd");
                    
                    queryForMainDataGridView(dataMysqlFrom, dataMysqlFrom);
                    clearControls();
                    dataGridViewTbMain.Focus();
                }
                else
                {
                    MessageBox.Show("You cannot update, because you are not owner the row.");
                }

            }

            else if (activeDataGridView == "dataGridViewSubTbMain")
            {
                if (id_employee == id_user_main)
                {
                    string query = "update `tb_worksheet` set ";
                    //string query = "DELETE FROM `tb_worksheet` WHERE id_worksheet = " + id_worksheet;
                    
                    
                    
                    
                    data.fillMyAdapter(query);
                    populateDataGridViewSubTbMain(id_main);

                }
                else
                {
                    MessageBox.Show("You cannot update, because you are not owner the row.");
                }


            }
            */
        }
        private void butonUpdateForFinalQc()
        {
            if (finalazeCheckedFlag == true)
            {
                butonUpdateForAll();
            }
            else
            {

               if (cmbox_Stage.SelectedValue != null)
                {
                    MessageBox.Show("The row will be unfinalize");
                    string idStage = cmbox_Stage.SelectedValue.ToString();
                    string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
                    string query = "update `tb_main` set main_data_finalize = ''  WHERE main_wo = '" + wo_number + "'";
                    data.fillMyAdapter(query);

                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                    "FROM tb_main " +
                    "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                    "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                    "WHERE   main_wo REGEXP  '" + textBox_WO.Text + "'" +
                    " order by  id_main desc";
                    //Clipboard.SetDataObject(selectQuery);

                    populateDataGridViewMain(selectQuery);

                    clearControls();
                    //dataGridViewTbMain.Focus();



                    //dataGridViewSubTbMain.IsEnabled = true;
                    finalazeCheckedFlag = true;
                    BtUpdate.IsEnabled = true;
                }
               else
                {
                    MessageBox.Show("Fill field stage");
                }


               
            }
            
        }
        private void buttonDeleteForPreClean()
        {
            if (activeDataGridView == "dataGridViewTbMain" & rowCountSubDataGrid > 0)
            {

                MessageBox.Show("Cannot delete the WO. Delete rows in worksheet first");
                dataGridViewTbMain.Focus();


            }
            else if (activeDataGridView == "dataGridViewTbMain" & rowCountSubDataGrid == 0)
            {

                MessageBox.Show("row will be delete");
                string query = "DELETE FROM `tb_main` WHERE id_main = " + id_main;


                data.fillMyAdapter(query);
                DateTime dateFrom = datePickerFrom.SelectedDate.Value.Date;
                string dataMysqlFrom = dateFrom.ToString("yyyy-MM-dd");
                DateTime dateTo = datePickerTo.SelectedDate.Value.Date;
                string dataMysqlTo = dateTo.ToString("yyyy-MM-dd");
                queryForMainDataGridView(dataMysqlFrom, dataMysqlTo);
                clearControls();


                /*
                string query = "DELETE FROM `tb_worksheet` WHERE id_worksheet = " + id_worksheet;
                data.fillMyAdapter(query);
                populateDataGridViewSubTbMain(id_main);
                */
            }

            else if (activeDataGridView == "dataGridViewSubTbMain")
            {

                string query = "DELETE FROM `tb_worksheet` WHERE id_worksheet = " + id_worksheet;
                data.fillMyAdapter(query);
                populateDataGridViewSubTbMain(id_main);

            }
        }

        private void ChkBoxAllDays_Checked(object sender, RoutedEventArgs e)
        {

            datePickerFrom.IsEnabled = false;
            datePickerTo.IsEnabled = false;

        }

        private void ChkBoxAllDays_Unchecked(object sender, RoutedEventArgs e)
        {
            datePickerFrom.IsEnabled = true;
            datePickerTo.IsEnabled = true;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateValue = DateTime.Now; 

                if (ChkFinalize.IsChecked ?? true & (lbNameWorkSpace.Content.ToString() == "Final QC"))
                {

                    string dateTimeMysql = dateValue.ToString("yyyy-MM-dd");
                    string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();

                    string query = "update `tb_main` set id_tb_stage = '1', main_data_finalize = '" + dateTimeMysql + "'  WHERE main_wo = '" + wo_number + "'";
                    data.fillMyAdapter(query);
                    string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                     "FROM tb_main " +
                     "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                     "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                     "WHERE main_wo = '" + wo_number + "'" +
                     " order by  id_main desc";
                    populateDataGridViewMain(selectQuery);

                    dateTimeMysql = dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                    idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
                    query = "insert INTO tb_worksheet(`worksheet_date`, `worksheet_notes`, `id_tb_main`, `id_tb_employee" +
                         "`, `id_tb_failure_code`) VALUES('" + dateTimeMysql + "','" + textBoxNotes.Text + "','" + id_main + "', '" + data.Id_employee + "', '" + idFailureCode + "')";
                    //Clipboard.SetDataObject(query);
                    data.fillMyAdapter(query);
                    populateDataGridViewSubTbMain(id_main);
                    ChkFinalize.IsChecked = false;
                    clearControls();
                }
                else
                {
                    if (cmbox_Stage.SelectedValue == null)
                    {
                        MessageBox.Show("Fill field stage");
                    }
                    else
                    {
                        string idStage = cmbox_Stage.SelectedValue.ToString();
                        string query = "update `tb_main` set   id_tb_stage = '" + idStage + "'  WHERE main_wo = '" + wo_number + "'";
                        data.fillMyAdapter(query);
                        string selectQuery = "SELECT tb_main.*, tb_stage.stage_type, tb_employee.employee_first_name " +
                        "FROM tb_main " +
                        "left Join tb_stage ON tb_main.id_tb_stage = tb_stage.id_stage " +
                        "left Join tb_employee ON id_tb_employee = tb_employee.id_employee " +
                        "WHERE main_wo = '" + wo_number + "'" +
                        " order by  id_main desc";
                         populateDataGridViewMain(selectQuery);
                    //queryForMainDataGridView(datatimeWO, datatimeWO);

                        string dateTimeMysql = dateValue.ToString("yyyy-MM-dd HH:mm:ss");
                        string idFailureCode = cmbox_FailureCode.SelectedValue.ToString();
                        query = "insert INTO tb_worksheet(`worksheet_date`, `worksheet_notes`, `id_tb_main`, `id_tb_employee" +
                             "`, `id_tb_failure_code`) VALUES('" + dateTimeMysql + "','" + textBoxNotes.Text + "','" + id_main + "', '" + data.Id_employee + "', '" + idFailureCode + "')";
                        //Clipboard.SetDataObject(query);
                        data.fillMyAdapter(query);
                        populateDataGridViewSubTbMain(id_main);
                        clearControls();
                    }
               
            }
        }

        private void ChkFinalize_Unchecked(object sender, RoutedEventArgs e)
        {
           
            
            
           finalazeCheckedFlag = false;
           BtUpdate.IsEnabled = true;
        }
    }
}
