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

namespace pz1_penkina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int[] numbers = GenerateRandomArray(9);
            List<int> sorted1 = BubbleSort(numbers);
            List<int> sorted2 = SelectionSort(numbers);
            List<int> sorted3 = InsertionSort(numbers);
            List<int> sorted4 = QuickSort(numbers);
            List<SortResult> results = new List<SortResult>
            {
                new SortResult {Method1 = string.Join(", ", sorted1)},
                new SortResult {Method2 = string.Join(", ", sorted2)},
                new SortResult {Method3 = string.Join(", ", sorted3)},
                new SortResult {Method4 = string.Join(", ", sorted4)}
            };
            sortedListView.ItemsSource = results;
        }
        private int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(100);
            }
            return array;
        }

        // Проходит по массиву многократно, сравнивая соседние элементы и меняя их местами,
        // если они стоят в неправильном порядке. Повторяет этот процесс до тех пор,
        // пока массив не будет отсортирован.
        private List<int> BubbleSort(int[] array)
        {
            List<int> sortedArray = new List<int>(array);
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < sortedArray.Count - 1; i++)
                {
                    if (sortedArray[i] > sortedArray[i + 1])
                    {
                        int temp = sortedArray[i];
                        sortedArray[i] = sortedArray[i + 1];
                        sortedArray[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
            return sortedArray;
        }

        //На каждом шаге находит минимальный элемент в оставшейся части массива и меняет
        //его местами с первым неотсортированным элементом. Повторяет этот процесс для
        //оставшейся части массива.
        private List<int> SelectionSort(int[] array)
        {
            List<int> sortedArray = new List<int>(array);
            for (int i = 0; i < sortedArray.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < sortedArray.Count; j++)
                {
                    if (sortedArray[j] < sortedArray[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = sortedArray[i];
                sortedArray[i] = sortedArray[minIndex];
                sortedArray[minIndex] = temp;
            }
            return sortedArray;
        }

        //Проходит по массиву и вставляет каждый элемент на правильное место в
        //отсортированной части массива слева от текущего элемента.
        private List<int> InsertionSort(int[] array)
        {
            List<int> sortedArray = new List<int>(array);
            for (int i = 1; i < sortedArray.Count; i++)
            {
                int key = sortedArray[i];
                int j = i - 1;
                while (j >= 0 && sortedArray[j] > key)
                {
                    sortedArray[j + 1] = sortedArray[j];
                    j = j - 1;
                }
                sortedArray[j + 1] = key;
            }
            return sortedArray;
        }

        //Выбирает опорный элемент (pivot) и разделяет массив на две части: элементы
        //меньше опорного и элементы больше опорного. Затем рекурсивно применяет
        //этот процесс к каждой из частей.
        private List<int> QuickSort(int[] array)
        {
            List<int> sortedArray = new List<int>(array);
            QuickSortRecursive(sortedArray, 0, sortedArray.Count - 1);
            return sortedArray;
        }
        private void QuickSortRecursive(List<int> array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);
                QuickSortRecursive(array, low, partitionIndex - 1);
                QuickSortRecursive(array, partitionIndex + 1, high);
            }
        }
        private int Partition(List<int> array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            int temp2 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp2;
            return i + 1;
        }
        public class SortResult
        {
            public string Method1 { get; set; }
            public string Method2 { get; set; }
            public string Method3 { get; set; }
            public string Method4 { get; set; }
        }
    }
}