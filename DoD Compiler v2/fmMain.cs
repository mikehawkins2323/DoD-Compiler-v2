using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using iTextSharp;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoD_Compiler_v2
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();

            loadWorldAirfieldData();
            loadAreaDiscLocations();
        }

        string strDiscLoc;
        string strOutputLoc;
        List<string[]> areaDiscLocations;
        string strTerminalsLegend = @"\userinfo\terminal_legend.pdf";
        string docVersion;
        string[] effectiveDates = new string[2];
        string authorName = null;

        bool[] blAreaSelected = new bool[8]; //set size when disc locations are imported
        bool blOutputSingleDocument;

        List<airfieldWorldData> worldAirfieldData;
        List<string> listICAOcodes;

        //Thread termThread = new Thread(new ThreadStart(createTerminalsThread));

        //Application load
        private void loadPreviousDetails()
        {

        }
        private string findDirectory(string search)
        {
            //Takes a very long time, do not use unless refined massively
            //Finds the drives on computer and then gets searchDirectoryTree to recursively search
            List<string> possibleLocations = new List<string>();
            foreach (string drive in Directory.GetLogicalDrives())
            {
                string result = searchDirectoryTree(drive, search);
                //returns the first occurence of the search found
                if(result != null) { return result; }
            }
            //if no result on any drive return null
            return null;
        }
        private string searchDirectoryTree(string dirIn, string searchTerm)
        {
            //try catch statements in case folder access is denied
            //returns a result if the search term is found, only returns the first (alphabetical) result
            try { return Directory.GetDirectories(dirIn, searchTerm)[0]; } catch { }
            try
            {
                //searches each directory to the last level
                foreach (string dir in Directory.GetDirectories(dirIn))
                {
                    string result = searchDirectoryTree(dir, searchTerm);
                    //if a result is found it will be passed all the way to the top and back to findDirectory
                    if (result != null) { return result; }
                }
            }
            catch { }
            //at end of directory and no result return null
            return null;
        }

        private void createPDFTerminals(List<airfieldPlateData> inputAirfields, int[] areas)
        {         
            //Setup
            PdfDocument pdfDoc = new PdfDocument();
            PdfOutline.PdfOutlineCollection outlines = pdfDoc.Outlines;
            int intPageNo = 0;
            int intPagesAdded = 0;
            PdfDocument inputDocument;
            //Cover page
            PdfPage p = new PdfPage();
            p.Size = PdfSharp.PageSize.A5;
            pdfDoc.AddPage(p);
            createCoverPage(p, areas);
            outlines.Add("Cover", pdfDoc.Pages[intPageNo]);
            intPagesAdded++;
            intPageNo = intPagesAdded;
            //Special Notices
            bool mainBookmark = false;
            for (int m = 0; m < areas.Length; m++)
            {
                inputDocument = CompatiblePdfReader.Open(strDiscLoc + areaDiscLocations[areas[m]][0]);
                for (int k = 0; k < inputDocument.PageCount; k++)
                {
                    pdfDoc.AddPage(inputDocument.Pages[k]);
                    intPagesAdded++;
                }
                //Bookmark
                outlines = pdfDoc.Outlines;
                if (!mainBookmark)
                {
                    outlines.Add("Special Notices", pdfDoc.Pages[intPageNo]); mainBookmark = true;
                }
                outlines = outlines[outlines.Count - 1].Outlines;
                outlines.Add(areaDiscLocations[areas[m]][4], pdfDoc.Pages[intPageNo]);
                //add to page count   
                intPageNo = intPagesAdded;
            }
            //Legends
            inputDocument = CompatiblePdfReader.Open(strDiscLoc + strTerminalsLegend);
            List<bookmarksLayout> inputBookmarks = importPDFDocumentBookmarks(strDiscLoc + strTerminalsLegend);
            mainBookmark = false;
            bool includePage = false;
            for (int k = 0; k < inputDocument.PageCount; k++)
            {
                bookmarksLayout inputLevelOneBook = inputBookmarks.FindAll(x => x.Level == 1).Find(y => y.Page == k + 1);
                if (inputLevelOneBook != null)
                {
                    includePage = false;
                    for (int m = 0; m < areas.Length; m++)
                    {
                        if (inputLevelOneBook.Text.Contains(areaDiscLocations[areas[m]][3]))
                        {
                            includePage = true;
                        }
                    }
                }
                if (includePage)
                {
                    pdfDoc.AddPage(inputDocument.Pages[k]);
                    //bookmarks
                    outlines = pdfDoc.Outlines;
                    //-main
                    if (!mainBookmark) {
                        outlines.Add("Legend", pdfDoc.Pages[intPageNo]); mainBookmark = true; }
                    //-area
                    outlines = outlines[outlines.Count - 1].Outlines;
                    if(inputLevelOneBook != null) {
                        outlines.Add(inputLevelOneBook.Text, pdfDoc.Pages[intPageNo]); }
                    //-page
                    bookmarksLayout pageBookmark = inputBookmarks.FindAll(x => x.Level == 2).Find(y => y.Page == k + 1);
                    outlines = outlines[outlines.Count - 1].Outlines;
                    if (pageBookmark != null) { outlines.Add(pageBookmark.Text, pdfDoc.Pages[intPageNo]); }
                    //add to page count
                    intPagesAdded++;
                    intPageNo = intPagesAdded;
                }
            }

            //Add approach plates
            for (int i = 0; i < inputAirfields.Count(); i++)
            {
                for (int j = 0; j < inputAirfields[i].Plates.Count; j++)
                {

                    inputDocument = CompatiblePdfReader.Open(inputAirfields[i].Plates[j][1]);
                    for (int k = 0; k < inputDocument.PageCount; k++)
                    {
                        pdfDoc.AddPage(inputDocument.Pages[k]);
                        intPagesAdded++;
                    }
                    //add bookmarks
                    outlines = pdfDoc.Outlines;                   
                    if (j == 0)
                    {
                        //if start of doc or previous country not the same add a new country bookmark
                        //absolute allows same statemant for first and all values of i, caught by i == 0 check
                        if (i == 0 || inputAirfields[Math.Abs(i - 1)].Country != inputAirfields[i].Country)
                        {
                            outlines.Add(inputAirfields[i].Country, pdfDoc.Pages[intPageNo]);
                        }
                        outlines = outlines[outlines.Count - 1].Outlines;
                        outlines.Add(inputAirfields[i].Name, pdfDoc.Pages[intPageNo]);
                    }
                    else { outlines = outlines[outlines.Count - 1].Outlines; }
                    outlines = outlines[outlines.Count - 1].Outlines;
                    outlines.Add(inputAirfields[i].Plates[j][0], pdfDoc.Pages[intPageNo]);
                    intPageNo = intPagesAdded;
                }
            }
            //Save and close
            string areaFileName = "";
            for (int m = 0; m < areas.Length; m++)
            {
                areaFileName += areaDiscLocations[areas[m]][4] + " ";
            }
            pdfDoc.Save(strOutputLoc + @"\" + areaFileName + "Terminals " + docVersion  + ".pdf");
            pdfDoc.Close();
        }

        private void loadWorldAirfieldData()
        {
            worldAirfieldData = new List<airfieldWorldData>();
            listICAOcodes = new List<string>();
            //get info from config file
            string fileContent = resFiles.airportsDirectory;
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //add line of chart info
                    string[] values = line.Split(new string[] { "," },
                    StringSplitOptions.None);
                    airfieldWorldData c = new airfieldWorldData(values);
                    listICAOcodes.Add(c.ICAO);
                    worldAirfieldData.Add(c);
                }
            }
        }
        private void loadAreaDiscLocations()
        {
            areaDiscLocations = new List<string[]>();
            //get info from config file
            string fileContent = resFiles.DoDdiskDirectories;
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //add line to list
                    areaDiscLocations.Add(line.Split(new string[] { "," }, StringSplitOptions.None));
                }
            }
        }
        private List<airfieldPlateData> getDoDPlatesInformation(int areaIdent)
        {
            List<airfieldPlateData> inputAirfieldsData = new List<airfieldPlateData>();
            string sourceFolder = strDiscLoc + areaDiscLocations[areaIdent][1];
            string[] airfields = Directory.GetDirectories(sourceFolder);
            for (int i = 0; i < airfields.Length; i++)
            {
                //Get ICAO code of input document
                string inputICAOcode = airfields[i].Remove(0, airfields[i].Length - 4).ToUpper().Replace("_","");
                //Get Name given by DoD document
                string airfieldLoc = TitleCase(Path.GetFileNameWithoutExtension(airfields[i]).Replace("__", "_").Replace("_", " ").Trim());
                //Compare ICAO code to database to get country
                airfieldWorldData worldData = worldAirfieldData.Find(x => x.ICAO == inputICAOcode);
                //if(worldData == null) { worldData = worldAirfieldData.Find(x => x.IATA == inputICAOcode); }
                if (worldData == null) { worldData = new airfieldWorldData(); worldData.Country = " Unknown"; worldData.ICAO = inputICAOcode; }
                //Get approach plates for the airfield and add to list
                List<string> inputPlateFiles = new List<string>();
                inputPlateFiles.AddRange(Directory.GetFiles(airfields[i]));
                List<string[]> inputPlates = new List<string[]>();
                foreach (string p in inputPlateFiles)
                {
                    //Get the name of the plate
                    string plateName = TitleCase(Path.GetFileNameWithoutExtension(p).Replace("__", "_").Replace("_", " ").Trim());
                    //Add to the dicionary
                    inputPlates.Add(new string[] { plateName, p });
                }
                //Find the supplement if available
                string[] strSuppPageFiles = Directory.GetFiles(strDiscLoc + areaDiscLocations[areaIdent][2]);
                for (int a = 0; a < strSuppPageFiles.Length; a++)
                {
                    if (airfields[i].Replace("_", "").Replace("-", "").Contains(Path.GetFileNameWithoutExtension(strSuppPageFiles[a]).Replace("_", "")))
                    {
                        inputPlates.Add(new string[] { "Supplement", strSuppPageFiles[a] });
                        break;
                    }

                }
                //Add airfield to list 
                inputAirfieldsData.Add(new airfieldPlateData(airfieldLoc, worldData.Country, worldData.ICAO, inputPlates));
            }
            return inputAirfieldsData;
        }

        private void createCoverPage(PdfPage p, int[] areas)
        {
            //graphics
            XGraphics xgr = XGraphics.FromPdfPage(p);
            //fonts, brushes, alignments
            const string facename = "Microsoft Sans Serif";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont[] fonts = new XFont[] { new XFont(facename, 50, XFontStyle.Regular, options),
                                          new XFont(facename, 20, XFontStyle.Regular, options),
                                          new XFont(facename, 34, XFontStyle.Bold, options),
                                          new XFont(facename, 20, XFontStyle.Bold, options),
                                          new XFont(facename, 20, XFontStyle.Regular, options),
                                          new XFont(facename, 16, XFontStyle.Regular, options),
                                          new XFont(facename, 11, XFontStyle.Regular, options)
                                          };
            XBrush brush = XBrushes.Black;
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            double y = p.Height * 0.05;
            //setup text
            List<string> linesOfText = new List<string>();
            List<int> fontID = new List<int>();
            linesOfText.AddRange(new string[] { "92WG", "COMPILED", "US DOD", "TERMINALS", "EFF: " + effectiveDates[0] + " - " + effectiveDates[1], "VERSION: " + docVersion, "", "THIS DOCUMENT CONTAINS:", "" });
            fontID.AddRange(new int[] { 0, 1, 2, 2, 3, 3, 3, 4, 4 });
            for (int q = 0; q < areas.Length; q++)
            {
                linesOfText.Add(areaDiscLocations[areas[q]][3] + " TERMINALS");
                fontID.Add(5);
            }
            string suppNote = "SUPPLEMENT INCLUDED FOR FIELDS IN TERMINAL";
            linesOfText.AddRange(new string[] { "", "TCNs EFFECTIVE THIS PERIOD ALSO INCLUDED", suppNote });
            fontID.AddRange(new int[] { 6, 6, 6 });
            //draw text
            double lineSpace = 0.0;
            XRect rect = new XRect(0, 0, 0, 0);
            for (int lineNo = 0; lineNo < linesOfText.Count; lineNo++)
            {
                lineSpace = fonts[fontID[lineNo]].GetHeight(xgr);
                rect = new XRect(0, y, p.Width, lineSpace);
                xgr.DrawString(linesOfText[lineNo], fonts[fontID[lineNo]], brush, rect, format);
                y += lineSpace;
            }
            //Bottom right text
            format.Alignment = XStringAlignment.Near;
            y = p.Height - p.Height * 0.1;
            double x = p.Width * 0.1;
            lineSpace = fonts[6].GetHeight(xgr);
            rect = new XRect(x, y, p.Width - x, lineSpace);
            xgr.DrawString("COMPILED BY: " + authorName, fonts[6], brush, rect, format);
            y += lineSpace;
            rect = new XRect(x, y, p.Width - x, lineSpace);
            xgr.DrawString("ON: " + DateTime.Now.ToString("ddMMMyy").ToUpper(), fonts[6], brush, rect, format);
        }

        private List<bookmarksLayout> importPDFDocumentBookmarks(string inputDocumentLocation)
        {
            //initialise reader to get bookmarks using itextsharp
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(inputDocumentLocation);
            //Get level 0 bookmarks
            IList<Dictionary<string, object>> bookmarks = iTextSharp.text.pdf.SimpleBookmark.GetBookmark(pdfReader);
            List<bookmarksLayout> allBookmarks = new List<bookmarksLayout>();
            //Loop to get all children bookmarks
            for (int x = 0; x < bookmarks.Count; x++)
            {
                bookmarksLayout book = new bookmarksLayout();
                string titleInput = (string)bookmarks[x]["Title"];
                book.Text = new string(titleInput.Where(c => !char.IsControl(c)).ToArray());
                book.Page = Convert.ToInt16(System.Text.RegularExpressions.Regex.Match((string)bookmarks[x]["Page"], @"\d+").Value);
                book.Level = 0;
                allBookmarks.Add(book);

                allBookmarks.AddRange(getKids(bookmarks[x], 1));
            }
            return allBookmarks;
        }
        private List<bookmarksLayout> getKids(Dictionary<string, object> bookmarks, int currentLevel)
        {
            List<bookmarksLayout> bookList = new List<bookmarksLayout>();
            if (bookmarks.ContainsKey("Kids"))
            {
                IList<Dictionary<string, object>> kids = (IList<Dictionary<string, object>>)bookmarks["Kids"];    
                for (int y = 0; y < kids.Count; y++)
                {
                    bookmarksLayout book = new bookmarksLayout();
                    string titleInput = (string)kids[y]["Title"];
                    book.Text = new string(titleInput.Where(c => !char.IsControl(c)).ToArray());
                    book.Page = Convert.ToInt16(System.Text.RegularExpressions.Regex.Match((string)kids[y]["Page"], @"\d+").Value);
                    book.Level = currentLevel;
                    bookList.Add(book);

                    bookList.AddRange(getKids(kids[y], currentLevel + 1));
                }
            }
            return bookList;
        }
        public class bookmarksLayout
        {
            public string Text { get; set; }
            public int Page { get; set; }
            public int Level { get; set; }
        }


        //User inputs
        //-Directories
        private void btnFindDisc_Click(object sender, EventArgs e)
        {
            //If default message is in textbox, folder browser will go to highest level, otherwise open folder in textbox
            string selectedLoc = getDirectoriesDialog(txtDiscLocation.Text, "Please select the location of the US DoD FLIP DVD.");
            if (selectedLoc != txtDiscLocation.Text)
            {
                txtDiscLocation.Text = selectedLoc;
                txtDiscLocation.ForeColor = Color.Black;
            }
        }
        private void btnChooseSave_Click(object sender, EventArgs e)
        {
            //If default message is in textbox, folder browser will go to highest level, otherwise open folder in textbox
            string selectedLoc = getDirectoriesDialog(txtSaveLoc.Text, "Please select the location you would like the output file to go.");
            if (selectedLoc != txtSaveLoc.Text)
            {
                txtSaveLoc.Text = selectedLoc;
                txtSaveLoc.ForeColor = Color.Black;
            }
        }
        private void txtDirectoriesSel_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if(t.ForeColor == Color.DarkGray)
            {
                t.Text = "";
                t.ForeColor = Color.Black;
            }
        }
        private void txtDirectoriesSel_Leave(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.Text = t.Text.Trim();
            if (t.Text.Length == 0)
            {
                t.Text = t.Tag.ToString();
                t.ForeColor = Color.DarkGray;
            }
        }
        //--Directory Browser
        private string getDirectoriesDialog(string inputPath, string message)
        {
            FolderBrowserDialog fbdFolderBrowser = new FolderBrowserDialog();
            fbdFolderBrowser.SelectedPath = inputPath;
            fbdFolderBrowser.Description = message;
            DialogResult result = fbdFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                return fbdFolderBrowser.SelectedPath;
            }
            return inputPath;
        }
        //-Areas
        private void chkAreas_CheckedChanged(object sender, EventArgs e)
        {
            int numAreasSel = 0;
            foreach(CheckBox chk in gpAreas.Controls)
            {
                if (chk.Checked == true) { numAreasSel++; }
            }
            if(numAreasSel > 1) { chkCombineAreas.Enabled = true; }
            else { chkCombineAreas.Enabled = false; }
        }
        //-Compile
        private void btnCreate_Click(object sender, EventArgs e)
        {
           // if (!backwkrCompileDoc.IsBusy)
           // {
                strDiscLoc = txtDiscLocation.Text;
                strOutputLoc = txtSaveLoc.Text;
                docVersion = txtVersion.Text;
                foreach (CheckBox chk in gpAreas.Controls)
                {
                    if (chk.Checked) { blAreaSelected[Convert.ToInt32(chk.Tag)] = true; } else { blAreaSelected[Convert.ToInt32(chk.Tag)] = false; }
                }
                blOutputSingleDocument = chkCombineAreas.Checked;
                effectiveDates[0] = dtpEffectiveFrom.Value.ToString("ddMMMyy").ToUpper();
                effectiveDates[1] = dtpEffectiveTo.Value.ToString("ddMMMyy").ToUpper();
                authorName = txtAuthorName.Text;
                if (checkDirectoriesCorrect(strDiscLoc, strOutputLoc))
                {
                    backwkrCompileDoc.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Unable to find the FLIP DVD at the specified location.\r\nPlease check the directory is correct and try again.", "DoD Compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
        }

        //--Check directories
        private bool checkDirectoriesCorrect(string inDir, string outDir)
        {
            if(!Directory.Exists(strOutputLoc))
            {
                strOutputLoc = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                MessageBox.Show("The save directory could not be found.\r\nThe file(s) will be saved to your desktop.", "DoD Compiler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Really rough check that the location selected is correct
            //Later add a more detailed check
            if (Directory.Exists(inDir) && Directory.Exists(inDir + @"\terminals") && Directory.Exists(inDir + @"\splitdocs")) { return true; }
            return false;
        }


        public string TitleCase(string value)
        {
            string titleString = ""; // destination string, this will be returned by function
            if (!String.IsNullOrEmpty(value))
            {
                string[] lowerCases = new string[] { "of", "the", "in", "a", "an", "to", "and", "at", "from", "by", "on", "or" }; // list of lower case words that should only be capitalised at start and end of title
                string[] specialCases = new string[] { "ARPT", "RWY", "RWYS", "ILS", "LOC", "LOCDME", "VOR", "VORDME", "DVOR", "DVORDME", "TACAN", "TCN", "NDB", "GNSS", "ARC", "DIA", "IFRTKOFF", "RADMIN", "ALTMIN", "RNAV", "GPS", "RADAR", "RDR", "DEP", "SID", "STAR", "INTL", "AB", "AAF", "AFB", "NAS", "PMRF" }; // list of words that need capitalisation preserved at any point in title

                string[] words = value.ToLower().Split(' ');
                bool wordAdded = false; // flag to confirm whether this word appears in special case list
                int counter = 1;
                foreach (string s in words)
                {

                    // check if word appears in lower case list
                    foreach (string lcWord in lowerCases)
                    {
                        if (s.ToLower() == lcWord)
                        {
                            // if lower case word is the first or last word of the title then it still needs capital so skip this bit.
                            if (counter == 0 || counter == words.Length) { break; };
                            titleString += lcWord;
                            wordAdded = true;
                            break;
                        }
                    }

                    // check if word appears in special case list odr ICAO
                    foreach (string scWord in specialCases)
                    {
                        if (s.ToUpper() == scWord.ToUpper())
                        {
                            titleString += scWord;
                            wordAdded = true;
                            break;
                        }
                    }
                    foreach (string scWord in listICAOcodes)
                    {
                        if (s.ToUpper() == scWord.ToUpper() && wordAdded == false)
                        {
                            titleString += scWord;
                            wordAdded = true;
                            break;
                        }
                    }

                    if (!wordAdded)
                    { // word does not appear in special cases or lower cases, so capitalise first letter and add to destination string
                        titleString += char.ToUpper(s[0]) + s.Substring(1).ToLower();
                    }
                    wordAdded = false;

                    if (counter < words.Length)
                    {
                        titleString += " "; //dont forget to add spaces back in again!
                    }
                    counter++;
                }
            }
            return titleString;
        }

        static public class CompatiblePdfReader
        {
            /// <summary>
            /// uses itextsharp 4.1.6 to convert any pdf to 1.4 compatible pdf, called instead of PdfReader.open
            /// </summary>
            static public PdfDocument Open(string pdfPath)
            {
                using (var fileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
                {
                    var len = (int)fileStream.Length;
                    var fileArray = new Byte[len];
                    fileStream.Read(fileArray, 0, len);
                    fileStream.Close();

                    return Open(fileArray);
                }
            }

            /// <summary>
            /// uses itextsharp 4.1.6 to convert any pdf to 1.4 compatible pdf, called instead of PdfReader.open
            /// </summary>
            static public PdfDocument Open(byte[] fileArray)
            {
                return Open(new MemoryStream(fileArray));
            }

            /// <summary>
            /// uses itextsharp 4.1.6 to convert any pdf to 1.4 compatible pdf, called instead of PdfReader.open
            /// </summary>
            static public PdfDocument Open(MemoryStream sourceStream)
            {
                PdfDocument outDoc;
                sourceStream.Position = 0;

                try
                {
                    outDoc = PdfReader.Open(sourceStream, PdfDocumentOpenMode.Import);
                }
                catch (PdfReaderException)
                {
                    //workaround if pdfsharp doesn't support this pdf
                    sourceStream.Position = 0;
                    var outputStream = new MemoryStream();
                    var reader = new iTextSharp.text.pdf.PdfReader(sourceStream);
                    var pdfStamper = new iTextSharp.text.pdf.PdfStamper(reader, outputStream) { FormFlattening = true };
                    pdfStamper.Writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_4);
                    pdfStamper.Writer.CloseStream = false;
                    pdfStamper.Close();

                    outDoc = PdfReader.Open(outputStream, PdfDocumentOpenMode.Import);
                }

                return outDoc;
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by:\r\nFLTLT M HAWKINS\r\n11SQN\r\nPh. 08 7383 3054", "DoD Compiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void backwkrCompileDoc_DoWork(object sender, DoWorkEventArgs e)
        {
            if (blOutputSingleDocument)
            {
                List<airfieldPlateData> plateInfo = new List<airfieldPlateData>();
                List<int> fieldsToAdd = new List<int>();
                for (int i = 0; i < blAreaSelected.Length; i++)
                {
                    //combine all the plates in one list and then create the pdf terminals
                    if (blAreaSelected[i])
                    {
                        plateInfo.AddRange(getDoDPlatesInformation(i));
                        fieldsToAdd.Add(i);
                    }
                }
                plateInfo = plateInfo.OrderBy(p => p.Country).ThenBy(q => q.Name).ToList<airfieldPlateData>();
                createPDFTerminals(plateInfo, fieldsToAdd.ToArray());
            }
            else
            {
                for (int i = 0; i < blAreaSelected.Length; i++)
                {
                    if (blAreaSelected[i])
                    {
                        List<airfieldPlateData> plateInfo = getDoDPlatesInformation(i).OrderBy(p => p.Country).ThenBy(q => q.Name).ToList<airfieldPlateData>(); ; //get plates individually for areas and then create pdf terminals
                        createPDFTerminals(plateInfo, new int[] { i });
                    }
                }
            }
        }
        private void backwkrCompileDoc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }
        private void backwkrCompileDoc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
