using UnityEngine;

/// <summary>
/// �����, ����������� ����������� ��������� �������� ���������������.
/// </summary>
public class AnimationContainerParallel : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// ���������������� ��������.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Animations;

    #endregion


    #region Protected API

    /// <summary>
    /// ��������������� ������ ���������� � ��������.
    /// </summary>
    public override void PrepareAnimation()
    {
        m_AnimatoinTime = 0;

        foreach (var v in m_Animations)
        {
            v.SetAnimationScale(m_AnimationScale);
            m_AnimatoinTime += Mathf.Max(m_AnimatoinTime, v.AnimationTime);

            v.PrepareAnimation();
        }
    }

    /// <summary>
    /// ���������������� ����� ��� ������ ��������.
    /// </summary>
    protected override void OnAnimationStart()
    {
        foreach (var v in m_Animations)
        {
            v.StartAnimation();
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

}