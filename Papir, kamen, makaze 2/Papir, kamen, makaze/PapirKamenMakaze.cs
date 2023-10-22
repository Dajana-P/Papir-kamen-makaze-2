using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Papir__kamen__makaze
{

    public partial class PapirKamenMakaze : Form
    {
        int roundNum = 0;
        Random rand = new Random();
        StreamWriter sw = new StreamWriter(@"C:\Users\PD\Desktop\Rezultati\Rezultat.txt");
        List<string> Log = new List<string>();
        int brKamen, brPapir, brMakaze, brNereseno;

        public PapirKamenMakaze()
        {
            InitializeComponent();
            lblWinner.Text = "";
            lblRoundNumber.Text = "";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                brKamen = 0;
                brPapir = 0;
                brMakaze = 0;
                brNereseno = 0;
                Round();
                timerRound.Start();
                btnStart.Text = "Stop";
            }
            else
            {
                timerRound.Dispose();
                Results();
                btnStart.Text = "Start";
                pBLeft.Image = Properties.Resources.img;
                pBRight.Image = Properties.Resources.img;
                lblWinner.Text = "";
                lblRoundNumber.Text = "";
                roundNum = 0;
            }

        }

        private Image GetImage(int br)
        {
            Image pic;
            switch (br)
            {
                case 1:
                    pic = Properties.Resources.p; //papir = 1
                    break;
                case 2:
                    pic = Properties.Resources.r; //kamen = 2
                    break;
                case 3:
                    pic = Properties.Resources.s; //makaze = 3
                    break;
                default:
                    pic = Properties.Resources.img;
                    break;
            }
            return pic;
        }
        private int RandomNumber()
        {
            return rand.Next(1, 4);
        }

        private String Winner(int brL, int brR)
        {
            if ((brL == 1 && brR == 2) || (brL == 2 && brR == 1))
            {
                ++brPapir;
                return "Papir je pobedio";
            }
            else if ((brL == 1 && brR == 3) || (brL == 3 && brR == 1))
            {
                ++brMakaze;
                return "Makaze su pobedile";
            }
            else if ((brL == 2 && brR == 3) || (brL == 3 && brR == 2))
            {
                ++brKamen;
                return "Kamen je pobedio";
            }
            else
            {
                ++brNereseno;
                return "Nereseno";
            }
        }

        private void timerRound_Tick(object sender, EventArgs e)
        {
            Round();
        }

        private void Round()
        {
            int rbrL = RandomNumber();
            int rbrR = RandomNumber();
            pBLeft.Image = GetImage(rbrL);
            pBRight.Image = GetImage(rbrR);
            lblWinner.Text = Winner(rbrL, rbrR);
            lblRoundNumber.Text = "Round: " + ++roundNum;
            Log.Add(lblRoundNumber.Text + "\t" + lblWinner.Text);
        }

        private void Results()
        {
            foreach (string rezultat in Log)
            {
                sw.WriteLine(rezultat);
            }
            sw.WriteLine();
            sw.WriteLine("Broj pobeda kamena: " + brKamen);
            sw.WriteLine("Broj pobeda papira: " + brPapir);
            sw.WriteLine("Broj pobeda makaza: " + brMakaze);
            sw.WriteLine("Broj neresenih rundi: " + brNereseno);
            sw.Close();
        }
    }
}
