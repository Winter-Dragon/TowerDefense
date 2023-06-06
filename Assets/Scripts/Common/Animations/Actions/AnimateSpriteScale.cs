using UnityEngine;

/// <summary>
/// Класс анимациии, изменяющий скейл спрайта.
/// </summary>
public class AnimateSpriteScale : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// Ссылка на Sprite Renderer объекта.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// Визуализация скейла спрайта по оси X.
    /// </summary>
    [SerializeField] private AnimationCurve m_CurveX;

    /// <summary>
    /// Визуализация скейла спрайта по оси Y.
    /// </summary>
    [SerializeField] private AnimationCurve m_CurveY;

    /// <summary>
    /// Вектор с текущим размером спрайта.
    /// </summary>
    private Vector2 m_InitialSize;

    #endregion


    #region Unity Events

    private void Start()
    {
        // Текущий размер = стартовый размер.
        m_InitialSize = m_Renderer.size;
    }

    #endregion


    #region Protected API

    /// <summary>
    /// Переопределение метода подготовки к анимации.
    /// </summary>
    public override void PrepareAnimation()
    {
        // X и Y изменяются в зависимости от 1-й точки скейта по курве.
        var x = m_CurveX.Evaluate(0) * m_InitialSize.x;
        var y = m_CurveY.Evaluate(0) * m_InitialSize.y;

        // Задаётся новый размер из имеющихся X и Y.
        m_Renderer.size = new Vector2(x, y);
    }

    /// <summary>
    /// Переопределяемый метод изменения фреймов анимации.
    /// </summary>
    protected override void AnimateFrame()
    {
        // X и Y изменяются в зависимости нормализованного значения анимации.
        var x = m_CurveX.Evaluate(NormalizedAnimationTime) * m_InitialSize.x;
        var y = m_CurveY.Evaluate(NormalizedAnimationTime) * m_InitialSize.y;

        // Задаётся новый скейл.
        m_Renderer.size = new Vector2(x, y);
    }

    /// <summary>
    /// Переопределяемый метод при завершении анимации.
    /// </summary>
    protected override void OnAnimationEnd() { }
    /// <summary>
    /// Переопределяемый метод при старте анимации.
    /// </summary>
    protected override void OnAnimationStart() { }

    #endregion

}