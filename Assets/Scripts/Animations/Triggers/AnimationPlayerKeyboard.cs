using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� �������� ��� ������� �������.
/// </summary>
public class AnimationPlayerKeyboard : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// ������� ��� ���������.
    /// </summary>
    [SerializeField] private KeyCode m_Key;

    /// <summary>
    /// ���� ��� ���������.
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