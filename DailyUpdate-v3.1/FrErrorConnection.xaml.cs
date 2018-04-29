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
using System.Data;

namespace DailyUpdate_v3._1
{
    /// <summary>
    /// Interaction logic for FrErrorConnection.xaml
    /// </summary>
    public partial class FrErrorConnection : Window
    {
        ClassSerializer _settings = null;
        public FrErrorConnection()
        {
            InitializeComponent();
            BtSave.Visibility = Visibility.Hidden;
 
            //_settings = ClassSerializer.GetSettings();
         
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtExit_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TxBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtVeryfy_Click(BtVeryfy, new RoutedEventArgs());
            }
        }



        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            EncryptDecrypt encrypt = new EncryptDecrypt();

            _settings = ClassSerializer.GetSettings();
            _settings.Type1 = encrypt.Encrypt(TextBoxHostName.Text);
            _settings.Type2 = encrypt.Encrypt(TextBoxPort.Text);
            _settings.Type3 = encrypt.Encrypt(TextBoxInitialCatalog.Text);
            _settings.Type4 = encrypt.Encrypt(TextBoxLogin.Text);
            _settings.Type5 = encrypt.Encrypt(TextBoxPassword.Password);


            //_settings.Type1 = hosneme;
            //_settings.Type1 = TextBoxHostName.Text;
            //_settings.Type2 = TextBoxPort.Text;
            //_settings.Type3 = TextBoxInitialCatalog.Text;
            //_settings.Type4 = TextBoxLogin.Text;
            //_settings.Type5 = TextBoxPassword.Password;
            _settings.Save();
            this.Close();
        }

        private void BtVeryfy_Click(object sender, RoutedEventArgs e)
        {

            MyData connection = new MyData();
            connection.HostNameDb = TextBoxHostName.Text;
            connection.PortDb = TextBoxPort.Text;
            connection.InitialCatalogDb = TextBoxInitialCatalog.Text;
            connection.LoginDb = TextBoxLogin.Text;
            connection.PasswordDb = TextBoxPassword.Password;

            connection.myConnection();
            connection.connectionOpen();
            connection.connectionClose();

            if (connection.Error == "0")
            {
                StackPanelSetup.Visibility = Visibility.Hidden;
                BtVeryfy.Visibility = Visibility.Hidden;
                BtSave.Visibility = Visibility.Visible;
                BtSave.Focus();
            }


            /*
            if (MyData.conn.State == ConnectionState.Open)

            {
                StackPanelSetup.Visibility = Visibility.Hidden;
                BtVeryfy.Visibility = Visibility.Hidden;
                BtSave.Visibility = Visibility.Visible;
                BtSave.Focus();
                //Lavender
            }
            */

        }    
    }
}
