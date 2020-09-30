using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    internal static PlayerController singleton;

    internal float runSpeed;

    private float playerWidth, playerHeight;
    internal float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0f, 100f);
            healthBar.SetHealthBar(value);
            if (_health == 0f)
            {
                KillPlayer();
            }
        }
    }
    // todo: Health doesn't persist through scenes
    private static float _health = 100f;

    private int frameIndx;

    public HealthBar healthBar;
    public Joystick joystick;
    public List<Button> characterButtons;
    public List<AnimationData> frames;

    private Dictionary<Character, List<Sprite>> pFrames;

    private Vector2 screenBounds;
    internal Vector2 startPos;
    private Vector2 movement;

    internal Character curCharacter;

    private Rigidbody2D body;
    internal BoxCollider2D playerCollider;
    internal SpriteRenderer render;

    private object curCharacterScript;

    private const float charChangeDelay = 0f;
    internal float charChangeTimer;
    internal bool areButtonsEnabled;

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        singleton = this;

        health = 100f;

        runSpeed = 6f;

        areButtonsEnabled = true;

        body = GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

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

        ChangeCharacter(4);
    }

    private void Update()
    {
        if (_health > 0f)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;

            if (movement != Vector2.zero)
            {
                render.flipX = movement.x < 0f;
            }

            if (charChangeTimer != 0f)
            {
                charChangeTimer = Mathf.Clamp(charChangeTimer - Time.deltaTime, 0f, charChangeDelay);
            }
            else if (!areButtonsEnabled)
            {
                SetButtons(true);
            }
        }
    }

    public void Attack()
    {
        if (_health > 0f)
        {
            switch (curCharacter)
            {
                case Character.Hailey:
                    ((HaileyPlayerScript)curCharacterScript).Attack();
                    break;
                case Character.Jack:
                    ((JackPlayerScript)curCharacterScript).Attack();
                    break;
                case Character.Todd:
                    ((ToddPlayerScript)curCharacterScript).Attack();
                    break;
                case Character.Winston:
                    ((WinstonPlayerScript)curCharacterScript).Attack();
                    break;
                case Character.Xan:
                    ((XanPlayerScript)curCharacterScript).Attack();
                    break;
            }
        }
    }

    internal void Spawn()
    {
        gameObject.transform.position = startPos;
    }

    private void KillPlayer()
    {
        render.sprite = pFrames[curCharacter][0];
        body.rotation = 90f;
        render.color = Color.red;
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

    private void FixedUpdate()
    {
        if (_health > 0f)
        {
            body.velocity = new Vector2(movement.x * runSpeed, movement.y * runSpeed);

            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + playerHeight, screenBounds.y - playerHeight);
            transform.position = viewPos;
        }
    }

    private void SetButtons(bool enabled)
    {
        areButtonsEnabled = enabled;
        foreach (Button button in characterButtons)
        {
            button.interactable = enabled;
        }

        if (!enabled) charChangeTimer = charChangeDelay;
    }

    private IEnumerator<float> AnimationLoop()
    {
        while (_health > 0f)
        {
            render.sprite = GetNextFrame();
            yield return Timing.WaitForSeconds(0.4f);
        }
    }

    public void ChangeCharacter(int character)
    {
        Character c = (Character)character;
        HaileyPlayerScript hailey = gameObject.GetComponent<HaileyPlayerScript>();
        JackPlayerScript jack = gameObject.GetComponent<JackPlayerScript>();
        ToddPlayerScript todd = gameObject.GetComponent<ToddPlayerScript>();
        WinstonPlayerScript winston = gameObject.GetComponent<WinstonPlayerScript>();
        XanPlayerScript xan = gameObject.GetComponent<XanPlayerScript>();

        if (hailey != null) Destroy(hailey);
        else if (jack != null) Destroy(jack);
        else if (todd != null) Destroy(todd);
        else if (winston != null) Destroy(winston);
        else if (xan != null) Destroy(xan);

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
}
