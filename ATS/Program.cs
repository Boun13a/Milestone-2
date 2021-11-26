using System;
using System.Threading;
using System.IO;

namespace AirTransmissionSystem
{
    class Program
    {
        public static ReadFromFile read = new ReadFromFile();


        static void Main(string[] args)
        {
            Thread thread1 = new Thread(ThreadProc);
            Thread thread2 = new Thread(ThreadProc);
            Thread thread3 = new Thread(ThreadProc);

            thread1.Name = "C-FGAX";
            thread2.Name = "C-GEFC";
            thread3.Name = "C-QWWT";

            thread1.Start();

            thread2.Start();

            thread3.Start();
        }


		//FUNCTION NAME				:				public static void ThreadProc()
		//PARAMETERS				:				VOID
		//DESCRIPTION				:				This function is to get how many line numbers in each file and pass it to ControlResource function
		//											which is going to read line by line from files.
		//RETURN					:				NONE
        public static void ThreadProc()
        {
            ReadFromFile read = new ReadFromFile();

            int[] fileLine = new int[3];

            string airPlane1 = "C-FGAX";
            string airPlane2 = "C-GEFC";
            string airPlane3 = "C-QWWT";

            fileLine[0] = read.CountLines(airPlane1);
            fileLine[1] = read.CountLines(airPlane2);
            fileLine[2] = read.CountLines(airPlane3);

            if (Thread.CurrentThread.Name == "C-FGAX")
            {
                ControlResource(fileLine[0]);
            }
            if (Thread.CurrentThread.Name == "C-GEFC")
            {
                ControlResource(fileLine[1]);
            }
            if (Thread.CurrentThread.Name == "C-QWWT")
            {
                ControlResource(fileLine[2]);
            }

        }




		
		//FUNCTION NAME				:				public static void ControlResource(int argLineNumbers)
		//PARAMETERS				:				int argLineNumbers - the number of lines contained in each txt file.
		//DESCRIPTION				:				This function is to validate which thread can read from which file, and pass in the total line numbers
		//											as well as the thread name which corresponds to the file name.
		//RETURN					:				NONE
        public static void ControlResource(int argLineNumbers)
        {
            int i = 0;
            string fileName;

            if (Thread.CurrentThread.Name == "C-FGAX")
            {
                fileName = Thread.CurrentThread.Name + ".txt";
                read.ReadData(argLineNumbers, fileName);
            }

            if (Thread.CurrentThread.Name == "C-GEFC")
            {
                fileName = Thread.CurrentThread.Name + ".txt";
                read.ReadData(argLineNumbers, fileName);
            }


            if (Thread.CurrentThread.Name == "C-QWWT")
            {
                fileName = Thread.CurrentThread.Name + ".txt";
                read.ReadData(argLineNumbers, fileName);
            }
        }
    }
}
