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
        public MainWindow()
        {

            InitializeComponent();
            
            //Thread thStart = new Thread(startServer);
            //thStart.Start();
        }



        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            
            //text2.Text = "Button is Checked";
            cb2.Content = "ON";
            txtArea.Visibility = Visibility.Visible;
        }
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            //text2.Text = "Button is unchecked.";
            cb2.Content = "OFF";
            txtArea.Visibility = Visibility.Hidden;
        }
    }

    
}
