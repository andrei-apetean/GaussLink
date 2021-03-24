using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Media.Media3D;

namespace GaussLink.Data
{
    public static class Extractor
    {

        public static Molecule3D ExtractMolecule3D(JobFile job, bool isStatic, bool isStandard)
        {
            if (isStatic)
            {
                MoleculeOrientation molOr = ExtractOrientation(job, isStandard);
                MoleculeBond bonds = ExtractMoleculeBond(job);
                return new Molecule3D(bonds, molOr, null);
            }
            List<VibrationMode> vms = ExtractVibrationModes(job);
            MoleculeOrientation mo = ExtractOrientation(job, isStandard);
            MoleculeBond b = ExtractMoleculeBond(job);
            return new Molecule3D(b, mo, vms);
        }
        public static MoleculeOrientation ExtractOrientation(JobFile job, bool isInput)
        {
            List<Atom> orientation = new List<Atom>();
            string parameter = isInput ? "Input orientation" : "Standard orientation";
            int flagCounter = 0;
            int index = GetLastOrientationIndex(job, parameter);
            int z = 0;
            bool flag = false;
            bool begin = false;

            foreach (string line in job.Content)
            {

                if (begin)
                {
                    if (line.Contains("-------"))
                    {
                        break;
                    }
                    Atom a = ExtractOrientation(line);
                    orientation.Add(a);
                    continue;
                }
                if (line.Contains(parameter))
                {
                    if (parameter == "Standard orientation")
                    {
                        z++;
                        if (z == index)
                        { flag = true; }
                    }
                    else { flag = true; }

                }
                if (flag)
                {
                    flagCounter++;
                }
                if (flagCounter == 5)
                {
                    begin = true;
                }
            }
            return new MoleculeOrientation(orientation);
        }

        public static MoleculeBond ExtractMoleculeBond(JobFile job)
        {
            MoleculeBond mb = new MoleculeBond();
            bool begin = false;

            for (int i = 0; i < job.Content.Count; i++)
            {
                if (begin)
                {
                    if (job.Content[i].Contains("A"))
                    {
                        break;
                    }
                    string[] data = RetrieveLineData(job.Content[i].Split(' '));
                    Bond bond = ExtractBond(data);
                    mb.Bonds.Add(bond);
                    continue;
                }
                if (job.Content[i].Contains("Initial Parameters"))
                {
                    i += 4;
                    begin = true;
                }
            }

            return mb;
        }

        private static Bond ExtractBond(string[] data)
        {
            char[] c = data[2].ToCharArray();
            string x = ""; string y = "";
            bool isFirst = true;
            for (int i = 2; i < c.Length - 1; i++)
            {
                if (c[i] == ',')
                {
                    isFirst = false;
                    continue;
                }
                if (isFirst)
                {
                    string s = c[i].ToString();
                    x += s;
                }
                else
                {
                    string s = c[i].ToString();
                    y += s;
                }

            }
            return new Bond(int.Parse(x), int.Parse(y));
        }
        private static int GetLastOrientationIndex(JobFile job, string param)
        {
            int index = 0;
            foreach (string l in job.Content)
            {
                if (l.Contains(param))
                {
                    index++;
                }
            }
            return index;
        }

        public static ExcitationEnergy ExtractExcitationEnergies(JobFile job)
        {
            List<ExcitedState> excitedStates = new List<ExcitedState>();
            ExcitedState e = new ExcitedState();
            HLGap hlgap;
            int count = 0;
            bool start = false;
            foreach (string line in job.Content)
            {
                if (line.Contains(" SavETr"))
                {
                    excitedStates.Add(e);
                    break;
                }
                if (start)
                {
                    if (!line.Contains("This") && !line.Contains("Total") && !line.Contains("Copy") && !string.IsNullOrWhiteSpace(line) && !line.Contains("Excited State"))
                    {
                        bool b = string.IsNullOrWhiteSpace(line);
                        //0,2,3
                        string[] data = RetrieveLineData(line.Split(' '));
                        hlgap = new HLGap(int.Parse(data[0]), int.Parse(data[2]), float.Parse(data[3], CultureInfo.InvariantCulture));
                        e.HLGaps.Add(hlgap);
                    }
                    if (line.Contains("Excited State"))
                    {
                        count++;
                        if (count > 1) { excitedStates.Add(e); }
                        e = new ExcitedState();
                        string[] data = RetrieveLineData(line.Split(' '));
                        string eId = data[2].Remove(data[2].Length - 1);
                        e.ID = int.Parse(eId);
                        e.QuantumState = data[3];
                        e.ExcitationEnergy = float.Parse(data[4], CultureInfo.InvariantCulture);
                        e.WaveLength = float.Parse(data[6], CultureInfo.InvariantCulture);
                        string f = data[8].Remove(0, 2);
                        e.OscillatorStrength = float.Parse(f, CultureInfo.InvariantCulture);
                    }
                }
                if (line.Contains("Excitation energies and oscillator strengths:"))
                {
                    start = true;
                }
            }
            return new ExcitationEnergy(excitedStates);
        }


