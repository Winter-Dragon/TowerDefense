using UnityEngine;

/// <summary>
/// Класс анимации, изменяющий цвет спрайта.
/// </summary>
public class AnimateSpriteColor : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// Ссылка на Sprite Renderer.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// Начальный цвет спрайта.
    /// </summary>
    [SerializeField] private Color m_colorA;

    /// <summary>
    /// Конечный цвет спрайта.
    /// </summary>
    [SerializeField] private Color m_colorB;

    /// <summary>
    /// Курва изменения цвета.
    /// </summary>
    [SerializeField] private AnimationCurve m_Curve;

    #endregion


    #region Protected API

    /// <summary>
    /// Переопределяемый метод изменения фреймов анимации.
    /// </summary>
    protected override void AnimateFrame()
    {
        m_Renderer.color = Color.Lerp(m_colorA, m_colorB, m_Curve.Evaluate(NormalizedAnimationTime));
    }

    /// <summary>
    /// Переопределяемый метод при завершении анимации.
    /// </summary>
    protected override void OnAnimationEnd() { }
    /// <summary>
    /// Переопределяемый метод при старте анимации.
    /// </summary>
    protected override void OnAnimationStart() { }
    /// <summary>
    /// Переопределение метода подготовки к анимации.
    /// </summary>
    public override void PrepareAnimation() { }

    #endregion

}