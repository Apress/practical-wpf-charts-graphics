using System;

namespace CurveFitting
{
    public struct VectorR:ICloneable
    {
        private int size;
        public double[] vector; 

        public VectorR(int size)
        {
            this.size = size;
            this.vector = new double[size];
            for (int i = 0; i < size; i++)
            {
                vector[i] = 0.0;
            }
        }

        public VectorR(double[] vector)
        {
            this.size = vector.Length;
            this.vector = vector;
        }

        #region Make a deep copy
        public VectorR Clone()
        {
            // returns a deep copy of the vector
            VectorR v = new VectorR(vector);
            v.vector = (double[])vector.Clone();
            return v;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion

        #region Equals and Hashing:
        public override bool Equals(object obj)
        {
            return (obj is VectorR) && this.Equals((VectorR)obj);
        }

        public bool Equals(VectorR v)
        {
            return vector == v.vector;
        }

        public override int GetHashCode()
        {
            return vector.GetHashCode();
        }

        public static bool operator ==(VectorR v1, VectorR v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(VectorR v1, VectorR v2)
        {
            return !v1.Equals(v2);
        }
        #endregion

        #region Definition and basics:
        public double this[int n]
        {
            get
            {
                if (n < 0 || n > size)
                {
                    throw new ArgumentOutOfRangeException(
                     "n", n, "n is out of range!");
                }
                return vector[n];
            }
            set { vector[n] = value; }
        }

        public double GetNorm()
        {
            double result = 0.0;
            for (int i = 0; i < size; i++)
            {
                result += vector[i] * vector[i];
            }
            return Math.Sqrt(result);
        }

        public double GetNormSquare()
        {
            double result = 0.0;
            for (int i = 0; i < size; i++)
            {
                result += vector[i] * vector[i];
            }
            return result;
        }

        public void Normalize()
        {
            double norm = GetNorm();
            if (norm == 0)
            {
                throw new DivideByZeroException("Normalize a vector with norm of zero!");
            }
            for (int i = 0; i < size; i++)
            {
                vector[i] /= norm;
            }
        }

        public VectorR GetUnitVector()
        {
            VectorR result = new VectorR(vector);
            result.Normalize();
            return result;
        }

        public int GetSize()
        {
            return size;
        }

        public VectorR GetSwap(int m, int n)
        {
            double temp = vector[m];
            vector[m] = vector[n];
            vector[n] = temp;
            return new VectorR(vector);
        }
        #endregion

        #region Mathematical operators:
        public static VectorR operator +(VectorR v)
        {
            return v;
        }

        public static VectorR operator -(VectorR v)
        {
            double[] result = new double[v.GetSize()];
            for(int i=0;i<v.GetSize();i++)
            {
                result[i] = -v[i];
            }
            return new VectorR(result);
        }

        public static VectorR operator +(VectorR v1, VectorR v2)
        {
            VectorR result = new VectorR(v1.size);
            for (int i = 0; i < v1.size; i++)
            {
                result[i] = v1[i] + v2[i];
            }
            return result;
        }

        public static VectorR operator +(VectorR v, double d)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = v[i] + d;
            }
            return result;
        }

        public static VectorR operator +(double d, VectorR v)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = v[i] + d;
            }
            return result;
        }

        public static VectorR operator -(VectorR v1, VectorR v2)
        {
            VectorR result = new VectorR(v1.size);
            for (int i = 0; i < v1.size; i++)
            {
                result[i] = v1[i] - v2[i];
            }
            return result;
        }

        public static VectorR operator -(VectorR v, double d)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = v[i] - d;
            }
            return result;
        }

        public static VectorR operator -(double d, VectorR v)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = d - v[i];
            }
            return result;
        }

        public static VectorR operator *(VectorR v, double d)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = v[i] * d;
            }
            return result;
        }

        public static VectorR operator *(double d, VectorR v)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = d * v[i];
            }
            return result;
        }

        public static VectorR operator /(VectorR v, double d)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = v[i] / d;
            }
            return result;
        }

        public static VectorR operator /(double d, VectorR v)
        {
            VectorR result = new VectorR(v.size);
            for (int i = 0; i < v.size; i++)
            {
                result[i] = d / v[i];
            }
            return result;
        }
        #endregion;

        #region Public methods:
        public static double DotProduct(VectorR v1, VectorR v2)
        {
            double result = 0.0;
            for (int i = 0; i < v1.size; i++)
            {
                result += v1[i] * v2[i];
            }
            return result;
        }

        public static VectorR CrossProduct(VectorR v1, VectorR v2)
        {
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v1", v1, "Vector v1 must be 3 dimensional!");
            }
            if (v2.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v2", v2, "Vector v2 must be 3 dimensional!");
            }
            VectorR result = new VectorR(3);
            result[0] = v1[1] * v2[2] - v1[2] * v2[1];
            result[1] = v1[2] * v2[0] - v1[0] * v2[2];
            result[2] = v1[0] * v2[1] - v1[1] * v2[0];
            return result;
        }

        public static double TriScalarProduct(VectorR v1, VectorR v2, VectorR v3)
        {
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v1", v1, "Vector v1 must be 3 dimensional!");
            }
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v2", v2, "Vector v2 must be 3 dimensional!");
            }
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v3", v3, "Vector v3 must be 3 dimensional!");
            }
            double result = v1[0] * (v2[1] * v3[2] - v2[2] * v3[1]) +
                            v1[1] * (v2[2] * v3[0] - v2[0] * v3[2]) +
                            v1[2] * (v2[0] * v3[1] - v2[1] * v3[0]);
            return result;
        }

        public static VectorR TriVectorProduct(VectorR v1, VectorR v2, VectorR v3)
        {
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v1", v1, "Vector v1 must be 3 dimensional!");
            }
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v2", v2, "Vector v2 must be 3 dimensional!");
            }
            if (v1.size != 3)
            {
                throw new ArgumentOutOfRangeException(
                 "v3", v3, "Vector v3 must be 3 dimensional!");
            }
            return v2 * VectorR.DotProduct(v1, v3) - v3 * VectorR.DotProduct(v1, v2);
        }

        #endregion;

        public override string ToString()
        {
            string s = "(";
            for (int i = 0; i < size - 1; i++)
            {
                s += vector[i].ToString() + ", ";
            }
            s += vector[size - 1].ToString() + ")";
            return s;
        }

    }
}
