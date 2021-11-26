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



		//FUNCTION NAME				:				public void ReadData(int argLineNumbers,string argFileName)
		//PARAMETERS				:				string argFileName - the txt file name to read line from.
		//											int argLineNumbers - contains the total lines in each of the file.
		//DESCRIPTION				:				This function is to read from text file and if it's the end of the file it's going to abort the thread
		//											that is reading from the corresponding file. mutex is implemented in order to control each resource
		//											and have a 1 second delay for every line read.
		//RETURN					:				NONE
        public void ReadData(int argLineNumbers,string argFileName)
        {
            int i = 0;

            if (Thread.CurrentThread.Name == "C-FGAX")
            {

                var path = Path.Combine(Directory.GetCurrentDirectory(), argFileName);						//get the txt file path dinamically
                using (StreamReader dataFile = new StreamReader(path))
                {

                    for (i = 0; i < argLineNumbers; i++)					//check if it is less than the total lines in C-FGAX.txt
                    {
                        if (i == argLineNumbers)
                        {
                            Thread.CurrentThread.Abort();					//aborth this thread if its = total lines.
                        }
                        else
                        {
                            lockThread.WaitOne();							//lock this resource for this thread
                            data.SequenceNumber += 1;						//To increment the sequence number.
                            data.Line = dataFile.ReadLine();				//Read the line from the txt file
                            data.StringToSend = format.StringToFormat(data.Line, Thread.CurrentThread.Name,(int)(data.SequenceNumber));				//format the string
                            sendLine.SendLines(data.StringToSend);				//send the formatted string to the send function to be sent to the server.
                            Thread.Sleep(1000);             					//sleep 1 second
                            lockThread.ReleaseMutex();							//release the mutex
                        }
                    }
                }
            }

            if (Thread.CurrentThread.Name == "C-GEFC")
            {
                data.Line = Thread.CurrentThread.Name + ".txt";
                var path = Path.Combine(Directory.GetCurrentDirectory(), argFileName);
                using (StreamReader dataFile = new StreamReader(path))
                {

                    for (i = 0; i < argLineNumbers; i++)
                    {
                        if (i == argLineNumbers)							//check if it is less than the total lines in C-GEFC.txt
                        {
                            Thread.CurrentThread.Abort();					//aborth this thread if its = total lines.
                        }
                        else
                        {
                            lockThread.WaitOne();							//lock this resource for this thread
                            data.SequenceNumber += 1;						//To increment the sequence number.
                            data.Line = dataFile.ReadLine();				//Read the line from the txt file
                            data.StringToSend = format.StringToFormat(data.Line, Thread.CurrentThread.Name, (int)(data.SequenceNumber));			//format the string
                            sendLine.SendLines(data.StringToSend);				//send the formatted string to the send function to be sent to the server.
                            Thread.Sleep(1000);            						//sleep 1 second
                            lockThread.ReleaseMutex();							//release the mutex
                        }
                    }
                }
            }

            if (Thread.CurrentThread.Name == "C-QWWT")
            {
                data.Line = Thread.CurrentThread.Name + ".txt";
                var path = Path.Combine(Directory.GetCurrentDirectory(), argFileName);
                using (StreamReader dataFile = new StreamReader(path))
                {

                    for (i = 0; i < argLineNumbers; i++)
                    {
                        if (i == argLineNumbers)							//check if it is less than the total lines in C-GEFC.txt
                        {
                            Thread.CurrentThread.Abort();					//aborth this thread if its = total lines.
                        }
                        else
                        {
                            lockThread.WaitOne();							//lock this resource for this thread
                            data.SequenceNumber += 1;						//To increment the sequence number.
                            data.Line = dataFile.ReadLine();				//Read the line from the txt file
                            data.StringToSend = format.StringToFormat(data.Line, Thread.CurrentThread.Name, (int)(data.SequenceNumber));			//format the string
                            sendLine.SendLines(data.StringToSend);				//send the formatted string to the send function to be sent to the server.
                            Thread.Sleep(1000);             					//sleep 1 second
                            lockThread.ReleaseMutex();							//release the mutex
                        }
                    }
                }
            }
           
        }
	


		//FUNCTION NAME				:				public int CountLines(string argFilePath)
		//PARAMETERS				:				string argFilePath - the txt file to count the number of lines from.
		//DESCRIPTION				:				This function is to count how many lines in each of the txt file.
		//RETURN					:				Int lineCounter - the number of lines corresponding the the txt file.
        public int CountLines(string argFilePath)
        {
            int lineCounter = 0;
            argFilePath += ".txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), argFilePath);
            foreach (string line in File.ReadLines(path))
            {
                lineCounter++;
            }
            return lineCounter;
        }


    }
}
