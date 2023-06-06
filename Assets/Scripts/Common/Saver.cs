using UnityEngine;
using System;
using System.IO;

/// <summary>
/// ���������� �����, ���������� � ��������. ����� ��������� � ��������� ����� � ������� JSON.
/// </summary>
/// <typeparam name="T">��� ������.</typeparam>
[Serializable]
public class Saver<T>
{

    #region Properties and Components

    /// <summary>
    /// �������� ���������� ����.
    /// </summary>
    public T data;

    #endregion


    #region Private API

    /// <summary>
    /// �������, ������������ ������ ���� � ����� �� ��� ��������.
    /// </summary>
    /// <param name="filename">�������� �����.</param>
    /// <returns>������ ���� � �����.</returns>
    private static string Path(string filename)
    {
        return $"{Application.persistentDataPath}/{filename}";
    }

    #endregion


    #region Public API

    /// <summary>
    /// ��������� ����.
    /// </summary>
    /// <param name="filename">��� �����.</param>
    /// <param name="data">��� ����� (�����).</param>
    public static void Save(string filename, T data)
    {
        // ������� ��������� ������ ������, ����� Json ��������� �������� ������.
        Saver<T> wrapper = new Saver<T> { data = data };

        // ������� ������� ��� ���������� � ������� Json.
        string dataString = JsonUtility.ToJson(data);

        // �������� ��������� ������ ���� � �����.
        string path = Path(filename);

        // ������������ ������ � ���� �� �������� �����.
        File.WriteAllText(path, dataString);
    }

    /// <summary>
    /// ��������� ����.
    /// </summary>
    /// <param name="filename">��� �����.</param>
    /// <param name="data">��� ����� (�����).</param>
    public static void TryLoad(string filename, ref T data)
    {
        // �������� ��������� ������ ���� � �����.
        string path = Path(filename);

        // ���� ���� � ����� ����������.
        if (File.Exists(path))
        {
            // ������� �� ����������� ����� ������ � �������� � ��������.
            string dataString = File.ReadAllText(path);

            // ��������� ������ �� Json � ����� Saver.
            Saver<T> saver = JsonUtility.FromJson<Saver<T>>(dataString);

            // ��������� ���� ��������.
            data = saver.data;
        }
    }

    /// <summary>
    /// ������� ���������� ����.
    /// </summary>
    /// <param name="filename">��� �����.</param>
    public static void Reset(string filename)
    {
        // �������� ��������� ������ ���� � �����.
        string path = Path(filename);

        // ���� ���� � ����� ����������.
        if (File.Exists(path))
        {
            // ������� ���� �� ���������� ����.
            File.Delete(path);
        }
    }

    /// <summary>
    /// �����, �����������, ���������� �� ��������� ����.
    /// </summary>
    /// <param name="filename">��� �����.</param>
    /// <returns>true ���� ���� ����, false ���� ����� ���.</returns>
    public static bool HasFile(string filename)
    {
        return File.Exists(filename);
    }

    #endregion

}
