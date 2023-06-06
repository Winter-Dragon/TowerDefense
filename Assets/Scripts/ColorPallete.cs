using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� � �������� ������ � ����.
/// </summary>
public class ColorPallete : Singleton<ColorPallete>
{

    #region Properties and Components

    /// <summary>
    /// ���� ������ ������ (��������).
    /// </summary>
    [SerializeField] private Color m_GoldTextColor;

    /// <summary>
    /// ���� ������ ������ (����������).
    /// </summary>
    [SerializeField] private Color m_GoldTextColorUnactive;

    /// <summary>
    /// ������ ����� ��� ������ (����������).
    /// </summary>
    [SerializeField] private Color m_GoldImageColorUnactive;


    #region Links

    /// <summary>
    /// ���� ������ ������ (��������).
    /// </summary>
    public Color GoldTextColor => m_GoldTextColor;

    /// <summary>
    /// ���� ������ ������ (����������).
    /// </summary>
    public Color GoldTextColorUnactive => m_GoldTextColorUnactive;

    /// <summary>
    /// ������ ����� ��� ������ (����������).
    /// </summary>
    public Color GoldImageColorUnactive => m_GoldImageColorUnactive;

    #endregion

    #endregion

}
