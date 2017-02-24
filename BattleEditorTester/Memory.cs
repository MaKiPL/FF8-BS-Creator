using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleEditorTester
{
    static class Memory
    {
        public static byte[] MIPS;
        public const string abspath = @"D:\FFVIII\FF8_2000\UNPACKED\battle\a0stg160.x";

        public const uint MIPSsize = 0x5D4;
        public const uint CameraSize = 0x1378;

        public static FileStream fs;
        public static BinaryReader br;
        public static BinaryWriter bw;

        public static Texture texture;
        public static MainSector MainSector;
        public static ObjectSection ObjectSection;
    }
}
