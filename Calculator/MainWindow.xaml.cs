using System.Windows;
using System.Windows.Controls;

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
            if (double.TryParse(resultLabel.Content.ToString(), out double curNumber))
            {
                curNumber = curNumber * -1;
                resultLabel.Content = curNumber.ToString();
            }
        }

        private void PercentageBtn_Click(object sender, RoutedEventArgs e)
        {
            // 需要注意计算器中百分数计算规则, 50 + 5% = 52.5 (50 + 50*0.05 = 52.5)
            if (double.TryParse(resultLabel.Content.ToString(), out double curNumber))
            {
                curNumber = curNumber / 100;
                if (lastNumber != 0)
                {
                    curNumber *= lastNumber;
                }
                resultLabel.Content = curNumber.ToString();
            }
        }

        private void AcBtn_Click(object sender, RoutedEventArgs e)
        {
            lastNumber = 0;
            result = 0;
            resultLabel.Content = "0";
        }

        private void PointBtn_Click(object sender, RoutedEventArgs e)
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