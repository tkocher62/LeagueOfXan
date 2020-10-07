﻿using Assets.Scripts;
using Assets.Scripts.General;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Entity
{
    internal static PlayerController singleton;

    internal float runSpeed;

    private float playerWidth, playerHeight;
    internal float health;

    private int frameIndx;

    public HealthBar healthBar;
    public Joystick joystick;
    public List<Button> characterButtons;
    public List<AnimationData> frames;

    private Dictionary<Character, List<Sprite>> pFrames;

    internal Vector2 startPos;
    private Vector2 movement;

    internal Character curCharacter;

    internal Rigidbody2D body;
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
        if (health > 0f)
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
        if (health > 0f)
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
        if (health > 0f)
        {
            body.velocity = new Vector2(movement.x * runSpeed, movement.y * runSpeed);

            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
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
        while (health > 0f)
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
        if (flashRed) FlashRed();
        float value = health - damage;
        health = Mathf.Clamp(value, 0f, 100f);
        healthBar.SetHealthBar(value);
        if (health == 0f)
        {
            KillPlayer();
        }
    }
}
