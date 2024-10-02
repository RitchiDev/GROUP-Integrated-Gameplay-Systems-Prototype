using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : IQuestDisplay
{
    private TextMeshProUGUI textObj;

    public void Init(string _text)
    {
        GameObject parentObj = CanvasBuilder.Instance.SearchInCanvas("QuestField");

        if (parentObj == null)
        {
            Image parentImage = CanvasBuilder.Instance.AddImageObj("QuestField", Vector2.zero, new Vector2(550, 0), new Color32(0, 0, 0, 100));
            parentObj = parentImage.gameObject;

            VerticalLayoutGroup vertical = parentObj.AddComponent<VerticalLayoutGroup>();
            vertical.padding = new RectOffset(10, 10, 10, 10);
            vertical.childAlignment = TextAnchor.MiddleLeft;
            vertical.childControlWidth = true;

            ContentSizeFitter content = parentObj.AddComponent<ContentSizeFitter>();
            content.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform parentRect = parentObj.GetComponent<RectTransform>();
            parentRect.anchorMin = Vector2.one;
            parentRect.anchorMax = Vector2.one;
            parentRect.pivot = Vector2.one;
        }

        textObj = CanvasBuilder.Instance.AddTextObj(_text, Vector2.zero, Vector2.zero, parentObj.transform, 40);
        ContentSizeFitter cf = textObj.gameObject.AddComponent<ContentSizeFitter>();
        cf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    public void UpdateText(string _text)
    {
        textObj.text = _text;
    }

    public void End()
    {
        GameObject.Destroy(textObj.gameObject);
    }
}
