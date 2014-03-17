using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tool;

namespace GeneratePD
{
    public partial class GeneratePD : Form
    {
        public GeneratePD()
        {
            InitializeComponent();
        }

        private void Genarate_Click(object sender, EventArgs e)
        {
            string pd = Encrypt.EncryptDES(this.OldP.Text,"MRABBITW");

            this.NEWP.Text = pd;
        }

       

       
    }
}
