using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Base class for UI panels that can be hidden or shown, with a fade-in fade-out effect
/// </summary>

[RequireComponent(typeof(CanvasGroup))]
public class UI_Base : MonoBehaviour
{
    public float display_speed = 4f;

    public UnityAction onShowCallBack;
    public UnityAction onHideCallBack;

    protected CanvasGroup canvas_group;
    protected bool visible;

    protected virtual void Awake()
    {
        canvas_group = GetComponent<CanvasGroup>();
        canvas_group.alpha = 0f;
        visible = false;
    }

    protected virtual void Update()
    {
        AlphaAnime();
    }

    protected void AlphaAnime()
    {
        if (visible && canvas_group.alpha >= 1f) return;

        float add = visible ? display_speed : -display_speed;
        float alpha = Mathf.Clamp01(canvas_group.alpha + add * Time.deltaTime);
        canvas_group.alpha = alpha;
        if (!visible && alpha < 0.01f) AfterHide();

    }
    public virtual void Toggle(bool instant = false)
    {
        if (IsVisible())
            Hide(instant);
        else
            Show(instant);
    }

    public virtual void Show(bool instant = false)
    {
        visible = true;
        gameObject.SetActive(true);

        if (instant || display_speed < 0.01f)
            canvas_group.alpha = 1f;

        onShowCallBack?.Invoke();
    }

    public virtual void Hide(bool instant = false)
    {
        visible = false;
        if (instant || display_speed < 0.01f)
            canvas_group.alpha = 0f;


        onHideCallBack?.Invoke();
    }

    public void SetVisible(bool visi)
    {
        if (!visible && visi)
            Show();
        else if (visible && !visi)
            Hide();
    }

    public virtual void AfterHide()
    {
        gameObject.SetActive(false);
    }

    public bool IsVisible()
    {
        return visible;
    }

    public bool IsFullyVisible()
    {
        return visible && canvas_group.alpha > 0.99f;
    }

    public float GetAlpha()
    {
        return canvas_group.alpha;
    }
}
