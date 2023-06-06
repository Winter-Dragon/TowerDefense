using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����� �������� ��������� ��������.
/// </summary>
public class ImpactEffect : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// ����� ����� ��������.
    /// </summary>
    [SerializeField] private float m_LifeTime;

    /// <summary>
    /// ���������� ������.
    /// </summary>
    private Timer m_Timer;

    #endregion


    #region Unity Events

    private void Start()
    {
        m_Timer = new Timer(m_LifeTime, false);
    }

    protected virtual void FixedUpdate()
    {
        // ��������� ������.
        m_Timer.UpdateTimer();

        // ���������� ������, ���� ������ ����������.
        if (m_Timer.IsFinished) Destroy(gameObject);
    }

    #endregion

}