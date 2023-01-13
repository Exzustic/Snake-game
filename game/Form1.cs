using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;
using System.Collections.Generic;
using System.Collections;
using System.Net.NetworkInformation;

namespace game
{

    public partial class Form1 : Form
    {
        public Bitmap HeadTexture = Resource1.head,
                      SquareTexture = Resource1.square,
                      AppleTexture = Resource1.apple;

        Random random = new Random();


        Point p = new Point(100, 200);
        List<int> _list = new List<int>();

        int speed = 5;
        int moving;

        bool rightleft = false;
        bool updown = true;

        bool apple = true;
        int appleChecker = 0;
        int x;
        int xr;
        int yr;

        int pos1 = 0;
        int pos2 = 0;
        int pos3 = 0;
        int pos4 = 0;
        int pos5 = 0;
        int pos6 = 0;
        int pos7 = 0;
        int pos8 = 0;

        string position = "up";
        string btn = "w";



        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            timer1.Enabled = false;

            xr = random.Next(1, 18);
            yr = random.Next(1, 14);

            _list.Add(260340);
            //_list.Add(260380);
            //_list.Add(260420);
            //_list.Add(280460);
            x = _list.Count;


            UpdateStyles();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _list.Clear();
            _list.Add(260340);
            _list.Add(260380);
            _list.Add(260420);
            x = _list.Count;
            timer1.Enabled = true;
            btnStart.Visible = false;
            moving = -1 * speed;
            rightleft = false;
            updown = true;
            labelScore.Text = ("Score:0");
            switch (btn)
            {
                case ("d"):
                    HeadTexture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
                case ("a"):
                    HeadTexture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case ("s"):
                    HeadTexture.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                default: 
                    break;
            }
            position = "up";
            btn = "w";


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && rightleft == false)
            {
                btn = "d";
            }
            if (e.KeyCode == Keys.A && rightleft == false)
            {
                btn = "a";
            }
            if (e.KeyCode == Keys.W && updown == false)
            {
                btn = "w";
            }
            if (e.KeyCode == Keys.S && updown == false)
            {
                btn = "s";
            }
            

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
            if (btn == "d" && (_list[0] / 1000 - 20) % 40 == 0 && (_list[0] % 1000 - 20) % 40 == 0)
            {
                moving = 1000 * speed;
                rightleft = true;
                updown = false;
                if (position == "up")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (position == "down")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                position = "right";
            }
            if (btn == "a" && (_list[0] / 1000 - 20) % 40 == 0 && (_list[0] % 1000 - 20) % 40 == 0)
            {
                moving = -1000 * speed;
                rightleft = true;
                updown = false;
                if (position == "down")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (position == "up")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                position = "left";
            }
            if (btn == "w" && (_list[0] / 1000 - 20) % 40 == 0 && (_list[0] % 1000 - 20) % 40 == 0)
            {
                moving = -1 * speed;
                rightleft = false;
                updown = true;
                if (position == "left")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (position == "right")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                position = "up";
            }
            if (btn == "s" && (_list[0] / 1000 - 20) % 40 == 0 && (_list[0] % 1000 - 20) % 40 == 0)
            {
                moving = speed;
                rightleft = false;
                updown = true;
                if (position == "right")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (position == "left")
                {
                    HeadTexture.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                position = "down";
            }

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(AppleTexture, new Rectangle(xr * 40 + 20, yr * 40 - 20, 40, 40));

            for (int i = 0; i < x; i++)
            {
                if (i == 0)
                {
                    g.DrawImage(HeadTexture, new Rectangle(_list[i] / 1000, _list[i] % 1000, 40, 40));
                }
                else
                {
                    if (i % 8 == 0)
                    {
                        g.DrawImage(SquareTexture, new Rectangle((_list[i]) / 1000, (_list[i]) % 1000, 40, 40));
                    }

                }
                if (_list[0] == _list[i] && i != 0)
                {
                    timer1.Enabled = false;
                    btnStart.Visible = true;
                    btnStart.Enabled = true;
                    btnStart.Text = "Restart";
                }
            }
            _list.Insert(0, _list[0] + moving);
            if (_list[0] / 1000 == xr * 40 + 20 && _list[0] % 1000 == yr * 40 - 20)
            {
                apple = true;

                while (apple == true)
                {
                    xr = random.Next(1, 19);
                    yr = random.Next(1, 14);
                    appleChecker = 0;
                    for (int i = 0; i < x; i++)
                    {
                        if (_list[i] / 1000 == xr * 40 + 20 && _list[i] % 1000 == yr * 40 - 20)
                        {
                            appleChecker++;
                        }
                    }
                    if (appleChecker == 0) { apple = false; }
                }

                _list.Add(pos1);
                _list.Add(pos2);
                _list.Add(pos3);
                _list.Add(pos4);
                _list.Add(pos5);
                _list.Add(pos6);
                _list.Add(pos7);
                _list.Add(pos8);

                x = _list.Count;
                labelScore.Text = ($"Score: {x / 8}");
            }
            else if ((_list[0] / 1000) < 20 || (_list[0] / 1000 > 740) || (_list[0] % 1000) < 20 || (_list[0] % 1000 > 540))
            {
                timer1.Enabled = false;
                btnStart.Visible = true;
                btnStart.Enabled = true;
                btnStart.Text = "Restart";
            }
            else
            {
                pos8 = pos7;
                pos7 = pos6;
                pos6 = pos5;
                pos5 = pos4;
                pos4 = pos3;
                pos3 = pos2;
                pos2 = pos1;
                pos1 = _list[x];
                _list.RemoveAt(x);
            }
        }
    }
}
