﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D3strUct0r
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //The only reason for this to be a form, a timer.
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            Program.startMove();
        }
     

     
    }
}