using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using GaussLink.Models;

namespace GaussLink.Data.DataAccess
{
    public static class FileManager
    {
        /// <summary>
        /// Creates a File Dialog to open a Gaussian *.out file and extracts the jobs inside
        /// </summary>
        /// <returns>List of DataFile</returns>
        public static List<JobFile> OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Gaussian Output File|*.out"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(fullPath);
                openFileDialog.Dispose();
                List<JobFile> dataItemJobs = DataFileJobSplit(fileName, File.ReadAllLines(fullPath).ToList());
                return dataItemJobs;
            }
            else return null;
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

      
        public static MeshGeometry3D Open3DFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Wavefront|*.obj"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;
                openFileDialog.Dispose();
                List<string> lines = File.ReadAllLines(fullPath).ToList();
                Point3DCollection vertices = new Point3DCollection();
                Int32Collection triangleIndices = new Int32Collection();
                StringBuilder sb = new StringBuilder();
                foreach (string l in lines)
                {
                    if (l.StartsWith("v"))
                    {
                        string[] splits = l.Split(' ');
                        sb.Append($"SphereGeometry.Positions.Add(new Point3D({splits[1]}, {splits[2]}, {splits[3]}));").AppendLine();
                        vertices.Add(new Point3D(double.Parse(splits[1], CultureInfo.InvariantCulture),
                            double.Parse(splits[2], CultureInfo.InvariantCulture),
                            double.Parse(splits[3], CultureInfo.InvariantCulture)));
                    }
                    if (l.StartsWith("f"))
                    {
                        string[] splits = l.Split(' ');
                        int index = int.Parse(splits[1]) - 1;
                        sb.Append($"SphereGeometry.TriangleIndices.Add({index});").AppendLine();

                        triangleIndices.Add(index);
                        index = int.Parse(splits[2]) - 1;
                        sb.Append($"SphereGeometry.TriangleIndices.Add({index});").AppendLine();

                        triangleIndices.Add(index);
                        index = int.Parse(splits[3]) - 1;
                        sb.Append($"SphereGeometry.TriangleIndices.Add({index});").AppendLine(); ;

                        triangleIndices.Add(index);

                    }
                }
                string s = sb.ToString();
                MeshGeometry3D mesh = new MeshGeometry3D();
                mesh.Positions = vertices;
                mesh.TriangleIndices = triangleIndices;
                return mesh;
            }
            else return null;
        }
    }
}

