using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : GameBehaviour, IStatHolder, ILevelHolder
{
    // Note: Prototype player to be able to focus on the game systems.

    [Header("Component")]
    private Rigidbody2D rigidBody;
    private const string PREFAB_NAME = "Player";

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5;
    private Vector2 keyboardInput;

    [Header("Stats")]
    private Stats stats;
    public Stats CurrentStats => stats;
    public ILeveling Leveling { get; private set; }
    private Camera cam;

    public Player()
    {
        stats = new Stats(1, 100, 6, 1, Elements.NONE);
        Leveling = new Leveling(this);
    }

    public override void Awake()
    {
        Initialize();
    }

    public override void Start()
    {
        cam = GameObject.FindAnyObjectByType<Camera>();
    }

    public override void Update()
    {
        UpdateInput();
    }

    public override void FixedUpdate()
    {
        UpdatePosition();
    }

    public override void OnDestroy()
    {
        Leveling.RemoveListeners();
    }

    private void Initialize()
    {
        GameObject playerPrefab = Resources.Load<GameObject>(PREFAB_NAME);
        GameObject player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = PREFAB_NAME;

        rigidBody = player.GetComponent<Rigidbody2D>();
    }

    private void UpdateInput()
    {
        keyboardInput.x = Input.GetAxis("Horizontal");
        keyboardInput.y = Input.GetAxis("Vertical");

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    EventSystem<KeyCode>.InvokeEvent(EventType.KEY_PRESSED, key);
                }
            }
        }

        MouseInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0)) EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(0, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition), stats));
        if (Input.GetMouseButtonDown(1)) EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(1, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition), stats));
        if (Input.GetMouseButtonDown(2)) EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(2, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition), stats));
    }

    private void UpdatePosition()
    {
        if (rigidBody == null)
        {
            return;
        }

        Vector2 position = rigidBody.position;
        position += stats.GetSpeed() * keyboardInput * Time.fixedDeltaTime;

        rigidBody.MovePosition(position);
    }

    //public void SetMovementSpeed(float value)
    //{
    //    movementSpeed = value;
    //}

    public Stats GetStats()
    {
        return stats;
    }
}

public struct MouseClickEvent
{
    public int button;
    public Vector2 mousePosition;
    public Vector2 worldMousePosition;
    public Stats stats;

    public MouseClickEvent(int _button, Vector2 _mousePosition, Vector2 _worldMousePosition, Stats _stats)
    {
        button = _button;
        mousePosition = _mousePosition;
        worldMousePosition = _worldMousePosition;
        stats = _stats;
    }
}