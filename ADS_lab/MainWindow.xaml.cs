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

namespace ADS_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void InsertSortButton_Click(object sender, RoutedEventArgs e)
        {
            int[] array = Array.GenerateRandomArray(10, 100);
            StringBuilder steps = new StringBuilder();

            steps.AppendLine("Несортований масив:");
            steps.AppendLine(string.Join(" ", array));

            Array.InsertionSort(array, steps);

            outputTextBlock.Text = steps.ToString();
        }

        private void ShellSortButton_Click(object sender, RoutedEventArgs e)
        {
            int[] array = Array.GenerateRandomArray(10, 100);
            StringBuilder steps = new StringBuilder();

            steps.AppendLine("Несортований масив:");
            steps.AppendLine(string.Join(" ", array));

            Array.ShellSort(array, steps);

            outputTextBlock.Text = steps.ToString();
        }

        private void QuickSortButton_Click(object sender, RoutedEventArgs e)
        {
            int[] array = Array.GenerateRandomArray(10, 100);
            StringBuilder steps = new StringBuilder();

            steps.AppendLine("Несортований масив:");
            steps.AppendLine(string.Join(" ", array));

            Array.QuickSort(array, 0, array.Length - 1, steps);

            outputTextBlock.Text = steps.ToString();
        }
    }

    public class Array
        {
            public static int[] GenerateRandomArray(int size, int maxValue)
            {
                Random random = new Random();
                int[] array = new int[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = random.Next(1, maxValue);
                }
                return array;
            }

            public static void InsertionSort(int[] array, StringBuilder steps)
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

            public static void ShellSort(int[] array, StringBuilder steps)
            {
                int d = array.Length / 2;
                while (d >= 1)
                {
                    for (int i = d; i < array.Length; i++)
                    {
                        int j = i;
                        while (j >= d && array[j - d] > array[j])
                        {
                            Swap(ref array[j], ref array[j - d]);
                            j = j - d;
                        }
                    }

                    steps.AppendLine($"Крок зі зсувом {d}: {string.Join(" ", array)}");

                    d = d / 2;
                }
            }

            public static void QuickSort(int[] array, int left, int right, StringBuilder steps)
            {
                if (left < right)
                {
                    int pivotIndex = Partition(array, left, right, steps);

                    QuickSort(array, left, pivotIndex - 1, steps);
                    QuickSort(array, pivotIndex + 1, right, steps);
                }
            }

            private static int Partition(int[] array, int left, int right, StringBuilder steps)
            {
                int pivot = array[right];
                int i = left - 1;

                for (int j = left; j < right; j++)
                {
                    if (array[j] < pivot)
                    {
                        i++;
                        Swap(ref array[i], ref array[j]);
                    }
                }

                Swap(ref array[i + 1], ref array[right]);

                steps.AppendLine($"Підмасив після розділення на опорний елемент {pivot}: {string.Join(" ", array)}");

                return i + 1;
            }

            private static void Swap(ref int a, ref int b)
            {
                int temp = a;
                a = b;
                b = temp;
            }
        }
    }
