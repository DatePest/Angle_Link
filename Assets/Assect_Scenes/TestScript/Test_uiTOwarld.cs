using UnityEngine;

public class Test_uiTOwarld : MonoBehaviour
{
    public RectTransform uiTarget1, uiTarget2, uiTarget3;
    public RectTransform Find;

    public GameObject Fx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Find =GameObject.Find("Image").GetComponent<RectTransform>();
    }
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(120, 40, 120, 40), "T1"))
    //    {
    //        Test1(uiTarget1);
    //    }
    //    if (GUI.Button(new Rect(120, 90, 120, 40), "T2"))
    //    {
    //        Test1(uiTarget2);
    //    }
    //    if (GUI.Button(new Rect(120, 140, 120, 40), "T3"))
    //    {
    //        Test1(uiTarget3);
    //    }
    //}
    void Test1(GameObject Target , RectTransform Goto)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, Goto.position);
        float depth = 10.0f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));
        Target.transform.position = worldPosition;
    }
}
