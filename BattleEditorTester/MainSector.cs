using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEditorTester
{
    class MainSector
    {
        public uint SectionCount;
        public uint OBJ1pt;
        public uint OBJ2pt;
        public uint OBJ3pt;
        public uint OBJ4pt;
        public uint EOF;

        public void ReadMainPointers()
        {
            Memory.fs.Seek(Memory.CameraSize, SeekOrigin.Begin);
            SectionCount = Memory.br.ReadUInt32();
            OBJ1pt = Memory.br.ReadUInt32() + Memory.CameraSize;
            OBJ2pt = Memory.br.ReadUInt32() + Memory.CameraSize ;
            OBJ3pt = Memory.br.ReadUInt32() + Memory.CameraSize ;
            OBJ4pt = Memory.br.ReadUInt32() + Memory.CameraSize ;
            Memory.fs.Seek(8, SeekOrigin.Current);
            EOF = Memory.br.ReadUInt32() + Memory.CameraSize ;
            Form1.Rewind();
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_1", true)[0], SectionCount);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_2", true)[0], OBJ1pt);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_3", true)[0], OBJ2pt);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_4", true)[0], OBJ3pt);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_5", true)[0], OBJ4pt);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("MS_6", true)[0], EOF);
        }

        public void UpdatePointers(uint a, string sender)
        {
            Memory.fs.Seek(Memory.CameraSize, SeekOrigin.Begin);
            switch (sender)
            {
                case "MS_1":
                    Memory.fs.Seek(0, SeekOrigin.Begin);
                    Memory.bw.Write(a);
                    break;
                case "MS_2":
                    Memory.fs.Seek(4, SeekOrigin.Begin);
                    Memory.bw.Write(a - Memory.CameraSize);
                    break;
                case "MS_3":
                    Memory.fs.Seek(8, SeekOrigin.Begin);
                    Memory.bw.Write(a - Memory.CameraSize);
                    break;
                case "MS_4":
                    Memory.fs.Seek(12, SeekOrigin.Begin);
                    Memory.bw.Write(a - Memory.CameraSize);
                    break;
                case "MS_5":
                    Memory.fs.Seek(16, SeekOrigin.Begin);
                    Memory.bw.Write(a - Memory.CameraSize);
                    break;
                case "MS_6":
                    Memory.fs.Seek(28, SeekOrigin.Begin);
                    Memory.bw.Write(a - Memory.CameraSize);
                    break;
                default:
                    return;
            }
            Form1.Rewind();
        }
    }
}
