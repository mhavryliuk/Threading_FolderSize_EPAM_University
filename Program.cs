using System;
using System.IO;

/**<remark>
 * Calculate the folder size.
 * Start n threads simultaneously.Each thread calculates the sum of size for the 10 files.
 * Do this task using an array(or list) of threads working independently.
 * Perform this task using task parallel library.
 *
 * Вычислите размер папки.
 * Начать n потоков одновременно. Каждый поток вычисляет сумму размера для 10 файлов.
 * Выполняйте эту задачу с использованием массива (или списка) потоков, работающих независимо.
 * Выполните эту задачу, используя параллельную библиотеку задач.
 * </remark> */

namespace _20180403_Task1_FolderSize
{
    internal class Program
    {
        private static void Main()
        {
            const string pathToDirectory = @"W:\Soft";

            try
            {
                MyDirectory myDirectory = new MyDirectory();
                myDirectory.SizeOfDirectory(pathToDirectory);
            }

            // DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Внимание: проверьте путь к директории.");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            // UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Внимание: проверьте доступ к директории.");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            // Во всех остальных случаях
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Внимание: Произошла ошибка!");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.ReadKey();
        }
    }
}