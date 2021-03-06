using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema
{
    public partial class NameForm : Form
    {
        public static event Action <string> NameChanged;

        public NameForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorLbl.Text = "You must enter name";
            } 
            else
            {
                NameChanged.Invoke(textBox1.Text);
                Hide();
            }
        }
    }
}
