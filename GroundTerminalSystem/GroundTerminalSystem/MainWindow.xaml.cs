/*
* FILE : GroundTerminalSystem
* Authors: Asad Ahmed, Mohammed Abusultan, Mohamed Halbouni
* DESCRIPTION : This file contains The program that simulates the ground terminal system
* of the Air Transportation System. This program receives telemetry data from the Air Transmission
* System or the simulated plane that sends data every 1 second.
* The GroundTerminalSytem, displays the telemetry database on the UI in realtime and also 
* persistes the data to the database
* this includes opening and closing of the database connections as well as inserting
* telemetry database into their respective talbles
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using MySql.Data.MySqlClient;

namespace GroundTerminalSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        MySqlConnection cnn;
        string connetionString = null;
        public static TcpListener server = null;

        //variables to hold the telemetry data as they come in
        string tailNumber;                               
        string accelX;                                  
        string accelY;                                  
        string accelZ;                                  
        string Weight;                                  
        string altitude;                             
        string pitch;                                   
        string bank;
        string checkSumReceived;
        int CheckSum;
        string[] spilittedPacket = new string[12];
        string seqNum;
        public MainWindow()
        {

            InitializeComponent();
            
            Thread thStart = new Thread(startServer);
            thStart.Start();
        }

        /*
         * Method Name: searchBtn()
         * Description: This method invokes the function that searches the database
         * Parameters: object sender, RoutedEventArgs e
         * Return value : void
         */
        private void searchBtn(object sender, RoutedEventArgs e)
        {
            //get the user input
            string searchTerm = txtSearch.Text;
            if (searchTerm == "C-FGAX" || searchTerm == "C-GEFC" || searchTerm == "C-QWWT")
            {

                //pass it to the function that searches the database
                databaseOperations.searchDtatBase(searchTerm);
            }
            else
            {
                MessageBox.Show("Please provide valid tail number.");
            }


        }



        /*
         * Method Name: startServer()
         * Description: This method starts the TCP/IP listener with the given port and 
         * Parameters: object sender, RoutedEventArgs e
         * Return value : void
         */
        private void startServer()
        {

            string threadName = Thread.CurrentThread.Name;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                
                this.Dispatcher.Invoke((Action)(() => { status.Content = "waiting for connectin..."; }));
                this.Dispatcher.Invoke((Action)(() => { status.Foreground = Brushes.Orange; }));
                
                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
                //connectin string for the database
                connetionString = "server=localhost;database=flightdata;uid=root;pwd=root";
                //open the connection to the database
                cnn = databaseOperations.ConnectToDatabase(connetionString);
                cnn.Open();

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
              
                    TcpClient client = server.AcceptTcpClient();
                    this.Dispatcher.Invoke((Action)(() => { status.Content = "Connected!"; }));
                    this.Dispatcher.Invoke((Action)(() => { status.Foreground = Brushes.Green; }));
                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;
                    int j = 1;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                  

                        //call splitPAcket here...
                        spilittedPacket = splitPacket(data);

                        //then save it to database
                        tailNumber = spilittedPacket[0];
                        //seqNum = spilittedPacket[1];
                        accelX = spilittedPacket[3];
                        accelY = spilittedPacket[4];
                        accelZ = spilittedPacket[5];
                        Weight = spilittedPacket[6];
                        altitude = spilittedPacket[7];
                        pitch = spilittedPacket[8];
                        bank = spilittedPacket[9];
 
                        //if it is valid Display to screen
                        this.Dispatcher.Invoke((Action)(() => { txtArea.Items.Add(data); }));
                        //insert into the attitude table
                        databaseOperations.insertToAttitudeTable(tailNumber, altitude, pitch, bank);
                        //insert into Gforce table
                        databaseOperations.insertToGforceTable(tailNumber, accelX, accelY, accelZ, Weight);

                    }


                    // Shutdown and end connection
                    client.Close();
                    
                }

               
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);

            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
                
            }


        }
        /*
         * Method Name: splitPacket()
         * Description: this method splits the incoming data into the required bits
         * Parameters: string data
         * Return value : string[]
         */
        private string[] splitPacket(string data)
        {

            string[] temp = data.Split(',');    //Second we split the string and get rid of the ","
            string[] result = new string[11];
            for (int i = 0; i < temp.Length; i++)
            {
                result[i] = temp[i].Trim();
            }
            checkSumReceived = result[10];

            
            CheckSum = (int)(Convert.ToDouble(result[7]) + Convert.ToDouble(result[8]) + Convert.ToDouble(result[9])) / 3;
            if (CheckSum == Convert.ToInt32(checkSumReceived))
            {
                return result;
            }
            else
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    result[i] = "";
                }
                return result;
            }



        }

        /*
         * Method Name: HandleCheck()
         * Description: This method turns on the live mood of the ground terminal system 
         * Parameters: object sender, RoutedEventArgs e
         * Return value : void
         */
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            //text2.Text = "Button is Checked";
            cb2.Content = "ON";
            txtArea.Visibility = Visibility.Visible;
            cb2.Background = Brushes.LightGreen;
        }

        /*
         * Method Name: HandleUnCheck()
         * Description: This method turns off the live mood of the ground terminal system 
         * Parameters: object sender, RoutedEventArgs e
         * Return value : void
         */
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            //text2.Text = "Button is unchecked.";
            cb2.Content = "OFF";
            txtArea.Visibility = Visibility.Hidden;
            cb2.Background = Brushes.Red;
        }

    }


}
