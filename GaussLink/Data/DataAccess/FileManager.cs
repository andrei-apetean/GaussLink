using GaussLink.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GaussLink.Data.DataAccess
{
    public static class FileManager
    {
        /// <summary>
        /// Creates a File Dialog to open a Gaussian *.out file and extracts the jobs inside
        /// </summary>
        /// <returns>List of DataFile</returns>
        public static List<JobFile> OpenFile(List<string> filePaths)
        {
                List<JobFile> dataItemJobs = new List<JobFile>();
                foreach (string s in filePaths)
                {
                    string fullPath = s;
                    string fileName = Path.GetFileNameWithoutExtension(fullPath);
                    dataItemJobs.AddRange(DataFileJobSplit(fileName, File.ReadAllLines(fullPath).ToList()));
                }

                return dataItemJobs;
        }

        /// <summary>
        /// Saves a DataFile as a Gaussian *.out file
        /// </summary>
        /// <param name="job">The job to be saved</param>
        public static void SaveJobFileContent(JobFile job)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Gaussian Output File|*.out",
                FileName = job.JobName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllLines(path, job.Content);
                saveFileDialog.Dispose();
            }


        }
        public static void SaveText(string fileName, string text)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File|*.txt",
                FileName = fileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                File.WriteAllText(path, text);
                saveFileDialog.Dispose();
            }
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
        private static List<Models.JobFile> DataFileJobSplit(string fileName, List<string> fileLines)
        {
            //List of JobFiles in DataFile
            List<JobFile> children = new List<JobFile>();
            //List of lines of the JobFile
            List<string> lines = new List<string>();
            //name of the job
            string name = "";
            JobType type = JobType.OPT;
            //iterate through the lines of the DataFile
            foreach (var l in fileLines)
            {
                //add lines to JobFile lines
                lines.Add(l);
                //if the line is the beginning of the Job
                if (l.StartsWith(" #p") || l.StartsWith(" #P") || l.StartsWith("#"))
                {
                    //Check the type of  Job and add a name
                    if (l.Contains("opt") || l.Contains("Opt")) { name = fileName + "_opt"; type = JobType.OPT; continue; }
                    if (l.Contains("Freq") || l.Contains("freq") || l.EndsWith("Fr")) { name = fileName + "_freq"; type = JobType.FREQ; }
                    if (l.Contains("td")) { name = fileName + "_td"; type = JobType.TD; }
                    if (l.Contains("nmr")) { name = fileName + "_nmr"; type = JobType.NMR; }
                }
                //If end of JobFile has been reached
                if (l.Contains("Normal termination of Gaussian"))
                {
                    if (name == "") { name = fileName + "_unknown"; type = JobType.UNKNOWN; }
                    //add new JobFile to the list of Jobs
                    children.Add(new JobFile(name, type, lines));
                    //empty the temporary list
                    lines = new List<string>();
                    //clear name
                    name = "";
                }
            }
            //return list of JobFiles
            return children;
        }


    }
}

