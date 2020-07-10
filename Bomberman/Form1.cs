using System;
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
            game.Step();
            pictureBox1.Refresh();
            if (game.gameOver)
            {
                timer1.Stop();
                if (game.players[0].IsDead() && game.players[1].IsDead())
                    MessageBox.Show("Both of you died. There's no winner");
                else if (game.players[0].IsDead())
                    MessageBox.Show("Player 1 is dead, player 2 wins");
                else
                    MessageBox.Show("Player 2 is dead, player 1 wins");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            game.KeyDown(e.KeyCode);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            game.KeyUp(e.KeyCode);

        }
    }
}
