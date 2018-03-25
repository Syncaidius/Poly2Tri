using Poly2Tri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Shape = Poly2Tri.Shape;

namespace PolygonExample.WPF
{
    /// <summary>
    /// Interaction logic for PolygonRenderer.xaml
    /// </summary>
    public partial class PolygonRenderer : UserControl
    {
        Point[] _points;
        List<Geometry> _holes;
        Triangle[] _triangles;
        Pen _linePen;
        Pen _trianglePen;
        Pen _holeLinePen;
        Brush _holeFillBrush;
        Brush _defaultBrush;
        FormattedText _designText;

        public PolygonRenderer()
        {
            InitializeComponent();
            _defaultBrush = new SolidColorBrush(Color.FromRgb(0,0,0));
            _linePen = new Pen(new SolidColorBrush(Color.FromRgb(250, 0,0)), 1);
            _holeLinePen = new Pen(new SolidColorBrush(Color.FromRgb(20,20,200)), 1);
            _trianglePen = new Pen(new SolidColorBrush(Color.FromRgb(250, 250, 0)), 1);
            _holeFillBrush = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
            _holes = new List<Geometry>();

            Typeface test = new Typeface("Arial");
            _designText = new FormattedText("Name" ?? "Polygon Renderer", 
                CultureInfo.InvariantCulture, 
                FlowDirection.LeftToRight, 
                test, 
                16,
                _defaultBrush,
                1.0f);

            _designText.TextAlignment = TextAlignment.Center;
        }

        private void TriPointsToArray(IList<TriPoint> points, out Point[] destination)
        {
            int count = points.Count;
            count += points[0] != points[points.Count - 1] ? 1 : 0;

            int i = 0;
            destination = new Point[count];
            foreach (TriPoint p in points)
                destination[i++] = new Point((float)p.X, (float)p.Y);

            // Add final point to complete outline if the provided points did not form a complete loop/contour
            if (count > points.Count)
                destination[points.Count] = destination[0];
        }

        public void SetPoints(IList<TriPoint> points)
        {
            TriPointsToArray(points, out _points);
        }

        public void SetHoles(IList<Shape> holes)
        {
            foreach (Shape h in holes)
            {
                List<LineSegment> segs = new List<LineSegment>();

                Point start = new Point(h.Points[0].X, h.Points[0].Y);
                for (int i = 1; i < h.Points.Count; i++)
                    segs.Add(new LineSegment(new Point(h.Points[i].X, h.Points[i].Y), true));

                PathFigure figure = new PathFigure(start, segs, true);
                _holes.Add(new PathGeometry(new[] { figure }));
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

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                Point center = new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                context.DrawText(_designText, center);
            }
            else
            {
                // Draw shape outline.
                if (_points != null && _points.Length > 1)
                {
                    int last = _points.Length - 1;
                    for (int i = 0; i < last; i++)
                        context.DrawLine(_linePen, _points[i], _points[i + 1]);

                    // Connect end point to start point
                    context.DrawLine(_linePen, _points[last], _points[0]);
                }

                // Draw triangle outlines
                if (_triangles != null)
                {
                    foreach (Triangle t in _triangles)
                    {
                        Point p0 = new Point((float)t.Points[0].X, (float)t.Points[0].Y);
                        Point p1 = new Point((float)t.Points[1].X, (float)t.Points[1].Y);
                        Point p2 = new Point((float)t.Points[2].X, (float)t.Points[2].Y);
                        context.DrawLine(_linePen, p0, p1);
                        context.DrawLine(_linePen, p1, p2);
                        context.DrawLine(_linePen, p2, p0);
                    }
                }

                // Draw hole outlines
                foreach (Geometry hole in _holes)
                    context.DrawGeometry(_holeFillBrush, _holeLinePen, hole);
            }
        }
    }
}
