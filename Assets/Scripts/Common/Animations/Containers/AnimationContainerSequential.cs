using UnityEngine;


/// <summary>
/// ������ ������������ ������ �������� ���������������.
/// </summary>
public class AnimationContainerSequential : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// ���������������� ��������.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Animations;

    /// <summary>
    /// ������ ������� ��������.
    /// </summary>
    private int m_CurrentSubAnimation;

    #endregion


    #region Protected API

    /// <summary>
    /// ���������������� ����� ��� ������ ��������.
    /// </summary>
    protected override void OnAnimationStart()
    {
        // ������ ������� �������� � 0.
        m_CurrentSubAnimation = 0;

        // �������� �� ������� ����� �������� � �������� ��������.
        m_Animations[m_CurrentSubAnimation].OnEventEnd += OnSubAnimationEnded;
        // ����� �������� �������� ��������.
        m_Animations[m_CurrentSubAnimation].StartAnimation();
    }

    /// <summary>
    /// ��������������� ������ ���������� � ��������.
    /// </summary>
    public override void PrepareAnimation()
    {
        m_AnimatoinTime = 0;

        // � ������ �� �������� ����������� ���������� ��������.
        foreach(var v in m_Animations)
        {
            v.SetAnimationScale(m_AnimationScale);
            m_AnimatoinTime += v.AnimationTime;

            v.PrepareAnimation();
        }
    }

    /// <summary>
    /// ���������������� ����� ��������� ������� ��������.
    /// </summary>
    protected override void AnimateFrame() { }
    /// <summary>
    /// ���������������� ����� ��� ���������� ��������.
    /// </summary>
    protected override void OnAnimationEnd() { }

    #endregion


    #region Private API

    /// <summary>
    /// �����, ������������� ����� ������������� ���� ��������.
    /// </summary>
    private void OnSubAnimationEnded()
    {
        // ���������� �� ������� ����� ��������.
        m_Animations[m_CurrentSubAnimation].OnEventEnd -= OnSubAnimationEnded;

        // ������ ��������++.
        m_CurrentSubAnimation++;

        // ���� �������� �� ���������.
        if (m_CurrentSubAnimation < m_Animations.Length)
        {
            // ����������� �� ������� ����� ��������.
            m_Animations[m_CurrentSubAnimation].OnEventEnd += OnSubAnimationEnded;
            // ��������� ��������.
            m_Animations[m_CurrentSubAnimation].StartAnimation();
        }
        // ���� �������� ���� ���������.
        else
        {
            // ��������� ��������.
            StopAnimation();
        }
    }

    #endregion

}