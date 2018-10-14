using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD_Compiler_v2
{
    class airfieldWorldData
    {
        //http://openflights.org/data.html

        public string IDNo { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Alt { get; set; }
        public string TimeZone { get; set; }
        public string DST { get; set; }
        public string TimeZoneDatabase { get; set; }

        public airfieldWorldData() { }

        public airfieldWorldData(string[] data)
        {
            IDNo = data[0];
            Name = data[1];
            City = data[2];
            Country = data[3];
            IATA = data[4];
            ICAO = data[5];
            Lat = data[6];
            Long = data[7];
            Alt = data[8];
            TimeZone = data[9];
            DST = data[10];
            TimeZoneDatabase = data[11];
        }
    }
}
