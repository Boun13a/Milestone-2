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


        /*
         * Method Name: insertIntoAttitudeTable()
         * Description: This method inserts telemetry data into the attitude table of the database
         * Parameters: string tailNumber, string altitude, string pitch, string bank
         * Return value : void
         */
        public void insertToAttitudeTable(string tailNumber, string altitude, string pitch, string bank)
        {
            try
            {
                string attitudeQuery = "INSERT INTO attitude(tailNumber,altitude,pitch,bank) VALUES('" + tailNumber + "','" + altitude + "','" + pitch + "','" + bank + "'); ";
                //start the SQL command using the connection string
                MySqlCommand sqlAttitudeCommand = new MySqlCommand(attitudeQuery, cnn);
                sqlAttitudeCommand.Prepare();
                sqlAttitudeCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                cnn.Close();
            }

        }


        /*
         * Method Name: insertIntoGforceTable()
         * Description: This method inserts telemetry data into the Gforce table of the database
         * string
         * Parameters: string tailNumber, string accelX, string accelY, string accelZ, string weight
         * Return value : void
         */
        public void insertToGforceTable(string tailNumber, string accelX, string accelY, string accelZ, string weight)
        {
            try
            {
                string gForceQuery = "INSERT INTO gforce(tailNumber,accelX,accelY,accelZ,weight) VALUES('" + tailNumber + "','" + accelX + "','" + accelY + "','" + accelZ + "', '" + weight + "'); ";
                //start the SQL commnad with provided query
                MySqlCommand sqlGforceCommand = new MySqlCommand(gForceQuery, cnn);
                sqlGforceCommand.Prepare();
                sqlGforceCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                cnn.Close();
            }
        }

        /*
         * Method Name: searchDtabase()
         * Description: This method searches the database with the given tailNumber string
         * Parameters: string result
         * Return value : void
         */
        public void searchDtatBase(string result)
        {

            try
            {

                //prepare the search query
                string QuerySearch = "SELECT G._timeStamp, G.tailNumber, G.accelX,G.accelY,G.accelZ,G.weight,A.altitude, A.pitch,A.bank FROM gforce G INNER JOIN attitude A ON A.aID = G.gID WHERE A.tailNumber  = ('" + result + "');";
                //use the opened connection
                using (cnn)
                {
                    // connection.Open();
                    using (var command = new MySqlCommand(QuerySearch, cnn))
                    {

                        command.Prepare();

                        using (var reader = command.ExecuteReader())
                        {
                            //read all the data that is associated with the given tailnumber
                            while (reader.Read())
                            {
                                var col1 = reader.GetString(0);
                                var col2 = reader.GetString(1);
                                var col3 = reader.GetString(2);
                                var col4 = reader.GetString(3);
                                var col5 = reader.GetString(4);
                                var col6 = reader.GetString(5);
                                var col7 = reader.GetString(6);
                                var col8 = reader.GetString(7);
                                var col9 = reader.GetString(8);
                                var entireCol = col1 + ", " + col2 + ", " + col3 + ", " + col4 + ", " + col5 + ", " + col6 + ", " + col7 + ", " + col8 + ", " + col9;


                                //display the data on the UI
                                foreach (Window window in Application.Current.Windows)
                                {
                                    if (window.GetType() == typeof(MainWindow))
                                    {
                                        (window as MainWindow).txtArea.Items.Add(entireCol);

                                    }
                                    //also write to the text file


                                }
                                WriteToTextFile.persistToTextFile(result, entireCol);

                            }

                        }

                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                cnn.Close();
            }
        }


    }
}
