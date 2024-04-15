using Jeu_de_cartes.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jeu_de_cartes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<Card> Deck = new List<Card>();
            string filePath = "..\\Cards\\";
            DirectoryInfo d = new DirectoryInfo(filePath);
            foreach (var file in d.GetFiles("*.txt")) {
               
                string[] lines = File.ReadAllLines(filePath + file.ToString());
                Monster card = new Monster()
                {
                    Name = lines[0],
                    Id = Int32.Parse(Path.GetFileNameWithoutExtension(filePath + file.ToString())),
                    Description = lines[1],
                    Stars = Int32.Parse(lines[2]),
                    SubType = (SubType)Enum.Parse(typeof(SubType), lines[3]),
                    ATK = Int32.Parse(lines[4]),
                    DEF = Int32.Parse(lines[5]),
                    CardAttribute = (CardAttribute)Enum.Parse(typeof(CardAttribute), lines[6])
                };
                Deck.Add(card);
               
            }
            foreach (var card in Deck)
            {
                string[] row = { card.Id.ToString("000000"), card.Name, card.Description };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id2 = listView1.SelectedItems[0];
            var id = id2.SubItems[0].Text.ToString();
            
            //var id = listView1.SelectedItems[0].SubItems[0].Text.ToString();
            
            string url = "..\\CardsIMG\\" +  id + ".png";
            pictureBox1.Image = Image.FromFile(url);
        }
    }
}
