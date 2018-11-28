using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace LearningGraphics
{
    public partial class Form1 : Form
    {
        Graphics _device;
        Bitmap _character;
        Sprite _tester;
        int _currentTime;
        int _startTime;

        PictureBox bg;
        Bitmap surface;

        int frameCount;
        int frameTimer;
        float frameRate;

        public Form1()
        {
            InitializeComponent();

            bg = new PictureBox();
            bg.Parent = this.FindForm();
            bg.Dock = DockStyle.Fill;
            bg.BackColor = Color.Black;

            surface = new Bitmap(Width, Height);
            bg.Image = surface;
            _device = Graphics.FromImage(surface);
            _character = DataControl.LoadBitmap("CharacterTest.png");
            _tester = new Sprite(_character, 4, 16);
        }        

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;

            while (true)
            {
                _currentTime = Environment.TickCount;

                if (_currentTime > _startTime + 16)
                {
                    _startTime = _currentTime;

                    _tester.Animate();
                    _tester.Draw(ref _device);

                    Application.DoEvents();

                    bg.Image = surface;
                }

                frameCount++;
                if (_currentTime > frameTimer + 1000)
                {
                    frameTimer = _currentTime;
                    frameRate = frameCount;
                    frameCount = 0;
                }


            }
        }
    }
}
