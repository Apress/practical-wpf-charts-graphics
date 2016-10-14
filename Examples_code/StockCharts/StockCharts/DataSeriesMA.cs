using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StockCharts
{
    public class DataSeriesMA : DataSeries
    {
        private int nDays = 5;
        public int NDays
        {
            get { return nDays; }
            set { nDays = value; }
        }

        // Simple Moving Average:
        private Polyline smaLineSeries = new Polyline();
        private Brush smaLineColor = Brushes.Black;
        private double smaLineThickness = 1;

        public Polyline SMALineSeries
        {
            get { return smaLineSeries; }
            set { smaLineSeries = value; }
        }

        public Brush SMALineColor
        {
            get { return smaLineColor; }
            set { smaLineColor = value; }
        }
        
        public double SMALineThickness
        {
            get { return smaLineThickness; }
            set { smaLineThickness = value; }
        }

        public double[] SimpleMovingAverage()
        {
            int m = DataString.GetLength(1);
            double[] data = new double[m];
            for (int i = 0; i < m; i++)
                data[i] = Convert.ToDouble(DataString[1, i]);

            double[] sma = new double[m - NDays + 1];
            if (m > NDays)
            {
                double sum = 0.0;
                for (int i = 0; i < NDays; i++)
                {
                    sum += data[i];
                }

                sma[0] = sum / NDays;

                for (int i = 1; i <= m - NDays; i++)
                {
                    sma[i] = sma[i - 1] + (data[NDays + i - 1] - data[i - 1]) / NDays;
                }
            }
            return sma;
        }

        // Weighted Moving Average:
        private Polyline wmaLineSeries = new Polyline();
        private Brush wmaLineColor = Brushes.Black;
        private double wmaLineThickness = 1;

        public Polyline WMALineSeries
        {
            get { return wmaLineSeries; }
            set { wmaLineSeries = value; }
        }

        public Brush WMALineColor
        {
            get { return wmaLineColor; }
            set { wmaLineColor = value; }
        }

        public double WMALineThickness
        {
            get { return wmaLineThickness; }
            set { wmaLineThickness = value; }
        }

        public double[] WeightedMovingAverage()
        {
            int m = DataString.GetLength(1);
            double[] data = new double[m];
            for (int i = 0; i < m; i++)
                data[i] = Convert.ToDouble(DataString[1, i]);

            double[] wma = new double[m - NDays + 1];
            double psum = 0.0;
            double numerator = 0.0;
            double[] numerator1 = new double[m - NDays + 1];
            double[] psum1 = new double[m - NDays + 1];

            if (m > NDays)
            {
                for (int i = 0; i < NDays; i++)
                {
                    psum += data[i];
                    numerator += (i + 1) * data[i];
                }
                psum1[0] = psum;
                numerator1[0] = numerator;
                wma[0] = 2 * numerator / NDays / (NDays + 1);

                for (int i = 1; i <= m - NDays; i++)
                {
                    numerator1[i] =
                        numerator1[i - 1] + NDays * data[i + NDays - 1] - psum1[i - 1];
                    psum1[i] = psum1[i - 1] + data[i + NDays - 1] - data[i - 1];
                    wma[i] = 2 * numerator1[i] / NDays / (NDays + 1);
                }
            }
            return wma;
        }



        // Exponential Moving Average:
        private Polyline emaLineSeries = new Polyline();
        private Brush emaLineColor = Brushes.Black;
        private double emaLineThickness = 1;

        public Polyline EMALineSeries
        {
            get { return emaLineSeries; }
            set { emaLineSeries = value; }
        }

        public Brush EMALineColor
        {
            get { return emaLineColor; }
            set { emaLineColor = value; }
        }

        public double EMALineThickness
        {
            get { return emaLineThickness; }
            set { emaLineThickness = value; }
        }

        public double[] ExponentialMovingAverage()
        {
            int m = DataString.GetLength(1);
            double[] data = new double[m];
            for (int i = 0; i < m; i++)
                data[i] = Convert.ToDouble(DataString[1, i]);

            double[] ema = new double[m - NDays + 1];
            double psum = 0.0;
            double alpha = 2.0 / NDays;

            if (m > NDays)
            {
                for (int i = 0; i < NDays; i++)
                {
                    psum += data[i];
                }
                ema[0] = psum / NDays + alpha * (data[NDays - 1] - psum / NDays);

                for (int i = 1; i <= m - NDays; i++)
                {
                    ema[i] = ema[i - 1] + alpha * (data[i + NDays - 1] - ema[i - 1]);
                }
            }
            return ema;
        }

    }
}
