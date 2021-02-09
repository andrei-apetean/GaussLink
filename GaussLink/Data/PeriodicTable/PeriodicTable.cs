using System;
using System.Collections.Generic;
using System.IO;

namespace GaussLink.Data.PeriodicTable
{
    //TO DO: Refactor this somehow
    public  class PeriodicTable
    {
        private static List<Element> Elements = new List<Element>();
        public PeriodicTable()
        {
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = Path.Combine(projectDirectory, @"Data\PeriodicTable\periodicTable.txt");

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);

                foreach(string l in lines)
                {
                    string[] splits = l.Split(',');
                    Elements.Add(new Element(int.Parse(splits[0]), splits[1], byte.Parse(splits[2]), byte.Parse(splits[3]), byte.Parse(splits[4])));
                }
                
            }
        }
        
        public  (byte,byte,byte) GetAtomColor(int atomicNumber)
        {
            return (Elements[atomicNumber].R, Elements[atomicNumber].G, Elements[atomicNumber].B);
        }

        public string GetAtomName(int atomicNumber)
        {
            return Elements[atomicNumber].Symbol;
        }
    }
}

