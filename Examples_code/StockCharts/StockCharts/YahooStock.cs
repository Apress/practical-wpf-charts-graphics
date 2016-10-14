using System;
using System.ComponentModel;
using System.Windows.Threading;
using System.Net;
using System.Windows;

namespace StockCharts
{
    public class YahooStock
    {
        private static string chartURI = @"http://ichart.yahoo.com/z?z=m&a=vm";
        private string symbol = "IBM";
        private string chartType = "l";
        private string logScale = "off";
        private string movingAverage = "m50";
        private string stockPeriod = "6m";
        private int updateIntervalInSeconds = 12;

        public int UpdateIntervalInSeconds
        {
            get { return updateIntervalInSeconds; }
            set { updateIntervalInSeconds = value; }
        }

        public string ChartURI
        {
            get
            {
                return chartURI + "&s=" + this.Symbol + "&q=" + this.ChartType + "&l=" + this.LogScale +
                                  "&p=" + this.MovingAverage + "&t=" + this.StockPeriod;
            }
        }

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }
        public string StockPeriod
        {
            get { return stockPeriod; }
            set { stockPeriod = value; }
        }
        
        public string MovingAverage
        {
            get { return movingAverage; }
            set { movingAverage = value; }
        }

        public string ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }

        public string LogScale
        {
            get { return logScale; }
            set { logScale = value; }
        }

        public bool CheckInternetConnection()
        {
            try
            {
                System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient("www.microsoft.com", 80);
                clnt.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
