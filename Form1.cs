using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation.Lab._10
{
    public partial class Form1 : Form
    {
        string[] teams = new string[8] {"RUS", "ARG", "ESP", "GER", "ENG", "POR", "FRA", "WAL"};
        float[] team_lambda = new float[8];


        public Form1()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_rnd_teams_Click(object sender, EventArgs e)
        {
            grid_team_lambda.Rows.Clear();
            Random rnd = new Random();
            for (int i = 0; i < teams.Length; i++)
            {
                team_lambda[i] = (float)rnd.Next(201) / 100;
                grid_team_lambda.Rows.Add(teams[i], team_lambda[i]);
            }
        }
    }
}
