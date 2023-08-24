using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroSnaker
{
    public partial class Form1 : Form
    {
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标记是否为左键
        //PictureBox[] PI = new PictureBox[1000];
        private int fk_top;//当前食物坐标
        private int fk_left;//当前食物坐标
        private ArrayList PI = new ArrayList();//食物对象
        private ArrayList ts_top = new ArrayList();//吞噬坐标
        private ArrayList ts_left = new ArrayList();//吞噬坐标
        private string tit;//当前头方向
        private int fk_i;//食物个数
        private int fx_fangxiang;//键盘方案
        private int pffa;//皮肤方案
        private Boolean swhy;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            default_ch();
        }
        private void default_ch() {
            //this.BackgroundImage = RetroSnaker.Properties.Resources.bj;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            pffa = comboBox2.SelectedIndex;
            fx_fangxiang = comboBox3.SelectedIndex;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    timer1.Interval = 1000;
                    break;
                case 1:
                    timer1.Interval = 500;
                    break;
                case 2:
                    timer1.Interval = 100;
                    break;
                case 3:
                    timer1.Interval = 50;
                    break;
                case 4:
                    timer1.Interval = 20;
                    break;
                case 5:
                    timer1.Interval = 1;
                    break;
            }           
            for (int i = 0; i < 5; i++)
            {
                input_zb();
                sc_an(false);
                fk_i = fk_i + 1;
            }
            sc_an(true);
            comboBox1.SelectedIndex = 1;
            switch (pffa)
            {
                case 0:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.sanketit1;
                    break;
                case 1:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.snaketit2;
                    break;
                case 2:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.snaketit3;
                    break;
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        private void sc_an(Boolean scfs)//生成方块
        {
            Random r = new Random();
            int num = r.Next(10, 350);
            while (num % 10 != 0)
            {
                num = r.Next(10, 350);
            }
            int nub = r.Next(10, 690);
            while (nub % 10 != 0)
            {
                nub = r.Next(10, 690);
            }
            //int numm = r.Next(1, 100);//生成特殊块随机数

            PictureBox MYPI= new PictureBox();
            
            MYPI.Name = "fk" + fk_i.ToString();
            MYPI.Width = 10;
            MYPI.Height = 10;
            switch (pffa)
            {
                case 0:
                    MYPI.BackgroundImage = RetroSnaker.Properties.Resources.sankebody1;
                    break;
                case 1:
                    MYPI.BackgroundImage = RetroSnaker.Properties.Resources.snakebody2_;
                    break;
                case 2:
                    MYPI.BackgroundImage = RetroSnaker.Properties.Resources.snakebody3_;
                    break;
            }
            MYPI.BackColor = Color.White;
            if (scfs == true)
            {
                MYPI.Top = num;
                fk_top = num;
                MYPI.Left = nub;
                fk_left = nub;
            }
            else
            {
                MYPI.Top = (int)ts_top[fk_i]+10;
                fk_top = (int)ts_top[fk_i]+10;
                MYPI.Left = (int)ts_left[fk_i];
                fk_left = (int)ts_left[fk_i];
            }
            PI.Add(MYPI);
            this.Controls.Add((PictureBox)PI[fk_i]);
            this.ResumeLayout(false);
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (fx_fangxiang == 0)
            {
                HotKey.RegisterHotKey(Handle, 100, 0, Keys.W);
                HotKey.RegisterHotKey(Handle, 101, 0, Keys.S);
                HotKey.RegisterHotKey(Handle, 102, 0, Keys.A);
                HotKey.RegisterHotKey(Handle, 103, 0, Keys.D);
            }
            else{
                HotKey.RegisterHotKey(Handle, 100, 0, Keys.Up);
                HotKey.RegisterHotKey(Handle, 101, 0, Keys.Down);
                HotKey.RegisterHotKey(Handle, 102, 0, Keys.Left);
                HotKey.RegisterHotKey(Handle, 103, 0, Keys.Right);
            }
            HotKey.RegisterHotKey(Handle, 104, 0, Keys.Space);
            HotKey.RegisterHotKey(Handle, 105, 0, Keys.Tab);
        }
        protected override void WndProc(ref Message m)//热键事件
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            if (tit !="s") { tit = "w"; }
                            break;
                        case 101:
                            if (tit != "w") { tit = "s"; }
                            break;
                        case 102:
                            if (tit != "d") { tit = "a"; }
                            break;
                        case 103:
                            if (tit != "a") { tit = "d"; }
                            break;
                        case 104:
                            if (timer1.Enabled)
                            {
                                timer1.Enabled = false;
                                label3.Visible = true;
                            }
                            else {
                                timer1.Enabled = true;
                                label3.Visible = false;
                            }
                            break;
                        case 105:
                            if (tabControl1.Visible == false)
                            {
                                tabControl1.Visible = true;
                                timer1.Enabled = false;
                                label3.Visible = true;
                            }
                            else {
                                tabControl1.Visible = false;
                            }                        
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        private void pictureBox1_Move(object sender, EventArgs e)//移动事件
        {
            //判断死亡
            gameover();
            //吞食
            eat_fk();
            //子体坐标跟随
            gs_zb();
            //记录当前坐标
            input_zb();
        }
        private void gameover()//判断死亡
        {
            //撞到墙壁上，死！
            if (pictureBox1.Left < 10 || pictureBox1.Top < 10 || pictureBox1.Left > (690 - pictureBox1.Width) || pictureBox1.Top > 340)
            {
                if (swhy == false)
                {
                    timer1.Enabled = false;
                    label3.Visible = true;
                    MessageBox.Show("您已经死亡！");
                    hy_default();
                    return;
                }       
            }
            for (int i = 0; i < ts_left.Count; i++)//撞到身体上，也死！
            {
                int a = (int)ts_left[i];
                int b = (int)ts_top[i];
                if (pictureBox1.Left == a && pictureBox1.Top==b && swhy == false)
                {
                    timer1.Enabled = false;
                    label3.Visible = true;
                    MessageBox.Show("您已经死亡！");
                    hy_default();
                    return;
                }
            }
        }
        private void gs_zb()//坐标跟随
        {
            for (int i = 0; i < fk_i; i++)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name == "fk" + i.ToString())
                    {
                        if (fk_i == 1)                     
                        {
                            c.Top = (int)ts_top[0];
                            c.Left = (int)ts_left[0];
                        }
                        else
                        {
                            c.Top = (int)ts_top[i];
                            c.Left = (int)ts_left[i];
                        }
                    }
                }
            }
        }
        private void input_zb()//记录坐标
        {
            if (ts_top.Count == 0)
            {
                ts_top.Add(pictureBox1.Top);
                ts_left.Add(pictureBox1.Left);

            }
            else
            {
                ts_top[0] = pictureBox1.Top;
                ts_left[0] = pictureBox1.Left;
            }
            for (int i = 1; i < fk_i+1; i++)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.Name == "fk" + (i-1).ToString())
                    {
                        if (ts_top.Count >= i+1)
                        {
                            ts_top[i] = c.Top;
                            ts_left[i] = c.Left;
                        }
                        else
                        {
                            ts_top.Add(c.Top);
                            ts_left.Add(c.Left);
                        }
                    }
                }
            }
        }
        private void eat_fk()//吞食事件
        {
            if (pictureBox1.Left == fk_left && pictureBox1.Top == fk_top)
            {
                fk_i = fk_i + 1;//吞食数量增加
                         
                //坐标跟随
                if (fk_i == 1)//如果当前只有一个主块，则为主块移动前坐标，如果有子块，则为最后一个子块移动前坐标
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == "fk" + fk_i.ToString())
                        {
                            c.Top = (int)ts_top[0];
                            c.Left = (int)ts_left[0];
                            ts_top.Add(c.Top);
                            ts_left.Add(c.Left);
                        }
                    }
                }
                else
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == "fk" + (fk_i-1).ToString())
                        {
                            c.Top = (int)ts_top[ts_top.Count - 1];
                            c.Left = (int)ts_left[ts_top.Count - 1];
                        }
                    }
                }
                sc_an(true);//随即增加方块
            }
        }
        private void timer1_Tick(object sender, EventArgs e)//时钟事件
        {
            switch (tit)
            {
                case "w":
                    pictureBox1.Top = pictureBox1.Top - pictureBox1.Height;
                    break;
                case "s":
                    pictureBox1.Top = pictureBox1.Top + pictureBox1.Height;
                    break;
                case "a":
                    pictureBox1.Left = pictureBox1.Left - pictureBox1.Width;
                    break;
                case "d":
                    pictureBox1.Left = pictureBox1.Left + pictureBox1.Width;
                    break;
                default:
                    break;
            }
        }
        private void hy_default() //死亡还原
        {
            swhy = true;
            pictureBox1.Left = 340;
            pictureBox1.Top = 160;
            ts_top.Clear();
            ts_left.Clear();
            for (int i = 0; i < PI.Count; i++)
            {
                this.Controls.Remove((PictureBox)PI[i]);
            }
            PI.Clear();
            tit = "w";
            timer1.Enabled = false;
            fk_i = 0;
            default_ch();
            swhy = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label3.Visible = true;
            pffa = comboBox2.SelectedIndex;
            switch (pffa)
            {
                case 0:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.sanketit1;
                    break;
                case 1:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.snaketit2;
                    break;
                case 2:
                    pictureBox1.BackgroundImage = RetroSnaker.Properties.Resources.snaketit3;
                    break;
            }
            for (int i = 0; i < PI.Count; i++)
            {
                switch (pffa)
                {
                    case 0:
                        ((PictureBox)PI[i]).BackgroundImage = RetroSnaker.Properties.Resources.sankebody1;
                        break;
                    case 1:
                        ((PictureBox)PI[i]).BackgroundImage = RetroSnaker.Properties.Resources.snakebody2_;
                        break;
                    case 2:
                        ((PictureBox)PI[i]).BackgroundImage = RetroSnaker.Properties.Resources.snakebody3_;
                        break;
                }
            }
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    timer1.Interval = 1000;
                    break;
                case 1:
                    timer1.Interval = 500;
                    break;
                case 2:
                    timer1.Interval = 100;
                    break;
                case 3:
                    timer1.Interval = 50;
                    break;
                case 4:
                    timer1.Interval = 20;
                    break;
                case 5:
                    timer1.Interval = 1;
                    break;
            }
            if (comboBox3.SelectedIndex == 0)
            {
                fx_fangxiang = 1;
            }
            else {
                fx_fangxiang = 2;
            }
            MessageBox.Show("设置成功！");
            tabControl1.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.Visible == false) {
                tabControl1.Visible = true;
                timer1.Enabled = false;
                label3.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)//退出游戏
        {
            timer1.Enabled = false;
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要退出游戏吗?", "退出游戏", messButton);
            if (dr == DialogResult.OK)//如果点击“确定”按钮
            {
                System.Environment.Exit(0);
            }
            timer1.Enabled = true;
        }
    }
}
