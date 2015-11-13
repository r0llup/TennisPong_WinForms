using System.Windows.Forms;

namespace Projet5
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }

        public WinForm(string msg)
        {
            InitializeComponent();
            this.label1.Text = msg;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}