using Poly2Tri;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Take the example data for a number two and throw it into our "before" polygon control.
            Shape testShape = new Shape(ExampleData.NumberTwo, new Vector2(80, 130), 0.05f);
            polyBefore.SetPoints(testShape);

            // Now triangulate and output the triangles into our "after" polygon control.
            List<Triangle> triangles = new List<Triangle>();
            testShape.Triangulate(triangles);
            polyAfter.SetTriangles(triangles);

            // Now triangulate again, but this time with a hole. These are added as shapes which cut into the base shape.
            // Holes must be fully contained within the parent shape. They cannot intersect an outer edge.
            triangles.Clear();
            Shape hole = new Shape(new Vector2(110, 150), new Vector2(150, 90), new Vector2(150, 190), new Vector2(100, 200));
            testShape.Holes.Add(hole);
            testShape.Triangulate(triangles);
            polyWithHoles.SetTriangles(triangles);
            polyWithHoles.SetHoles(testShape.Holes);
        }
    }
}