        private static Atom ExtractOrientation(string line)
        {
            string[] splits = line.Split(' ');
            float[] values = new float[6];
            int z = 0;
            for (int i = 0; i < splits.Length; i++)
            {
                if ((!string.IsNullOrEmpty(splits[i]) && (!string.IsNullOrWhiteSpace(splits[i]))))
                {
                    values[z] = float.Parse(splits[i], CultureInfo.InvariantCulture);
                    z++;
                }
            }

            return new Atom(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), new Vector3D(values[3], values[4], values[5]));

        }

        public static string ExtractJobFileContent(JobFile file)
        {
            StringBuilder stringBuilder = new StringBuilder();


            foreach (string line in file.Content)
            {
                stringBuilder.Append(line).AppendLine();
            }

            return stringBuilder.ToString(); ;
        }

        public static string ExtractEnergyData(JobFile file)
        {
            ExcitationEnergy e = ExtractExcitationEnergies(file);
            StringBuilder sb = new StringBuilder();
            foreach (ExcitedState state in e.ExcitedStates)
            {
                sb.Append("Excited State: ").Append(state.ID).Append(" ").Append(state.QuantumState).Append(" ").Append(state.ExcitationEnergy).
                    Append(" ").Append("eV").Append(" ").Append(state.WaveLength).Append(" ").Append("nm").Append(" ").Append("f=").Append(state.OscillatorStrength).AppendLine();
                foreach (HLGap g in state.HLGaps)
                {
                    sb.Append("\t").Append(g.HOMO).Append(" ").Append("->").Append(" ").Append(g.LUMO).Append("\t").Append(g.EnergyDelta).AppendLine();
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string ExtractFreqData(JobFile file)
        {
            List<string> lines = ExtractFreqDataList(file);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in lines)
            {
                stringBuilder.Append(s).AppendLine();
            }
            return stringBuilder.ToString();
        }
        private static List<string> ExtractFreqDataList(JobFile file)
        {
            List<string> lines = new List<string>();
            bool flag = false;
            bool safetyCheck = false;

            int i = 0;
            foreach (string line in file.Content)
            {
                i++;
                if (safetyCheck)
                {
                    if (line == "- Thermochemistry -")
                    {
                        return lines;
                    }
                    else
                    {
                        safetyCheck = false;
                    }
                }
                if (line == " -------------------")
                {
                    safetyCheck = true;
                    flag = false;
                }
                if (flag)
                {
                    lines.Add(line);
                    continue;
                }
                if (line.Contains("and normal coordinates:"))
                {
                    flag = true;
                }

            }
            return lines;
        }

        public static List<VibrationMode> ExtractVibrationModes(JobFile file)
        {
            List<VibrationMode> vms = new List<VibrationMode>();
            VibrationMode vm1 = new VibrationMode();
            VibrationMode vm2 = new VibrationMode();
            VibrationMode vm3 = new VibrationMode();
            bool isFirst = true;


            List<string> lines = ExtractFreqDataList(file);
            foreach (string l in lines)
            {
                string[] data = RetrieveLineData(l.Split(' '));

                if (data != null)
                {
                    if (data.Length == 3)
                    {
                        if (!data[0].Contains("A") && !data[0].Contains("B"))
                        {
                            if (!isFirst)
                            {
                                vms.Add(vm1);
                                vms.Add(vm2);
                                vms.Add(vm3);

                            }
                            vm1 = new VibrationMode();
                            vm2 = new VibrationMode();
                            vm3 = new VibrationMode();

                            vm1.Mode = int.Parse(data[0]);
                            vm2.Mode = int.Parse(data[1]);
                            vm3.Mode = int.Parse(data[2]);
                            isFirst = false;

                        }
                        else
                        {
                            vm1.QuantumState = data[0];
                            vm2.QuantumState = data[1];
                            vm3.QuantumState = data[2];
                        }
                    }
                    if (data.Length == 5 || data.Length == 4)
                    {
                        switch (data[0])
                        {
                            case "Frequencies":
                                vm1.Frequencies = float.Parse(data[1], CultureInfo.InvariantCulture);
                                vm2.Frequencies = float.Parse(data[2], CultureInfo.InvariantCulture);
                                vm3.Frequencies = float.Parse(data[3], CultureInfo.InvariantCulture);
                                break;
                            case "Red.":
                                vm1.RedMasses = float.Parse(data[2], CultureInfo.InvariantCulture);
                                vm2.RedMasses = float.Parse(data[3], CultureInfo.InvariantCulture);
                                vm3.RedMasses = float.Parse(data[4], CultureInfo.InvariantCulture);
                                break;
                            case "Frc":
                                vm1.FrcConsts = float.Parse(data[2], CultureInfo.InvariantCulture);
                                vm2.FrcConsts = float.Parse(data[3], CultureInfo.InvariantCulture);
                                vm3.FrcConsts = float.Parse(data[4], CultureInfo.InvariantCulture);
                                break;
                            case "IR":
                                vm1.IRInten = float.Parse(data[2], CultureInfo.InvariantCulture);
                                vm2.IRInten = float.Parse(data[3], CultureInfo.InvariantCulture);
                                vm3.IRInten = float.Parse(data[4], CultureInfo.InvariantCulture);
                                break;
                            case "Raman":
                                vm1.RamanActive = float.Parse(data[2], CultureInfo.InvariantCulture);
                                vm2.RamanActive = float.Parse(data[3], CultureInfo.InvariantCulture);
                                vm3.RamanActive = float.Parse(data[4], CultureInfo.InvariantCulture);
                                break;
                            case "Depolar":
                                switch (data[1])
                                {
                                    case "(P)":
                                        vm1.DepolarP = float.Parse(data[2], CultureInfo.InvariantCulture);
                                        vm2.DepolarP = float.Parse(data[3], CultureInfo.InvariantCulture);
                                        vm3.DepolarP = float.Parse(data[4], CultureInfo.InvariantCulture);
                                        break;
                                    case "(U)":
                                        vm1.DepolarU = float.Parse(data[2], CultureInfo.InvariantCulture);
                                        vm2.DepolarU = float.Parse(data[3], CultureInfo.InvariantCulture);
                                        vm3.DepolarU = float.Parse(data[4], CultureInfo.InvariantCulture);
                                        break;
                                }
                                break;
                        }

                    }
                    if (data.Length == 11)
                    {
                        vm1.AtomVibrations.Add(new AtomDelta(
                            int.Parse(data[0]),
                            int.Parse(data[1]),
                            new Vector3D(float.Parse(data[2], CultureInfo.InvariantCulture), float.Parse(data[3], CultureInfo.InvariantCulture), float.Parse(data[4], CultureInfo.InvariantCulture))));
                        vm2.AtomVibrations.Add(new AtomDelta(
                           int.Parse(data[0]),
                           int.Parse(data[1]),
                           new Vector3D(float.Parse(data[5], CultureInfo.InvariantCulture), float.Parse(data[5], CultureInfo.InvariantCulture), float.Parse(data[7], CultureInfo.InvariantCulture))));
                        vm3.AtomVibrations.Add(new AtomDelta(
                           int.Parse(data[0]),
                           int.Parse(data[1]),
                           new Vector3D(float.Parse(data[8], CultureInfo.InvariantCulture), float.Parse(data[9], CultureInfo.InvariantCulture), float.Parse(data[10], CultureInfo.InvariantCulture))));

                    }
                }
                if (l.Equals(""))
                {
                    vms.Add(vm1);
                    vms.Add(vm2);
                    vms.Add(vm3);
                }

            }
            return vms;
        }

        private static string[] RetrieveLineData(string[] splits)
        {
            List<string> items = new List<string>();
            foreach (string s in splits)
            {

                if (!string.IsNullOrWhiteSpace(s) && !s.Equals("--"))
                {
                    items.Add(s);
                }
                if (s == "Atom")
                {
                    return null;
                }
            }
            string[] newSplits = items.ToArray();
            return newSplits;
        }
    }
}
