using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEditorTester
{
    class ObjectSection
    {
        public uint NumberOfSections;
        public uint Settings1;
        public uint _3DModelPointer;
        public uint Settings2;
        public uint relEOF;

        public ushort UNKNOWN1;
        public ushort scale;
        public uint UNKNOWN2;
        public uint UNKNOWN3;
        public ushort FFFF;
        public short translation;

        public uint geomCount;
        public uint[] geomPointers;

        private uint relativejumper;
        private char groupmode;

        public void ReadGroup(int group)
        {
            SetFS(group);
            NumberOfSections = Memory.br.ReadUInt32();
            Settings1 = Memory.br.ReadUInt32() + relativejumper;
            _3DModelPointer = Memory.br.ReadUInt32() + relativejumper;
            Settings2 = Memory.br.ReadUInt32() + relativejumper;
            relEOF = Memory.br.ReadUInt32() + relativejumper;

            Form1.Rewind();

            ReadSettings1();

            Form1.Rewind();

            ReadGeometryHeader();

            Form1.Rewind();

            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_1", true)[0], NumberOfSections);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_2", true)[0], Settings1);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_3", true)[0], _3DModelPointer);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_4", true)[0], Settings2);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_5", true)[0], relEOF);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_6", true)[0], UNKNOWN1);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_7", true)[0], scale);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_8", true)[0], UNKNOWN2);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_9", true)[0], UNKNOWN3);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_10", true)[0], FFFF);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_11", true)[0], translation);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_12", true)[0], geomCount);
            Form1.UpdateMyControl(Form1.ControlCollection.Find($"G{groupmode}_13", true)[0], geomPointers);

        }

        private void ReadGeometryHeader()
        {
            Memory.fs.Seek(_3DModelPointer, SeekOrigin.Begin);
            geomCount = Memory.br.ReadUInt32();
            geomPointers = new uint[geomCount];
            for (int i = 0; i < geomCount; i++)
                geomPointers[i] = Memory.br.ReadUInt32() + _3DModelPointer;
        }

        private void ReadSettings1()
        {
            Memory.fs.Seek(Settings1, SeekOrigin.Begin);
            UNKNOWN1 = Memory.br.ReadUInt16();
            scale = Memory.br.ReadUInt16();
            Memory.fs.Seek(4, SeekOrigin.Current);
            UNKNOWN2 = Memory.br.ReadUInt32();
            UNKNOWN3 = Memory.br.ReadUInt32();
            FFFF = Memory.br.ReadUInt16();
            translation = Memory.br.ReadInt16();
        }

        private void SetFS(int group)
        {
            switch (group)
            {
                case 1:
                    Memory.fs.Seek(Memory.MainSector.OBJ1pt, SeekOrigin.Begin);
                    relativejumper = Memory.MainSector.OBJ1pt;
                    groupmode = 'A';
                    break;
                case 2:
                    Memory.fs.Seek(Memory.MainSector.OBJ2pt, SeekOrigin.Begin);
                    relativejumper = Memory.MainSector.OBJ2pt;
                    groupmode = 'B';
                    break;
                case 3:
                    Memory.fs.Seek(Memory.MainSector.OBJ3pt, SeekOrigin.Begin);
                    relativejumper = Memory.MainSector.OBJ3pt;
                    groupmode = 'C';
                    break;
                case 4:
                    Memory.fs.Seek(Memory.MainSector.OBJ4pt, SeekOrigin.Begin);
                    relativejumper = Memory.MainSector.OBJ4pt;
                    groupmode = 'D';
                    break;
                default:
                    goto case 1;
            }
        }

        public void UpdatePointer(uint a, int group, string sender)
        {
            SetFS(group);
            string locsender = sender.Substring(sender.Length - 1);
            switch (sender)
            {
                case "1":
                    Memory.fs.Seek(0, SeekOrigin.Begin);
                    Memory.bw.Write(a);
                    break;
                case "2":
                    Memory.fs.Seek(4, SeekOrigin.Begin);
                    Memory.bw.Write(a - relativejumper);
                    break;
                case "3":
                    Memory.fs.Seek(8, SeekOrigin.Begin);
                    Memory.bw.Write(a - relativejumper);
                    break;
                case "4":
                    Memory.fs.Seek(12, SeekOrigin.Begin);
                    Memory.bw.Write(a - relativejumper);
                    break;
                case "5":
                    Memory.fs.Seek(16, SeekOrigin.Begin);
                    Memory.bw.Write(a - relativejumper);
                    break;
                case "6": //fixed 2
                    Memory.fs.Seek(Settings1, SeekOrigin.Begin);
                    Memory.bw.Write((ushort)a);
                    break;
                case "7": //scale
                    Memory.fs.Seek(Settings1+2, SeekOrigin.Begin);
                    Memory.bw.Write((ushort)a);
                    break;
                case "8":
                    Memory.fs.Seek(Settings1 + 2 +4 , SeekOrigin.Begin);
                    Memory.bw.Write(a);
                    break;
                case "9":
                    Memory.fs.Seek(Settings1 + 2 +4 +4, SeekOrigin.Begin);
                    Memory.bw.Write(a);
                    break;
                case "10": //FFFF
                    Memory.fs.Seek(Settings1 + 2+4+4+4, SeekOrigin.Begin);
                    Memory.bw.Write((ushort)a);
                    break;
                case "11": //rot X/Y
                    Memory.fs.Seek(Settings1 + 2+4+4+4+2, SeekOrigin.Begin);
                    Memory.bw.Write((short)a);
                    break;
                default:
                    return;
            }
            Form1.Rewind();
        }
    }
}
