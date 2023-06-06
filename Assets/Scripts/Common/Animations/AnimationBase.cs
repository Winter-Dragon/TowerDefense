using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ������� ����� ��������.
/// </summary>
public abstract class AnimationBase : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// ������ ����� ��������.
    /// </summary>
    [SerializeField] protected float m_AnimatoinTime;

    /// <summary>
    /// �������� ������� ��������.
    /// </summary>
    [SerializeField] protected float m_AnimationScale;

    /// <summary>
    /// ���� ������������� ��������.
    /// </summary>
    [SerializeField] private bool m_Looping;

    /// <summary>
    /// ���� �������� ��������.
    /// </summary>
    [SerializeField] private bool m_Reverse;

    /// <summary>
    /// ������� ������ ��������.
    /// </summary>
    public event EmptyDelegate OnEventStart;

    /// <summary>
    /// ������� ����� ��������.
    /// </summary>
    public event EmptyDelegate OnEventEnd;

    /// <summary>
    /// ��������� ����, ������������ ������������� �� ��������.
    /// </summary>
    private bool m_IsAnimationPlaying;

    /// <summary>
    /// ��������� ������ ��������.
    /// </summary>
    private Timer m_Timer;

    #region Links

    /// <summary>
    /// ������ ����� �������� � ������ ���������.
    /// </summary>
    public float AnimationTime => m_AnimatoinTime / m_AnimationScale;

    /// <summary>
    /// ���������� ��������������� �������� �������� �� 0 �� 1.
    /// </summary>
    public float NormalizedAnimationTime
    {
        get
        {
            var t = Mathf.Clamp01(m_Timer.GetCurrentTime() / m_AnimatoinTime);
            return m_Reverse ? (1.0f - t) : t;
        }
    }

    #endregion

    #endregion


    #region UnityEvents

    private void Start()
    {
        // ������������� �������.
        m_Timer = new Timer(AnimationTime, m_Looping);
    }

    private void FixedUpdate()
    {
        // ���� �������� ������������� - �����.
        if (!m_IsAnimationPlaying) return;

        // �������� ������.
        m_Timer.UpdateTimer();

        // ����������� �����.
        AnimateFrame();

        // ���� ������ ��������.
        if (m_Timer.IsFinished)
        {
            // ���� �������� �� ����������� - ��������� ��������.
            if (!m_Looping)
            {
                StopAnimation();
            }
        }
    }

    #endregion


    #region Public API

    /// <summary>
    /// ���������� ����� �������� ������ ��������.
    /// </summary>
    /// <param name="scale">�������� ������.</param>
    public void SetAnimationScale(float scale)
    {
        m_AnimationScale = scale;
    }

    /// <summary>
    /// ����� ������� ��������.
    /// </summary>
    /// <param name="prepare">true ���� ����� ���������� � ��������.</param>
    public void StartAnimation(bool prepare = true)
    {
        // ���� �������� ��� ������������� - �����.
        if (m_IsAnimationPlaying) return;

        // ���� ���� ���������� � �������� - ���������.
        if (prepare) PrepareAnimation();

        // ���� ������������ �������� true.
        m_IsAnimationPlaying = true;

        // ����� ������ ��������.
        OnAnimationStart();

        // ����� ������� ������ ��������.
        OnEventStart?.Invoke();
    }

    /// <summary>
    /// ����� ������������ ��������.
    /// </summary>
    public void StopAnimation()
    {
        // ���� �������� �������������� - �����.
        if (!m_IsAnimationPlaying) return;

        // ���� ������������ �������� false.
        m_IsAnimationPlaying = false;

        // ����� ���������� ��������.
        OnAnimationEnd();

        // ����� ������� ����� ��������.
        OnEventEnd?.Invoke();
    }

    #endregion


    #region Abstract API

    /// <summary>
    /// ��������� ������� ����� ��������.
    /// </summary>
    protected abstract void AnimateFrame();
    /// <summary>
    /// �������� ��� ������ ��������.
    /// </summary>
    protected abstract void OnAnimationStart();
    /// <summary>
    /// �������� ��� ���������� ��������.
    /// </summary>
    protected abstract void OnAnimationEnd();
    /// <summary>
    /// ���������� ���������� ��������� ��������.
    /// </summary>
    public abstract void PrepareAnimation();

    #endregion

}