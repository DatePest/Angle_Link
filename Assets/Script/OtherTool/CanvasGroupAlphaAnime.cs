using UnityEngine;

public class CanvasGroupAlphaAnime
{
    public float display_speed = 0.5f;
    bool visible;
    CanvasGroup canvas_group;
    GameObject gameObject;
    public CanvasGroupAlphaAnime(GameObject gameObject,float display_speed)
    {
        this.gameObject = gameObject;
        this.display_speed = display_speed;
        var temp = gameObject.GetComponent<CanvasGroup>();
        if (temp == null)
        {
            canvas_group = gameObject.AddComponent<CanvasGroup>();
        }
        else
        {
            canvas_group = temp;
        }
    }
    public void SetVisible(bool visi)
    {
        if (visi)
        {
            visible = true;
            gameObject.SetActive(true);
            canvas_group.alpha = 0;
        }
        else
        {
            visible = false;
            canvas_group.alpha = 1f;
        }
    }
    public void FlashingAlphaAnime()
    {
        if (canvas_group.alpha >= 1f)
        {
            visible = false;
        }
        else if (canvas_group.alpha <= 0.2f)
        {
            visible = true;
        }
        float add = visible ? display_speed : -display_speed;
        float alpha = Mathf.Clamp01(canvas_group.alpha + add * Time.deltaTime);
        canvas_group.alpha = alpha;

    }
    public void SwitchaAnime()
    {
        if (!gameObject.activeSelf) return;
        if (visible && canvas_group.alpha >= 1f) return;
        float add = visible ? display_speed : -display_speed;
        float alpha = Mathf.Clamp01(canvas_group.alpha + add * Time.deltaTime);
        canvas_group.alpha = alpha;
        if (!visible && alpha < 0.01f) gameObject.SetActive(false);
    }
}
