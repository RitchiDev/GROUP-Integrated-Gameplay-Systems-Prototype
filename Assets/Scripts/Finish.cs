using PlaceHolder;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Finish : GameBehaviour
{
    private Vector2 finishPos;
    private Vector2 playerPos;
    private GameObject instance;
    private Player player;

    // variables
    private float distanceFromFinish = 3f;

    private float timer;
    private float checkForPlayerInterval = .75f;

    private Button quitButton;

    public override void Start()
    {
        FinishSetup();
    }

    public override void Update()
    {
        if(timer < Time.time)
        {
            if (CheckForPlayer())
            {
                CreateFinishScreen();
            }

            timer = Time.time + checkForPlayerInterval;
        }

        if(Input.GetKeyUp(KeyCode.L)) 
        {
            CreateFinishScreen();
        }
    }

    public override void OnDestroy()
    {
        quitButton.onClick.RemoveAllListeners();
    }

    private void FinishSetup()
    {
        finishPos = new Vector2(74, 16);
        player = Game.GetObjectOfType<Player>();

        GameObject finishPrefab = Resources.Load<GameObject>("Finish");

        instance = GameObject.Instantiate(finishPrefab);

    }

    private bool CheckForPlayer()
    {
        playerPos = player.player.transform.position;
        float distance = Vector2.Distance(playerPos, finishPos);

        if(distance <= distanceFromFinish) { return true; }

        return false;
    }

    private void CreateFinishScreen()
    {
        Image backDrop = CanvasBuilder.Instance.AddImageObj("FinishScreen", Vector2.zero, Vector2.one, new Color32(200,200,200,255));
        backDrop.rectTransform.anchorMin = Vector2.zero;
        backDrop.rectTransform.anchorMax = Vector2.one;

        TextMeshProUGUI finishText = CanvasBuilder.Instance.AddTextObj("You reached the end!", new Vector2(0, 240f), new Vector2(1920/2, 1080/2), backDrop.transform, 56);
        finishText.alignment = TextAlignmentOptions.Center;
        finishText.alignment = TextAlignmentOptions.Midline;
        finishText.color = Color.black;

        Image quitImage = CanvasBuilder.Instance.AddImageObj("QuitImage", new Vector2(0, -240f), new Vector2(220, 120), new Color32(255, 255, 255, 255), backDrop.transform);
        quitImage.rectTransform.anchoredPosition = new Vector2(0, -240f);
        quitButton = quitImage.AddComponent<Button>();

        quitButton.onClick.AddListener(CloseGame);

        TextMeshProUGUI quitText = CanvasBuilder.Instance.AddTextObj("Quit", Vector2.zero, Vector2.one, quitImage.transform);
        quitText.rectTransform.anchorMin = Vector2.zero;
        quitText.rectTransform.anchorMax = Vector2.one;
        quitText.alignment = TextAlignmentOptions.Center;
        quitText.alignment = TextAlignmentOptions.Midline;
        quitText.enableAutoSizing = true;
        quitText.color = Color.black;
    }

    private void CloseGame()
    {
        Application.Quit();
    }
}
