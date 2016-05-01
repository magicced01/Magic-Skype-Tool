﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;

namespace Magic_Skype_Tool
{
    public partial class Form1 : Form
    {
        Skype skype;
        List<String> contacts = new List<string>();
        AutoCompleteStringCollection searchindex = new AutoCompleteStringCollection();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            skype = new Skype();
            timer1.Enabled = true;
            timer1.Start();

        }
        private void loadContacts()
        {
            foreach (User user in skype.Friends)
            {
                contacts.Add(user.Handle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skype.Attach();
            loadContacts();
            indexContacts();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int usage = PerfUtils.getCpuUsage();
            progressBar1.Value = usage;
            label1.Text = "CPU usage:" + usage.ToString() + "%";
        }
        private void indexContacts()
        {
            foreach (User user in skype.Friends)
            {
                searchindex.Add(user.Handle);
            }
            textBox1.AutoCompleteCustomSource = searchindex;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (var user in contacts)
            {
                checkedListBox1.Items.Add(user);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count - 1; i++)
            {
                if (checkedListBox1.Items[i].ToString().ToLower().Contains(textBox1.Text))
                {
                    checkedListBox1.SetSelected(i, true);
                    checkedListBox1.SelectedIndex = i;
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < checkedListBox1.CheckedItems.Count)
            {
                skype.SendMessage(checkedListBox1.CheckedItems[i].ToString(), richTextBox1.Text);
                i++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "Speed: " + trackBar1.Value + "ms";
            timer2.Interval = trackBar1.Value;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (User user in skype.Friends)
            {
                skype.SendMessage(user.Handle, richTextBox1.Text);
            }
        }
    }
}
