using Atestat.AtestatDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat
{
    public partial class DashboardControl : UserControl
    {
        ProduseTableAdapter db_produse = new ProduseTableAdapter();
        ProdusControl produsControl;

        List<string> cart = new List<string>();
        public DashboardControl()
        {
            InitializeComponent();
            addProductsToMenu();

            Console.WriteLine(UserConfig.user_id);
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void addProductsToMenu()
        {
            DataTable produseList = db_produse.GetProduse();
            product_panel.Controls.Clear();
            foreach(DataRow row in produseList.Rows)
            {
                ProdusControl component = new ProdusControl();


                component.picture.Image = Image.FromFile("media/" + row["ProdusID"]+".jpg");
                component.pret_label.Text = row["Pret"].ToString();
                component.valori_label.Text = row["Valori"].ToString();

                string id = Convert.ToString(row["ProdusID"]);
                component.AddProduct += (sender, e) => AddProduct(id);

                product_panel.Controls.Add(component);
                component.Dock = DockStyle.Top;
            }
        }

        public void AddProduct(string id)
        {
            Console.WriteLine("pressed");
            cart.Add(id);
            Console.WriteLine(cart);
        }


    }
}
