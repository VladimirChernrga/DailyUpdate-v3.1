using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace DailyUpdate_v3._1
{
    // https://metanit.com/sharp/wpf/20.1.php 
    class MyData

     
    {
        private static MySqlConnection conn;
        //private static DataSet ds = new DataSet();
        private static string _ipAddressDb, _portDb, _loginDb, _initialCatalogDb, _passwordDb;
        

        public string HostNameDb
        {
            get { return _ipAddressDb; }
            set
            {
                _ipAddressDb = value;             
            }

        }
        public string PortDb
        {
            get { return _portDb; }
            set
            {
                _portDb = value;
            }

        }

        

        public string InitialCatalogDb
        {
            get { return _initialCatalogDb; }
            set
            {
                _initialCatalogDb = value;
            }
        }

        public string LoginDb
        {
            get { return _loginDb; }
            set
            {
                _loginDb = value;
            }

        }

        public string PasswordDb
        {
            get { return _passwordDb; }
            set
            {
                _passwordDb = value;
            }
        }




        static string _id_employee, _firstName_employee, _dismissed_employee, _login_employee, _password_employee, _privilege_employee, _ap_employee;

        public string Id_employee
        {
            get { return _id_employee; }
            set
            {
                _id_employee = value;
            }
        }

        public string FirstName_employee
        {
            get { return _firstName_employee; }
            set
            {
               _firstName_employee = value;
            }
        }

        public string Dismissed_employee
        {
            get { return _dismissed_employee; }
            set
            {
                _dismissed_employee = value;
            }
        }

        public string Login_employee
        {
            get { return _login_employee; }
            set
            {
                _login_employee = value;
            }
        }

        public string Password_employee
        {
            get { return _password_employee; }
            set
            {
                _password_employee = value;
            }
        }

        
        public string Privilege_employee
        {
            get { return _privilege_employee; }
            set
            {
                _privilege_employee = value;
            }
        }

        
        public string AP_employee
        {
            get { return _ap_employee; }
            set
            {
                _ap_employee = value;
            }
        }

        static string _administrator, _dismissed, _preClean, _testing, _repaire, _debug, _finalQC, _report;
        public string Administrator
        {
            get { return _administrator; }
            set
            {
                _administrator = value;
            }
        }

        public string Dismissed
        {
            get { return _dismissed; }
            set
            {
                _dismissed = value;
            }
        }

        public string PreClean
        {
            get { return _preClean; }
            set
            {
                _preClean = value;
            }

        }

        public string Testing
        {
            get { return _testing; }
            set
            {
                _testing = value;
            }

        }

        public string Repaire
        {
            get { return _repaire; }
            set
            {
                _repaire = value;
            }

        }

        public string Debug
        {
            get { return _debug; }
            set
            {
                _debug = value;
            }
        }

        public string FinalQC
        {
            get { return _finalQC; }
            set
            {
                _finalQC = value;
            }
        }

        public string Report
        {
            get { return _report; }
            set
            {
                _report = value;
            }
        }



        //MySqlConnection conn;

        public MySqlConnection myConnection()// ??  public
        {

            string connection = "datasource =" + _ipAddressDb + "; port = " + _portDb + "; Initial Catalog = " + _initialCatalogDb + "; username = " + _loginDb + "; password =" + _passwordDb;
            //MessageBox.Show(connection);
            //string connection = "datasource = 192.168.0.7; port = 3306; Initial Catalog = test_db_Daily_Update; username = root; password = ";
            conn = new MySqlConnection(connection);
            return conn;
        }
        
        public MySqlConnection Connection
        {
            get { return conn; }
            set
            {
                conn = value;
            }
        }
        static string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
            }
        }

        static string _activeWorkSpace;
        public string ActiveWorkSpace
        {
            get { return _activeWorkSpace; }
            set
            {
                _activeWorkSpace = value;
            }
        }

        public void connectionOpen()
        {
            {
                try
                {
                    conn.Open();
                    
                    _error = "0";
                    //MessageBox.Show("Connection open!");
                }
                catch
                {
                    
                    _error = "Connection Failed";
                    //MessageBox.Show("Connection fail!!");

                }
            }
        }
        public void connectionClose()
        {
            {
                conn.Close();
                //MessageBox.Show("Connection close!");

            }
        }

       
        public DataTable fillMyAdapter(string selectQuery)
        {
            //myConnection();
            DataTable dt = new DataTable();
            connectionOpen();
            if (Error == "0")
            {
            MySqlDataAdapter da = new MySqlDataAdapter(selectQuery, conn);
                //MessageBox.Show(da.Fill(dt).ToString());

            try
                {
                da.Fill(dt);
                }
             catch (Exception e)
                {
                    _error = e.Message.ToString();
                    //MessageBox.Show(e.Message);
                    //Clipboard.SetDataObject(e.Message.ToString());
                    //MessageBox.Show("Exeption MySQL");
                    //String exDetail = String.Format(e.Message, Environment.NewLine, e.Source, e.StackTrace);
                    //MessageBox.Show(e.Message.ToString());
                    //MessageBox.Show(e.ToString());
                }
            connectionClose();
        }
            return dt;
        }
        public void addDeleteUpdateData(string query)
        {
            connectionOpen(); 
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            connectionClose(); 
            
        }
        public MySqlDataReader MySqlDataReader(string selectQuery)
        {
            connectionOpen();

            MySqlCommand command = new MySqlCommand(selectQuery, conn);
            MySqlDataReader reader = command.ExecuteReader();
            return reader;

        }
    }
}

