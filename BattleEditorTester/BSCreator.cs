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
    public partial class BSCreator : Form
    {
        public BSCreator()
        {
            InitializeComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog {Multiselect = true};

            if (ofd.ShowDialog() != DialogResult.OK) return;
            uint Filecount = (uint)ofd.FileNames.Length;
            long HowBig = ofd.FileNames.Select(s => new FileInfo(s)).Select(fi => fi.Length).Sum();
            byte[] buffer = new byte[HowBig + 4 + 4*Filecount];
            byte[] fileCountBuffer = BitConverter.GetBytes(Filecount);
            Buffer.BlockCopy(fileCountBuffer,0,buffer,0,4);
            uint local = (4+4*Filecount);
            for (int i = 0; i < Filecount; i++)
            {
                byte[] buf = File.ReadAllBytes(ofd.FileNames[i]);
                Buffer.BlockCopy(buf, 0, buffer, (int) local, buf.Length);

                buf = BitConverter.GetBytes(local);
                Buffer.BlockCopy(buf, 0, buffer, 4+i*4,4);
                local += (uint)buffer.Length;
            }
            SaveFile(buffer);
        }

        private void SaveFile(byte[] buffer)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, buffer);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox9.Text = ofd.FileName;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox11.Text = ofd.FileName;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox10.Text = ofd.FileName;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == null || textBox10.Text == null || textBox11.Text == null) return;
            uint Sector = 3; //const
            uint sec1Pointer = 0x14; //const
            uint sec2Pointer = 0; 
            uint sec3Pointer = 0; 
            uint eof = 0;

            byte[] sett1 = File.ReadAllBytes(textBox9.Text);
            byte[] sett2 = File.ReadAllBytes(textBox10.Text);
            byte[] sett3 = File.ReadAllBytes(textBox11.Text);

            byte[] buffer = new byte[5*4 + sett1.Length + sett2.Length + sett3.Length];

            byte[] temp = BitConverter.GetBytes(Sector);
            Buffer.BlockCopy(temp, 0, buffer, 0, 4);
            temp = BitConverter.GetBytes(sec1Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 4, 4);

            Buffer.BlockCopy(sett1, 0, buffer, 20, sett1.Length);

            sec2Pointer = (uint) (sec1Pointer + sett1.Length);
            temp = BitConverter.GetBytes(sec2Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 8, 4);

            Buffer.BlockCopy(sett2, 0, buffer, (int) sec2Pointer, sett2.Length);

            sec3Pointer = (uint) (sec2Pointer + sett2.Length);
            temp = BitConverter.GetBytes(sec3Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 12, 4);

            Buffer.BlockCopy(sett3, 0, buffer, (int)sec3Pointer, sett3.Length);

            eof = (uint) buffer.Length;

            temp = BitConverter.GetBytes(eof);
            Buffer.BlockCopy(temp, 0, buffer, 16, 4);

            SaveFile(buffer);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox5.Text = ofd.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox6.Text = ofd.FileName;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox7.Text = ofd.FileName;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox8.Text = ofd.FileName;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == null || textBox6.Text == null || textBox7.Text == null || textBox8.Text == null) return;
            uint Sector = 6; //const
            uint sec1Pointer = 0x20; //const
            uint sec2Pointer = 0;
            uint sec3Pointer = 0;
            uint sec4Pointer = 0;
            uint text1Pointer = 0;
            uint text2Pointer = 0;
            uint eof = 0; //FullSize-CameraSize (MIPS+camera)

            byte[] sett1 = File.ReadAllBytes(textBox5.Text);
            byte[] sett2 = File.ReadAllBytes(textBox6.Text);
            byte[] sett3 = File.ReadAllBytes(textBox7.Text);
            byte[] sett4 = File.ReadAllBytes(textBox8.Text);

            byte[] buffer = new byte[8 * 4 + sett1.Length + sett2.Length + sett3.Length + sett4.Length];

            byte[] temp = BitConverter.GetBytes(Sector);
            Buffer.BlockCopy(temp, 0, buffer, 0, 4);

            temp = BitConverter.GetBytes(sec1Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 4, 4);

            Buffer.BlockCopy(sett1, 0, buffer, (int) sec1Pointer, sett1.Length);

            sec2Pointer = (uint)(sec1Pointer + sett1.Length);
            temp = BitConverter.GetBytes(sec2Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 8, 4);

            Buffer.BlockCopy(sett2, 0, buffer, (int)sec2Pointer, sett2.Length);

            sec3Pointer = (uint)(sec2Pointer + sett2.Length);
            temp = BitConverter.GetBytes(sec3Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 12, 4);

            Buffer.BlockCopy(sett3, 0, buffer, (int)sec3Pointer, sett3.Length);

            sec4Pointer = (uint)(sec3Pointer + sett3.Length);
            temp = BitConverter.GetBytes(sec4Pointer);
            Buffer.BlockCopy(temp, 0, buffer, 16, 4);

            Buffer.BlockCopy(sett4, 0, buffer, (int)sec4Pointer, sett4.Length);
            MessageBox.Show("Texture pointers and EOF are unknown! They are set in final combination stage");
            SaveFile(buffer);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox1.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox2.Text = ofd.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox3.Text = ofd.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox4.Text = ofd.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null || textBox3.Text == null || textBox4.Text == null) return;
            byte[] sett1 = File.ReadAllBytes(textBox1.Text);
            byte[] sett2 = File.ReadAllBytes(textBox2.Text);
            byte[] sett4 = File.ReadAllBytes(textBox3.Text);
            byte[] sett3 = File.ReadAllBytes(textBox4.Text);

            byte[] buffer = new byte[sett1.Length + sett2.Length + sett3.Length + sett4.Length];

            Buffer.BlockCopy(sett1, 0, buffer, 0, sett1.Length); //MIPS

            Buffer.BlockCopy(sett2, 0, buffer, sett1.Length, sett2.Length); //Camera

            Buffer.BlockCopy(sett3, 0, buffer, sett1.Length+sett2.Length, sett3.Length); //Group

            uint TextureLocation = (uint) (sett1.Length + sett2.Length + sett3.Length);
            
            Buffer.BlockCopy(sett4, 0, buffer, (int) TextureLocation, sett4.Length); //Texture
            TextureLocation -= (uint)(sett1.Length + sett2.Length);
            byte[] temp = BitConverter.GetBytes(TextureLocation);
            Buffer.BlockCopy(temp, 0, buffer, sett1.Length + sett2.Length + 20, 4);
            Buffer.BlockCopy(temp, 0, buffer, sett1.Length + sett2.Length + 24, 4);

            temp = BitConverter.GetBytes(buffer.Length - (sett1.Length + sett2.Length));

            Buffer.BlockCopy(temp, 0, buffer, sett1.Length + sett2.Length + 28, 4);

            SaveFile(buffer);
        }
    }
}
