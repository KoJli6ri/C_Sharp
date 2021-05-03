using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Курсовая_работа_Sharp
{
    public partial class Form1 : Form
    {   //картинки которые я использую на бэкграунде для выполнени поставленной задачи
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        public Bitmap HandlerTexutere = Resource1.handler1,
                      TargetTexture = Resource1.target;
        private Point _targetPosition = new Point(300, 300);
        private Point _direction = Point.Empty;
        private int _score = 0;

        public Form1()
        {
            InitializeComponent();

            player.URL = "Ace of Bace.mp3";

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();

            timer2.Interval = r.Next(25, 100);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }

        //использую переменные для формы рисовки
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var LocalPosition = this.PointToClient(Cursor.Position);

            _targetPosition.X += _direction.X * 5;
            _targetPosition.Y += _direction.Y * 5;

            if(_targetPosition.X < 0|| _targetPosition.X > 500)
            {
                _direction.X *= -1;
            }
            if (_targetPosition.Y < 0 || _targetPosition.Y > 500)
            {
                _direction.Y *= -1;
            }

            Point between = new Point(LocalPosition.X - _targetPosition.X, LocalPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt (between.X * between.X) + (between.Y * between.Y);   

            if(distance <25)
            {
                AddScore(1);
            }

            var hendlerRect = new Rectangle(LocalPosition.X - 50, LocalPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);

            g.DrawImage(HandlerTexutere, hendlerRect);  
            g.DrawImage(TargetTexture, targetRect);
        }
        private void AddScore(int score)
        {
            _score += score;
            scorelabel1.Text = _score.ToString();
        }

    }
}
