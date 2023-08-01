using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tirage
{
    public partial class Form1 : Form
    {
        List<string> listName = new List<string>();
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!listName.Contains(textBox1.Text) && textBox1.Text != "")
            {
                listName.Add(textBox1.Text);
                listBox1.Items.Add(textBox1.Text);
            }
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var list = Shuffle(listName);
            listBox2.DataSource = list;
        }

        private List<string> Shuffle(List<string> list)
        {
            Random rand = new Random();
            bool good = false;
            var shuffled = new List<string>();
            var count = list.Count();
            int ct = 0;
            var isF = true;
            List<string> emptyList = new List<string>();
            while (!good)
            {
                isF = true;
                shuffled = list.OrderBy(_ => rand.Next()).ToList();
                foreach (var item in shuffled)
                {
                    foreach (var item2 in list)
                    {
                        if ((item == item2) && (shuffled.IndexOf(item) == list.IndexOf(item2)))
                        {
                            ct++;
                            isF = false;                           
                        }

                    }
                }
                if (isF)
                {
                    good = true;
                }
            }
            foreach(var item in shuffled)
            {
                var newItem = list[shuffled.IndexOf(item)] + " offre à " + item;
                emptyList.Add(newItem);
            }
            return emptyList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox1.SelectedItem;
            if (selectedItem != null)
            {
                listBox1.Items.Remove(selectedItem);
                listName.Remove(selectedItem.ToString());
            }
        }
    }
}
