using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������� �������� ���� ����.
/// </summary>
public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
{

    #region Properties and Components

    /// <summary>
    /// ������ ����������� ����.
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
    /// ����� ������ ����� ����.
    /// </summary>
    public void StartNewGame()
    {

    }

    /// <summary>
    /// �����, ����������� ������� ����.
    /// </summary>
    public void ContinueGame()
    {

    }

    /// <summary>
    /// �����, ��������� �� ����.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

}
