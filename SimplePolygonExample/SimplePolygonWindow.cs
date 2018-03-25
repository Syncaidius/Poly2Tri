using Poly2Tri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePolygonExample
{
    public partial class SimplePolygonWindow : Form
    {
        public SimplePolygonWindow()
        {
            InitializeComponent();

            // Take the example data for a number two and throw it into our "before" polygon control.
            Shape testShape = new Shape(ExampleData.NumberTwo, new Vector2(130,130), 0.05f);
            polyBefore.SetPoints(testShape);

            // Now triangulate and output the triangles into our "after" polygon control.
            List<Triangle> triangles = new List<Triangle>();
            testShape.Triangulate(triangles);
            polyAfter.SetTriangles(triangles);

            // Now triangulate again, but this time with a hole. These are added as shapes which cut into the base shape.
            // Holes must be fully contained within the parent shape. They cannot intersect an outer edge.
            triangles.Clear();
            Shape hole = new Shape(new Vector2(160, 150), new Vector2(200, 90), new Vector2(200, 190), new Vector2(150, 200));
            testShape.Holes.Add(hole);
            testShape.Triangulate(triangles);
            polyWithHoles.SetTriangles(triangles);
            polyWithHoles.SetHoles(testShape.Holes);
        }
    }
}
