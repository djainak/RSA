using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
    public partial class UserApp : Form
    {
        RSA r;
        public UserApp()
        {
            InitializeComponent();
            buttonGen.Click += new System.EventHandler(this.buttonGen_Click);
            radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            textBoxP.Click += new System.EventHandler(this.textBoxP_Click);
            textBoxQ.Click += new System.EventHandler(this.textBoxQ_Click);
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBoxP.ReadOnly = false;
                textBoxQ.ReadOnly = false;
                buttonGen.Enabled = false;
            }
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBoxP.ReadOnly = true;
                textBoxQ.ReadOnly = true;
                buttonGen.Enabled = true;
            }
        }

        
        private void textBoxP_Click(object sender, EventArgs e)
        {
            textBoxP.SelectionStart = 0;
            textBoxP.SelectionLength = textBoxP.Text.Length;
        }

        private void textBoxQ_Click(object sender, EventArgs e)
        {
            textBoxQ.SelectionStart = 0;
            textBoxQ.SelectionLength = textBoxQ.Text.Length;
        }


        private void buttonGen_Click(object sender, EventArgs e)
        {
            BigInteger p = BigInteger.Generate(new BigInteger("200"));

            while (!BigInteger.IsPrimeMillerRabin(p, 100))
                p = BigInteger.Generate(new BigInteger("200"));

            BigInteger q = BigInteger.Generate(new BigInteger("200"));

            while (!BigInteger.IsPrimeMillerRabin(q, 100))
                q = BigInteger.Generate(new BigInteger("200"));

            textBoxP.Text = p.ToString();
            textBoxQ.Text = q.ToString();
        }
    }
}
