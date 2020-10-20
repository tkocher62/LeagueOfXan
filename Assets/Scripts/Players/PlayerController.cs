using Assets.Scripts;
using Assets.Scripts.Enemies;
using Assets.Scripts.General;
using Assets.Scripts.UI;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{
    internal static PlayerController singleton;

    internal float movementSpeed;

    private float playerWidth, playerHeight;
    internal float health;

    private int frameIndx;

    public HealthBar healthBar;
    public Joystick joystick;
    public List<Button> characterButtons;
    public List<AnimationData> frames;

    private Dictionary<Character, List<Sprite>> pFrames;

    internal Vector2 startPos;
    internal Vector2 movement;

    internal Character curCharacter;

    internal Rigidbody2D body;
    internal BoxCollider2D playerCollider;
    internal SpriteRenderer render;

    private PlayerScript curCharacterScript;

    private const float charChangeDelay = 0f;

    [Serializable]
    public struct AnimationData
    {
        public Character character;
        public List<Sprite> frames;
    }

    public enum Character
    {
        None = -1,
        Hailey = 0,
        Jack = 1,
        Todd = 2,
        Winston = 3,
        Xan = 4
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        singleton = this;

        health = 100f;

        movementSpeed = 6f;

        body = GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();

        startPos = gameObject.transform.position;

        SpriteRenderer renderer = transform.GetComponent<SpriteRenderer>();
        playerWidth = renderer.bounds.extents.x;
        playerHeight = renderer.bounds.extents.y;

        pFrames = new Dictionary<Character, List<Sprite>>();
        foreach (AnimationData data in frames)
        {
            pFrames[data.character] = data.frames;
        }

        Timing.RunCoroutine(AnimationLoop().CancelWith(gameObject));

        ChangeCharacter((int)Character.Xan);
    }

    private void Update()
    {
        if (health > 0f)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            if (movement != Vector2.zero)
            {
                render.flipX = movement.x < 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        Move(body, movement, playerWidth, playerHeight, movementSpeed);
    }

    public void Attack()
    {
        curCharacterScript.Attack();
    }

    internal void Spawn()
    {
        gameObject.transform.position = startPos;
    }

    private void KillPlayer()
    {
        render.sprite = pFrames[curCharacter][0];
        body.rotation = render.flipX ? -90f : 90f;
        render.color = Color.red;

        CanvasController.singleton.deathScreen.SetActive(true);

        foreach (Enemy obj in GameObject.FindObjectsOfType<Enemy>())
        {
            Destroy(obj);
        }
        movement = Vector2.zero;

        SaveManager.saveData.deathCount++;
        if (SaveManager.saveData.deathCount >= 100)
        {
            AchievementManager.Achieve("die_100_times");
        }
        SaveManager.SaveData();

        Destroy(this);
    }

    private Sprite GetNextFrame()
    {
        frameIndx++;
        if (frameIndx >= pFrames[curCharacter].Count)
        {
            frameIndx = 0;
        }
        return pFrames[curCharacter][frameIndx];
    }

    private void SetButtons(bool enabled)
    {
        foreach (Button button in characterButtons)
        {
            button.interactable = enabled;
        }

        if (!enabled) Timing.CallDelayed(charChangeDelay, () => SetButtons(true));
    }

    private IEnumerator<float> AnimationLoop()
    {
        while (this)
        {
            render.sprite = GetNextFrame();
            yield return Timing.WaitForSeconds(0.35f);
        }
    }

    public void ChangeCharacter(int character)
    {
        Character c = (Character)character;
        Destroy(GetComponent<PlayerScript>());

        switch (c)
        {
            case Character.Hailey:
                curCharacterScript = gameObject.AddComponent<HaileyPlayerScript>();
                break;
            case Character.Jack:
                curCharacterScript = gameObject.AddComponent<JackPlayerScript>();
                break;
            case Character.Todd:
                curCharacterScript = gameObject.AddComponent<ToddPlayerScript>();
                break;
            case Character.Winston:
                curCharacterScript = gameObject.AddComponent<WinstonPlayerScript>();
                break;
            case Character.Xan:
                curCharacterScript = gameObject.AddComponent<XanPlayerScript>();
                break;
        }

        curCharacter = c;
        render.sprite = pFrames[c][0];
        SetButtons(false);
    }

    internal void Damage(float damage, bool flashRed = true)
    {
        float value = health - damage;
        health = Mathf.Clamp(value, 0f, 100f);
        if (flashRed && health > 0) FlashRed();
        healthBar.SetHealthBar(value);
        if (health == 0f)
        {
            KillPlayer();
        }
    }

    internal void Heal(float amount)
    {
        float value = health + amount;
        health = Mathf.Clamp(value, 0f, 100f);
        healthBar.SetHealthBar(value);
    }
}
