using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEditorTester
{
    class Texture
    {
        public uint texturePointer1;
        public uint texturePointer2;

        public void ReadTexturePointers()
        {
            Memory.fs.Seek(Memory.CameraSize+ 20, SeekOrigin.Begin);
            texturePointer1 = Memory.br.ReadUInt32();
            texturePointer2 = Memory.br.ReadUInt32();
            Form1.Rewind();
            Form1.UpdateMyControl(Form1.ControlCollection.Find("Texture1pointer",true)[0], texturePointer1);
            Form1.UpdateMyControl(Form1.ControlCollection.Find("Texture2pointer", true)[0], texturePointer2);
        }

        public void UpdateTexturePointerA(uint a)
        {
            Memory.fs.Seek(Memory.CameraSize + 20, SeekOrigin.Begin);
            Memory.bw.Write(a - Memory.CameraSize);
            Form1.Rewind();
        }

        public void UpdateTexturePointerB(uint a)
        {
            Memory.fs.Seek(Memory.CameraSize + 24, SeekOrigin.Begin);
            Memory.bw.Write(a - Memory.CameraSize);
            Form1.Rewind();
        }
    }
}
