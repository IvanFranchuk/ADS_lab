using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace ADS_lab
{
    public partial class MainWindow : Window
    {
        private int[] generatedIntArray; // Поле для зберігання згенерованого масиву
        private string[] generatedCityArray; // Поле для зберігання згенерованого масиву
        private double[][] generated2DDoubleArray; // Поле для зберігання згенерованого масиву
        private double[] generatedDoubleArray; // Поле для зберігання згенерованого масиву

        private int comparisonsCount = 0; // Змінна для підрахунку кількості порівнянь
        private int swapsCount = 0;      // Змінна для підрахунку кількості перестановок
        private Stopwatch sortingStopwatch = new Stopwatch();

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

        //////////////////////////////////////////////////////////////////////////////
        private void GenerateDouble2DButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                generated2DDoubleArray = Array.GenerateDouble2DArray(size);
                StringBuilder output = new StringBuilder();
                output.AppendLine("Generated 2D double array:");
                for (int i = 0; i < size; i++)
                {
                    output.AppendLine(string.Join(" ", generated2DDoubleArray[i].Select(d => d.ToString("F2"))));
                }
                inputArrayTextBlock.Text = output.ToString();
            }
            else
            {
                MessageBox.Show("Enter a valid array size.");
            }
        }

        private void GenerateDoubleButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(arraySizeTextBox.Text, out int size))
            {
                generatedDoubleArray = Array.GenerateDoubleArray(size);

                // Display the generated double array in the inputArrayTextBlock
                StringBuilder output = new StringBuilder();
                output.AppendLine("Generated double array:");
                output.AppendLine(string.Join(" ", generatedDoubleArray.Select(d => d.ToString())));
                inputArrayTextBlock.Text = output.ToString();
            }
            else
            {
                MessageBox.Show("Enter a valid array size.");
            }
        }

        /////////////////////////////////////////////////
        private void SelectionSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedIntArray != null)
            {
                int[] array1 = Array.ProcessArray(generatedIntArray);
                StringBuilder steps = new StringBuilder();
                steps.AppendLine("An array after the function is executed:");
                steps.AppendLine(string.Join(" ", array1));

                sortingStopwatch.Restart(); // Початок вимірювання часу сортування
                Array.SelectionSortDescending(array1, steps);
                sortingStopwatch.Stop(); // Зупинити вимірювання часу сортування

                // Виведіть час сортування у ваш TextBox
                outputTextBlock.Text = steps.ToString();
                outputInfoTextBlock.Text = $"Sorting time: {sortingStopwatch.ElapsedMilliseconds} ms";
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

                sortingStopwatch.Restart();
                Array.ShellSortCities(sortedCities, steps);
                sortingStopwatch.Stop();

                outputTextBlock.Text = steps.ToString();
                outputInfoTextBlock.Text = $"Sorting time: {sortingStopwatch.ElapsedMilliseconds} ms";
            }
            else
            {
                MessageBox.Show("Generate a city array before sorting.");
            }
        }

        private void QuickSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generated2DDoubleArray != null)
            {
                Dictionary<int, double> columnSums = Array.CalculateColumnSums(generated2DDoubleArray);

                StringBuilder output = new StringBuilder();
                output.AppendLine("Unsorted Column Sums:");
                foreach (var kvp in columnSums)
                {
                    output.Append($"[{kvp.Key}]: {kvp.Value:F2}  ");
                }
                output.AppendLine();

                Dictionary<int, double> sortedColumnSums = Array.QuickSortDictionary(columnSums, output);

                output.AppendLine("Sorted Column Sums:");
                foreach (var kvp in sortedColumnSums)
                {
                    output.Append($"[{kvp.Key}]: {kvp.Value:F2} ");
                }
                outputTextBlock.Text = output.ToString();

                // Виклик функції для перестановки стовпців і отримання переставленого масиву
                double[][] rearrangedArray = Array.RearrangeColumns(generated2DDoubleArray, sortedColumnSums);

                // Виведення переставленого масиву у текстовий блок
                StringBuilder rearrangedOutput = new StringBuilder();
                rearrangedOutput.AppendLine("Rearranged 2D Double Array:");
                for (int i = 0; i < rearrangedArray.Length; i++)
                {
                    rearrangedOutput.AppendLine(string.Join(" ", rearrangedArray[i].Select(d => d.ToString("F2"))));
                }
                outputArrayTextBlock.Text = rearrangedOutput.ToString();
            }
            else
            {
                MessageBox.Show("Generate a 2D double array first.");
            }
        }

        private void MergeSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedDoubleArray != null)
            {
                Array.MultiplyNegativeElementsByMinimum(generatedDoubleArray);

                // Display the updated array in the inputArrayTextBlock
                StringBuilder steps = new StringBuilder();
                StringBuilder output = new StringBuilder();
                steps.AppendLine("Updated double array (negative elements multiplied by minimum negative value):");
                steps.AppendLine(string.Join(" ", generatedDoubleArray.Select(d => d.ToString("F2"))));
                //outputArrayTextBlock.Text = output.ToString();

                // Declare the steps variable here
                Array.MergeSortDescending(generatedDoubleArray, steps);
                // Display the steps in the inputArrayTextBlock
                outputTextBlock.Text = steps.ToString();
                output.AppendLine("Sorted double array (using Merge Sort):");
                output.AppendLine(string.Join(" ", generatedDoubleArray.Select(d => d.ToString("F2"))));
                outputArrayTextBlock.Text = output.ToString();
            }
            else
            {
                MessageBox.Show("Generate a double array first.");
            }
        }

        private void CountSortButton_Click(object sender, RoutedEventArgs e)
        {
            if (generatedDoubleArray != null)
            {
                double[] array1 = generatedDoubleArray.ToArray();
                StringBuilder steps = new StringBuilder();
                steps.AppendLine("An array after the function is executed:");
                steps.AppendLine(string.Join(" ", array1.Select(d => d.ToString("F2"))));

                Array.CountingSortDescending(array1, steps);

                // Виведіть відсортований масив у ваш текстовий блок
                outputTextBlock.Text = steps.ToString();

                // Оновіть outputTextBlock, щоб відобразити відсортований масив
                outputArrayTextBlock.Text = "Sorted double array:\n" + string.Join(" ", array1.Select(d => d.ToString("F2")));
            }
            else
            {
                MessageBox.Show("Спочатку згенеруйте масив double.");
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
        {
            if (generatedCityArray != null)
            {
                string newCity = arrayElementTextBox.Text;

                if (!string.IsNullOrWhiteSpace(newCity))
                {
                    // Створюємо новий масив, який буде більшим на один елемент
                    string[] newArray = new string[generatedCityArray.Length + 1];

                    // Копіюємо існуючі міста в новий масив
                    for (int i = 0; i < generatedCityArray.Length; i++)
                    {
                        newArray[i] = generatedCityArray[i];
                    }

                    // Додаємо нове місто в кінець масиву
                    newArray[newArray.Length - 1] = newCity;

                    // Оновлюємо змінну generatedCityArray
                    generatedCityArray = newArray;

                    // Оновлюємо текстовий блок зі списком міст
                    StringBuilder steps = new StringBuilder();
                    steps.AppendLine("Generated city array (after adding a city):");
                    steps.AppendLine(string.Join(", ", generatedCityArray));
                    outputTextBlock.Text = steps.ToString();
                }
                else
                {
                    MessageBox.Show("Enter the city name.");
                }
            }
            else
            {
                MessageBox.Show("Generate a city array before adding a city.");
            }
        }

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
                    "Lviv", "Vilnius", "Riga", "Tallinn",
                    "Berlin", "Paris", "London", "Amsterdam", "Madrid",
                    "New York", "Los Angeles", "Chicago", "Houston", "Philadelphia",
                    "San Francisco", "Miami", "Las Vegas", "Boston", "Washington D.C.",
                    "Toronto", "Vancouver", "Montreal", "Sydney", "Melbourne",
                    "Tokyo", "Beijing", "Shanghai", "Seoul", "Bangkok",
                    "Izyaslav", "Turka"
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

        public static double[][] GenerateDouble2DArray(int n)
        {
            double[][] doubleArray = new double[n][];

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                doubleArray[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    doubleArray[i][j] = Math.Round(random.NextDouble() * 100.0, 2);
                }
            }

            return doubleArray;
        }
        internal static double[][] RearrangeColumns(double[][] sourceArray, Dictionary<int, double> sortedColumnSums)
        {
            // Створюємо новий двовимірний масив для зберігання переставлених стовпців
            double[][] rearrangedArray = new double[sourceArray.Length][];
            for (int i = 0; i < sourceArray.Length; i++)
            {
                rearrangedArray[i] = new double[sourceArray[i].Length];
            }

            // Створюємо список стовпців в потрібному порядку
            List<int> columnOrder = sortedColumnSums.Keys.ToList();

            // Переставляємо стовпці в новому масиві в порядку, вказаному у columnOrder
            for (int i = 0; i < sourceArray.Length; i++)
            {
                for (int j = 0; j < columnOrder.Count; j++)
                {
                    int columnIndex = columnOrder[j];
                    rearrangedArray[i][j] = sourceArray[i][columnIndex];
                }
            }

            // Повертаємо переставлений масив
            return rearrangedArray;
        }






        public static double[] GenerateDoubleArray(int size)
        {
            Random random = new Random();
            double[] doubleArray = new double[size];

            for (int i = 0; i < size; i++)
            {
                doubleArray[i] = Math.Round((random.NextDouble() * 200.0) - 100.0, 2); // Generates random doubles between -100 and 100
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
                        j -= d;
                    }
                }

                steps.AppendLine($"Крок зі зсувом {d}: {string.Join(" ", array)}");

                d /= 2;
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
            (b, a) = (a, b);
        }
        public static int[] ProcessArray(int[] array)
        {
            return array.Where(item => item % 3 != 0).Select(item => Function(item)).ToArray();
        }

        private static int Function(int x)
        {
            return x * x;
        }


        public static Dictionary<int, double> CalculateColumnSums(double[][] array)
        {
            Dictionary<int, double> columnSums = new Dictionary<int, double>();

            int n = array.Length;
            int m = array[0].Length;

            for (int j = 0; j < m; j++)
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += array[i][j];
                }
                columnSums[j] = sum;
            }

            return columnSums;
        }

        public static Dictionary<int, double> QuickSortDictionary(Dictionary<int, double> dictionary, StringBuilder output)
        {
            List<KeyValuePair<int, double>> list = dictionary.ToList();

            QuickSortKeyValuePair(list, 0, list.Count - 1, output);

            return list.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static void QuickSortKeyValuePair(List<KeyValuePair<int, double>> list, int left, int right, StringBuilder output)
        {
            if (left < right)
            {
                int pivotIndex = Partition(list, left, right, output);

                QuickSortKeyValuePair(list, left, pivotIndex - 1, output);
                QuickSortKeyValuePair(list, pivotIndex + 1, right, output);
            }
        }

        private static int Partition(List<KeyValuePair<int, double>> list, int left, int right, StringBuilder output)
        {
            KeyValuePair<int, double> pivot = list[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (list[j].Value < pivot.Value) // Змініть на list[j].Value, якщо ви хочете сортувати за значеннями
                {
                    i++;
                    SwapKeyValuePair(list, i, j);
                }
            }

            SwapKeyValuePair(list, i + 1, right);

            output.AppendLine($"Partition Step: Pivot Element - [{pivot.Key}]: {pivot.Value:F2}");
            output.AppendLine($"Left Partition: {string.Join(", ", list.Skip(left).Take(i - left + 1).Select(kvp => $"[{kvp.Key}]: {kvp.Value:F2} "))}");
            output.AppendLine($"Right Partition: {string.Join(", ", list.Skip(i + 2).Take(right - i - 1).Select(kvp => $"[{kvp.Key}]: {kvp.Value:F2} "))}");

            return i + 1;
        }

        private static void SwapKeyValuePair(List<KeyValuePair<int, double>> list, int i, int j)
        {
            KeyValuePair<int, double> temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }


        //lab4 task
        public static double FindMinimumNegativeValue(double[] array)
        {
            double minNegativeValue = double.MaxValue;

            foreach (double num in array)
            {
                if (num < 0 && num < minNegativeValue)
                {
                    minNegativeValue = num;
                }
            }

            return minNegativeValue;
        }

        public static void MultiplyNegativeElementsByMinimum(double[] array)
        {
            double minNegativeValue = FindMinimumNegativeValue(array);

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    array[i] *= minNegativeValue;
                }
            }
        }
        // lab 4 merge
        public static void MergeSortDescending(double[] array, StringBuilder steps)
        {
            MergeSortDescending(array, 0, array.Length - 1, steps);
        }

        private static void MergeSortDescending(double[] array, int left, int right, StringBuilder steps)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                steps.AppendLine($"Dividing: left={left}, mid={mid}, right={right}");
                MergeSortDescending(array, left, mid, steps);
                MergeSortDescending(array, mid + 1, right, steps);
                MergeDescending(array, left, mid, right, steps);
            }
        }

        private static void MergeDescending(double[] array, int left, int mid, int right, StringBuilder steps)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            double[] leftArray = new double[n1];
            double[] rightArray = new double[n2];

            for (int i = 0; i < n1; i++)
            {
                leftArray[i] = array[left + i];
            }

            for (int i = 0; i < n2; i++)
            {
                rightArray[i] = array[mid + 1 + i];
            }

            int leftIndex = 0;
            int rightIndex = 0;
            int mergedIndex = left;

            while (leftIndex < n1 && rightIndex < n2)
            {
                if (leftArray[leftIndex] >= rightArray[rightIndex]) // Change the comparison here
                {
                    array[mergedIndex] = leftArray[leftIndex];
                    leftIndex++;
                }
                else
                {
                    array[mergedIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < n1)
            {
                array[mergedIndex] = leftArray[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < n2)
            {
                array[mergedIndex] = rightArray[rightIndex];
                rightIndex++;
                mergedIndex++;
            }

            steps.AppendLine($"Merging: left={left}, mid={mid}, right={right}");
            steps.AppendLine($"Step: {string.Join(" ", array)}");
        }

        //lab 5
        public static double[] ApplyFunctionsToElements(double[] array)
        {
            double[] resultArray = new double[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 0) // Check if the index is even
                {
                    resultArray[i] = Math.Tan(array[i]) - array[i];
                }
                else
                {
                    resultArray[i] = Math.Abs(array[i]);
                }
            }

            return resultArray;
        }
        // Інші методи класу Array ...

        public static void CountingSortDescending(double[] array, StringBuilder steps)
        {
            if (array == null || array.Length <= 1)
            {
                // Масив вже відсортований або порожній, нема потреби сортувати.
                return;
            }

            double maxValue = array.Max();
            double minValue = array.Min();
            int range = (int)Math.Ceiling(maxValue - minValue) + 1;

            double[] count = new double[range];
            double[] output = new double[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                count[(int)(array[i] - minValue)]++;
            }

            for (int i = 1; i < range; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[(int)(count[(int)(array[i] - minValue)] - 1)] = array[i];
                count[(int)(array[i] - minValue)]--;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = output[i];
                steps.AppendLine($"Step {i + 1}: {string.Join(" ", array.Select(d => d.ToString("F2")))}");
            }
        }



    }
}




