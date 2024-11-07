using System.Text;
using System.Windows;
using System;
using System.IO;
using System.Linq;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography.X509Certificates;

namespace Postnova_Task_Two
{
  
    public partial class MainWindow : Window
    {
        private List<double> xValues = new List<double>();
        private List<double> yValues = new List<double>();
        private StringBuilder output = new StringBuilder();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string filePath = @"C:\Users\Kamal\Desktop\data.txt";

           
            StringBuilder output = new StringBuilder();
            List<double> yValues = new List<double>();

            try
            {
               
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    
                    if (line.StartsWith("x,y")) continue;

                    
                    var parts = line.Split(',');

                    if (parts.Length == 2 && double.TryParse(parts[0], out double x) && double.TryParse(parts[1], out double y))
                    {
                        
                        output.AppendLine($"x: {x}, y: {y}");

                        
                        yValues.Add(y);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Could not parse line: {line}");
                    }
                }

                


                ResultTextBox.Text = output.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }


        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            string filePath = @"C:\Users\Kamal\Desktop\data.txt";
            StringBuilder output = new StringBuilder();
            List<double> yValues = new List<double>();

            try
            { 
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    
                    if (line.StartsWith("x,y")) continue;

                    var parts = line.Split(',');

                    if (parts.Length == 2 && double.TryParse(parts[0], out double x) && double.TryParse(parts[1], out double y))
                    {
                        double X = x;
                        double Y = y;
                        yValues.Add(Y);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Could not parse line: {line}");
                    }
                }
                //
                if (yValues.Count == 0)
                {
                    ResultTextBox.Text = "No data to calculate statistics.";
                    return;
                }

                
                double mean = yValues.Average();
                double variance = yValues.Sum(y => Math.Pow(y - mean, 2)) / yValues.Count;
                double standardDeviation = Math.Sqrt(variance);

                StringBuilder result = new StringBuilder();
                result.AppendLine($"Mean of y-values: {mean}");
                result.AppendLine($"Standard Deviation of y-values: {standardDeviation}");
                ResultTextBox.Text = result.ToString();
    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                ResultTextBox.Text = "Error reading the file. Please check the file path or contents.";
            }


        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\Users\Kamal\Desktop\data.txt";
            List<double> sigYValues = new List<double>();

            if (string.IsNullOrEmpty(SignificantvalueTxtBox.Text) || !int.TryParse(SignificantvalueTxtBox.Text, out int p))
            {
                MessageBox.Show("Please enter a valid number in the SignificantvalueTxtBox.");
                return;
            }
            List<double> yValues = new List<double>();
            StringBuilder output = new StringBuilder();

            try
            {
               
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    if (line.StartsWith("x,y")) continue;
                    var parts = line.Split(',');

                    if (parts.Length == 2 && double.TryParse(parts[1], out double y))
                    {
                        
                        output.AppendLine($"y: {y}");
                        yValues.Add(y);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Could not parse line: {line}");
                    }
                }

                if (yValues.Count == 0)
                {
                    MessageBox.Show("No data available in the file.");
                    return;
                }

                SignificantValue significant = new SignificantValue();

                foreach (var y in yValues)
                {
                    double sigY = significant.CalculateSignificantFigures(y, p);
                    sigYValues.Add(sigY); 
                }
                StringBuilder resultText = new StringBuilder();
                resultText.AppendLine("Significant figures of y-values:");
                foreach (var sig in sigYValues)
                {
                    resultText.AppendLine(sig.ToString());
                }

                ResultTextBox.Text = resultText.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }


        }
    }
}