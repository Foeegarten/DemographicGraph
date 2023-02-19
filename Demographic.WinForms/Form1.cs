using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demographic.FileOperations;
using ZedGraph;
namespace Demographic.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.YAxis.Title.Text = "Number (million)";
            pane.XAxis.Title.Text = "Year";
            pane.Title.Text = "Demographic chart";

            GraphPane pane2 = zedGraphControl2.GraphPane;
            pane2.YAxis.Title.Text = "Number (million)";
            pane2.XAxis.Title.Text = "Year";
            pane2.Title.Text = "Demographic chart";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenFileInitialAge_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                FileManager.Instance.FileName1 = openFileDialog1.FileName;
               
            }
        }

        private void btnOpenFileDeathRules_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                FileManager.Instance.FileName2 = openFileDialog1.FileName;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileManager.Instance.FileName1 =textBox1.Text;
            FileManager.Instance.FileName2 = textBox2.Text;
            Engine engine = new Engine(int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text));
            engine.Cycle();
            
            GenPopulation(engine.dictPopulation,zedGraphControl1,Color.Blue,"People");
            GenPopulation(engine.dictMen, zedGraphControl2,Color.Red,"Men");
            GenPopulation(engine.dictWomen, zedGraphControl2, Color.DarkBlue,"Women");
        }
        private void GenPopulation(Dictionary<int,int> population, ZedGraphControl control,Color color,string title)
        {
            GraphPane pane = control.GraphPane;
            
           // pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            foreach (var item in population)
            {
                list.Add(item.Key, item.Value);
            }
            LineItem myCurve = pane.AddCurve(title, list, color, SymbolType.None);

            control.AxisChange();
            control.Invalidate();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
