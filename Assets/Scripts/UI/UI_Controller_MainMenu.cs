using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Контроллёр главного меню игры.
/// </summary>
public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
{

    #region Properties and Components

    /// <summary>
    /// Кнопка продолжения игры.
    /// </summary>
    [SerializeField] private Button m_ButtonContinue;

    #endregion


    #region Unity Events

    private void Start()
    {
        // if (Saver<UI_Controller_MainMenu>.HasFile())
    }

    #endregion


    #region Public API

    /// <summary>
    /// Метод начала новой игры.
    /// </summary>
    public void StartNewGame()
    {

    }

    /// <summary>
    /// Метод, загружающий прошлую игру.
    /// </summary>
    public void ContinueGame()
    {

    }

    /// <summary>
    /// Метод, выходящий из игры.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

}
