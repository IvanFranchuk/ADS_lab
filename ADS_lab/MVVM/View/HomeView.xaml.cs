using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADS_lab.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            int[] array = GenerateRandomArray(10, 100);
            StringBuilder steps = new StringBuilder();

            steps.AppendLine("Несортований масив:");
            steps.AppendLine(string.Join(" ", array));

            InsertionSort(array, steps);

            outputTextBlock.Text = steps.ToString();
        }

        // Функція для генерації випадкового масиву чисел
        private int[] GenerateRandomArray(int size, int maxValue)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, maxValue);
            }
            return array;
        }

        // Функція для сортування методом вставки
        private void InsertionSort(int[] array, StringBuilder steps)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int currentElement = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > currentElement)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = currentElement;

                steps.AppendLine($"Крок {i}: {string.Join(" ", array)}");
            }
        }
    }
}
