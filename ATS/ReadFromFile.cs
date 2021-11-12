using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;

namespace AirTransmissionSystem
{
    class ReadFromFile
    {	
		
		
        ATSDataClass data = new ATSDataClass();
        StringFormat format = new StringFormat();
        Mutex lockThread = new Mutex();
        SendData sendLine = new SendData();
		
		
		//FUNCTION NAME				:				public ReadFromFile(string argFileName)
		//PARAMETERS				:				string argFileName - the txt file name to read line from.
		//DESCRIPTION				:				This is a constructor of the ReadFromFile class which initialize the fileName data.
		//RETURN					:				NONE
        public ReadFromFile(string argFileName)
        {
            data.FileName = argFileName;

        }
		
		
		//FUNCTION NAME				:				public ReadFromFile()
		//PARAMETERS				:				VOID
		//DESCRIPTION				:				Default constructor for the ReadFromFile class
		//RETURN					:				NONE
        public ReadFromFile()
        {
        }


		//FUNCTION NAME				:				public int CountLines(string argFilePath)
		//PARAMETERS				:				string argFilePath - the txt file to count the number of lines from.
		//DESCRIPTION				:				This function is to count how many lines in each of the txt file.
		//RETURN					:				Int lineCounter - the number of lines corresponding the the txt file.
        public int CountLines(string argFilePath)
        {
            int lineCounter = 0;
            argFilePath += ".txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), argFilePath);s
            foreach (string line in File.ReadLines(path))
            {
                lineCounter++;
            }
            return lineCounter;
        }


    }
}
