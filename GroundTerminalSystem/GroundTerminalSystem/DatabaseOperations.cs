/*
* Class : DatabaseOperations.cs
* DESCRIPTION : This file contains the class that manages the database operations
* this includes opening and closing of the database connections as well as inserting
* telemetry database into their respective talbles
*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GroundTerminalSystem
{
    class DatabaseOperations
    {
        private MySqlConnection cnn;

        /*
         * Method Name: ConnectToDatabase()
         * Description: This method opens the connection to the database using SQL connection
         * string
         * Parameters: string connectionString
         * Return value : void
         */

        public MySqlConnection ConnectToDatabase(string connectionString)
        {

            try
            {
                cnn = new MySqlConnection(connectionString);


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                cnn.Close();
            }
            return cnn;
        }
    }
}
