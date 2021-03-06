﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueEvent
{
    GAME_START,
    FIRST_WARP,
    COIN_COLLECT,
    SEED_COLLECT,
    GUN_COLLECT,
    SWEETS_COLLECT,
    MONSTER_ENCOUNTER,
    MONSTER_KILL,
    ROBOT_TALK,
    BOOK_COLLECT,
    BOOK_TRANSLATE,
    INCORRECT_CODE,
    GAME_WIN
}


public class DialogueController : MonoBehaviour
{
    [SerializeField] private Font m_PastFont;
    [SerializeField] private Font m_FutureFont;

    [SerializeField] private Sprite m_PastBox;
    [SerializeField] private Sprite m_FutureBox;

    [SerializeField] private Sprite m_PastAlex;
    [SerializeField] private Sprite m_FutureAlex;
    [SerializeField] private Sprite m_Robot;

    [SerializeField] private AudioClip[] m_RobotSounds;

    public static DialogueController Instance { get; private set; }

    private Image m_DialogueBox;
    private Image m_CharacterIcon;

    private Animator m_Animator;

    private Text m_Dialogue;

    private PlayerController m_Player;

    private void Awake()
    {
        TimeController.OnTimeSwap += OnTimeSwap;
        Instance = this;

        m_Animator = GetComponent<Animator>();
        m_DialogueBox = GetComponent<Image>();
        m_Dialogue = GetComponentInChildren<Text>();
        m_CharacterIcon = transform.GetChild(0).GetComponent<Image>();

        m_Player = FindObjectOfType<PlayerController>();

    }

    private void OnTimeSwap()
    {
        //m_Animator.SetBool("isVisible", false);
        //StopAllCoroutines();

        if (TimeController.Instance.CurrentState == TimeState.FUTURE)
        {
            m_DialogueBox.sprite = m_FutureBox;
            m_Dialogue.font = m_FutureFont;
        }
        else
        {
            m_DialogueBox.sprite = m_PastBox;
            m_Dialogue.font = m_PastFont;
        }
    }

    public void StartDialogue(DialogueEvent d)
    {
        m_Animator.SetBool("isVisible", true);

        StopAllCoroutines();
        StartCoroutine(RunDialogue(d, 0));

    }

    private IEnumerator RunDialogue(DialogueEvent d, int index)
    {
        bool isFinished = false;
        switch (d)
        {
            case DialogueEvent.GAME_START:
                switch(index)
                {
                    case 0:
                        m_CharacterIcon.sprite = m_PastAlex;
                        m_Dialogue.text = "Well here I am. ...Where am I?";
                        break;
                    case 1:
                        m_Dialogue.text = "The air feels… different… I should check my stat bio connectivity.";
                        break;
                    case 2:
                        m_Dialogue.text = "Wow! I'm way back! Time to forward this space!";
                        break;
                    case 3:
                        m_Dialogue.text = "Let's see… LB should take me to where I want to go…";
                        isFinished = true;
                        m_Player.SetMovementActive(true);
                        break;
                }
                break;
            case DialogueEvent.FIRST_WARP:
                m_CharacterIcon.sprite = m_FutureAlex;
                m_Dialogue.text = "Wow! It's been a while since I did a proper time warp...";
                isFinished = true;
                break;
            case DialogueEvent.SWEETS_COLLECT:
                m_CharacterIcon.sprite = m_PastAlex;
                m_Dialogue.text = "Mmmm yummy sweets! But I could use something more... organic.";
                isFinished = true;
                break;
            case DialogueEvent.BOOK_COLLECT:
                m_CharacterIcon.sprite = m_PastAlex;
                m_Dialogue.text = "Seems like gobbledigook to me…";
                isFinished = true;
                break;
            case DialogueEvent.COIN_COLLECT:
                m_CharacterIcon.sprite = m_PastAlex;
                m_Dialogue.text = "Who wants to be a millionaire? Me, actually.";
                isFinished = true;
                break;
            case DialogueEvent.GUN_COLLECT:
                m_CharacterIcon.sprite = m_FutureAlex;
                m_Dialogue.text = "It seems I’ve found a weapon of mass destruction.";
                isFinished = true;
                break;
            case DialogueEvent.INCORRECT_CODE:
                m_CharacterIcon.sprite = m_FutureAlex;
                m_Dialogue.text = "Pox! I’m terrible at these things...";
                isFinished = true;
                break;
            case DialogueEvent.MONSTER_KILL:
                switch (index)
                {
                    case 0:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "Seed you later… no… Grow back from whence you… no… tree-t me better next time…";
                        break;
                    case 1:
                        m_Dialogue.text = "Ah, sod it.\nSo much for creativity.";
                        isFinished = true;
                        break;
                }
                break;
            case DialogueEvent.ROBOT_TALK:
                switch (index)
                {
                    case 0:
                        m_CharacterIcon.sprite = m_Robot;
                        m_Dialogue.text = "Hello! I am Epoch, your robot companion for the evening! How may I be of service sir?";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 1:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "Hello there. ...So what can you do?";
                        break;
                    case 2:
                        m_CharacterIcon.sprite = m_Robot;
                        m_Dialogue.text = "A plethora of commands sir, I am specialised for education and leisure, would you like a backrub?";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 3:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "I am quite fine, thank you.\nWhat could you teach me?";
                        break;
                    case 4:
                        m_CharacterIcon.sprite = m_Robot;
                        m_Dialogue.text = "Anything documented sir, from how to create a homemade bomb to the translation of ancient texts.";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 5:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "Hmm... You might come in handy.";
                        isFinished = true;
                        break;
                }
                break;
            case DialogueEvent.BOOK_TRANSLATE:
                switch(index)
                {
                    case 0:
                        m_CharacterIcon.sprite = m_Robot;
                        m_Dialogue.text = "My scan tells me a passage of this book seems alien to you. Do you need assistance?";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 1:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "Yes please, Epoch.";
                        break;
                    case 2:
                        m_CharacterIcon.sprite = m_Robot;
                        m_Dialogue.text = "TRANSLATING…";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 3:
                        m_Dialogue.text = "TRANSLATION COMPLETE\nThe text goes as follows:";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 4:
                        m_Dialogue.text = "“My first plays with crosses,\nMy second is eighty before score";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 5:
                        m_Dialogue.text = "My third is a handful\nAnd my fourth’s a winner.”";
                        SoundPlayer.Instance.PlayRandom(m_RobotSounds);
                        break;
                    case 6:
                        m_CharacterIcon.sprite = m_FutureAlex;
                        m_Dialogue.text = "...\nCryptic.";
                        isFinished = true;
                        break;
                }
                break;
        }
        yield return null;
        while (!Input.GetButtonDown("Fire1"))
        {
            yield return null;
        }
        
        if (isFinished)
        {
            CloseBox();
        }
        else
        {
            StartCoroutine(RunDialogue(d, index + 1));
        }
    }

    private void CloseBox()
    {
        m_Animator.SetBool("isVisible", false);
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        TimeController.OnTimeSwap -= OnTimeSwap;
    }
    
    private void OnEnable()
    {
        TimeController.OnTimeSwap += OnTimeSwap;
    }
}
