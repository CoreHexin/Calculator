using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        double result;
        Operator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            resultLabel.Content = "0";

            acBtn.Click += AcBtn_Click;
            negBtn.Click += NegBtn_Click;
            percentageBtn.Click += PercentageBtn_Click;
            calcBtn.Click += CalcBtn_Click;
        }

        private void CalcBtn_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case Operator.Addition:
                        result = lastNumber + newNumber;
                        break;
                    case Operator.Substraction:
                        result = lastNumber - newNumber;
                        break;
                    case Operator.Division:
                        if (newNumber == 0)
                        {
                            MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                        result = lastNumber / newNumber;
                        break;
                    case Operator.Multiplication:
                        result = lastNumber * newNumber;
                        break;
                }
                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void OperationBtn_Click(Object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == addBtn)
                selectedOperator = Operator.Addition;
            else if (sender == subBtn)
                selectedOperator = Operator.Substraction;
            else if (sender == mulBtn)
                selectedOperator = Operator.Multiplication;
            else
                selectedOperator = Operator.Division;
        }

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string selectedValue = btn.Content.ToString()!;

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue;
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        private void NegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcBtn_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void pointBtn_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString()!.Contains(".") == false)
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }
    }

    public enum Operator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }
}