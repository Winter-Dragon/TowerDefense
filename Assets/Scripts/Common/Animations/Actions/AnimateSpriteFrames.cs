using UnityEngine;

/// <summary>
/// ����� ��������, ��������������� ���������� �������.
/// </summary>
public class AnimateSpriteFrames : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// ������ �� Sprite Renderer.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// ������� ��� ������������ ��������.
    /// </summary>
    [SerializeField] private Sprite[] m_Frames;

    #endregion


    #region Protected API

    /// <summary>
    /// ���������������� ����� ��������� ������� ��������.
    /// </summary>
    protected override void AnimateFrame()
    {
        // ����������� ����� �������� ����������� ������ �������.
        int frame = System.Convert.ToInt32(NormalizedAnimationTime * (m_Frames.Length - 1));

        // ����������� ������.
        m_Renderer.sprite = m_Frames[frame];
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


    #region Public API

    /// <summary>
    /// �����, �������� ����� ������ �� ��������� ��� ��������.
    /// </summary>
    /// <param name="animationFrames">����� ������ �� ��������� ��� ��������.</param>
    public void SetNewAnimationFrames(Sprite[] animationFrames)
    {
        m_Frames = animationFrames;
    }

    #endregion

}