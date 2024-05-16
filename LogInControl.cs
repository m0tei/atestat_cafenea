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
    public partial class LogInControl : UserControl
    {
        public event EventHandler LogInEvent;
        public LogInControl()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            LogInEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
