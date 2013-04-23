using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Admin.AccountMgr;
using log4net;
using AmbleClient.ComClass;




namespace AmbleClient
{
    public partial class Login : Form
    {
        AccountMgr accountMgr;
        PropertyClass accountProperty;

        
        public Login()
        {
            accountMgr = new AccountMgr();
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (LoginInfo.isChecked())
            {
                cbPasswdRemember.Checked = true;
                textBox1.Text = LoginInfo.GetAccount();
                maskedTextBox1.Text = LoginInfo.GetPassword();
            }
            else
            {
                cbPasswdRemember.Checked = false;
            }


        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                accountProperty =accountMgr.CheckNameAndPasswd(textBox1.Text.Trim(), maskedTextBox1.Text.Trim());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not connect to the server.Please contact the admin");
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                return;
            
            }

            if (accountProperty== null)
            {
                MessageBox.Show("Invalid Name or Password.");
                return;

            }
            else
            {
                Logger.Info(accountProperty.UserId + "," + accountProperty.Job + " logged in");

                UserInfo.UserId = accountProperty.UserId;
                UserInfo.UserName = accountProperty.AccountName;
                UserInfo.Job = (JobDescription)accountProperty.Job;

                //save password or not

                if (cbPasswdRemember.Checked)
                {
                    LoginInfo.SetChecked(true);
                    LoginInfo.SetAccount(textBox1.Text);
                    LoginInfo.SetPassword(maskedTextBox1.Text);
                
                }

                MainFrame mainFrame = new MainFrame();
                this.Hide();
                mainFrame.WindowState = FormWindowState.Maximized;
                mainFrame.Show();
               
           
            }
  
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        //enter enter
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                maskedTextBox1.Focus();
            }
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void cbPasswdRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPasswdRemember.Checked == false)
            {
                LoginInfo.SetChecked(false);
                LoginInfo.SetAccount(string.Empty);
                LoginInfo.SetPassword(string.Empty);
            
            }



        }


    }

    public static class LoginInfo
    {
        static string file=Environment.CurrentDirectory + "\\AmbleAppServer.ini";



        public static bool isChecked()
        {
           string isChecked=OperatorFile.GetIniFileString("Login", "Checked", "",file);
           if (isChecked == "y"||isChecked=="Y")
               return true;
           else
               return false;

        }

       public  static string GetAccount()
        {
            return OperatorFile.GetIniFileString("Login", "Account", "", file);
        
        }

        public static string GetPassword()
        {
            return DesOp.DecryptDES(OperatorFile.GetIniFileString("Login", "Pwd", "", file));
        }

       public  static void SetChecked(bool state)
        {
            if (state)
                OperatorFile.WriteIniFileString("Login", "Checked", "y", file);
            else
                OperatorFile.WriteIniFileString("Login", "Checked", "n", file);
        }

       public static void SetAccount(string account)
        {
            OperatorFile.WriteIniFileString("Login", "Account", account, file);
        }

        public static void SetPassword(string password)
        {
            OperatorFile.WriteIniFileString("Login", "Pwd",DesOp.EncryptDES(password), file);
        }


    
    
    
    }




}
