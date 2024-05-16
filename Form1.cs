using Atestat.AtestatDBDataSetTableAdapters;
using System;
using System.Windows.Forms;

namespace Atestat
{
    public partial class Form1 : Form
    {
        ClientiTableAdapter db_clienti = new ClientiTableAdapter();
        SignUpControl signUp;
        LogInControl logIn;
        DashboardControl dashboard;

        public Form1()
        {
            InitializeComponent();
            InitializeSingUp();

            signUp.LogInEvent += InitializeLogIn;
            signUp.SignUpEvent += SignUpDbInsert;
        }

        private void LogIn_LogInEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitializeSingUp()
        {
            signUp = new SignUpControl();
            Controls.Clear();
            Controls.Add(signUp);
            signUp.Dock = DockStyle.Fill;
        }
        private void InitializeLogIn(object sender, EventArgs e)
        {
            logIn = new LogInControl();
            Controls.Clear();
            Controls.Add(logIn);
            logIn.Dock = DockStyle.Fill;

            logIn.LogInEvent += LogInVerification;
        }
        private void InitializeDashboard(object sender, EventArgs e)
        {
            dashboard = new DashboardControl();
            Controls.Clear();
            Controls.Add(dashboard);
            dashboard.Dock = DockStyle.Fill;
        }

        //Sign up action
        private void SignUpDbInsert(object sender, EventArgs e)
        {
            string nume = signUp.nume_text_box.Text;
            string prenume = signUp.prenume_text_box.Text;
            string telefon = signUp.telefon_text_box.Text;

            db_clienti.InsertUser(nume, prenume, telefon);
            MessageBox.Show("User added: " + nume);
            InitializeLogIn(sender, e);

            signUp.nume_text_box.Clear();
            signUp.prenume_text_box.Clear();
            signUp.telefon_text_box.Clear();
        }


        // Login action
        private void LogInVerification(object sender, EventArgs e)
        {
            string nume = logIn.nume_text_box.Text;
            string prenume = logIn.prenume_text_box.Text;
            string telefon = logIn.telefon_text_box.Text;

            var client = db_clienti.GetUserLogIn(nume, prenume, telefon);
            Console.WriteLine(client.numeColumn); 

            if (client != null)
            {
                UserConfig.user_id = "1";
                InitializeDashboard(sender, e);
            }
            else
            {
                MessageBox.Show("Nu exista acest user!");
                logIn.nume_text_box.Clear();
                logIn.prenume_text_box.Clear();
                logIn.telefon_text_box.Clear();
            }
        }
    }
}
