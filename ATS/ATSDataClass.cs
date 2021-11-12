using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace AirTransmissionSystem
{
    class ATSDataClass
    {
        private string rawString;
        private string fileName;
        private string line;
        private string stringToSend;
        private string airCraftTail;
        private uint sequenceNumber;
        private int checkSum;


        public uint SequenceNumber{ get; set; }
        public int CheckSum { get; set; }
        public string AirCraftTail { get; set; }
        public string StringToSend{ get; set; }
        public string Line{ get; set; }
        public string FileName { get; set; }
        public string RawString { get; set; }
    }
}
