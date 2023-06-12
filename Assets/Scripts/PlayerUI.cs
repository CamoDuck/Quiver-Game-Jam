using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public Audio m_PlayerAudio;
    
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Canvas UI;
    RectTransform choice1Button;
    RectTransform choice2Button;
    RectTransform choice3Button;
    TextMeshProUGUI choice1Text;
    TextMeshProUGUI choice2Text;
    TextMeshProUGUI choice3Text;
    

    /// FOR UI TRANSITION ///
    [SerializeField] GameObject playerPortraitMove;
    [SerializeField] GameObject enemyPortraitMove;
    [SerializeField] GameObject choice1Box;
    [SerializeField] GameObject choice2Box;
    [SerializeField] GameObject choice3Box;
    [SerializeField] GameObject enemyHealthBox;
    [SerializeField] GameObject fadeOverlay;
    [SerializeField] CanvasGroup dialogueCanvasGroup; // new

 


    RectTransform dialogBox;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] TextMeshProUGUI enemyMaxHealthText; // new
    [SerializeField] Image enemyPortrait; 
    Image playerPortrait;

    TextMeshProUGUI dialogTextbox;
    [SerializeField] GameObject throwableCoworker;
    [SerializeField] Rigidbody2D body;
    [SerializeField] new BoxCollider2D collider;
    [SerializeField] float maxHealth;
    const float playerDamage = 2;
    [HideInInspector] public PlayerMovement moveScript;
    public GameOverScene gameOverScreen; 

    /// VARYING ///
    List<BaseCoworker> followers = new List<BaseCoworker>();
    BaseCoworker coworker; // current interacting coworker
    DialogChoices currentDialog;

    float currentHealth;
    const float anxietyThreshold = 50.0f;
    const float musicDelay = 1.0f; //Seconds

    void Start() {
        currentHealth = maxHealth;

        moveScript = GetComponent<PlayerMovement>();

        // HUD = transform.Find("OverworldHUD").GetComponent<Canvas>();

        // HUDHealthText = HUD.transform.Find("Anxiety Amount").GetComponent<TextMeshProUGUI>();

        choice1Button = (RectTransform)UI.transform.Find("Choice1");
        choice2Button = (RectTransform)UI.transform.Find("Choice2");
        choice3Button = (RectTransform)UI.transform.Find("Choice3");

        choice1Text = choice1Button.GetComponentInChildren<TextMeshProUGUI>();
        choice2Text = choice2Button.GetComponentInChildren<TextMeshProUGUI>();
        choice3Text = choice3Button.GetComponentInChildren<TextMeshProUGUI>();

        dialogBox = (RectTransform)UI.transform.Find("DialogueBox");
        playerPortrait = UI.transform.Find("PlayerPortrait").GetComponent<Image>();
        dialogBox = (RectTransform) UI.transform.Find("DialogueBox");
        dialogTextbox = dialogBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    IEnumerator buttonAnimation(int choice) {
        const float animationDuration = 0.5f;
        const float totalSpin = 360; // degrees
        float animationTimer = animationDuration;

        RectTransform button;
        if (choice == 1) {
            button = choice1Button;
            choice2Button.gameObject.SetActive(false);
            choice3Button.gameObject.SetActive(false);
        }
        else if (choice == 2) {
            button = choice2Button;
            choice1Button.gameObject.SetActive(false);
            choice3Button.gameObject.SetActive(false);
        }
        else {
            button = choice3Button;
            choice1Button.gameObject.SetActive(false);
            choice2Button.gameObject.SetActive(false);
        }

        Vector3 originalRotation = button.eulerAngles;
        Vector3 originalScale = button.localScale;

        while (animationTimer > 0) {
            float dt = Time.fixedDeltaTime;

            float angle = dt * totalSpin;
            button.Rotate(0,0, angle);

            Vector3 scaleDownAmount = Vector3.one * (dt / animationDuration);
            button.localScale = button.localScale - scaleDownAmount;

            animationTimer -= dt;

            yield return new WaitForFixedUpdate();
        }
        button.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        button.eulerAngles = originalRotation;
        button.localScale = originalScale;
        StartCoroutine(MoveToDesiredPosition(choice1Box, 0.0f));
        StartCoroutine(MoveToDesiredPosition(choice2Box, 0.15f));
        StartCoroutine(MoveToDesiredPosition(choice3Box, 0.3f));
        choice1Button.gameObject.SetActive(true);
        choice2Button.gameObject.SetActive(true);
        choice3Button.gameObject.SetActive(true);

    }

    IEnumerator shakePortrait(bool shakePlayer) {
        const float animationDuration = 0.2f;
        const float minShakeStrength = 250f;
        const float maxShakeStrength = 300;
        Vector2 shakeDirection = new Vector2(1,Random.Range(-1f,0)).normalized;
        float animationTimer = animationDuration;

        RectTransform portrait;
        if (shakePlayer) {
            portrait = playerPortrait.rectTransform;
            shakeDirection = new Vector2(-shakeDirection.x,shakeDirection.y);
        }
        else {
            portrait = enemyPortrait.rectTransform;
        }

        Vector2 originalPos = portrait.position;
        float shakeStrength = Random.Range(minShakeStrength, maxShakeStrength);

        while (animationTimer > 0) {
            float dt = Time.deltaTime;

            Vector2 shakeAmount = shakeDirection * shakeStrength * dt;
            shakeAmount = animationTimer > animationDuration/2? shakeAmount: -shakeAmount;
            portrait.Translate(shakeAmount);

            animationTimer -= dt;
            yield return new WaitForFixedUpdate();
        }

        portrait.position = originalPos;

    }

    public void addHealth(float value) {
        currentHealth = Mathf.Clamp(value+currentHealth, 0, maxHealth);
        updateHealthUI();
    }

    void updateHealthUI() {
        healthText.text = (maxHealth - currentHealth).ToString();
    }

    void updateEnemyHealthUI() {
        float enemyMaxHealth = coworker.getMaxHealth();
        float enemyHealth = coworker.getHealth();
        enemyHealthText.text = (enemyHealth).ToString();
        enemyMaxHealthText.text = "---- " + enemyMaxHealth.ToString();
    }
    public void TakeDamage() {
        float value = coworker.attackDamage;

        StartCoroutine(shakePortrait(true));
        currentHealth -= value;
        updateHealthUI();
        if (currentHealth <= 0) {
            Death();
        }
        else if (currentHealth < anxietyThreshold && m_PlayerAudio.isPlaying != "ANTY") {
            m_PlayerAudio.PlayAnxiety(musicDelay);
        }
    }

    void Death() {
        // send to game over screen
        SceneManager.LoadScene("GameOver");
    }

    int checkEffective(int choice) {
        Reaction coworkerType = coworker.getReactionType();
        Reaction choiceType = (Reaction)(choice-1);

        if (coworkerType == choiceType) {
            return 2;
        }

        if (coworkerType == Reaction.Sad) {
            return choiceType==Reaction.Joke? 1: 0;
        }
        else if (coworkerType == Reaction.Happy) {
            return choiceType==Reaction.Joke? 1: 0;
        }
        else { // Joke
            return choiceType==Reaction.Happy? 1: 0;
        }

    }

    public void onDialogClick(int choice) {
        m_PlayerAudio.PlaySelect();

        int Effectiveness = checkEffective(choice); // 0-2
        switch (Effectiveness) {
            case 0:
                dialogTextbox.text = "That was awkward...";
                break;
            case 1:
                dialogTextbox.text = "That was normal.";
                break;
            case 2:
                dialogTextbox.text = "They liked it alot!";
                break;
        }

        if (Effectiveness >= 1) {
            StartCoroutine(ThrowCoworkers());
        }
        
        StartCoroutine(buttonAnimation(choice));
        if (coworker != null) {
            updateEnemyPortrait((Reaction) (choice-1));
            if (Effectiveness <= 1) {
                TakeDamage();
            }
            updateChoices();
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Coworker") {
            coworker = other.GetComponent<BaseCoworker>();
            StartCoroutine(InteractionTransition(1));
        }
    }

     public void setMovementEnabled(bool value) {
        moveScript.playerCanMove = value;
    }

    void updateEnemyPortrait(Reaction choice) {
        Sprite sprite = coworker.getPortraitSprite(choice);
        enemyPortrait.sprite = sprite;
    }

    IEnumerator EndInteraction() {
        m_PlayerAudio.PlayBackground(musicDelay);
        coworker.followTarget = followers.Count==0? body: followers[followers.Count-1].body;
        followers.Add(coworker);
        coworker = null;
        int i = 0;
        yield return new WaitForSeconds(0.3f);
        while(i < 60)
        {
            dialogueCanvasGroup.alpha -= 0.04f;
            i++;
            yield return new WaitForEndOfFrame();
        }
        UI.transform.gameObject.SetActive(false);
        dialogueCanvasGroup.alpha = 1;
        collider.enabled = true;
        setMovementEnabled(true);
    }

    IEnumerator ThrowCoworkers() {
        Vector2 diretion = new Vector2(1,1).normalized;
        float maxTorque = 90;
        float minForceStregth = 4;
        float maxForceStrength = 7;
        float maxThrowDisplacement = 5;
        float maxWaitBetweenThrows = 0.3f;

        List<BaseCoworker> coworkerSynergy = new List<BaseCoworker>();

        // for (int i = 0; i < followers.Count; i++) {
        //     BaseCoworker follower = followers[i];
        //     Reaction reactionType = follower.getReactionType();
        //     if (reactionType == currentDialog.reaction) {
        //         coworkerSynergy.Add(follower);
        //     }
        // }

        coworkerSynergy = followers; // TEMP - this disables the classes system
        int count = coworkerSynergy.Count;
        Debug.Log("run");
        for (int i = 0; i < count; i++) {
            Transform clone = Instantiate(throwableCoworker).transform;
            clone.position = transform.position;
            Rigidbody2D body = clone.GetComponent<Rigidbody2D>();

            BaseCoworker currentFollower = coworkerSynergy[i];
            Sprite followerSprite = currentFollower.sprite;
            clone.GetComponent<SpriteRenderer>().sprite = followerSprite;
            float damage = currentFollower.attackDamage;
            if (coworker != null) {
                AttackCoworker(damage);
            }

            float displaceX = Random.Range(0, maxThrowDisplacement);
            float displaceY = Random.Range(0, maxThrowDisplacement);
            Vector2 throwDisplacement = new Vector2(displaceX, displaceY);
            Vector2 throwPosition = (Vector2)transform.position - throwDisplacement;

            Vector2 force = diretion * Random.Range(minForceStregth,maxForceStrength);
            body.AddForceAtPosition(force, throwPosition, ForceMode2D.Impulse);
            body.angularVelocity = Random.Range(-maxTorque, maxTorque);

            float waitTime = Random.Range(0, maxWaitBetweenThrows);
            yield return new WaitForSeconds(waitTime);
        }
        if (coworker != null) {
            AttackCoworker(playerDamage, true);
        }

    }


    void AttackCoworker(float damage, bool playAnimation=false) {
        bool isDead = coworker.Damage(damage);
        updateEnemyHealthUI();

        if (playAnimation) {
            StartCoroutine(shakePortrait(false));
        }

        if (isDead) {
            StartCoroutine(EndInteraction());
        }
    }

    IEnumerator InteractionTransition(int reverse)
    {
        setMovementEnabled(false);
        collider.enabled = false;
        if (currentHealth < anxietyThreshold) {
            m_PlayerAudio.PlayAnxiety(musicDelay);
        }
        else {
            if (Random.Range(0.0f, 1.0f) > 0.5f) {
                m_PlayerAudio.PlayBattle1(musicDelay);
            }
            else {
                m_PlayerAudio.PlayBattle2(musicDelay);
            }
        }
        updateHealthUI();
        updateEnemyHealthUI();
        updateEnemyPortrait(Reaction.Happy);
        updateChoices();
        StartCoroutine(MoveToDesiredPosition(dialogueBox, 0.0f));
        StartCoroutine(MoveToDesiredPosition(playerPortraitMove, 0.05f));
        StartCoroutine(MoveToDesiredPosition(enemyPortraitMove, 0.25f));
        StartCoroutine(MoveToDesiredPosition(enemyHealthBox, 0.25f));
        StartCoroutine(MoveToDesiredPosition(choice1Box, 0.7f));
        StartCoroutine(MoveToDesiredPosition(choice2Box, 0.85f));
        StartCoroutine(MoveToDesiredPosition(choice3Box, 1.0f));
        //fadeOverlay.gameObject.SetActive(true);
        UI.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.8f);
    }

    IEnumerator MoveToDesiredPosition(GameObject obj, float delay)
    {
        Vector2 desiredPos = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, obj.GetComponent<RectTransform>().anchoredPosition.y);

        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x, obj.GetComponent<RectTransform>().anchoredPosition.y - 500);
        yield return new WaitForSeconds(delay);
        while (obj.GetComponent<RectTransform>().anchoredPosition.y < (desiredPos.y - 4))
        {
            Vector2 interpolatedPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition, desiredPos, Time.deltaTime * 7.0f);
            obj.GetComponent<RectTransform>().anchoredPosition = interpolatedPosition; ;
            yield return new WaitForEndOfFrame();
        }
        obj.GetComponent<RectTransform>().anchoredPosition = desiredPos;
    }

    void updateChoices() {
        m_PlayerAudio.PlayDialogue(); //Audio
        DialogChoices[] currentDialog = coworker.GetInteraction();
        choice1Text.text = currentDialog[0].text;
        choice2Text.text = currentDialog[1].text;
        choice3Text.text = currentDialog[2].text;

        StartCoroutine(setReactionType());
    }

    IEnumerator setReactionType() {
        yield return new WaitForSeconds(1f);

        Reaction reactionType = (Reaction) Random.Range(0,3);
        coworker.setReactionType(reactionType);

        int randIndex;

        switch (reactionType) {
            case Reaction.Joke:
                randIndex = Random.Range(0,coworker.jokeR.Length);
                dialogTextbox.text = coworker.jokeR[randIndex].text;
                break;
            case Reaction.Happy:
                randIndex = Random.Range(0,coworker.happyR.Length);
                dialogTextbox.text = coworker.happyR[randIndex].text;
                break;
            case Reaction.Sad:
                randIndex = Random.Range(0,coworker.sadR.Length);
                dialogTextbox.text = coworker.sadR[randIndex].text;
                break;
        }
    }



}
