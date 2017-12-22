﻿using System;

namespace TriMesh
{
    public sealed class Vertex
    {
        internal bool isSuper = false;
        internal bool inputVertex = false;

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public static Vertex Zero { get { return new Vertex(0, 0, 0); } }

        public Vertex(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = Z;
        }

        public double DistanceTo2(Vertex other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double DistanceTo3(Vertex other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            double dz = Z - other.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static bool operator ==(Vertex a, Vector b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return Utility.AlmostEqual(a.X, b.X) && Utility.AlmostEqual(a.Y, b.Y);
        }

        public static bool operator !=(Vertex a, Vector b)
        {
            return !(a == b);
        }

        public static Vertex operator +(Vertex a, Vector b)
        {
            return new Vertex(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vertex operator -(Vertex a, Vector b)
        {
            return new Vertex(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator -(Vertex a, Vertex b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static double InterpolateZ(Vertex a, Vertex b, double x, double y)
        {
            double d2 = (b - a).Length2;
            if (Utility.AlmostZero(d2)) return (a.Z + b.Z) / 2;

            Vertex v = new Vertex(x, y, 0);
            double d1 = (v - a).Length2;

            return d1 / d2 * (b.Z - a.Z) + a.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            Vertex other = obj as Vertex;
            if (other == null) return false;

            return this == other;
        }

        public override int GetHashCode()
        {
            unchecked // Wrap if overflows
            {
                int hash = 17;

                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return X.ToString("F2") + ", " + Y.ToString("F2") + ", " + Z.ToString("F2");
        }

        public static Vertex Average(params Vertex[] vertices)
        {
            double n = vertices.Length;
            double x = 0, y = 0, z = 0;
            foreach (Vertex v in vertices)
            {
                x += v.X;
                y += v.Y;
                z += v.Z;
            }
            return new Vertex(x / n, y / n, z / n);
        }
    }
}