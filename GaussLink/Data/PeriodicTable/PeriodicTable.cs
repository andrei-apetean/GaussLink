using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GaussLink.Data.PeriodicTable
{
    //TO DO: Refactor this somehow
    public  class PeriodicTable
    {
        private static List<Element> Elements = new List<Element>();
        string sg;
      
        public PeriodicTable()
        {

            Elements.Add(new Element(0, "Bond", 102, 102, 102));
            Elements.Add(new Element(1, "H", 204, 204, 204));
            Elements.Add(new Element(2, "He", 216, 255, 255));
            Elements.Add(new Element(3, "Li", 204, 124, 204));
            Elements.Add(new Element(4, "Be", 204, 255, 0));
            Elements.Add(new Element(5, "B", 255, 181, 181));
            Elements.Add(new Element(6, "C", 142, 142, 142));
            Elements.Add(new Element(7, "N", 25, 25, 229));
            Elements.Add(new Element(8, "O", 229, 0, 0));
            Elements.Add(new Element(9, "F", 178, 255, 255));
            Elements.Add(new Element(10, "Ne", 175, 226, 244));
            Elements.Add(new Element(11, "Na", 170, 91, 242));
            Elements.Add(new Element(12, "Mg", 178, 204, 91));
            Elements.Add(new Element(13, "Al", 209, 165, 165));
            Elements.Add(new Element(14, "Si", 127, 153, 153));
            Elements.Add(new Element(15, "P", 255, 127, 0));
            Elements.Add(new Element(16, "S", 255, 198, 40));
            Elements.Add(new Element(17, "Cl", 25, 239, 25));
            Elements.Add(new Element(18, "Ar", 127, 209, 226));
            Elements.Add(new Element(19, "K", 142, 63, 211));
            Elements.Add(new Element(20, "Ca", 153, 153, 0));
            Elements.Add(new Element(21, "Sc", 229, 229, 226));
            Elements.Add(new Element(22, "Ti", 191, 193, 198));
            Elements.Add(new Element(23, "V", 165, 165, 170));
            Elements.Add(new Element(24, "Cr", 137, 153, 198));
            Elements.Add(new Element(25, "Mn", 155, 122, 198));
            Elements.Add(new Element(26, "Fe", 127, 122, 198));
            Elements.Add(new Element(27, "Co", 91, 109, 255));
            Elements.Add(new Element(28, "Ni", 91, 122, 193));
            Elements.Add(new Element(29, "Cu", 255, 122, 96));
            Elements.Add(new Element(30, "Zn", 124, 127, 175));
            Elements.Add(new Element(31, "Ga", 193, 142, 142));
            Elements.Add(new Element(32, "Ge", 102, 142, 142));
            Elements.Add(new Element(33, "As", 188, 127, 226));
            Elements.Add(new Element(34, "Se", 255, 160, 0));
            Elements.Add(new Element(35, "Br", 165, 33, 33));
            Elements.Add(new Element(36, "Kr", 91, 186, 209));
            Elements.Add(new Element(37, "Rb", 112, 45, 175));
            Elements.Add(new Element(38, "Sr", 127, 102, 0));
            Elements.Add(new Element(39, "Y", 147, 252, 255));
            Elements.Add(new Element(40, "Zr", 147, 224, 224));
            Elements.Add(new Element(41, "Nb", 114, 193, 201));
            Elements.Add(new Element(42, "Mo", 84, 181, 181));
            Elements.Add(new Element(43, "Tc", 58, 158, 168));
            Elements.Add(new Element(44, "Ru", 35, 142, 150));
            Elements.Add(new Element(45, "Rh", 10, 124, 140));
            Elements.Add(new Element(46, "Pd", 0, 104, 132));
            Elements.Add(new Element(49, "Ag", 153, 198, 255));
            Elements.Add(new Element(48, "Zr", 255, 216, 142));
            Elements.Add(new Element(49, "In", 165, 117, 114));
            Elements.Add(new Element(50, "Sn", 102, 127, 127));
            Elements.Add(new Element(51, "Sb", 158, 99, 181));
            Elements.Add(new Element(52, "Te", 211, 122, 0));
            Elements.Add(new Element(53, "I", 147, 0, 147));
            Elements.Add(new Element(54, "Xe", 66, 158, 175));
            Elements.Add(new Element(55, "Cs", 86, 22, 142));
            Elements.Add(new Element(56, "Ba", 102, 51, 0));
            Elements.Add(new Element(57, "La", 112, 221, 255));
            Elements.Add(new Element(58, "Ce", 255, 255, 198));
            Elements.Add(new Element(59, "Pr", 216, 255, 198));
            Elements.Add(new Element(60, "Nd", 198, 255, 198));
            Elements.Add(new Element(61, "Pm", 163, 255, 198));
            Elements.Add(new Element(62, "Sm", 142, 255, 198));
            Elements.Add(new Element(63, "Eu", 96, 255, 198));
            Elements.Add(new Element(64, "Gd", 68, 255, 198));
            Elements.Add(new Element(65, "Tb", 48, 255, 198));
            Elements.Add(new Element(66, "Dy", 30, 255, 181));
            Elements.Add(new Element(67, "Ho", 0, 255, 181));
            Elements.Add(new Element(68, "Er", 0, 229, 117));
            Elements.Add(new Element(69, "Tm", 0, 211, 81));
            Elements.Add(new Element(70, "Yb", 0, 191, 56));
            Elements.Add(new Element(71, "Lu", 0, 170, 35));
            Elements.Add(new Element(72, "Hf", 76, 193, 255));
            Elements.Add(new Element(73, "Ta", 76, 165, 255));
            Elements.Add(new Element(74, "W", 38, 147, 214));
            Elements.Add(new Element(75, "Re", 38, 124, 170));
            Elements.Add(new Element(76, "Os", 38, 102, 150));
            Elements.Add(new Element(77, "Ir", 22, 84, 135));
            Elements.Add(new Element(78, "Pt", 22, 91, 142));
            Elements.Add(new Element(79, "Au", 255, 209, 35));
            Elements.Add(new Element(80, "Hg", 181, 181, 193));
            Elements.Add(new Element(81, "Tl", 165, 84, 76));
            Elements.Add(new Element(82, "Pb", 86, 89, 96));
            Elements.Add(new Element(83, "Bi", 158, 79, 181));
            Elements.Add(new Element(84, "Po", 170, 91, 0));
            Elements.Add(new Element(85, "At", 117, 79, 68));
            Elements.Add(new Element(86, "Rn", 66, 130, 150));
            Elements.Add(new Element(87, "Fr", 66, 0, 102));
            Elements.Add(new Element(88, "Ra", 76, 25, 0));
            Elements.Add(new Element(89, "Ac", 112, 170, 249));
            Elements.Add(new Element(90, "Th", 0, 186, 255));
            Elements.Add(new Element(91, "Pa", 0, 160, 255));
            Elements.Add(new Element(92, "U", 0, 142, 255));
            Elements.Add(new Element(93, "Np", 0, 127, 242));
            Elements.Add(new Element(94, "Pu", 0, 107, 242));
            Elements.Add(new Element(95, "Am", 84, 91, 242));
            Elements.Add(new Element(96, "Cm", 119, 91, 226));
            Elements.Add(new Element(97, "Bk", 197, 64, 226));
            Elements.Add(new Element(98, "Cf", 160, 53, 211));
            Elements.Add(new Element(99, "Es", 168, 43, 198));
            Elements.Add(new Element(100, "Fm", 178, 30, 186));
            Elements.Add(new Element(101, "Md", 178, 12, 165));
            Elements.Add(new Element(102, "No", 188, 12, 135));
            Elements.Add(new Element(103, "Lr", 198, 0, 102));
            Elements.Add(new Element(104, "Rf", 255, 127, 127));
            Elements.Add(new Element(105, "Db", 229, 102, 102));
            Elements.Add(new Element(106, "Sg", 204, 76, 76));
            Elements.Add(new Element(107, "Bh", 178, 51, 51));
            Elements.Add(new Element(108, "Hs", 153, 25, 25));
            Elements.Add(new Element(109, "Mt", 127, 0, 0));


            //string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            // This will get the current PROJECT directory
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            //string path = Path.Combine(projectDirectory, @"Data\PeriodicTable\periodicTable.txt");

            //if (File.Exists(path))
            //{
            //    string[] lines = File.ReadAllLines(path);
            //    StringBuilder s = new StringBuilder(); 

            //    foreach (string l in lines)
            //    {
            //        string[] splits = l.Split(',');
            //        s.Append("Elements.Add(new Element(").Append(splits[0]).Append(",\"").Append(splits[1]).Append("\",")
            //            .Append(splits[2]).Append(",").Append(splits[3]).Append(",").Append(splits[4]).Append("));").AppendLine();
            //        Elements.Add(new Element(int.Parse(splits[0]), splits[1], byte.Parse(splits[2]), byte.Parse(splits[3]), byte.Parse(splits[4])));
            //    }
            //    string sg = s.ToString();
            //}
        }

        public  (byte, byte, byte) GetAtomColor(int atomicNumber)
        {
            // StringBuilder s = new StringBuilder();
            //if(Elements.Count == 0)
            //{
            //    string workingDirectory = Environment.CurrentDirectory;
            //    // or: Directory.GetCurrentDirectory() gives the same result

            //    // This will get the current PROJECT directory
            //    string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //    string path = Path.Combine(projectDirectory, @"Data\PeriodicTable\periodicTable.txt");
            //    if (File.Exists(path))
            //    {
            //        string[] lines = File.ReadAllLines(path);


            //        foreach (string l in lines)
            //        {
            //            string[] splits = l.Split(',');
            //            s.Append("Elements.Add(new Element(").Append(splits[0]).Append(",\"").Append(splits[1]).Append("\",")
            //                .Append(splits[2]).Append(",").Append(splits[3]).Append(",").Append(splits[4]).Append("));").AppendLine();
            //            Elements.Add(new Element(int.Parse(splits[0]), splits[1], byte.Parse(splits[2]), byte.Parse(splits[3]), byte.Parse(splits[4])));
            //        }
            //    }
            //}
            //sg = s.ToString();
            //sg += "\n";
            return (Elements[atomicNumber].R, Elements[atomicNumber].G, Elements[atomicNumber].B);
        }

        public  string GetAtomName(int atomicNumber)
        {
            //if (Elements.Count == 0)
            //{
            //    string workingDirectory = Environment.CurrentDirectory;
            //    // or: Directory.GetCurrentDirectory() gives the same result

            //    // This will get the current PROJECT directory
            //    string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //    string path = Path.Combine(projectDirectory, @"Data\PeriodicTable\periodicTable.txt");

            //    if (File.Exists(path))
            //    {
            //        string[] lines = File.ReadAllLines(path);
            //        StringBuilder s = new StringBuilder();

            //        foreach (string l in lines)
            //        {
            //            string[] splits = l.Split(',');
            //            s.Append("Elements.Add(new Element(").Append(splits[0]).Append(",\"").Append(splits[1]).Append("\",")
            //                .Append(splits[2]).Append(",").Append(splits[3]).Append(",").Append(splits[4]).Append("));").AppendLine();
            //            Elements.Add(new Element(int.Parse(splits[0]), splits[1], byte.Parse(splits[2]), byte.Parse(splits[3]), byte.Parse(splits[4])));
            //        }
            //        string sg = s.ToString();

            //    }
            //}
            return Elements[atomicNumber].Symbol;
        }


    }
}

