using UnityEngine;

/// <summary>
/// Класс, позволяющий проигрывать несколько анимаций последовательно.
/// </summary>
public class AnimationContainerParallel : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// Последовательные анимации.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Animations;

    #endregion


    #region Protected API

    /// <summary>
    /// Переопределение метода подготовки к анимации.
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
    /// Переопределяемый метод при старте анимации.
    /// </summary>
    protected override void OnAnimationStart()
    {
        foreach (var v in m_Animations)
        {
            v.StartAnimation();
        }
    }
    /// <summary>
    /// Переопределяемый метод изменения фреймов анимации.
    /// </summary>
    protected override void AnimateFrame() { }
    /// <summary>
    /// Переопределяемый метод при завершении анимации.
    /// </summary>
    protected override void OnAnimationEnd() { }

    #endregion

}