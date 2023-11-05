﻿namespace Poly2Tri
{
    internal class SweepBasin
    {
        public Node LeftNode;

        public Node BottomNode;

        public Node RightNode;

        public double Width;

        public bool LeftHighest;

        public void Clear()
        {
            LeftNode = null;
            BottomNode = null;
            RightNode = null;
            Width = 0;
            LeftHighest = false;
        }
    }
}
