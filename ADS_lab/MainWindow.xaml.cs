using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ADS_lab
{
    /// <summary>
    /// Inter   action logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] generatedIntArray; // Поле для зберігання згенерованого масиву
        private string[] generatedCityArray; // Поле для зберігання згенерованого масиву
        private double[] generatedDoubleArray; // Поле для зберігання згенерованого масиву

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                generatedIntArray = Array.GenerateRandomArray(size, 100);
                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Generated array:");
                steps.AppendLine(string.Join(" ", generatedIntArray));
                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("Enter the correct array size.");
            }
        }

        /*private void GenerateCityButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                Array.CityArrayGenerator cityArrayGenerator = new Array.CityArrayGenerator();
                generatedCityArray = cityArrayGenerator.GenerateCityArray(size);

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Generated city array:");
                steps.AppendLine(string.Join(", ", generatedCityArray));

                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("Enter the correct array size.");
            }
        }*/

        private void GenerateCityButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                Array.CityArrayGenerator cityArrayGenerator = new Array.CityArrayGenerator();
                generatedCityArray = cityArrayGenerator.GenerateCityArray(size);

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Generated city array (before removal):");
                steps.AppendLine(string.Join(", ", generatedCityArray));

                // Застосувати фільтрацію міст за кількістю букв
                generatedCityArray = cityArrayGenerator.RemoveCitiesWithMoreThan8Letters(generatedCityArray);

                steps.AppendLine("Generated city array (after removal):");
                steps.AppendLine(string.Join(", ", generatedCityArray));

                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("Enter the correct array size.");
            }
        }



        private void GenerateDoubleButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                generatedDoubleArray = Array.GenerateDoubleArray(size);

                StringBuilder steps = new StringBuilder();
                steps.AppendLine("Generated Double array:");
                steps.AppendLine(string.Join(", ", generatedDoubleArray));

                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("Enter the correct array size.");
            }
        }


        private void SelectionSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedIntArray != null)
            {
                int[] array1 = Array.ProcessArray(generatedIntArray);
                StringBuilder steps = new StringBuilder();
                steps.AppendLine("An array after the function is executed:");
                steps.AppendLine(string.Join(" ", array1));
                Array.SelectionSortDescending(array1, steps);
                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("First, generate the array.");
            }
        }
        private void InsertSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedIntArray != null)
            {
                int[] array1 = Array.ProcessArray(generatedIntArray);
                StringBuilder steps = new StringBuilder();
                steps.AppendLine("An array after the function is executed:");
                steps.AppendLine(string.Join(" ", array1));
                Array.InsertionSort(array1, steps);
                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("First, generate the array.");
            }
        }

        private void ShellSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedCityArray != null && generatedCityArray.Length > 1)
            {
                StringBuilder steps = new StringBuilder();
                string[] sortedCities = (string[])generatedCityArray.Clone();

                steps.AppendLine("Original city array:");
                steps.AppendLine(string.Join(", ", sortedCities));

                Array.ShellSortCities(sortedCities, steps);

                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("Generate a city array before sorting.");
            }
        }

        private void QuickSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedIntArray != null)
            {
                StringBuilder steps = new StringBuilder();
                Array.QuickSort(generatedIntArray, 0, generatedIntArray.Length - 1, steps);
                outputTextBlock.Text = steps.ToString();
            }
            else
            {
                MessageBox.Show("First, generate the array.");
            }
        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            string input = arrayElementTextBox.Text;

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] elements = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length > 0)
                {
                    if (generatedIntArray != null)
                    {
                        int[] newArray = new int[generatedIntArray.Length + elements.Length];
                        generatedIntArray.CopyTo(newArray, 0);

                        for (int i = 0; i < elements.Length; i++)
                        {
                            if (int.TryParse(elements[i], out int element))
                            {
                                newArray[generatedIntArray.Length + i] = element;
                            }
                        }

                        generatedIntArray = newArray;

                        StringBuilder steps = new StringBuilder();
                        steps.AppendLine("Generated array:");
                        steps.AppendLine(string.Join(" ", generatedIntArray));
                        outputTextBlock.Text = steps.ToString();
                    }
                    else
                    {
                        MessageBox.Show("First, generate the array.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to identify items. Check the input format.");
                }
            }
            else
            {
                MessageBox.Show("Enter the items.");
            }
        }

        private void AddCityButton_Click(object sender, RoutedEventArgs e)
        { }
        private void AddDoubleButton_Click(object sender, RoutedEventArgs e)
        { }


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

        public class CityArrayGenerator
        {
            private List<string> cities;

            public CityArrayGenerator()
            {
                cities = new List<string>
                {
                    "Kyiv", "Lviv", "Odessa", "Kharkiv", "Dnipro",
                    "Kherson", "Zaporizhzhia", "Poltava", "Vinnytsia", "Chernivtsi",
                    "Rivne", "Ternopil", "Ivano-Frankivsk", "Lutsk", "Uzhhorod",
                    "Mykolaiv", "Kropyvnytskyi", "Sumy", "Cherkasy", "Chernihiv",
                    "Khmelnytskyi", "Zhytomyr", "Mukachevo", "Kamianets-Podilskyi", "Kremenchuk",
                    "Yalta", "Simferopol", "Sevastopol", "Donetsk", "Luhansk",
                    "Lviv", "Minsk", "Vilnius", "Riga", "Tallinn",
                    "Berlin", "Paris", "London", "Amsterdam", "Madrid",
                    "New York", "Los Angeles", "Chicago", "Houston", "Philadelphia",
                    "San Francisco", "Miami", "Las Vegas", "Boston", "Washington D.C.",
                    "Toronto", "Vancouver", "Montreal", "Sydney", "Melbourne",
                    "Tokyo", "Beijing", "Shanghai", "Seoul", "Bangkok"

            };
            }

            public string[] GenerateCityArray(int size)
            {
                Random random = new Random();
                string[] cityArray = new string[size];

                for (int i = 0; i < size; i++)
                {
                    int randomIndex = random.Next(0, cities.Count);
                    cityArray[i] = cities[randomIndex];
                }

                return cityArray;
            }

            public string[] RemoveCitiesWithMoreThan8Letters(string[] cityArray)
            {
                List<string> filteredCities = new List<string>();

                foreach (string city in cityArray)
                {
                    if (city.Length <= 8)
                    {
                        filteredCities.Add(city);
                    }
                }

                return filteredCities.ToArray();
            }

        }

        public static double[] GenerateDoubleArray(int size)
        {
            Random random = new Random();
            double[] doubleArray = new double[size];

            for (int i = 0; i < size; i++)
            {
                doubleArray[i] = random.NextDouble() * 100.0; // Generates random doubles between 0 and 100
            }

            return doubleArray;
        }

        public static void SelectionSortDescending(int[] array, StringBuilder steps)
        {
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int maxIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] > array[maxIndex])
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    Swap(ref array[i], ref array[maxIndex]);
                }

                steps.AppendLine($"Step {i + 1}: {string.Join(" ", array)}");
            }
        }

        public static void ShellSortCities(string[] cityArray, StringBuilder steps)
        {
            int n = cityArray.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    string temp = cityArray[i];
                    int j = i;

                    while (j >= gap && string.Compare(cityArray[j - gap], temp) > 0)
                    {
                        cityArray[j] = cityArray[j - gap];
                        j -= gap;
                    }

                    cityArray[j] = temp;
                }

                steps.AppendLine($"Step with gap {gap}: {string.Join(", ", cityArray)}");
                gap /= 2;
            }
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

                steps.AppendLine($"Step {i}: {string.Join(" ", array)}");
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
        public static int[] ProcessArray(int[] array)
        {
            return array.Where(item => item % 3 != 0).Select(item => Function(item)).ToArray();
        }

        private static int Function(int x)
        {
            return x * x;
        }
    }
}


