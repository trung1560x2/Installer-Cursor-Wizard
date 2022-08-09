using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace TaoFileInstallCurSor

{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void initializeCursors()
        {



            var cursors = GetCursors();
            if (cursors.Count != 0)
            {
                for (var index = 0; index < cursors.Count; index++)
                {
                    switch (cursors[index].CursorName)
                    {
                        case "AppStarting":
                            AppStarting.Image = cursors[index].Icon.ToBitmap();
                            AppStarting.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "Arrow":
                            Arrow.Image = cursors[index].Icon.ToBitmap();
                            Arrow.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;

                        case "Hand":
                            Hand.Image = cursors[index].Icon.ToBitmap();
                            Hand.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "Crosshair":
                            Crosshair.Image = cursors[index].Icon.ToBitmap(); ;
                            Crosshair.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "Help":
                            Help.Image = cursors[index].Icon.ToBitmap();
                            Help.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;

                        case "No":
                            No.Image = cursors[index].Icon.ToBitmap();
                            No.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "IBeam":
                            IBeam.Image = cursors[index].Icon.ToBitmap();
                            IBeam.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;

                        case "NWPen":
                            NWPen.Image = cursors[index].Icon.ToBitmap();
                            NWPen.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "SizeAll":
                            SizeAll.Image = cursors[index].Icon.ToBitmap();
                            SizeAll.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "SizeNESW":
                            SizeNESW.Image = cursors[index].Icon.ToBitmap();
                            SizeNESW.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "SizeNS":
                            SizeNS.Image = cursors[index].Icon.ToBitmap();
                            SizeNS.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "SizeNWSE":
                            SizeNWSE.Image = cursors[index].Icon.ToBitmap();
                            SizeNWSE.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "SizeWE":
                            SizeWE.Image = cursors[index].Icon.ToBitmap();
                            SizeWE.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "UpArrow":
                            UpArrow.Image = cursors[index].Icon.ToBitmap();
                            UpArrow.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                        case "Wait":
                            Wait.Image = cursors[index].Icon.ToBitmap();
                            Wait.Name = cursors[index].FPath + "\\" + cursors[index].FName;
                            break;
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                AppStarting.Image = iconForFile.ToBitmap();
                AppStarting.Name = openFileDialog1.FileName;
                listBox1.Items.Add(file.FullName);
            }
        }


        private static List<CursorFile> GetCursors()
        {
            var icons = new List<CursorFile>();

            foreach (var regcur in getCursorRegList())
            {
                try
                {
                    var file = new System.IO.FileInfo(regcur.Value);
                    if (file.Extension == ".cur" || file.Extension == ".ani" ||file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".svg")
                    {
                        // Set a default icon for the file.
                        Icon iconForFile = SystemIcons.WinLogo;
                        iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                        iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                        var cursorFile = new CursorFile();
                        cursorFile.CursorName = regcur.Key;
                        cursorFile.FName = file.Name;
                        cursorFile.FPath = Path.GetDirectoryName(file.FullName);
                        cursorFile.Icon = iconForFile;
                        icons.Add(cursorFile);
                        
                        
                    }
                   

                }
                catch (System.ArgumentException ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            return icons;
        }
        private static List<RegistryCursor> getCursorRegList()
        {
            var RegCurList = new List<RegistryCursor>();
            var m = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors");
            var key = m.GetValueNames();
            for (var index = 0; index < key.Length; index++)
            {
                if (key[index] != "Scheme Source")
                {
                    RegCurList.Add(new RegistryCursor() { Key = key[index], Value = m.GetValue(key[index]).ToString() });
                }
            }
            return RegCurList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeCursors();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            txt_PacketName.Text = "";
            btSubmit.Enabled = true;
            initializeCursors();
        }

        private void btSubmit_Click(object sender, EventArgs e)
        {
            if (txt_PacketName.Text.Length != 0)
            {
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CreateInstallFile(folderBrowserDialog1.SelectedPath);
                    folderBrowserDialog1.SelectedPath = "";
                    initializeCursors();
                }
            }
            else
            {
                MessageBox.Show("Fill in the title, it's required.", "Empty title field", MessageBoxButtons.OK);
            }
        }
        private void CreateInstallFile(string path)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (var fn in Controls.OfType<PictureBox>())
                {
                    zip.AddFile(fn.Name, "");

                }


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + "\\Install.inf"))
                {
                    file.WriteLine("; " + txt_PacketName.Text + "\n");

                    file.WriteLine("[Version]");
                    file.WriteLine("signature=\"$CHICAGO$\"\n");

                    file.WriteLine("[DefaultInstall]");
                    file.WriteLine("CopyFiles = Scheme.Cur, Scheme.Txt");
                    file.WriteLine("AddReg    = Scheme.Reg\n");

                    file.WriteLine("[DestinationDirs]");
                    file.WriteLine("Scheme.Cur = 10,\"%CUR_DIR%\"");
                    file.WriteLine("Scheme.Txt = 10,\"%CUR_DIR%\"\n");

                    file.WriteLine("[Scheme.Reg]");
                    file.WriteLine("HKCU,\"Control Panel\\Cursors\\Schemes\",\"%SCHEME_NAME%\",,\"%10%\\%CUR_DIR%\\%pointer%,%10%\\%CUR_DIR%\\%help%,%10%\\%CUR_DIR%\\%work%,%10%\\%CUR_DIR%\\%busy%,%10%\\%CUR_DIR%\\%cross%,%10%\\%CUR_DIR%\\%Text%,%10%\\%CUR_DIR%\\%Hand%,%10%\\%CUR_DIR%\\%unavailiable%,%10%\\%CUR_DIR%\\%Vert%,%10%\\%CUR_DIR%\\%Horz%,%10%\\%CUR_DIR%\\%Dgn1%,%10%\\%CUR_DIR%\\%Dgn2%,%10%\\%CUR_DIR%\\%move%,%10%\\%CUR_DIR%\\%alternate%,%10%\\%CUR_DIR%\\%link%\"\n");

                    file.WriteLine("; -- Common Information\n");

                    file.WriteLine("[Scheme.Cur]");
                    file.WriteLine(Path.GetFileName(Arrow.Name));
                    file.WriteLine(Path.GetFileName(Help.Name));
                    file.WriteLine(Path.GetFileName(AppStarting.Name));
                    file.WriteLine(Path.GetFileName(Wait.Name));
                    file.WriteLine(Path.GetFileName(IBeam.Name));
                    file.WriteLine(Path.GetFileName(No.Name));
                    file.WriteLine(Path.GetFileName(SizeNS.Name));
                    file.WriteLine(Path.GetFileName(SizeWE.Name));
                    file.WriteLine(Path.GetFileName(SizeNWSE.Name));
                    file.WriteLine(Path.GetFileName(SizeNESW.Name));
                    file.WriteLine(Path.GetFileName(SizeAll.Name));
                    file.WriteLine(Path.GetFileName(Hand.Name));
                    file.WriteLine(Path.GetFileName(Crosshair.Name));
                    file.WriteLine(Path.GetFileName(NWPen.Name));
                    file.WriteLine(Path.GetFileName(UpArrow.Name));

                    file.WriteLine("[Strings]");
                    file.WriteLine("CUR_DIR       = \"Cursors\\" + txt_PacketName.Text + "\"");
                    file.WriteLine("SCHEME_NAME   = \"" + txt_PacketName.Text + "\"");

                    file.WriteLine("pointer       = \"" + Path.GetFileName(Arrow.Name) + "\"");
                    file.WriteLine("help          = \"" + Path.GetFileName(Help.Name) + "\"");
                    file.WriteLine("work          = \"" + Path.GetFileName(AppStarting.Name) + "\"");
                    file.WriteLine("busy          = \"" + Path.GetFileName(Wait.Name) + "\"");
                    file.WriteLine("text          = \"" + Path.GetFileName(IBeam.Name) + "\"");
                    file.WriteLine("unavailiable  = \"" + Path.GetFileName(No.Name) + "\"");
                    file.WriteLine("vert          = \"" + Path.GetFileName(SizeWE.Name) + "\"");
                    file.WriteLine("horz          = \"" + Path.GetFileName(SizeNS.Name) + "\"");
                    file.WriteLine("dgn1          = \"" + Path.GetFileName(SizeNWSE.Name) + "\"");
                    file.WriteLine("dgn2          = \"" + Path.GetFileName(SizeNESW.Name) + "\"");
                    file.WriteLine("move          = \"" + Path.GetFileName(SizeAll.Name) + "\"");
                    file.WriteLine("link          = \"" + Path.GetFileName(Hand.Name) + "\"");
                    file.WriteLine("cross         = \"" + Path.GetFileName(Crosshair.Name) + "\"");
                    file.WriteLine("hand          = \"" + Path.GetFileName(NWPen.Name) + "\"");
                    file.WriteLine("alternate     = \"" + Path.GetFileName(UpArrow.Name) + "\"");

                }

                zip.AddFile(path + "\\Install.inf", "");
                zip.Save(path + "\\" + txt_PacketName.Text + ".zip");

            }
            try
            {
                System.IO.File.Delete(path + "\\Install.inf");
            }
            catch (System.IO.IOException e)
            {
            }
            MessageBox.Show("Succesfuly created package", "File created", MessageBoxButtons.OK);
            txt_PacketName.Text = "";
            btSubmit.Enabled = false;
            initializeCursors();
        }

        private void Normal_Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                {
                    Console.WriteLine(openFileDialog1.FileName);
                    var file = new System.IO.FileInfo(openFileDialog1.FileName);

                    Icon iconForFile = SystemIcons.WinLogo;
                    iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                    Arrow.Image = iconForFile.ToBitmap();
                    Arrow.Name = openFileDialog1.FileName;
                    listBox1.Items.Add(Arrow.Name);
                }
                openFileDialog1.FileName = "";

            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                Help.Image = iconForFile.ToBitmap();
                Help.Name = openFileDialog1.FileName;
                listBox1.Items.Add(Help.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                Wait.Image = iconForFile.ToBitmap();
                Wait.Name = openFileDialog1.FileName;
                listBox1.Items.Add(Wait.Name);
            }
            openFileDialog1.FileName = "";

        }

        private void NSPenButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                NWPen.Image = iconForFile.ToBitmap();
                NWPen.Name = openFileDialog1.FileName;
                listBox1.Items.Add(NWPen.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                No.Image = iconForFile.ToBitmap();
                No.Name = openFileDialog1.FileName;
                listBox1.Items.Add(No.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void SizeNSButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                SizeNS.Image = iconForFile.ToBitmap();
                SizeNS.Name = openFileDialog1.FileName;
                listBox1.Items.Add(SizeNS.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void SizeWEButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                SizeWE.Image = iconForFile.ToBitmap();
                SizeWE.Name = openFileDialog1.FileName;
                listBox1.Items.Add(SizeWE.Name);
            }
            openFileDialog1.FileName = "";


        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                SizeNWSE.Image = iconForFile.ToBitmap();
                SizeNWSE.Name = openFileDialog1.FileName;
                listBox1.Items.Add(SizeNWSE.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void SizeNESWButton_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                SizeNESW.Image = iconForFile.ToBitmap();
                SizeNESW.Name = openFileDialog1.FileName;
                listBox1.Items.Add(SizeNESW.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void SizeAllButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                SizeAll.Image = iconForFile.ToBitmap();
                SizeAll.Name = openFileDialog1.FileName;
                listBox1.Items.Add(SizeAll.Name);

            }
            openFileDialog1.FileName = "";
        }

        private void UpArrowButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                UpArrow.Image = iconForFile.ToBitmap();
                UpArrow.Name = openFileDialog1.FileName;
                listBox1.Items.Add(UpArrow.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void HandButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                Hand.Image = iconForFile.ToBitmap();
                Hand.Name = openFileDialog1.FileName;
                listBox1.Items.Add(Hand.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void CrosshairButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                Crosshair.Image = iconForFile.ToBitmap();
                Crosshair.Name = openFileDialog1.FileName;
                listBox1.Items.Add(Crosshair.Name);
            }
            openFileDialog1.FileName = "";
        }

        private void IBeamButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                var file = new System.IO.FileInfo(openFileDialog1.FileName);

                Icon iconForFile = SystemIcons.WinLogo;
                iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                IBeam.Image = iconForFile.ToBitmap();
                IBeam.Name = openFileDialog1.FileName;
                listBox1.Items.Add(IBeam.Name);
            }
            openFileDialog1.FileName = "";
        }



        //////
    }
}
