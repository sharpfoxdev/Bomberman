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
        Game game;
        public Form1()
        {
            /*initialisation*/
            InitializeComponent();
            game = new Game(); //makes new map, players and loads pictures
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*tells game to draw itself (map, players, ...)*/
            game.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*is called regularly, thus making game loop until game over*/
            game.Step();
            pictureBox1.Refresh(); //triggers redrawing
            if (game.gameOver)
            {
                timer1.Stop();
                if (game.players[0].dead && game.players[1].dead) //who won
                    MessageBox.Show("Both of you died. There's no winner");
                else if (game.players[0].dead)
                    MessageBox.Show("Player 1 is dead, player 2 wins");
                else
                    MessageBox.Show("Player 2 is dead, player 1 wins");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /*passes pressed key into game class*/
            game.KeyDown(e.KeyCode);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            /*passes lifted key to the game class*/
            game.KeyUp(e.KeyCode);

        }
    }
}
