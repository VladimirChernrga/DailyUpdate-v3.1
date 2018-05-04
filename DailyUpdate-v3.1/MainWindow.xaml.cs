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

// old
// невидимое окно

// https://ru.stackoverflow.com/questions/671369/%D0%9A%D0%B0%D0%BA-%D0%BF%D0%BE%D0%BA%D0%B0%D0%B7%D0%B0%D1%82%D1%8C-%D0%BD%D0%B5%D0%B2%D0%B8%D0%B4%D0%B8%D0%BC%D0%BE%D0%B5-%D0%BE%D0%BA%D0%BD%D0%BE
namespace DailyUpdate_v3._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

   
    public partial class MainWindow : Window
    {

        
        ClassSerializer _settings = null;
        public MainWindow()
        {
            InitializeComponent();
            //this.Visibility = Visibility.Visible;
           
                        // делаем окно невидимым 
                        this.ShowInTaskbar = false;
                        this.WindowState = WindowState.Minimized;

                        // показываем и скрываем окно
                        this.Show();
                        this.Hide();
             
        }
                  
                private void Open_FrMain1 (object sender, EventArgs e)
                {

                    this.Show();
                    this.WindowState = WindowState.Normal;
                    MyData data = new MyData();
                    lbUser.Content = data.FirstName_employee;

                    if (data.Administrator =="0") { BtReport.Visibility = Visibility.Hidden; }
                    if (data.PreClean == "0") { BtPreClean.Visibility = Visibility.Hidden; }
                    if (data.FinalQC == "0") { BtFinalQc.Visibility = Visibility.Hidden; }
                    if (data.Testing == "0") { BtTesting.Visibility = Visibility.Hidden; }
                    if (data.Repaire == "0") { BtRepaire.Visibility = Visibility.Hidden; }
                    if (data.Debug == "0") { BtDebug.Visibility = Visibility.Hidden; }
                    if (data.Report == "0") { BtReport.Visibility = Visibility.Hidden; }

                    //userFormsUC();
                    stPanelUserControl.Children.Clear();
                    WelcomeFormUC welcomeFormUC = new WelcomeFormUC();
                    //TestingUC testingUC = new TestingUC();
                    stPanelUserControl.Children.Add(welcomeFormUC);

            //this.Visibility = Visibility.Visible;
            //errorConnection.Owner = this;

            //this.Visibility = Visibility.Visible;
        }

        /*   Show how I can delegate for event Closed form  +++++++++++++ Don't delete +++++++++++++++++++
                // frAuthorization.Closed += Open_FrMain;
                //frAuthorization.BtOk.Click += new RoutedEventHandler(Open_FrMain1);

                    private void Open_FrMain(object sender, EventArgs e)
                {

                    this.Show();
                    this.WindowState = WindowState.Normal;
                    FrTester();


                    //this.Visibility = Visibility.Visible;
                    //errorConnection.Owner = this;

                    //this.Visibility = Visibility.Visible;
                }
        ++++++++++++++++ DON'T Delaee!!!!!!!!!!!!!!!! ++++++++++++++++++++++
        */
        
        private void userFormsUC()
        {
            stPanelUserControl.Children.Clear();
            UsersFormUC userFormsUC = new UsersFormUC();
            //TestingUC testingUC = new TestingUC();
            
            stPanelUserControl.Children.Add(userFormsUC);
        }

        private void Open_FrAuthorization(object sender, RoutedEventArgs e)
        {
            FrAuthorization frAuthorization = new FrAuthorization();
            //frAuthorization.Owner = this;
            frAuthorization.Event += Open_FrMain1;
            frAuthorization.Show();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //FrTester(); // Delete this For normal loading
            // Screen sizeint --> theHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            // Screen Size --> int theWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;

            /* ================== Size my App
            var h = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight;
            var w = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualWidth;
            this.Height = 300;
            this.Width = 986;
            */

            MyData connection = new MyData();

            EncryptDecrypt encrypt = new EncryptDecrypt();
            _settings = ClassSerializer.GetSettings();

            if (_settings.Type1 != null & _settings.Type2 != null
                & _settings.Type3 != null & _settings.Type4 != null & _settings.Type5 != null)
            {
                connection.HostNameDb = encrypt.Decrypt(_settings.Type1);
                connection.PortDb = encrypt.Decrypt(_settings.Type2);
                connection.InitialCatalogDb = encrypt.Decrypt(_settings.Type3);
                connection.LoginDb = encrypt.Decrypt(_settings.Type4);
                connection.PasswordDb = encrypt.Decrypt(_settings.Type5);
            }
            connection.myConnection();
            connection.connectionOpen();
            connection.connectionClose();

            if (connection.Error == "0")
            {
                Open_FrAuthorization(MainWindow1, new RoutedEventArgs()); 
            }
            else
            {
                
                MessageBox.Show(connection.Error);
                FrErrorConnection frErrorConnection = new FrErrorConnection();
                frErrorConnection.Owner = this;
                frErrorConnection.BtSave.Click += new RoutedEventHandler(Open_FrAuthorization);
                frErrorConnection.Show();
                //this.Show();
            }
           
        }

        private void BtExit_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //Application.Current.MainWindow.Close();
        }
        
        private void BtSetup_Click(object sender, RoutedEventArgs e)
        {
            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "BtSetup";

            enableButon();
            stPanelUserControl.Children.Clear();
            SetupUC setupUC = new SetupUC();
            stPanelUserControl.Children.Add(setupUC);
            buttonName = "BtSetup";
            BtSetup.IsEnabled = false;

            //BtSetup.Visibility = Visibility.Collapsed;
            //BtTesting.Visibility = Visibility.Visible;
        }

       string buttonName;
        private void BtTesting_Click(object sender, RoutedEventArgs e)
        {
            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "BtTesting";

            enableButon();          
            buttonName = "BtTesting";
            BtTesting.IsEnabled = false;
            stPanelUserControl.Children.Clear();
            UsersFormUC usersFormUC = new UsersFormUC();
            usersFormUC.test("Testing");
            stPanelUserControl.Children.Add(usersFormUC);
            //BtTesting.Visibility = Visibility.Collapsed;
            //BtSetup.Visibility = Visibility.Visible;

        }
        
        private void BtPreClean_Click(object sender, RoutedEventArgs e)
        {
            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "PreClean";
            enableButon();

            stPanelUserControl.Children.Clear();
            UsersFormUC usersFormUC = new UsersFormUC();
            usersFormUC.preCleanFinal("Pre Clean");
            stPanelUserControl.Children.Add(usersFormUC);
            buttonName = "BtPreClean";
            BtPreClean.IsEnabled = false;
            
        }

        private void enableButon()
        {
            if (buttonName == "BtPreClean") { BtPreClean.IsEnabled = true; }
            if (buttonName == "BtFinalQc") { BtFinalQc.IsEnabled = true; }
            if (buttonName == "BtTesting") { BtTesting.IsEnabled = true; }
            if (buttonName == "BtDebug") { BtDebug.IsEnabled = true; }
            if (buttonName == "BtRepaire") { BtRepaire.IsEnabled = true; }
            if (buttonName == "BtReport") { BtReport.IsEnabled = true; }
            if (buttonName == "BtSetup") { BtSetup.IsEnabled = true; }
 
        }

        private void BtReport_Click(object sender, RoutedEventArgs e)
        {
            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "BtReport";

            enableButon();
            buttonName = "BtReport";
            BtReport.IsEnabled = false;
            stPanelUserControl.Children.Clear();
            ReportUC reportUc = new ReportUC();
            stPanelUserControl.Children.Add(reportUc);

        }

        private void BtFinalQc_Click(object sender, RoutedEventArgs e)
        {
            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "FinalQc";

            enableButon();
            buttonName = "BtFinalQc";
            BtFinalQc.IsEnabled = false;
            stPanelUserControl.Children.Clear();
            UsersFormUC usersFormUC = new UsersFormUC();
            usersFormUC.finalQc("Final QC");
            stPanelUserControl.Children.Add(usersFormUC);
        }

        private void BtDebug_Click(object sender, RoutedEventArgs e)
        {

            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "BtDebug";

            enableButon();
            buttonName = "BtDebug";
            BtDebug.IsEnabled = false;
            stPanelUserControl.Children.Clear();
            UsersFormUC usersFormUC = new UsersFormUC();
            usersFormUC.debugRepair("Debugin");
            stPanelUserControl.Children.Add(usersFormUC);
        }

        private void BtRepaire_Click(object sender, RoutedEventArgs e)
        {

            MyData workSpace = new MyData();
            workSpace.ActiveWorkSpace = "BtDebug";

            enableButon();
            buttonName = "BtRepaire";
            BtRepaire.IsEnabled = false;
            stPanelUserControl.Children.Clear();
            UsersFormUC usersFormUC = new UsersFormUC();
            usersFormUC.debugRepair("Repaire");
            stPanelUserControl.Children.Add(usersFormUC);
        }

        private void BtMyreport_Click(object sender, RoutedEventArgs e)
        {
            stPanelUserControl.Children.Clear();
            MyReportUc myReportUc = new MyReportUc();
            stPanelUserControl.Children.Add(myReportUc);
            
        }
    }

}
    


