using System;
using System.Windows.Forms;
using ScottPlot;
using Final;

namespace GraphPlotter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FormsPlot formsPlot = new FormsPlot();
            formsPlot.Dock = DockStyle.Fill;
            Controls.Add(formsPlot);

            double[] x = ScottPlot.DataGen.Range(0, 10, 0.1);
            double[] sinY = ScottPlot.DataGen.Sin(x);
            double[] cosY = ScottPlot.DataGen.Cos(x);
            double[] tanY = ScottPlot.DataGen.Tan(x);
            double[] cotY = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                cotY[i] = 1.0 / tanY[i];
            }

            formsPlot.Plot.AddScatter(x, sinY, label: "sin(x)");
            formsPlot.Plot.AddScatter(x, cosY, label: "cos(x)");
            formsPlot.Plot.AddScatter(x, tanY, label: "tan(x)");
            formsPlot.Plot.AddScatter(x, cotY, label: "cot(x)");

            formsPlot.Plot.XLabel("x");
            formsPlot.Plot.YLabel("y");
            formsPlot.Plot.Legend(true);

            formsPlot.Render();
        }
    }
}
