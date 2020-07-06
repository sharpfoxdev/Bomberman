﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    public partial class Form1 : Form
    {
        Game game = new Game(); //makes new map, players and loads pics
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            game.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.map.Step();
            pictureBox1.Refresh();
            if (game.gameOver)
            {
                timer1.Stop();
                MessageBox.Show("end");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //TODO
        }
    }
}