using System.Collections;
using System.Collections.Generic;
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

    #region Animators

    /// <summary>
    /// 
    /// </summary>
    protected override void AnimateFrame()
    {
        int frame = System.Convert.ToInt32(NormalizedAnimationTime * (m_Frames.Length - 1));
        m_Renderer.sprite = m_Frames[frame];
    }

    protected override void OnAnimationEnd()
    {
            
    }

    protected override void OnAnimationStart()
    {
            
    }

    public override void PrepareAnimation()
    {
            
    }

    #endregion

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