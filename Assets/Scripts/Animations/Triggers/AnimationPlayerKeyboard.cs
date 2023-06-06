using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Активатор анимации при нажатии клавиши.
/// </summary>
public class AnimationPlayerKeyboard : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// Клавиша для активации.
    /// </summary>
    [SerializeField] private KeyCode m_Key;

    /// <summary>
    /// Цели для активации.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Targets;

    #endregion


    #region Unity Events

    private void Update()
    {
        if (Input.GetKeyDown(m_Key))
        {
            if (m_Targets != null && m_Targets.Length > 0)
            {
                for (int i = 0; i < m_Targets.Length; i++)
                {
                    m_Targets[i].StartAnimation(true);
                }
            }
        }
    }

    #endregion

}