using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client.Login
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Ui_GameStart : MonoBehaviour , IPointerClickHandler
    {
        public Action ClickHandler;
        [SerializeField] float display_speed = 0.5f;
        CanvasGroup canvas_group;
        bool visible;
        Image Obj;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            canvas_group = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if(gameObject.activeSelf) { AlphaAnime(); }
        }

        void AlphaAnime()
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

        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
            ClickHandler?.Invoke();
        }
    }
}
