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
    public partial class ProdusControl : UserControl
    {
        public event EventHandler AddProduct;
        public ProdusControl()
        {
            InitializeComponent();
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            AddProduct?.Invoke(this, EventArgs.Empty);
        }
    }
}
