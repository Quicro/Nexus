using System.Data;

namespace NexusAI
{
    public partial class AIForm : Form
    {
        public AIForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string question = listBox.Text;

            CacheItem cacheItem = Cache.items[question];
            txtSQL.Text = cacheItem.sql;
            txtResponse.Text = cacheItem.answer;   
        }

        private void AIForm_Load(object sender, EventArgs e)
        {
            listBox.Items.AddRange(Cache.items.Keys.ToArray());

            foreach (string question in listBox.Items.Cast<string>())
            {
                try
                {
                    CacheItem cacheItem = Cache.items[question];
                    txtSQL.Text = cacheItem.sql;
                    txtResponse.Text = cacheItem.answer;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private void listBox_Click(object sender, EventArgs e)
        {
            btnSend_Click(null, null);
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NexusAI {
    public partial class AIForm : Form {
        public AIForm() {
            InitializeComponent();


        }

        //

        Dictionary<string, string> qa = new Dictionary<string, string>() {
            ["Ik wil graag de permissies zien van user WILLEM"] = @"In dit geval heeft de gebruiker ""WILLEM"" toegang tot alle permissies omdat hij is toegewezen aan de ""ALL"" rol.",
            ["Ik wil graag de polissen zien van de klant A"] = @"Het lijkt erop dat klant A drie polissen heeft:

Autoverzekering met polisnummer 485984868
Brandverzekering met polisnummer 484984894
Aansprakelijkheidsverzekering (BA) met polisnummer 959559489"
           ,
            ["Welke permissies heeft de rol ADMIN?"] = @"De permissies die bij de rol ADMIN horen zijn:

StartMenu.Admin (ID 2)
StartMenu.Dossier.Schade (ID 3)
StartMenu.Dossier.Polis (ID 4)"
           ,
            ["Welke claims heeft polis A"] = @"Polis A heeft de volgende claims: Valongeval (nummer 23512345)"
           ,
            [""] = @""
           ,
            [""] = @""
           ,
            [""] = @""

        };

        Dictionary<string, string> sql = new Dictionary<string, string>() {
            ["Ik wil graag de permissies zien van user WILLEM"] = @"SELECT p.*
FROM [pa].[User] u
JOIN [pa].[UserRole] ur ON ur.UserId = u.Id
JOIN [pa].[RolePermission] rp ON rp.RoleId = ur.RoleId
JOIN [pa].[Permission] p ON p.Id = rp.PermissionId
WHERE u.Name = 'WILLEM'
",
            ["Ik wil graag de polissen zien van de klant A"] = @"SELECT *
FROM Policy
WHERE ClientID = 'A'"
            ,
            ["Welke permissies heeft de rol ADMIN?"] = @"SELECT p.*
FROM[pa].[Role] r
JOIN[pa].[RolePermission] rp ON rp.RoleId = r.Id
JOIN[pa].[Permission] p ON p.Id = rp.PermissionId
WHERE r.Name = 'Administrator'"
           ,
            ["Welke claims heeft polis A"] = @"SELECT * FROM Claim WHERE PolicyID = (SELECT ID FROM Policy WHERE Number = 'A')
"
           ,
            [""] = @""
           ,
            [""] = @""
           ,
            [""] = @""

        };

        private void btnSend_Click(object sender, EventArgs e) {
            string question = listBox.Text;

            txtSQL.Text = sql[question];
            txtResponse.Text = this.qa[question];
        }

        private void AIForm_Load(object sender, EventArgs e) {
            listBox.Items.AddRange(qa.Keys.ToArray());

            foreach (string question in listBox.Items.Cast<string>()) {
                try {
                    var qa = this.qa[question];
                    var sql = this.sql[question];
                } catch (Exception ex) {
                    throw;
                }
            }
        }
    }
}
*/
