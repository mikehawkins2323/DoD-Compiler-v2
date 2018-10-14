using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD_Compiler_v2
{
    class airfieldPlateData
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string ICAO { get; set; }
        public List<string[]> Plates { get; set; }

        public airfieldPlateData() { }

        public airfieldPlateData(string name, string country, string icao, List<string[]> appPlates)
        {
            //set values that directly transfer
            Name = name;
            Country = country;
            ICAO = icao;
            //add the supplement and the approach plates to the dictionary
            Plates = appPlates;
            //sort the plates to standard order
            //reorder strings for plate order
            int currentPlatePos = 0;
            int currentSearchTerm = 0;
            string[] sortOrder = new string[] { "supplement", "arpt dia", "ifrtkoff", "radar", "dep", "ils", "loc", "tacan", "vor", "ndb", "dme", "radmin", "altmin", "rnav" };
            while (currentSearchTerm < sortOrder.Length && currentPlatePos < Plates.Count)
            {
                for (int q = currentPlatePos; q < Plates.Count(); q++)
                {
                    if (Plates[q][0].ToLower().Contains(sortOrder[currentSearchTerm]))
                    {
                        string[] tempVal = Plates[currentPlatePos];
                        Plates[currentPlatePos] = Plates[q];
                        Plates[q] = tempVal;
                        currentPlatePos++;
                        break;
                    }
                    else { if (q == Plates.Count - 1) { currentSearchTerm++; } }
                }
            }
        }
    }
}
