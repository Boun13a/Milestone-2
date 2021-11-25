/*
* Class : WriteToTextFiLE
* DESCRIPTION : This file contains the class that manages writing of telemetry data the user
*  searches in a text file that has the same name as the tailnumber used to search
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundTerminalSystem
{
     class WriteToTextFile
    {

        /*
         * Method Name: persistToTextFile()
         * Description: This method writes the telemetry data to the text file that
         * is named after the tailnumber being used to search the database
         * Return value : void
         */
        public static void persistToTextFile(string filename, string message)
        {
            // Write file using StreamWriter  
            filename = AppDomain.CurrentDomain.BaseDirectory + filename + ".txt";
            using (StreamWriter writer = File.AppendText(filename))
            {

                writer.WriteLine(message);
            }
        }
    }
}
