using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineTestApp.UI
{
    public partial class d : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Encrypt_Click(object sender, EventArgs e)
        {
            lblResult.Text = Utilities.Encryption.EncryptText(
                 text: txtUserPassword.Text,
                 saltValue: txtUserName.Text,
                 passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector
                 );
        }

        protected void Dencrypt_Click(object sender, EventArgs e)
        {
            lblResult.Text = Utilities.Encryption.DecryptText(
                 text: txtUserPassword.Text,
                 saltValue: txtUserName.Text,
                 passPhrase: SystemSettings.PasswordPassPhrase,
                 passwordIterations: SystemSettings.PasswordIterations,
                 initVector: SystemSettings.PasswordInitVector
                 );
        }
    }
}