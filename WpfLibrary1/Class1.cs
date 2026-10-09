using System;
using System.IO;

namespace LibMas
{
    /// <summary>
    /// Библиотека модулей для работы с одномерным массивом.
    /// </summary>
    public class Massiv
    {
        /// <summary>
        /// Заполнение массива случайными значениями.
        /// </summary>
        /// <param name="mas">Массив</param>
        /// <param name="column">Количество ячеек</param>
        /// <param name="randMax">Диапазон случайных значений 0..randMax</param>
        public static void InitMas(out int[] mas, int column, int randMax)
        {
            Random rnd = new Random();
            mas = new int[column];
            for (int i = 0; i < column; i++)
                mas[i] = rnd.Next(randMax);
        }

        /// <summary>
        /// Очистка массива (обнуление всех элементов).
        /// </summary>
        /// <param name="mas">Массив</param>
        public static void ClearMas(ref int[] mas)
        {
            if (mas == null) return;
            for (int i = 0; i < mas.Length; i++)
                mas[i] = 0;
        }

        /// <summary>
        /// Сохранение массива в файл.
        /// </summary>
        /// <param name="mas">Массив</param>
        /// <param name="fileName">Путь к файлу</param>
        public static void SaveMas(int[] mas, string fileName)
        {
            StreamWriter file = new StreamWriter(fileName);
            file.WriteLine(mas.Length);
            for (int i = 0; i < mas.Length; i++)
                file.WriteLine(mas[i]);
            file.Close();
        }

        /// <summary>
        /// Чтение массива из файла.
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Прочитанный массив</returns>
        public static int[] OpenMas(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            int len = Convert.ToInt32(file.ReadLine());
            int[] mas = new int[len];
            for (int i = 0; i < len; i++)
                mas[i] = Convert.ToInt32(file.ReadLine());
            file.Close();
            return mas;
        }

        /// <summary>
        /// Вычислительный модуль по варианту 4.
        /// Для чисел > 0 вычисляет квадратный корень.
        /// Результаты записываются в выходной массив (0 — для неположительных чисел).
        /// </summary>
        /// <param name="mas">Исходный массив</param>
        /// <param name="result">Массив результатов</param>
        public static void SqrtPositive(int[] mas, out double[] result)
        {
            result = new double[mas.Length];
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] > 0)
                    result[i] = Math.Sqrt(mas[i]);
                else
                    result[i] = 0;
            }
        }
    }
}