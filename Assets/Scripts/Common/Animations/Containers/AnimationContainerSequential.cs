using UnityEngine;


/// <summary>
/// Скрипт проигрывания набора анимация последовательно.
/// </summary>
public class AnimationContainerSequential : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// Последовательные анимации.
    /// </summary>
    [SerializeField] private AnimationBase[] m_Animations;

    /// <summary>
    /// Индекс текущей анимации.
    /// </summary>
    private int m_CurrentSubAnimation;

    #endregion


    #region Protected API

    /// <summary>
    /// Переопределяемый метод при старте анимации.
    /// </summary>
    protected override void OnAnimationStart()
    {
        // Индекс текущей анимации в 0.
        m_CurrentSubAnimation = 0;

        // Подписка на событие конца анимации у текущего элемента.
        m_Animations[m_CurrentSubAnimation].OnEventEnd += OnSubAnimationEnded;
        // Старт анимации текущего элемента.
        m_Animations[m_CurrentSubAnimation].StartAnimation();
    }

    /// <summary>
    /// Переопределение метода подготовки к анимации.
    /// </summary>
    public override void PrepareAnimation()
    {
        m_AnimatoinTime = 0;

        // В каждой из анимаций запускается подготовка анимации.
        foreach(var v in m_Animations)
        {
            v.SetAnimationScale(m_AnimationScale);
            m_AnimatoinTime += v.AnimationTime;

            v.PrepareAnimation();
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


    #region Private API

    /// <summary>
    /// Метод, срабатывающий когда заканчивается одна анимация.
    /// </summary>
    private void OnSubAnimationEnded()
    {
        // Отписаться от события конца анимации.
        m_Animations[m_CurrentSubAnimation].OnEventEnd -= OnSubAnimationEnded;

        // Индекс анимации++.
        m_CurrentSubAnimation++;

        // Если анимация не последняя.
        if (m_CurrentSubAnimation < m_Animations.Length)
        {
            // Подписаться на событие конца анимации.
            m_Animations[m_CurrentSubAnimation].OnEventEnd += OnSubAnimationEnded;
            // Запустить анимацию.
            m_Animations[m_CurrentSubAnimation].StartAnimation();
        }
        // Если анимация была последней.
        else
        {
            // Завершить анимацию.
            StopAnimation();
        }
    }

    #endregion

}