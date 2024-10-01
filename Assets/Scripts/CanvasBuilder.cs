using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBuilder : GameBehaviour
{
    public static CanvasBuilder Instance { get; private set; }

    private Canvas canvas;

    public override void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Dispose();
            return;
        }

        CreateCanvas();
    }

    private void CreateCanvas()
    {
        //If canvas is found just use that one.
        canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas != null) return;

        GameObject canvasObj = new GameObject("Canvas");

        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();
    }

    public TextMeshProUGUI AddTextObj(string _text, Vector2 _position, Vector2 _size, Transform _parent = null, int _fontSize = 36)
    {
        GameObject textObj = new GameObject(_text + " < textObj");

        //If no parent is provided. Set canvas as default parent.
        if (_parent == null) _parent = canvas.transform;
        textObj.transform.parent = _parent;

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = _text;
        tmp.fontSize = _fontSize;
        
        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.localPosition = _position;
        rect.sizeDelta = _size;
        rect.anchoredPosition = Vector2.zero;

        return tmp;
    }

    public GameObject AddEmptyObj(string _objName)
    {
        return AddEmptyObj(_objName, new Vector2(0, 0), new Vector2(100, 100));
    }

    public GameObject AddEmptyObj(string _objName, Vector2 _position, Vector2 _size, Transform _parent = null)
    {
        GameObject emptyObj = new GameObject(_objName);

        //If no parent is provided. Set canvas as default parent.
        if (_parent == null) _parent = canvas.transform;
        emptyObj.transform.parent = _parent;

        RectTransform rect = emptyObj.AddComponent<RectTransform>();
        rect.localPosition = _position;
        rect.sizeDelta = _size;
        rect.anchoredPosition = Vector2.zero;

        return emptyObj;
    }

    public Image AddImageObj(string _objName, Vector2 _position, Vector2 _size, Color32 _color, Transform _parent = null)
    {
        GameObject emptyObj = AddEmptyObj(_objName, _position, _size, _parent);

        Image image = emptyObj.AddComponent<Image>();
        image.color = _color;

        return image;
    }

    public GameObject SearchInCanvas(string _objName)
    {
        return SearchFromObject(canvas.gameObject, _objName);
    }

    public GameObject SearchFromObject(GameObject _objToSearchFrom, string _objName)
    {
        Transform searchObj = _objToSearchFrom.transform.Find(_objName);

        if (searchObj == null) return null;
        else return searchObj.gameObject;
    }
}
