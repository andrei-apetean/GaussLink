using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GaussLink.Data.DataAccess
{
    public static class FileManager
    {
        static int unknownCount =0;
        static int fileCount = 0;
        /// <summary>
        /// Creates a File Dialog to open a Gaussian *.out file and extracts the jobs inside
        /// </summary>
        /// <returns>List of DataFile</returns>
        public static List<JobFile> OpenFile(List<string> filePaths)
        {
            unknownCount = 0;
            fileCount = 0;
            List<JobFile> dataItemJobs = new List<JobFile>();
            foreach (string s in filePaths)
            {
                string fullPath = s;
                string fileName = Path.GetFileNameWithoutExtension(fullPath);
                dataItemJobs.AddRange(DataFileJobSplit(fileName, File.ReadAllLines(fullPath).ToList()));
            }
            string message = $"{fileCount} files opened successfully! ";
            if (unknownCount > 0) message += $"{unknownCount} unknown.";
            Messenger.Default.Send(new ConsoleMessage(message));
            return dataItemJobs;
        }


        public static void SaveOrientation(string fileName, MoleculeOrientation orientation)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File|*.txt",
                FileName = fileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, orientation.ToString());
                saveFileDialog.Dispose();
            }

        }

        /// <summary>
        /// Splits a DataFile into JobFiles 
        /// </summary>
        /// <param name="fileName">Name of the DataFile</param>
        /// <param name="fileLines">Text lines contained in the DataFile</param>
        /// <returns></returns>
        private static List<JobFile> DataFileJobSplit(string fileName, List<string> fileLines)
        {
            //List of JobFiles in DataFile
            List<JobFile> children = new List<JobFile>();
            //List of lines of the JobFile
            List<string> lines = new List<string>();
            //name of the job
            string name = "";
            JobType type = JobType.OPT;
            //iterate through the lines of the DataFile
            for (int i=0; i< fileLines.Count; i++)
            {
                //add lines to JobFile lines
                lines.Add(fileLines[i]);
                //if the line is the beginning of the Job
                if (fileLines[i].StartsWith(" #p") || fileLines[i].StartsWith(" #P") || fileLines[i].StartsWith("#"))
                {
                    //Check the type of  Job and add a name
                    if (fileLines[i].Contains("opt") || fileLines[i].Contains("Opt") || fileLines[i + 1].Contains("opt") || fileLines[i + 1].Contains("Opt")) { name = fileName + "_opt"; type = JobType.OPT; continue; }
                    if (fileLines[i].Contains("Freq") || fileLines[i].Contains("freq") || fileLines[i+1].Contains("Freq") || fileLines[i+1].Contains("freq")){ name = fileName + "_freq"; type = JobType.FREQ; }
                    if (fileLines[i].Contains("td")) { name = fileName + "_td"; type = JobType.TD; }
                    if (fileLines[i].Contains("nmr")) { name = fileName + "_nmr"; type = JobType.NMR; }
                }
                //If end of JobFile has been reached
                if (fileLines[i].Contains("Normal termination of Gaussian"))
                {
                    if (name == "") { name = fileName + "_unknown"; type = JobType.UNKNOWN; unknownCount++; fileCount--; }
                    //add new JobFile to the list of Jobs
                    children.Add(new JobFile(name, type, lines));
                    //empty the temporary list
                    lines = new List<string>();
                    //clear name
                    name = "";
                    fileCount++;
                }
            }
            //return list of JobFiles
            return children;
        }


    }
}

