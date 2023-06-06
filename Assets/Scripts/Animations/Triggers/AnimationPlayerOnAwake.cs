using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� �������� � Awake.
/// </summary>
public class AnimationPlayerOnAwake : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// �������� ��� ���������.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Targets;

    #endregion


    #region Unity Events

    /// <summary>
    /// ���������� �������� ��� ����������� �������.
    /// </summary>
    private void Awake()
    {
        if (m_Targets != null && m_Targets.Length > 0)
        {
            for(int i = 0; i < m_Targets.Length; i++)
            {
                m_Targets[i].StartAnimation(true);
            }
        }
    }

    #endregion
}
