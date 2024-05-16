using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class SignUpControl : UserControl
    {
        public event EventHandler LogInEvent;
        public event EventHandler SignUpEvent;
        public SignUpControl()
        {
            InitializeComponent();
        }
        private void login_button_Click_1(object sender, EventArgs e)
        {
            LogInEvent?.Invoke(this, EventArgs.Empty);
        }

        private void signup_button_Click(object sender, EventArgs e)
        {
            SignUpEvent?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
