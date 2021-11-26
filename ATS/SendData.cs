using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;


namespace AirTransmissionSystem
{
    class SendData
    {
		
		
		
		//FUNCTION NAME				:				public void SendLines(string argLineToSend)
		//PARAMETERS				:				string argLineToSend 	- the string we are to send to the server
		//DESCRIPTION				:				This function is to establish a connection between the AirTransmissionSystem which acts as the client
		//											and the Ground Terminal system which acts as the server, and send the fromatted string to it.
		//RETURN					:				NONE
        public void SendLines(string argLineToSend)
        {
            try
            {
                Int32 port = 13000;
                string server = "172,26,159,80";
                TcpClient client = new TcpClient(server, port);
                Byte[] data = Encoding.ASCII.GetBytes(argLineToSend);
                Stream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", argLineToSend);
                data = new Byte[256];
                String responseData = String.Empty;
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }


    }
}
