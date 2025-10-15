using System;
using System.Windows;
using System.Windows.Controls;

namespace TriangleChecker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем ввод на корректность
            if (!TryParseSide(txtSideA.Text, out double a, "A") ||
                !TryParseSide(txtSideB.Text, out double b, "B") ||
                !TryParseSide(txtSideC.Text, out double c, "C"))
            {
                return;
            }

            // Проверяем, могут ли стороны образовать треугольник
            string triangleValidation = ValidateTriangle(a, b, c);
            if (triangleValidation != "VALID")
            {
                txtResult.Text = triangleValidation;
                return;
            }

            // Определяем тип треугольника
            string triangleType = DetermineTriangleType(a, b, c);
            txtResult.Text = $"Результат: {triangleType}";
        }

        private bool TryParseSide(string input, out double side, string sideName)
        {
            side = 0;

            if (string.IsNullOrWhiteSpace(input))
            {
                txtResult.Text = $"Ошибка: сторона {sideName} не может быть пустой!";
                return false;
            }

            if (!double.TryParse(input, out side))
            {
                txtResult.Text = $"Ошибка: сторона {sideName} должна быть числом!";
                return false;
            }

            if (Math.Abs(side) < 0.0001)
            {
                txtResult.Text = $"Ошибка: сторона {sideName} не может быть равна нулю!";
                return false;
            }

            if (side < 0)
            {
                txtResult.Text = $"Ошибка: сторона {sideName} не может быть отрицательной!";
                return false;
            }

            return true;


        }

        private string ValidateTriangle(double a, double b, double c)
        {
            if (a + b <= c)
            {
                return $"Ошибка: сумма сторон A и B ({a + b}) меньше или равна стороне C ({c})!";
            }
            if (a + c <= b)
            {
                return $"Ошибка: сумма сторон A и C ({a + c}) меньше или равна стороне B ({b})!";
            }
            if (b + c <= a)
            {
                return $"Ошибка: сумма сторон B и C ({b + c}) меньше или равна стороне A ({a})!";
            }
            return "VALID";
        }

        private string DetermineTriangleType(double a, double b, double c)
        {
            // Проверка на равносторонний треугольник
            if (Math.Abs(a - b) < 0.0001 && Math.Abs(b - c) < 0.0001)
            {
                return "равносторонний треугольник";
            }

            // Проверка на равнобедренный треугольник
            if (Math.Abs(a - b) < 0.0001 || Math.Abs(a - c) < 0.0001 || Math.Abs(b - c) < 0.0001)
            {
                return "равнобедренный треугольник";
            }

            // Проверка на прямоугольный треугольник
            if (IsRightTriangle(a, b, c))
            {
                return "прямоугольный треугольник";
            }

            // Во всех остальных случаях - разносторонний треугольник
            return "разносторонний треугольник";
        }

        private bool IsRightTriangle(double a, double b, double c)
        {
            // Квадраты сторон
            double a2 = a * a;
            double b2 = b * b;
            double c2 = c * c;

            // Проверяем теорему Пифагора для всех комбинаций
            return Math.Abs(a2 + b2 - c2) < 0.0001 ||
                   Math.Abs(a2 + c2 - b2) < 0.0001 ||
                   Math.Abs(b2 + c2 - a2) < 0.0001;
        }
    }
}
