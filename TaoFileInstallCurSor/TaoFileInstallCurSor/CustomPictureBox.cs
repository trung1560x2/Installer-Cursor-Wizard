using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaoFileInstallCurSor
{
    public partial class CustomPictureBox : System.Windows.Forms.PictureBox
    {
        public string CursorPath { get; set; }
        public CustomPictureBox()
        {
            InitializeComponent();
        }

        public CustomPictureBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
