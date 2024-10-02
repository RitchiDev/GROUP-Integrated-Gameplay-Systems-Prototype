using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : GameBehaviour
{
    private Upgrade[] possibleUpgrades;

    private GameObject upgradeUI;
    private Image upgradeChoice;    // choice panel
    private List<Button> optionButtons;       // upgrade options objects

    private Image expFill;
    private TextMeshProUGUI levelText;

    private Queue<IStatHolder> choices;

    //public UpgradeSystem(Upgrade[] possibleUpgrades, GameObject upgradeUI)
    //{
    //    this.possibleUpgrades = possibleUpgrades;
    //    this.upgradeUI = GameObject.Instantiate(upgradeUI);
    //    Debug.Log("[UpgradeSystem] UpgradeUI: " + this.upgradeUI);
    //    ChoiceUISetup();

    //    // add listeners
    //    EventSystem<ExperienceData>.AddListener(EventType.EXP_GAINED, OnExpGained);
    //    EventSystem<LevelUpData>.AddListener(EventType.EXP_LEVELUP, OnLevelUp);
    //}

    public override void Awake()
    {
        // get all Upgrade ScriptableObjects to store in possibleUpgrades


        // create upgrade ui
        UISetUp();

        // add listeners
        EventSystem<ExperienceData>.AddListener(EventType.EXP_GAINED, OnExpGained);
        EventSystem<LevelUpData>.AddListener(EventType.EXP_LEVELUP, OnLevelUp);
    }

    public override void OnDestroy()
    {
        EventSystem<ExperienceData>.RemoveListener(EventType.EXP_GAINED, OnExpGained);
        EventSystem<LevelUpData>.RemoveListener(EventType.EXP_LEVELUP, OnLevelUp);
    }

    private void UISetUp()
    {
        choices = new Queue<IStatHolder>();

        upgradeUI = CanvasBuilder.Instance.AddEmptyObj("UpgradeUI");
        RectTransform upgradeUIRect = upgradeUI.GetComponent<RectTransform>();
        upgradeUIRect.anchoredPosition = new Vector2(0.5f, 0.5f);
        upgradeUIRect.anchorMin = Vector2.zero;
        upgradeUIRect.anchorMax = Vector2.one;
        upgradeUIRect.offsetMin = Vector2.zero;
        upgradeUIRect.offsetMax = Vector2.zero;

        upgradeChoice = CanvasBuilder.Instance.AddImageObj("Choice", Vector2.zero, Vector2.one, new Color32(200, 200, 200, 100), upgradeUI.transform);
        upgradeChoice.rectTransform.anchorMin = Vector2.zero;
        upgradeChoice.rectTransform.anchorMax = Vector2.one;
        upgradeChoice.rectTransform.offsetMin = Vector2.zero;
        upgradeChoice.rectTransform.offsetMax = Vector2.zero;
        HorizontalLayoutGroup choiceLayout = upgradeChoice.AddComponent<HorizontalLayoutGroup>();
        choiceLayout.childAlignment = TextAnchor.MiddleCenter;
        choiceLayout.padding.top = 27;
        choiceLayout.padding.bottom = 27;
        choiceLayout.padding.left = 48;
        choiceLayout.padding.right = 48;
        choiceLayout.childControlHeight = false;
        choiceLayout.childControlWidth = false;

        Image[] optionObjects = new Image[]
        {
            CanvasBuilder.Instance.AddImageObj("Option1", Vector2.zero, new Vector2(420, 540), new Color32(255,255,255,255), upgradeChoice.transform),
            CanvasBuilder.Instance.AddImageObj("Option2", Vector2.zero, new Vector2(420, 540), new Color32(255,255,255,255), upgradeChoice.transform),
            CanvasBuilder.Instance.AddImageObj("Option3", Vector2.zero, new Vector2(420, 540), new Color32(255,255,255,255), upgradeChoice.transform),
        };

        optionButtons = new List<Button>();

        // add a button component and text object for each option
        foreach (Image option in optionObjects)
        {
            optionButtons.Add(option.AddComponent<Button>());
            TextMeshProUGUI tempText = CanvasBuilder.Instance.AddTextObj("[Upgrade stats]", Vector2.zero, Vector2.one, option.transform);
            tempText.color = new Color32(0, 0, 0, 255);
            tempText.rectTransform.anchorMin = Vector2.zero;
            tempText.rectTransform.anchorMax = Vector2.one;
            tempText.enableAutoSizing = true;
            tempText.fontSizeMin = 6;
        }

        upgradeChoice.gameObject.SetActive(false);

        // create exp bar
        Image expBar = CanvasBuilder.Instance.AddImageObj("ExpBar", new Vector2(280, -64), new Vector2(480, 56), new Color32(0, 0, 0, 255), upgradeUI.transform);
        expBar.rectTransform.anchorMin = new Vector2(0, 1);
        expBar.rectTransform.anchorMax = new Vector2(0, 1);
        expBar.rectTransform.anchoredPosition = new Vector2(280, -64);

        expFill = CanvasBuilder.Instance.AddImageObj("ExpFill", Vector2.zero, Vector2.one, new Color32(60, 100, 60, 255), expBar.transform);
        expFill.rectTransform.anchorMin = new Vector2(0, 0);
        expFill.rectTransform.anchorMax = new Vector2(1, .8f);
        Sprite defaultSprite = Resources.Load<Sprite>("Sprites/DefaultSprite");
        if (defaultSprite == null) { Debug.Log("DefaultSprite is null"); }
        expFill.sprite = defaultSprite;
        expFill.type = Image.Type.Filled;
        expFill.fillMethod = Image.FillMethod.Horizontal;
        expFill.fillOrigin = (int)Image.OriginHorizontal.Left;
        expFill.fillAmount = 0;

        levelText = CanvasBuilder.Instance.AddTextObj("Lv: 0", new Vector2(-158, 0), new Vector2(56,56), expBar.transform);
        levelText.color = new Color32(255, 255, 255, 255);
        levelText.enableAutoSizing = true;
        levelText.fontSizeMin = 6;
        levelText.alignment = TextAlignmentOptions.Midline;
        levelText.alignment = TextAlignmentOptions.Center;
    }

    private void OnExpGained(ExperienceData _data)
    {
        Debug.Log("[UpgradeSystem] On exp gained");

        float fillAmount = (_data.currentExp % _data.neededExp) / _data.neededExp;
        expFill.fillAmount = fillAmount;
    }

    private void OnLevelUp(LevelUpData _data)
    {
        Debug.Log(_data.target);
        Debug.Log(_data.newLevel);
        Debug.Log(_data.currentLevel);

        int count = _data.newLevel - _data.currentLevel;

        //xpBar.GetComponentInChildren<TextMeshProUGUI>().text = "lv:" + _data.newLevel.ToString();

        Debug.Log(count);

        for(int i = 0; i < count; i++)
        {
            choices.Enqueue(_data.target);
            Debug.Log("Enqueued");
        }

        OfferChoice(choices.Peek());
    }

    private void OfferChoice(IStatHolder target)
    {
        upgradeChoice.gameObject.SetActive(true);

        foreach (Button option in optionButtons)
        {
            // choose a random upgrade
            int randomIndex = Random.Range(0, possibleUpgrades.Length);
            Upgrade newUpgrade = possibleUpgrades[randomIndex];

            // update visuals
            //TextMeshProUGUI textMesh = option.gameObject.GetComponentInChildren<TextMeshProUGUI>();

            string upgradeText = "";
            if(newUpgrade.damageChange != 0) { upgradeText = upgradeText + "Damage: +" + newUpgrade.damageChange.ToString() + "% <br>" ; }
            if(newUpgrade.healthChange != 0) { upgradeText = upgradeText + "Health: +" + newUpgrade.healthChange.ToString() + "% <br>" ; }
            if(newUpgrade.speedChange != 0) { upgradeText = upgradeText + "Speed: +" + newUpgrade.speedChange.ToString() + "% <br>" ; }
            if(newUpgrade.cooldownChange != 0) { upgradeText = upgradeText + "Cooldown: -" + newUpgrade.cooldownChange.ToString() + "% <br>" ; }
            if(newUpgrade.experienceBoostChange != 0) { upgradeText = upgradeText + "Experience Collection: +" + newUpgrade.experienceBoostChange.ToString() + "% <br>" ; }
            if(newUpgrade.elementChange != 0) { upgradeText = upgradeText + "Element: " + newUpgrade.elementChange.ToString() + "<br>" ; }

            option.GetComponentInChildren<TextMeshProUGUI>().text = upgradeText;

            // add correct listener to button unityevent
            option.onClick.AddListener(() => ApplyChoice(newUpgrade)) ;
        }
    }

    private void ApplyChoice(Upgrade chosenUpgrade)
    {
        choices.Dequeue();

        // TODO eventsystem
        EventSystem<Upgrade>.InvokeEvent(EventType.UPGRADE_AQCUIRED, chosenUpgrade);

        foreach(Button option in optionButtons)
        {
            option.onClick.RemoveAllListeners();
        }

        // if queue is not empty offer new choice
        if (choices.Count > 0) { OfferChoice(choices.Peek()); }
    }
}
