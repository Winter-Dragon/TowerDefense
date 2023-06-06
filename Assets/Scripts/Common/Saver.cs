using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Глобальный класс, работающий с классами. Может сохранять и загружать файлы с помощью JSON.
/// </summary>
/// <typeparam name="T">Тип класса.</typeparam>
[Serializable]
public class Saver<T>
{

    #region Properties and Components

    /// <summary>
    /// Локально сохранённый файл.
    /// </summary>
    public T data;

    #endregion


    #region Private API

    /// <summary>
    /// Функция, возвращающия полный путь к файлу по его названию.
    /// </summary>
    /// <param name="filename">Название файла.</param>
    /// <returns>Полный путь к файлу.</returns>
    private static string Path(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }

    #endregion


    #region Public API

    /// <summary>
    /// Загрузить файл.
    /// </summary>
    /// <param name="filename">Имя файла.</param>
    /// <param name="data">Тип файла (класс).</param>
    public static void Save(string filename, T data)
    {
        // Создать локальную обёртку данных, чтобы Json корректно сохранил данные.
        Saver<T> wrapper = new Saver<T> { data = data };

        // Создать строчку для сохранения в формате Json.
        string dataString = JsonUtility.ToJson(data);

        // Локально сохраняем полный путь к файлу.
        string path = Path(filename);

        // Перезаписать строку в файл по названию файла.
        File.WriteAllText(path, dataString);
    }

    /// <summary>
    /// Сохранить файл.
    /// </summary>
    /// <param name="filename">Имя файла.</param>
    /// <param name="data">Тип файла (класс).</param>
    public static void TryLoad(string filename, ref T data)
    {
        // Локально сохраняем полный путь к файлу.
        string path = Path(filename);

        // Если путь к файлу существует.
        if (File.Exists(path))
        {
            // Достать из сохранённого файла строку и записать её локально.
            string dataString = File.ReadAllText(path);

            // Перевести строку из Json в класс Saver.
            Saver<T> saver = JsonUtility.FromJson<Saver<T>>(dataString);

            // Сохранить дату локально.
            data = saver.data;
        }
    }

    /// <summary>
    /// Удалить сохранённый файл.
    /// </summary>
    /// <param name="filename">Имя файла.</param>
    public static void Reset(string filename)
    {
        // Локально сохраняем полный путь к файлу.
        string path = Path(filename);

        // Если путь к файлу существует.
        if (File.Exists(path))
        {
            // Удалить файл по указанному пути.
            File.Delete(path);
        }
    }

    /// <summary>
    /// Метод, проверяющий, существует ли указанный файл.
    /// </summary>
    /// <param name="filename">Имя файла.</param>
    /// <returns>true если файл есть, false если файла нет.</returns>
    public static bool HasFile(string filename)
    {
        return File.Exists(filename);
    }

    #endregion

}
