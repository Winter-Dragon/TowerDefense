using UnityEngine;

/// <summary>
/// Класс анимации, последовательно заменяющий спрайты.
/// </summary>
public class AnimateSpriteFrames : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// Ссылка на Sprite Renderer.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// Спрайты для проигрывания анимации.
    /// </summary>
    [SerializeField] private Sprite[] m_Frames;

    #endregion


    #region Protected API

    /// <summary>
    /// Переопределяемый метод изменения фреймов анимации.
    /// </summary>
    protected override void AnimateFrame()
    {
        // Вычисляется целое значение порядкового номера спрайта.
        int frame = System.Convert.ToInt32(NormalizedAnimationTime * (m_Frames.Length - 1));

        // Назначается спрайт.
        m_Renderer.sprite = m_Frames[frame];
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


    #region Public API

    /// <summary>
    /// Метод, задающий новый массив со спрайтами для анимации.
    /// </summary>
    /// <param name="animationFrames">Новый массив со спрайтами для анимации.</param>
    public void SetNewAnimationFrames(Sprite[] animationFrames)
    {
        m_Frames = animationFrames;
    }

    #endregion

}