using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransmissionSystem
{
    class StringFormat
    {
        ATSDataClass objATS = new ATSDataClass();
		
		
		
		//FUNCTION NAME				:				public string StringToFormat(string argStringToFormat, string argAirCraftTail,int argSequenceNumber)
		//PARAMETERS				:				string argStringToFormath - the raw string we gonna have to format.
		//											string argAirCraftTail 	  - the Aircraft tail #.
		//											int argSequenceNumber     - The sequence number of each packet.
		//DESCRIPTION				:				This function is to format the string as required, calculate the checksum and construct the string before sending it
		//											to the server
		//RETURN					:				objATS.StringToSend - the formatted string.
        public string StringToFormat(string argStringToFormat, string argAirCraftTail,int argSequenceNumber)
        {
            int checkSum;
            string formatedString;



            string[] temp = argStringToFormat.Split(",");
            string[] result = new string[8];;

            for(int i = 0; i < 8; i++)
            {
                result[i] = temp[i].Trim();
            }

            checkSum = (int)(Convert.ToDouble(result[5]) + Convert.ToDouble(result[6]) + Convert.ToDouble(result[7])) / 3;

            Console.WriteLine("CheckSum = {0}", checkSum);
            formatedString = argAirCraftTail + "," + argSequenceNumber + "," + string.Join(",", result) + "," + checkSum;
            objATS.StringToSend = formatedString;

            Console.WriteLine(formatedString);
            Console.WriteLine("\n");

            return objATS.StringToSend;
        }
    }
}