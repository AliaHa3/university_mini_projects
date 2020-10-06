using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using MathParserTK;
using ZedGraph;

namespace Numerical_analysis
{
    public partial class Form1 : Form
    {
        private List<double> _xAxis = new List<double>();
        private List<double> _yAxis = new List<double>();
        private bool calculate = false;
        MathParser parser = new MathParser();
       
        public Form1()
        {
            InitializeComponent();
           
        }


        private bool inegral_InformationReady()   //check is all the data is ready on form
        {
            bool temp;
            
            temp= (!((string.IsNullOrEmpty(Function.Text))
                    || (string.IsNullOrEmpty(a.Text))
                    || (string.IsNullOrEmpty(b.Text))
                    || (string.IsNullOrEmpty(Method_tpye.Text))
                    || (string.IsNullOrEmpty(hORn.Text))));
            if (radioButton1.Enabled && radioButton2.Enabled)
            {
                return (temp && (radioButton1.Checked || radioButton2.Checked));
            }
            else
                return temp;

        }

        private bool non_linear_InformationReady()          //check is all the data is ready on form
        {
            return !((string.IsNullOrEmpty(a_non_liner.Text))
                     || (string.IsNullOrEmpty(b_non_liner.Text))
                     || (string.IsNullOrEmpty(func_non_liner.Text)) ) ;

        }

        private bool differentially_InformationReady()          //check is all the data is ready on form
        {
            return !((string.IsNullOrEmpty(a_dif.Text))
                     || (string.IsNullOrEmpty(b_dif.Text))
                     || (string.IsNullOrEmpty(x0_dif.Text))
                     || (string.IsNullOrEmpty(y0_dif.Text))
                     || (string.IsNullOrEmpty(h_dif.Text))
                     || (string.IsNullOrEmpty(dy_dif.Text)));


        }
        private bool interpaltion_InformationReady()        //check is all the data is ready on form
        {
            return (_xAxis.Count != 0) && (_yAxis.Count != 0);

        }

        private void clear_dif()        //clear all the data on the tab to input new one
        {
            a_dif.Clear();
            b_dif.Clear();
            x0_dif.Clear();
            y0_dif.Clear();
            dy_dif.Clear();
            h_dif.Clear();
            listBox1.Items.Clear();

        }
        private void clear_non_liner()      //clear all the data on the tab to input new one
        {
            a_non_liner.Clear();
            b_non_liner.Clear();
            epsilon_non_liner.Clear();
            func_non_liner.Clear();
            root_non_liner.Clear();
           
        }

