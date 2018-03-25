using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poly2Tri;

namespace SimplePolygonExample
{
    /// <summary>
    /// A helper control for drawing our example data.
    /// </summary>
    public partial class PolygonRenderer : UserControl
    {
        public enum Mode
        {
            Wireframe,
            Fill
        }

        class PointBatch
        {
            public PointF[] Points;
            public Pen Pen;
        }

        /// <summary>
        /// Gets or sets the render mode of the current <see cref="PolygonRenderer"/>
        /// </summary>
        public Mode RenderMode
        {
            get => _mode;
            set
            {
                if(_mode != value)
                {
                    _mode = value;
                    // TODO repaint with new mode.
                }
            }
        }

        List<PointBatch> _points;
        Mode _mode;
        Brush _defaultBrush;

        public PolygonRenderer()
        {
            InitializeComponent();
            _points = new List<PointBatch>();
            _mode = Mode.Wireframe;
            _defaultBrush = new SolidBrush(Color.Black);
        }

        public void SetPoints(IList<TriPoint> points, Color color)
        {
            PointBatch pb = new PointBatch();
            pb.Pen = new Pen(new SolidBrush(color));

            int i = 0;
            pb.Points = new PointF[points.Count];
            foreach (TriPoint p in points)
                pb.Points[i++] = new PointF((float)p.X, (float)p.Y);
        }

        public void SetPoints(Shape s, Color color)
        {
            SetPoints(s.Points, color);
        }

        public void ClearPoints()
        {
            _points.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                string str = this.Name ?? "Polygon Renderer";
                PointF center = new PointF(Bounds.Width / 2, Bounds.Height / 2);
                SizeF textHalf = e.Graphics.MeasureString(str, Font);
                e.Graphics.DrawString(str, Font, _defaultBrush, new PointF()
                {
                    X = center.X - (textHalf.Width / 2),
                    Y = center.Y - (textHalf.Height / 2)
                });
            }
            else
            {
                switch (_mode)
                {
                    case Mode.Wireframe:
                        foreach (PointBatch pb in _points)
                            e.Graphics.DrawLines(pb.Pen, pb.Points);
                        break;

                    case Mode.Fill:

                        break;
                }
            }
        }
    }
}
