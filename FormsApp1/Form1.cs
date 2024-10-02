using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp1
{
    public partial class Form1 : Form
    {
        public int turn = 0;
        private List<Button> player1Buttons = new List<Button>();
        private List<Button> player2Buttons = new List<Button>();
        public Form1()
        {
            InitializeComponent();
        }

        void Reset() { 
            turn = 0;
            foreach (Button btn in panelMain.Controls) {
                btn.BackColor = SystemColors.Control;
                btn.Text=string.Empty;
                btn.Enabled = true;
            }
            player1Buttons.Clear();
            player2Buttons.Clear();
        }
        void Action(Button btn)
        {
            
            //Button btn1 = panelMain.Controls.Find(btnName, false).FirstOrDefault() as Button; 
            if (btn != null)
            {
                if(turn % 2 == 0)
                {
                    btn.BackColor = System.Drawing.Color.MediumSpringGreen;
                    btn.Text = "X";
                    player1Buttons.Add(btn);
                    lblTurn.Text = "بازیکن دوم";
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.Gold;
                    btn.Text = "O";
                    player2Buttons.Add(btn);
                    lblTurn.Text = "بازیکن اول";
                }
                btn.Enabled = false;
                panelMain.Focus();     
            }
        }
        bool Check_diagonal(List<Button> player,int d) 
        {
           
            if (d == 1)
            {
                
                for (int i = 1; i <= 9; i += 4)
                {
                  
                    if (!player.Any(btn => btn.Name == $"button{i}")) return false;
                }
            }else
            {
                
                for (int i = 3; i <= 7; i += 2)
                {
                   
                    if (!player.Any(btn => btn.Name == $"button{i}")) return false;
                }
            }
            return true;
        }

        bool Check_vertical(List<Button> player, int c)
        {
            MessageBox.Show($"vertical", "", MessageBoxButtons.OK);
            for (int i = c; i <= c+6; i+=3)
            {
                
                if (!player.Any(btn => btn.Name == $"button{i}")) {
                   
                    return false; 
                }
            }
            
            return true;
        }

        bool Check_horizental(List<Button> player, int r)
        {
           
            for (int i = (3*r) - 2; i <= 3*r; i += 1)
            {
                
                if (!player.Any(btn => btn.Name == $"button{i}")) return false;
            }
            return true;
        }
        bool Check_winner(char player,Button move)
        {
            List<Button> currentButtons = (player == '1') ? player1Buttons : player2Buttons;
            
            switch (move.Name)
            {
                case "button1":
                    {
                        
                        return ( Check_vertical(currentButtons,1) || Check_horizental(currentButtons, 1)  || Check_diagonal(currentButtons, 1));
                    }
                case "button2":
                    {
                        
                        return (Check_vertical(currentButtons, 2) || Check_horizental(currentButtons, 1));
                    }
                case "button3":
                    {
                        
                        return Check_vertical(currentButtons, 3) || Check_horizental(currentButtons,1) || Check_diagonal(currentButtons, 1);
                    }
                case "button4":
                    {
                       
                        return Check_vertical(currentButtons, 1) || Check_horizental(currentButtons, 2) ;
                    }
                case "button5":
                    {
                        return Check_vertical(currentButtons, 2) || Check_horizental(currentButtons, 2) || Check_diagonal(currentButtons, 1) || Check_diagonal(currentButtons,2);
                    }
                case "button6":
                    {
                        return Check_vertical(currentButtons, 3) || Check_horizental(currentButtons, 2) ;
                    }
                case "button7":
                    {
                        return Check_vertical(currentButtons, 1) || Check_horizental(currentButtons, 3) || Check_diagonal(currentButtons, 2);
                    }
                case "button8":
                    {
                        return Check_vertical(currentButtons, 2) || Check_horizental(currentButtons, 3) ;
                    }
                case "button9":
                    {
                        return Check_vertical(currentButtons, 3) || Check_horizental(currentButtons, 3) || Check_diagonal(currentButtons, 1);
                    }
                default: return false;
            }
        }
        void Check(Button move)
        {
            if (turn >= 4)
            {
                
                if (turn % 2 == 0)
                {
                    if(Check_winner('1', move))
                    {
                        MessageBox.Show("بازیکن اول برنده شد","",MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblPlayer1Score.Text = (Convert.ToInt16(lblPlayer1Score.Text) + 1).ToString();
                        Reset();
                        return;
                    }
                }
                else
                {
                    if(Check_winner('2', move))
                    {
                        MessageBox.Show("بازیکن دوم برنده شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblPlayer2Score.Text = (Convert.ToInt16(lblPlayer2Score.Text) + 1).ToString();
                        Reset();
                        return;
                    }
                }
                if (isAllButtonDisable())
                {
                    MessageBox.Show("بازی مساوی شد", "", MessageBoxButtons.OK);
                    Reset();
                }
            }
            turn++;
        }
        bool isAllButtonDisable()
        {
            foreach(Button btn in panelMain.Controls)
            {
                if (btn.Enabled) return false;
            }
            return true;
        }
        private void btnResetGame_Click(object sender, EventArgs e) {
            
            DialogResult result = MessageBox.Show("بازی ریست شود؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                Reset();
                lblTurn.Text = "بازیکن اول";
                lblPlayer1Score.Text = lblPlayer2Score.Text = "0";
            }
        }

        private void btnPlayAgain_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("بازی جدید شروع شود", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lblTurn.Text = "بازیکن اول";
                Reset();
                
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Action(sender as Button);
            Check(sender as Button);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
