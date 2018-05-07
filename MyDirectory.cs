using System;
using System.IO;
using System.Threading.Tasks;

/**<remark>
 * https://msdn.microsoft.com/ru-ru/library/system.io.fileinfo.length(v=vs.110).aspx
 * Определить размер каталога на C# https://alekseygulynin.ru/razmer-kataloga-c/
 * </remark> */

namespace _20180403_Task1_FolderSize
{
    internal class MyDirectory
    {
        private double directorySize = 0.0d;            // Размер директории
        private int startOfRange = 0, endOfRange = 9;   // Отсчет по 10 файлов на поток

        internal void SizeOfDirectory(string pathToDirectory)
        {
            DirectoryInfo linkToDirectory = new DirectoryInfo(pathToDirectory);   // Создаем ссылку на директорию.
            FileInfo[] fileInfo = linkToDirectory.GetFiles();                     // Получаем ссылку на каждый файл в заданной директории.

            // В цикле пробегаемся по всем файлам директории и складываем их размеры
            foreach (FileInfo file in fileInfo)
            {
                directorySize = directorySize + file.Length;              //Записываем размер файла в байтах
            }

            directorySize = Math.Round(directorySize / 1024 / 1024, 1);   // 1МБ = 1024 Байта * 1024 КБайта

            Console.WriteLine(directorySize != 0
                ? $"Размер директории {pathToDirectory} составляет: {directorySize} МБ"
                : $"Каталог {pathToDirectory} пуст.");

            Console.WriteLine($"Количество файлов в директории: {fileInfo.Length}\n");


            //Создаем в цикле n потоков, каждый из которых друг за другом расчитывает размер 10 файлов
            for (int i = 0; i < fileInfo.Length; i += 10)
            {
                Task task = Task.Run(() =>
                {
                    if (endOfRange > fileInfo.Length) endOfRange = startOfRange + fileInfo.Length % 10 - 1;

                    for (int j = startOfRange; j <= endOfRange; j++)
                    {
                        directorySize += fileInfo[j].Length;
                    }

                    directorySize = Math.Round(directorySize / 1024 / 1024, 1);

                    Console.WriteLine($"{Task.CurrentId} поток -> размер файлов с {startOfRange + 1} по {endOfRange + 1}: {directorySize} МБ");

                    startOfRange += 10;
                    endOfRange += 10;
                });

                task.Wait();      // Ожидать завершения задачи task
                task.Dispose();   // Освободить задачу task
            }
        }
    }
}