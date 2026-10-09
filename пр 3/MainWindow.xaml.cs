using System;
using System.Data;
using System.Windows;
using Microsoft.Win32;
using LibMas;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Исходный массив
        private int[] mas;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заполнение массива случайными значениями.
        /// </summary>
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            int count = Convert.ToInt32(txtCount.Text);
            int range = Convert.ToInt32(txtRange.Text);
            Massiv.InitMas(out mas, count, range);
            dgInput.ItemsSource = ToDataTable(mas).DefaultView;
            dgResult.ItemsSource = null;
        }

        /// <summary>
        /// Очистка массива.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null) return;
            Massiv.ClearMas(ref mas);
            dgInput.ItemsSource = ToDataTable(mas).DefaultView;
            dgResult.ItemsSource = null;
        }

        /// <summary>
        /// Сохранение массива в файл.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null) { MessageBox.Show("Массив пуст."); return; }

            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение массива";

            if (save.ShowDialog() == true)
            {
                Massiv.SaveMas(mas, save.FileName);
                MessageBox.Show("Массив сохранён.");
            }
        }

        /// <summary>
        /// Открытие массива из файла.
        /// </summary>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие массива";

            if (open.ShowDialog() == true)
            {
                mas = Massiv.OpenMas(open.FileName);
                dgInput.ItemsSource = ToDataTable(mas).DefaultView;
                dgResult.ItemsSource = null;
            }
        }

        /// <summary>
        /// Вычисление по варианту 4: √x для чисел > 0.
        /// </summary>
        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null) { MessageBox.Show("Массив пуст."); return; }

            // Считываем массив из таблицы (если пользователь правил вручную)
            mas = FromDataView(dgInput.ItemsSource as DataView);

            double[] result;
            Massiv.SqrtPositive(mas, out result);

            dgResult.ItemsSource = ToDataTable(result).DefaultView;
        }

        /// <summary>
        /// Выход.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// О программе.
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Практическая работа №1\n" +
                "Вариант 4: Ввести n целых чисел. Вычислить для чисел > 0 функцию √x.\n" +
                "Результат обработки каждого числа вывести на экран.\n\n" +
                "Разработчик: Бондаренко Ангелина, группа ИСП-31",
                "О программе");
        }

        // ====== Вспомогательные методы для DataGrid ======

        /// <summary>
        /// Преобразование int[] в DataTable (для DataGrid).
        /// </summary>
        private DataTable ToDataTable(int[] arr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Индекс", typeof(int));
            dt.Columns.Add("Значение", typeof(int));
            for (int i = 0; i < arr.Length; i++)
                dt.Rows.Add(i, arr[i]);
            return dt;
        }

        /// <summary>
        /// Преобразование double[] в DataTable (для DataGrid).
        /// </summary>
        private DataTable ToDataTable(double[] arr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Индекс", typeof(int));
            dt.Columns.Add("√x", typeof(double));
            for (int i = 0; i < arr.Length; i++)
                dt.Rows.Add(i, Math.Round(arr[i], 4));
            return dt;
        }

        /// <summary>
        /// Обратное преобразование DataView в int[].
        /// </summary>
        private int[] FromDataView(DataView dv)
        {
            if (dv == null) return mas;
            int[] result = new int[dv.Count];
            for (int i = 0; i < dv.Count; i++)
                result[i] = Convert.ToInt32(dv[i]["Значение"]);
            return result;
        }
    }
}