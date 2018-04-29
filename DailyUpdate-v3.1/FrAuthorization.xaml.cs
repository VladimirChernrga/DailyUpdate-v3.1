using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Windows.Media;
using System;

namespace DailyUpdate_v3._1
{
    /// <summary>
    /// Interaction logic for FrAuthorization.xaml
    /// </summary>
    

public partial class FrAuthorization : Window
    {
        public FrAuthorization()
        {
            InitializeComponent();
            

            if (DailyUpdate_v3._1.Properties.Settings.Default.TxBoxLogin != string.Empty)
            {
                CheckBoxRememberMe.IsChecked = true;

                TxBoxLogin.Text = DailyUpdate_v3._1.Properties.Settings.Default.TxBoxLogin;
                TxPassword.Focus();
                //FocusManager.SetFocusedElement(this, TxPassword);
                //TxPassword.Focusable = true; // Set Logical Focus
                //Keyboard.Focus(TxPassword); // Set Keyboard Focus



            }
            //DailyUpdate_v3._1.Propertrties.Settings.Default.TxBoxLogin = TxBoxLogin.Text;
        }

        // ______________  EventHandler for Delegate from Main form ______________
        public event EventHandler<EventArgs> Event = delegate { };
        public void FireEvent()
        {
            Event(this, EventArgs.Empty);
        }
        // ________________________________________________________________________

        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
               
            string selectQuery = "SELECT * FROM `tb_employee` WHERE employee_login = '" + TxBoxLogin.Text + "' and employee_password = '"+ TxPassword.Password.ToString()+"'";
            //Clipboard.SetDataObject(selectQuery);
            //MessageBox.Show("Ok");
            lbMessages.Visibility = Visibility.Hidden;
            //TxBoxLogin.IsEnabled = false;
            
            MyData data = new MyData();
           
            DataTable dt = data.fillMyAdapter(selectQuery);
            
            if (data.Error != "0")
            {
                
                lbMessages.Background = new SolidColorBrush(Colors.Red);
                lbMessages.Visibility = Visibility.Visible;
                lbMessages.Content = data.Error;
                //GridTest.IsEnabled = true;
            }
            else
            {
                try
                {
                    //Clipboard.SetDataObject(dt.Rows[0][1].ToString());
                    data.Id_employee = dt.Rows[0][0].ToString();
                    data.FirstName_employee = dt.Rows[0][1].ToString();                
                    //data.Dismissed_employee = dt.Rows[0][4].ToString();
                    data.Login_employee = dt.Rows[0][5].ToString();               
                    data.Password_employee = dt.Rows[0][6].ToString();
                    data.Privilege_employee = dt.Rows[0][7].ToString();
                    string s = data.Privilege_employee.ToString();
                    data.Administrator = s[0].ToString();
                    data.Dismissed = s[1].ToString();
                    data.PreClean = s[2].ToString();
                    data.Testing = s[3].ToString();
                    data.Repaire = s[4].ToString();
                    data.Debug = s[5].ToString();
                    data.FinalQC = s[6].ToString();
                    data.Report = s[7].ToString();

                    //GridTest.IsEnabled = true;
                }
                catch
                {
                    
                    lbMessages.Background = new SolidColorBrush(Colors.Red);
                    lbMessages.Visibility = Visibility.Visible;
                    lbMessages.Content = "Wrong login or password!!";
                    
                    //GridTest.IsEnabled = true;

                }

                                
                if (data.Dismissed == "0")

                //if (data.Dismissed_employee == "1")


                {
                    if (!CheckBoxRememberMe.IsChecked ?? true)
                    {

                        DailyUpdate_v3._1.Properties.Settings.Default.TxBoxLogin = string.Empty;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {

                        DailyUpdate_v3._1.Properties.Settings.Default.TxBoxLogin = TxBoxLogin.Text;
                        Properties.Settings.Default.Save();

                    }

                    

                    FireEvent();
                    this.Close();


                }
                else
                {
                    lbMessages.Background = new SolidColorBrush(Colors.Red);
                    lbMessages.Visibility = Visibility.Visible;
                    lbMessages.Content = "Wrong login or password!!";
                }


                //MessageBox.Show(data.Login_employee);
                
                    //MessageBox.Show(data.Privilege_employee);
                    //this.Close();
                

            }
            
            //lbMessages.Visibility = Visibility.Hidden;
            //lbMessages.Visibility = Visibility.Collapsed;

            /*
            MyData data = new MyData();
            DataTable dt = new DataTable();
            data.connectionOpen();
            if (data.Connection.State == ConnectionState.Open)
            {
                MySqlDataAdapter da = new MySqlDataAdapter(selectQuery, data.Connection);
                da.Fill(dt);
                data.connectionClose();
            }

            try
            {
                MessageBox.Show(dt.Rows[0][3].ToString());
            }
            catch
            {
                MessageBox.Show("Wrong password!!");
            }
            */





            // DataTable dt = da.fillMyAdapter(selectQuery);
            //MessageBox.Show(dt.Rows[0][3].ToString());



            /* works!!
            {
                try
                {
                    MyData da = new MyData();
                    DataTable dt = da.fillMyAdapter(selectQuery);
                    MessageBox.Show(dt.Rows[0][3].ToString());
                }
                catch
                {
                    MessageBox.Show("Wrong password!!");
                }
            }
            */



            //this.Close();



            //connection.connectionOpenClose();
            //MessageBox.Show("Connection pass");

        }

  
        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
        private void TxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtOk_Click(BtOk, new RoutedEventArgs());
            }
        }
    }
}
