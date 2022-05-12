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
        int[] teams_id = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
        float[] team_lambda = new float[8];
        int[,] team_matches = new int[8, 8];
        int rounds = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void main_diagonale_setter()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (i == j) team_matches[i, j] = 1;
        }

        private int[] teams_next_round_matches()
        {
            int[] teams_id_next = new int[8];
            int k = 0;
            int[] teams_temp = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < 8; i++)
            {
                if (teams_temp[i] == 0)
                    for (int j = 0; j < 8; j++)
                    {
                        if (team_matches[i,j] != 1 && teams_temp[j] == 0)
                        {
                            teams_id_next[k] = i;
                            teams_id_next[k+1] = j;
                            k += 2;

                            team_matches[i, j] = 1;

                            teams_temp[i] = 1;
                            teams_temp[j] = 1;
                            break;
                        }
                    }
            }
            return teams_id_next;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_rnd_teams_Click(object sender, EventArgs e)
        {
            if (btn_start.Enabled == false) btn_start.Enabled = true;
            grid_team_lambda.Rows.Clear();
            Random rnd = new Random();
            for (int i = 0; i < teams.Length; i++)
            {
                team_lambda[i] = (float)rnd.Next(201) / 100;
                grid_team_lambda.Rows.Add(teams[i], team_lambda[i]);
            }
        }

        private void btn_next_round_Click(object sender, EventArgs e)
        {
            if (btn_reset.Enabled == false) btn_reset.Enabled = true;
            for (int i = 0; i < teams.Length; i++)
                for (int j = 0; j < teams.Length; j++)
                    grid_match_results[i, j].Style.BackColor = Color.White;
                    
            lbl_rounds.Text = $"Round: {rounds}/14";

            Random rnd = new Random();
            int[] next_round = new int[8];
            next_round = teams_next_round_matches();

            for (int i = 0; i < teams.Length; i += 2)
            {
                int a = next_round[i];
                int b = next_round[i + 1];
                // match(next_round[i], next_round[i + 1]);
                int goals_a = rnd.Next(10);
                int goals_b = rnd.Next(10);

                grid_match_results[a, b].Value = $"{goals_a} - {goals_b}";
                grid_match_results[a, b].Style.BackColor = Color.Green;
            }

            if (rounds == 14) btn_next_round.Enabled = false;
            rounds += 1;
        }

        private void match(int a, int b)
        {
            Random rnd = new Random();

            int goals_a = rnd.Next(10);
            int goals_b = rnd.Next(10);

            grid_match_results[a, b].Value = $"{goals_a} - {goals_b}";
            grid_match_results[a, b].Style.BackColor = Color.Green;
        }

        private int get_goals()
        {
            Random rnd = new Random();
            return rnd.Next(10);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            main_diagonale_setter();
            for (int i = 0; i < teams.Length; i++)
            {
                grid_match_results.Columns.Add(new DataGridViewColumn { HeaderText = teams[i], CellTemplate = new DataGridViewTextBoxCell() });
            }

            for (int i = 0; i < teams.Length; i++)
            {
                grid_match_results.Rows.Add();
                grid_match_results.Rows[i].HeaderCell = new DataGridViewRowHeaderCell { Value = teams[i] };
            }

            for (int i = 0; i < teams.Length; i++)
                for (int j = 0; j < teams.Length; j++)
                    if (i == j) grid_match_results[i, j].Value = "---";

            for (int i = 0; i < teams.Length; i++)
                grid_team_points.Rows.Add(teams[i], "0", "0", "0", "0");

            btn_start.Visible = false;
            btn_next_round.Visible = true;
            btn_rnd_teams.Enabled = false;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
