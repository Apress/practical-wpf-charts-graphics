using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineCharts
{
    public class DataSeries2Y : DataSeries
    {
        private bool isY2Data = false;

        public bool IsY2Data
        {
            get { return isY2Data; }
            set { isY2Data = value; }
        }
    }
}
