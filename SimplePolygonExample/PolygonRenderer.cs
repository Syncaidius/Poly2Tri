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
        PointF[] _points;
        List<PointF[]> _holePoints;
        Triangle[] _triangles;
        Pen _pen;
        Pen _holePen;
        Pen _holePenBg;
        Brush _defaultBrush;

        public PolygonRenderer()
        {
            InitializeComponent();
            _defaultBrush = new SolidBrush(Color.Black);
            _pen = new Pen(new SolidBrush(Color.Red));
            _holePen = new Pen(new SolidBrush(Color.Blue));
            _holePenBg = new Pen(new SolidBrush(Color.FromArgb(255, 100, 100, 100)));
            _holePoints = new List<PointF[]>();
        }

        private void TriPointsToArray(IList<TriPoint> points, out PointF[] destination)
        {
            int count = points.Count;
            count += points[0] != points[points.Count - 1] ? 1 : 0;

            int i = 0;
            destination = new PointF[count];
            foreach (TriPoint p in points)
                destination[i++] = new PointF((float)p.X, (float)p.Y);

            // Add final point to complete outline if the provided points did not form a complete loop/contour
            if (count > points.Count)
                destination[points.Count] = destination[0];
            Refresh();
        }

        public void SetPoints(IList<TriPoint> points)
        {
            TriPointsToArray(points, out _points);
        }

        public void SetHoles(IList<Shape> holes)
        {
            foreach(Shape h in holes)
            {
                PointF[] hPoints = new PointF[h.Points.Count];
                TriPointsToArray(h.Points, out hPoints);
                _holePoints.Add(hPoints);
            }
        }

        public void SetPoints(Shape s)
        {
            SetPoints(s.Points);
        }

        public void SetTriangles(IList<Triangle> triangles)
        {
            _triangles = new Triangle[triangles.Count];
            triangles.CopyTo(_triangles, 0);
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
                if (_points != null)
                    e.Graphics.DrawLines(_pen, _points);

                if (_triangles != null)
                {
                    foreach (Triangle t in _triangles)
                    {
                        PointF p0 = new PointF((float)t.Points[0].X, (float)t.Points[0].Y);
                        PointF p1 = new PointF((float)t.Points[1].X, (float)t.Points[1].Y);
                        PointF p2 = new PointF((float)t.Points[2].X, (float)t.Points[2].Y);
                        e.Graphics.DrawLine(_pen, p0, p1);
                        e.Graphics.DrawLine(_pen, p1, p2);
                        e.Graphics.DrawLine(_pen, p2, p0);
                    }
                }

                foreach (PointF[] hPoints in _holePoints)
                {
                    e.Graphics.FillPolygon(_holePenBg.Brush, hPoints);
                    e.Graphics.DrawLines(_holePen, hPoints);
                }
            }
        }
    }
}
