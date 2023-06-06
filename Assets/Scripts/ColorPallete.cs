using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ласс с палеткой цветов в игре.
/// </summary>
public class ColorPallete : Singleton<ColorPallete>
{

    #region Properties and Components

    /// <summary>
    /// ÷вет текста золота (активный).
    /// </summary>
    [SerializeField] private Color m_GoldTextColor;

    /// <summary>
    /// ÷вет текста золота (неактивный).
    /// </summary>
    [SerializeField] private Color m_GoldTextColorUnactive;

    /// <summary>
    /// ‘ильтр цвета дл€ золота (неактивное).
    /// </summary>
    [SerializeField] private Color m_GoldImageColorUnactive;


    #region Links

    /// <summary>
    /// ÷вет текста золота (активный).
    /// </summary>
    public Color GoldTextColor => m_GoldTextColor;

    /// <summary>
    /// ÷вет текста золота (неактивный).
    /// </summary>
    public Color GoldTextColorUnactive => m_GoldTextColorUnactive;

    /// <summary>
    /// ‘ильтр цвета дл€ золота (неактивное).
    /// </summary>
    public Color GoldImageColorUnactive => m_GoldImageColorUnactive;

    #endregion

    #endregion

}