        private void clear_interpolation()      //clear all the data on the tab to input new one
        {
            xvalue.Clear();
            yvalue.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox4.Clear();
        }
        private void clear_integeral()      //clear all the data on the tab to input new one
        {
            Function.Clear();
            a.Clear();
            b.Clear();
            hORn.Clear();
            Result.Clear();
        }
       private void Form1_Load(object sender, EventArgs e)
        {
            
        }
       private void CreateGraph_integral(ZedGraphControl zgc)
       {
           GraphPane myPane = zgc.GraphPane;
           myPane.CurveList.Clear();
           // Set the titles and axis labels
           myPane.Title.Text = "Graph";
           myPane.XAxis.Title.Text = "X Axis";
           myPane.YAxis.Title.Text = "Y Axis";
           PointPairList list = new PointPairList();
           double Precision;
           switch (Method_tpye.SelectedIndex)
           {
               case 0:
                   {
                       
                       for (int i = 0; i < Integral.NumSubdivisions; i++)
                       {
                           PointPairList list1 = new PointPairList();
                           list1.Add(Integral.XAxis[i], Integral.YAxis[i]);
                           list1.Add(Integral.XAxis[i], 0);
                           myPane.AddCurve("", list1, Color.Blue, SymbolType.None);
                       }
                       for (int i = 0; i < Integral.NumSubdivisions; i++)
                       {
                           PointPairList list1 = new PointPairList();
                           list1.Add(Integral.XAxis[i], Integral.YAxis[i]);
                           list1.Add(Integral.XAxis[i + 1], Integral.YAxis[i]);
                           myPane.AddCurve("", list1, Color.Blue, SymbolType.None);
                       }

                       for (int i = 0; i < Integral.NumSubdivisions; i++)
                       {
                           PointPairList list1 = new PointPairList();
                           list1.Add(Integral.XAxis[i + 1], Integral.YAxis[i]);
                           list1.Add(Integral.XAxis[i + 1], 0);
                           myPane.AddCurve("", list1, Color.Blue, SymbolType.None);
                       }
                       Precision = Integral.End - Integral.Start;
                       Precision /= myPane.Rect.Size.Width;
                       for (double i = Integral.Start; i < Integral.End; i += Precision)
                       {
                           string temp = Integral.FunctionExp.Replace("x", i.ToString());
                           double y = parser.Parse(temp, false);
                           list.Add(i, y);
                       }
                       LineItem myLine1 = new LineItem("التابع", list, Color.Red, SymbolType.None);
                       myLine1.Line.IsSmooth = true;
                       myLine1.Line.SmoothTension = Convert.ToSingle(Precision);
                       myPane.CurveList.Add(myLine1);
                       myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 450F);
                   }

                   break;
               case 1:
                   for (int i = 0; i < Integral.NumSubdivisions + 1; i++)
                   {
                       PointPairList list1 = new PointPairList();
                       list1.Add(Integral.XAxis[i], Integral.YAxis[i]);
                       list1.Add(Integral.XAxis[i], 0);
                       myPane.AddCurve("", list1, Color.Blue, SymbolType.Default);
                   }
                   for (int i = 0; i < Integral.NumSubdivisions; i++)
                   {
                       PointPairList list1 = new PointPairList();
                       list1.Add(Integral.XAxis[i], Integral.YAxis[i]);
                       list1.Add(Integral.XAxis[i + 1], Integral.YAxis[i + 1]);
                       myPane.AddCurve("", list1, Color.Blue, SymbolType.None);
                   }
                   Precision = Integral.End - Integral.Start;
                   Precision /= myPane.Rect.Size.Width;
                   for (double i = Integral.Start; i < Integral.End; i += Precision)
                   {
                       string temp = Integral.FunctionExp.Replace("x", i.ToString());
                       double y = parser.Parse(temp, false);
                       list.Add(i, y);
                   }
                   LineItem myLine2 = new LineItem("التابع", list, Color.Red, SymbolType.None);
                   myLine2.Line.IsSmooth = true;
                   myLine2.Line.SmoothTension = Convert.ToSingle(Precision);
                   myPane.CurveList.Add(myLine2);
                   myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 450F);
                   break;
               case 2:
                   for (int i = 0; i < Integral.NumSubdivisions + 1; i++)
                   {
                       PointPairList list1 = new PointPairList();
                       list1.Add(Integral.XAxis[i], Integral.YAxis[i]);
                       list1.Add(Integral.XAxis[i], 0);
                       myPane.AddCurve("", list1, Color.Blue, SymbolType.None);
                   }
                    Precision = Integral.End - Integral.Start;
                   Precision /= myPane.Rect.Size.Width;
                   for (double i = Integral.Start; i < Integral.End; i += Precision)
                   {
                       string temp = Integral.FunctionExp.Replace("x", i.ToString());
                       double y = parser.Parse(temp, false);
                       list.Add(i, y);
                   }
                   LineItem myLine3 = new LineItem("التابع", list, Color.Red, SymbolType.None);
                   myLine3.Line.IsSmooth = true;
                   myLine3.Line.SmoothTension = Convert.ToSingle(Precision);
                   myPane.CurveList.Add(myLine3);
                   myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 450F);
                   break;

           }
           // Calculate the Axis Scale Ranges
           zgc.AxisChange();
       }
        private void CreateGraph_interpaltion(ZedGraphControl zgc)
        {
                GraphPane myPane = zgc.GraphPane;
                myPane.CurveList.Clear();
                myPane.Title.Text = "Graph";
                myPane.XAxis.Title.Text = "X Axis";
                myPane.YAxis.Title.Text = "Y Axis";
                var list = new PointPairList();
            switch (comboBox1.SelectedIndex)
            {
                 
                case 0:
                case 1:
                    {
                        double Precision = Interpolation.Xaxis.Max() - Interpolation.Xaxis.Min();
                        Precision /= myPane.Rect.Size.Width;
                        for (double i = Interpolation.Xaxis.Min(); i < Interpolation.Xaxis.Max(); i += Precision)
                        {
                            double y;
                            y = Interpolation.poly.Evaluate(i);
                            list.Add(i, y);
                        }
                        LineItem myLine1 = new LineItem("كثير الحدود", list, Color.Red, SymbolType.None);
                        myLine1.Line.IsSmooth = true;
                        float f = Convert.ToSingle(Precision);
                        myLine1.Line.SmoothTension = f;
                        myPane.CurveList.Add(myLine1);
                        myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
                        myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
                        break;
                    }

                case 2:
                    {
                        for (int i = 0; i < Interpolation.NumPoint; i++)
                            list.Add(Interpolation.Xaxis[i],Interpolation.Yaxis[i]);
                        LineItem myLine1 = new LineItem("كثير الحدود", list, Color.Red, SymbolType.Circle);
                        myPane.CurveList.Add(myLine1);
                        myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
                        myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
                        break;
                    }
            }
                zgc.AxisChange();
            
        }
        private void SetSize()
        {
            zg1.Location = new Point(10, 10);
            // Leave a small margin around the outside of the control
        }
        private void tabPage2_Resize(object sender, EventArgs e)
        {
            SetSize();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

            double res;
            
            if (inegral_InformationReady())
            {
                if (radioButton1.Checked)
                {
                    Integral.SetH(Convert.ToDouble(a.Text), Convert.ToDouble(b.Text), Convert.ToInt32(hORn.Text));
                }
                else
                {
                    if (radioButton2.Checked)
                    {

                        Integral.SetN(Convert.ToDouble(a.Text), Convert.ToDouble(b.Text), Convert.ToDouble(hORn.Text));
                    }
                }
                switch (Method_tpye.SelectedIndex)
                {

                    case 0:
                        res = Integral.RegtanglesMethod(Function.Text);
                        Result.Text = res.ToString();
                        break;
                    case 1:
                        res = Integral.TrapezoidMethod(Function.Text);
                        Result.Text = res.ToString();
                        break;
                    case 2:
                        if ((Convert.ToInt32(hORn.Text) % 2 != 0))
                        {
                            MessageBox.Show("Simpson require even number of subdivisions\nwe will divide integral into two integral", "Warning");
                            var temp1 = (Convert.ToDouble(b.Text) - Convert.ToDouble(a.Text))/
                                           Convert.ToDouble(hORn.Text);
                            double temp2 = Convert.ToDouble(b.Text) - temp1;
                            Integral.SetH(Convert.ToDouble(a.Text), temp2, Convert.ToInt32(hORn.Text) - 1);
                            res = Integral.SimpsonMethod(Function.Text);
                            string temp = Function.Text.Replace("x",temp2.ToString());
                            temp2 = parser.Parse(temp, false);
                            temp = Function.Text.Replace("x",b.Text);
                            temp2 += parser.Parse(temp, false);
                            res += (temp1*temp2)/2;

                            Result.Text = res.ToString();
                        }
                        else
                        {
                            Integral.SetH(Convert.ToDouble(a.Text), Convert.ToDouble(b.Text), Convert.ToInt32(hORn.Text));
                            res = Integral.SimpsonMethod(Function.Text);
                            Result.Text = res.ToString();
                        }
                        
                        break;

                }
            }
            else
            {
                MessageBox.Show("Please Complate Integration Info", "Error");
            }
        }
        private void Method_tpye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Method_tpye.SelectedIndex==2)
            {
                label3.Text = "N";
                radioButton2.Enabled = false;
                radioButton1.Enabled = false;
            }
            else
            {
                label3.Text = " ";
                radioButton2.Enabled = true;
                radioButton1.Enabled = true;
            }
        
            
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "N";
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label3.Text = "H";
        }
        private void savepoint_Click(object sender, EventArgs e)
        {

            if (calculate)
            {
                calculate = false;
                MessageBox.Show("We will delete previous data", "warning");
                comboBox1.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                _xAxis.Clear();
                _yAxis.Clear();

            }
            if ((!string.IsNullOrEmpty(xvalue.Text) && (!string.IsNullOrEmpty(yvalue.Text))))
            {
                _xAxis.Add((Convert.ToDouble(xvalue.Text)));
                _yAxis.Add((Convert.ToDouble(yvalue.Text)));
                pointnum.Text = "Number of point " + (_xAxis.Count);
                xvalue.Clear();
                yvalue.Clear();
                
            }
            else
            {
                MessageBox.Show("Please make sure to enter point (X,Y)", "warning");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (interpaltion_InformationReady())
            {
                calculate = true;
                Interpolation.Setvalue(_xAxis.Count, _xAxis.ToArray(), _yAxis.ToArray());
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            if (!string.IsNullOrEmpty(textBox3.Text))
                            {
                                var temp = Interpolation.CalculateLagrangePoint(Convert.ToDouble(textBox3.Text));     
                                textBox5.Text = temp.ToString();
                            }
                            else
                            {
                                var temp = Interpolation.CalculateLagrangePolynomial();
                                textBox4.Text = temp;
                            }
                            break;
                        }
                    case 1:
                        {
                            double h = _xAxis[1] - _xAxis[0];
                            for (int i = 0; i < _xAxis.Count - 1; i++)
                            {
                                if ((_xAxis[i + 1] - _xAxis[i]) - h > 0.0000001)
                                {
                                    MessageBox.Show("Newton need a constent step\n we will use lagrange method", "warning");
                                    goto case 0;

                                }
                            }
                            if (!string.IsNullOrEmpty(textBox3.Text))
                            {
                                var temp = Interpolation.CalculateNewtonPoint(Convert.ToDouble(textBox3.Text));
                                textBox5.Text = temp.ToString();
                            }
                            else
                            {
                                var temp = Interpolation.CalculateNewtonPolynomial();
                                textBox4.Text = temp;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (!string.IsNullOrEmpty(textBox3.Text))
                            {
                                var temp = Interpolation.CalculteSplinePoint(Convert.ToDouble(textBox3.Text));
                                textBox5.Text = temp.ToString();

                            }
                            else
                            {
                                MessageBox.Show("Enter Point Calculate polynomial", "warning");
                            }
                            break;
                        }
                }
            }

        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (interpaltion_InformationReady())
            {
                tabControl1.SelectedTab = tabPage2;
                CreateGraph_interpaltion(zg1);
            }
            else
            {
                MessageBox.Show("You can't draw,Plese insert points first", "warning");
            }
        }
       public void button2_Click(object sender, EventArgs e) //exit the program
        {
           
            Close();
          

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (differentially_InformationReady())
            {
                fild_dif.Text = "[ " + a_dif.Text + " , " + b_dif.Text + " ]"; //display the field on form to label

                //make new sloution
                Differentially solution = new Differentially(double.Parse(h_dif.Text),double.Parse(x0_dif.Text),double.Parse(y0_dif.Text),double.Parse(a_dif.Text),double.Parse(b_dif.Text),dy_dif.Text);
                
                //make array to save the solutions
                double[,] solve = new double[solution.n+1, 2];
              
                switch (method_dif.SelectedIndex)
                {
                    case 0: //rangcuta
                        {
                             solution.calculate_RongCuta(solve);
                            
                            
                            break;
                        }
                    case 1://uler
                        {
                            
                            solution.calculate_uler(solve);
                          

                            break;
                        }
                }

                listBox1.Items.Clear();
                for (int i = 0; i < solution.n + 1; i++) //print solutions
                {
                  
                    listBox1.Items.Add(solve[i, 0].ToString() + "\t" + solve[i, 1].ToString() + Environment.NewLine);
                }                
            }
            else
            {
               MessageBox.Show("You can't calculate,Plese insert everything first", "warning");
           }

        }
        

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void a_non_liner_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
           
            

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }

        private void calculate_non_liner_Click(object sender, EventArgs e)
        {
            if (non_linear_InformationReady())
            {
                
                Non_linear solution;
              
                if (string.IsNullOrEmpty(epsilon_non_liner.Text)) //if the custmor did not input epsilon

                {
                      solution = new Non_linear(double.Parse(a_non_liner.Text), double.Parse(b_non_liner.Text),
                                                         func_non_liner.Text);  
                }
             
            else //the custmor input epsilon
                {
                    solution = new Non_linear(double.Parse(a_non_liner.Text), double.Parse(b_non_liner.Text),
                                                         double.Parse(epsilon_non_liner.Text), func_non_liner.Text);
                }
                //print the solution
                root_non_liner.Text = solution.find_bisection().ToString();
            }
            else
            {
                MessageBox.Show("You can't calculate,Plese insert everything first", "warning");

            }
        }

        private void root_non_liner_TextChanged(object sender, EventArgs e)
        {

        }

        private void method_dif_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fild_dif_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)    
        {
            clear_non_liner();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clear_dif();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Integral.XAxis.Length != 0 && Integral.YAxis.Length != 0)
            {
                tabControl1.SelectedTab = tabPage2;
                CreateGraph_integral(zg1);
            }
            else
            {
                MessageBox.Show("You can't draw,Plese insert points first", "warning");
            }
        }
    }
}