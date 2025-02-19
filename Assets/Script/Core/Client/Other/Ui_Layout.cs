using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client
{

    public class Ui_Layout : MonoBehaviour
    {
        [field: SerializeField] public GameObject ObjContenet { get; private set; }
        public GameObject DefaultObj;
        GridLayoutGroup GridLayoutGroup => ObjContenet.GetComponent<GridLayoutGroup>();
        public void SetSize(int x, int y) => GridLayoutGroup.cellSize = new Vector2(x, y);
        public void AddSelectContent(GameObject gameObject)
        {
            gameObject.transform.SetParent(ObjContenet.transform, false);
        }
        public void AddSelectContent(GameObject gameObject, Action action)
        {
            AddSelectContent(gameObject);
            if(gameObject.TryGetComponent<Button>(out var t))
            {
                t.onClick.AddListener(()=> action());
            }
            else
            {
                EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry onChick = new EventTrigger.Entry();
                onChick.eventID = EventTriggerType.PointerClick; onChick.callback.AddListener((data) =>
                {
                    action();
                });
                eventTrigger.triggers.Add(onChick);
            }
          
        }
       
        public void AddSelectContent(NewSelectHandle obj)
        {
            var g = new GameObject();
            AddSelectContent(g);
            var image = g.AddComponent<RawImage>();
            if(obj.sprite !=null)
            image.texture = obj.sprite.texture;
            EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
            EventTrigger.Entry onChick = new EventTrigger.Entry();
            onChick.eventID = EventTriggerType.PointerClick; onChick.callback.AddListener((data) =>
            {
                obj.Chick();
            });
            eventTrigger.triggers.Add(onChick);
        }

        public static NewSelectHandle CreatNewSelectHandle(Sprite sprite, Action chick) => new NewSelectHandle(sprite, chick);
        public void Clear()
        {
            var L = new List<GameObject>();
            foreach (Transform obj in ObjContenet.transform) { L.Add(obj.gameObject); }
            foreach (GameObject i in L) { Destroy(i); };
        }
        private void OnEnable()
        {
            transform.SetSiblingIndex(-1);
        }
        private void OnDestroy()
        {
            Clear();
        }

        public class NewSelectHandle
        {
            public Sprite sprite;
            public Action Chick;
            public NewSelectHandle(Sprite sprite, Action chick)
            {
                this.sprite = sprite;
                Chick = chick;
            }
        }
    }
}
