using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Transformation3D
{
    /// <summary>
    /// Interaction logic for Matrix3DOperation.xaml
    /// </summary>

    public partial class Matrix3DOperation : System.Windows.Window
    {

        public Matrix3DOperation()
        {
            InitializeComponent();

            Matrix3D M =  new Matrix3D(1, 2, 3, 4,
                                       2, 1, 0, 0,
                                       0, 0, 1, 0,
                                       1, 2, 3, 1);
            Matrix3D M1 = M;
            Matrix3D M2 = new Matrix3D(1, 2, 0, 0,
                                       2, 1, 0, 3,
                                       0, 0, 1, 2,
                                       1, 2, 3, 1);

            tbOriginal.Text = "M = (" + M.ToString() + ")";
            tbOriginal1.Text = "M1 = M";

            // Invert matrix:
            M.Invert();
            tbInvert.Text = "(" + Utility.Matrix3DRound(M, 3).ToString() + ")";
            tbInvert1.Text = "(" + (M1 * M).ToString() + ")";

            // Matrix multiplication:
            Matrix3D M12 = M1 * M2;
            Matrix3D M21 = M2 * M1;
            tbM1.Text = "M1 = (" + M1.ToString() + ")";
            tbM2.Text = "M2 = (" + M2.ToString() + ")";
            tbM12.Text = "(" + M12.ToString() + ")";
            tbM21.Text = "(" + M21.ToString() + ")";
        }
    }
}