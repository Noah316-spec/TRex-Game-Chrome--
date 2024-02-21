using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();
            GameReset();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space && jumping == false) jumping = true;

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(jumping == true)jumping = false;

            if (e.KeyCode == Keys.E && isGameOver == true) GameReset();


        }
        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            label1.Text = "Score: " + score;
            pictureBox1.Image = Properties.Resources.running;
            isGameOver = false;
            pictureBox1.Top = 367;

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    x.Left = position;
                }
            }

            timer1.Start();

        }

        private void MainGame(object sender, EventArgs e)
        {
            pictureBox1.Top += jumpSpeed;

            label1.Text = "Score: " + score;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }


            if (pictureBox1.Top > 366 && jumping == false)
            {
                force = 12;
                pictureBox1.Top = 367;
                jumpSpeed = 0;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (pictureBox1.Bounds.IntersectsWith(x.Bounds))
                    {
                        timer1.Stop();
                        pictureBox1.Image = Properties.Resources.dead;
                        MessageBox.Show("Press E to restart the game!");
                        isGameOver = true;
                    }
                }
            }

            if (score > 5)
            {
                obstacleSpeed = 15;
            }


        }
    }
}
