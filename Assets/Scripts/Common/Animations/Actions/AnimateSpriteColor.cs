using UnityEngine;

/// <summary>
/// ����� ��������, ���������� ���� �������.
/// </summary>
public class AnimateSpriteColor : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// ������ �� Sprite Renderer.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// ��������� ���� �������.
    /// </summary>
    [SerializeField] private Color m_colorA;

    /// <summary>
    /// �������� ���� �������.
    /// </summary>
    [SerializeField] private Color m_colorB;

    /// <summary>
    /// ����� ��������� �����.
    /// </summary>
    [SerializeField] private AnimationCurve m_Curve;

    #endregion


    #region Protected API

    /// <summary>
    /// ���������������� ����� ��������� ������� ��������.
    /// </summary>
    protected override void AnimateFrame()
    {
        m_Renderer.color = Color.Lerp(m_colorA, m_colorB, m_Curve.Evaluate(NormalizedAnimationTime));
    }

    /// <summary>
    /// ���������������� ����� ��� ���������� ��������.
    /// </summary>
    protected override void OnAnimationEnd() { }
    /// <summary>
    /// ���������������� ����� ��� ������ ��������.
    /// </summary>
    protected override void OnAnimationStart() { }
    /// <summary>
    /// ��������������� ������ ���������� � ��������.
    /// </summary>
    public override void PrepareAnimation() { }

    #endregion

}