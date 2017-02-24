using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleEditorTester
{
    public partial class Form1 : Form
    {
        public static Control.ControlCollection ControlCollection;
        public Form1()
        {
            InitializeComponent();
            ControlCollection = Controls;
            Memory.fs = new FileStream(Memory.abspath, FileMode.Open, FileAccess.ReadWrite);
            Memory.br = new BinaryReader(Memory.fs);
            Memory.bw = new BinaryWriter(Memory.fs);
            Memory.texture = new Texture();
            Memory.MainSector = new MainSector();
            Memory.ObjectSection = new ObjectSection();

            Memory.MIPS = Memory.br.ReadBytes((int)Memory.MIPSsize);

            InitialRead();
        }

        void InitialRead()
        {
            Memory.texture.ReadTexturePointers();
            Memory.MainSector.ReadMainPointers();
            for(int i = 1; i<5; i++)
                Memory.ObjectSection.ReadGroup(i);
        }

        public static void UpdateMyControl(Control control, object value)
        {
            if (control.GetType() == typeof(TextBox))
                (control as TextBox).Text = value.ToString();
            if (control.GetType() == typeof(ListBox))
            {
                ListBox refer = control as ListBox;
                refer.Items.Clear();
                uint[] values = value as uint[];
                foreach (uint t in values)
                    refer.Items.Add(t);
            }
        }

        public static void Rewind() => Memory.fs.Position = 0;

        private void UpdateData(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                UpdateTextBoxValue(sender);
            }
        }

        private void UpdateTextBoxValue(object sender)
        {
            switch ((sender as TextBox).Name)
            {
                case "Texture1pointer":
                    Memory.texture.UpdateTexturePointerA(uint.Parse((sender as TextBox).Text));
                    return;
                case "Texture2pointer":
                    Memory.texture.UpdateTexturePointerB(uint.Parse((sender as TextBox).Text));
                    return;

                case "MS_1":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_1");
                    return;
                case "MS_2":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_2");
                    return;
                case "MS_3":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_3");
                    return;
                case "MS_4":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_4");
                    return;
                case "MS_5":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_5");
                    return;
                case "MS_6":
                    Memory.MainSector.UpdatePointers(uint.Parse((sender as TextBox).Text), "MS_6");
                    return;

                case "GA_1":
                    Memory.ObjectSection.UpdatePointer(uint.Parse((sender as TextBox).Text), 1,"GA_1");
                    return;
                case "GA_2":
                    Memory.ObjectSection.UpdatePointer(uint.Parse((sender as TextBox).Text), 1, "GA_2");
                    return;
                case "GA_3":
                    Memory.ObjectSection.UpdatePointer(uint.Parse((sender as TextBox).Text), 1, "GA_3");
                    return;
                case "GA_4":
                    Memory.ObjectSection.UpdatePointer(uint.Parse((sender as TextBox).Text), 1, "GA_4");
                    return;
                case "GA_5":
                    Memory.ObjectSection.UpdatePointer(uint.Parse((sender as TextBox).Text), 1, "GA_5");
                    return;

            }
        }

        private void bSCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
