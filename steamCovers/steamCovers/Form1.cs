using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace steamCovers
{
    public partial class Form1 : Form
    {
        private const string URL = "http://api.steampowered.com/ISteamApps/";

        public class Steam
        {
            public AppList applist { get; set; }
        }

        public class AppList
        {
            public List<App> apps { get; set; }
        }

        public class App
        {
            public long appId { get; set; }
            public string name { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            Steam data = GetApiData().Result;
            comboBox1.DataSource = data.applist.apps.ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "appid";
            string userName = Environment.UserName;
            tbFilePath.Text = "C:\\Users\\" + userName + "\\Pictures";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tbFilePath.Text = fbd.SelectedPath;
                }
                else
                {
                    tbFilePath.Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control c in panel2.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control c in panel2.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Checked = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            long selectedValue = 255220;
            try
            {
                selectedValue = (long)comboBox1.SelectedValue;
            }
            catch
            {
                App selectedValueApp = (App)comboBox1.SelectedValue;
                selectedValue = selectedValueApp.appId;
            }

            //header           
            LoadPicture(selectedValue, 1, "header.jpg");

            //logo
            LoadPicture(selectedValue, 2, "logo.png");

            //library_hero
            LoadPicture(selectedValue, 3, "library_hero.jpg");

            //library_600x900
            LoadPicture(selectedValue, 4, "library_600x900.jpg");

            //page_bg_generated
            LoadPicture(selectedValue, 5, "page_bg_generated.jpg");

            //page_bg_generated_v6b
            LoadPicture(selectedValue, 6, "page_bg_generated_v6b.jpg");

        }

        private void LoadPicture(long selectedValue, int number, string url)
        {
            string PictureBoxName = "pictureBox" + number;
            PictureBox PB = (PictureBox)Controls.Find(PictureBoxName, true)[0];
            string CheckBoxName = "checkBox" + number;
            CheckBox CB = (CheckBox)Controls.Find(CheckBoxName, true)[0];

            try
            {
                PB.Load("https://steamcdn-a.akamaihd.net/steam/apps/" + selectedValue + "/" + url);
                CB.Checked = true;

            }
            catch
            {
                PB.Load("https://tacm.com/wp-content/uploads/2018/01/no-image-available.jpeg");
                CB.Checked = false;

            }
            PB.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private static async Task<Steam> GetApiData()
        {
            Steam responseObj = new Steam();
            Steam steamList = new Steam();
            responseObj.applist = new AppList();
            steamList.applist = new AppList();
            responseObj.applist.apps = new List<App>();
            steamList.applist.apps = new List<App>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync("GetAppList/v0002/").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    responseObj = JsonConvert.DeserializeObject<Steam>(result);
                }

                foreach (var app in responseObj.applist.apps)
                {
                    if (app.name != "")
                    {
                        steamList.applist.apps.Add(app);
                    }
                }
            }

            steamList.applist.apps = steamList.applist.apps.OrderBy(n => n.name).ToList();
            return steamList;
        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> numberList = new List<int>();

                foreach (Control c in panel2.Controls)
                {
                    if ((c is CheckBox) && ((CheckBox)c).Checked)
                    {
                        var name = c.Name;
                        var num = name.Substring(name.Length - 1, 1);
                        var num2 = Int32.Parse(num);
                        numberList.Add(num2);
                    }
                }

                foreach (int i in numberList)
                {
                    string PictureBoxName = "pictureBox" + i;
                    PictureBox PB = (PictureBox)Controls.Find(PictureBoxName, true)[0];
                    string CheckBoxName = "checkBox" + i;
                    CheckBox CB = (CheckBox)Controls.Find(CheckBoxName, true)[0];
                    string ext = ".jpg";
                    if (i == 2)
                    {
                        ext = ".png";
                    }

                    string path = tbFilePath.Text;
                    var selectedItem = (App)comboBox1.SelectedItem;
                    var selectedName = RemoveSpecialCharacters(selectedItem.name);
                    if (!Directory.Exists(path + "\\" + selectedName))
                    {
                        Directory.CreateDirectory(path + "\\" + selectedName);
                    }
                    string fullPath = path + "\\" + selectedName + "\\" + CB.Text + ext;

                    PB.Image.Save(fullPath);
                }
                string message = "Sauvegarde réussie !";
                MessageBox.Show(message, "Succès");
            }
            catch
            {
                string message = "La sauvegarde n'a pas pu être effectuée !";
                DialogResult result = MessageBox.Show(message, "Erreur", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if (result == DialogResult.Abort)
                {
                    this.Close();
                }
                else if (result == DialogResult.Retry)
                {
                    button2_Click(new object(), new EventArgs());
                }
            }

        }

    }
}
