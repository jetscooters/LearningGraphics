using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Sprite
    {
        public enum AnimateDir
        {
            NONE = 0,
            FORWARD = 1,
            BACKWARD = 1
        }

        public enum AnimateWrap
        {
            WRAP = 0,
            BOUNCE = 1
        }

        private PointF _position;
        private PointF _velocity;
        private Size _size;
        private Bitmap _bitmap;
        private bool _alive;
        private int _columns;
        private int _totalFrames;
        private int _currentFrame;
        private AnimateDir _animationDir;
        private AnimateWrap _animationWrap;
        private int _lastTime;
        private int _animationRate;

        public bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        public Bitmap Image
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public PointF Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public PointF Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public int Width
        {
            get { return _size.Width; }
            set { _size.Width = value; }
        }

        public int Height
        {
            get { return _size.Height; }
            set { _size.Height = value; }
        }

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public int TotalFrames
        {
            get { return _totalFrames; }
            set { _totalFrames = value; }
        }

        public int CurrentFrame
        {
            get { return _currentFrame; }
            set { _currentFrame = value; }
        }

        public AnimateDir AnimateDirection
        {
            get { return _animationDir; }
            set { _animationDir = value; }
        }

        public AnimateWrap AnimateWrapMode
        {
            get { return _animationWrap; }
            set { _animationWrap = value; }
        }

        public int AnimationRate
        {
            get { return 1000 / _animationRate; }
            set
            {
                if (value == 0) value = 1;
                _animationRate = 1000 / value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                Rectangle rect = new Rectangle(
                    (int)_position.X, (int)_position.Y, 
                    _size.Width, _size.Height);

                return rect;
            }
        }

        public bool IsColliding(ref Sprite other)
        {
            return Bounds.IntersectsWith(other.Bounds);
        }

        public Sprite()
        {
            _position = new PointF(0,0);
            _velocity = new PointF(0, 0);
            _size = new Size(0, 0);
            _bitmap = null;
            _alive = true;
            _columns = 1;
            _totalFrames = 1;
            _currentFrame = 0;
            _animationDir = AnimateDir.FORWARD;
            _animationWrap = AnimateWrap.WRAP;
            _lastTime = 0;
            _animationRate = 20;
        }

        public Sprite(Bitmap sheet, int columns, int frames)
        {
            _position = new PointF(0, 0);
            _velocity = new PointF(0, 0);
            _size = new Size(sheet.Width / columns, sheet.Height / columns);
            _bitmap = sheet;
            _alive = true;
            _columns = columns;
            _totalFrames = frames;
            _currentFrame = 0;
            _animationDir = AnimateDir.FORWARD;
            _animationWrap = AnimateWrap.WRAP;
            _lastTime = 0;
            _animationRate = 500;
        }

        public void Animate()
        {
            Animate(0, _totalFrames - 1);
        }

        public void Animate(int startFrame, int endFrame)
        {
            //Do we need to animate?
            if (_totalFrames <= 0)
            {
                return;
            }

            //Check animation timing
            int time = Environment.TickCount;
            if (time > _lastTime + _animationRate)
            {
                _lastTime = time;

                //go to next frame
                _currentFrame += (int)_animationDir;
            }

            //Wrap the animation
            if (_currentFrame < startFrame)
            {
                if (_animationWrap == AnimateWrap.WRAP)
                {
                    _currentFrame = endFrame;
                }
                else if (_animationWrap == AnimateWrap.BOUNCE)
                {
                    _currentFrame = startFrame;
                    _animationDir = AnimateDir.FORWARD;
                }
            }
            else if (_currentFrame > endFrame)
            {
                if (_animationWrap == AnimateWrap.WRAP)
                {
                    _currentFrame = startFrame;
                }
                else if (_animationWrap == AnimateWrap.BOUNCE)
                {
                    _currentFrame = endFrame;
                    _animationDir = AnimateDir.BACKWARD;
                }
            }
        }

        public void Draw(ref Graphics device)
        {
            Rectangle frame = new Rectangle();
            frame.X = (_currentFrame % _columns) * _size.Width; //Why?
            frame.Y = (_currentFrame / _columns) * _size.Height; //^^
            frame.Width = _size.Width;
            frame.Height = _size.Height;
            device.DrawImage(_bitmap, Bounds, frame, GraphicsUnit.Pixel);
        }
    }
}
