using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity15
{
    public partial class ResultForm : Form
    {
        // This a constructor for this form. It takes a double as a parameter and sets it as the text of a label in the form
        public ResultForm(double number)
        {
            InitializeComponent();
            NumberLabel.Text = number.ToString(); // Sets the text of the NumberLabel to the parameter
        }
    }
}
