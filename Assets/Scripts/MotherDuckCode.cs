using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MotherDuckCode : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float movementX;
    private float movementY;
    private bool m_FacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Shooting shooting;

    private int fishScore = 0;
    public BabyDucksCode babyDucks;

    private int fishPerDuckling = 2;
    private int fishPerLevel = 20;

    [SerializeField] private int currentLevel = 0;   // 0,1,2,3,4
    [SerializeField] private string winSceneName = "WinScene";

    [SerializeField] private Color level0Color = Color.yellow;
    [SerializeField] private Color level1Color = new Color(1f, 0.6f, 0.8f);
    [SerializeField] private Color level2Color = Color.red;
    [SerializeField] private Color level3Color = Color.blue;
    [SerializeField] private Color level4Color = new Color(1f, 0.84f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shooting = GetComponent<Shooting>();

        ApplyLevelVisuals();
    }

    public void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }

    void Update()
    {
        animator.SetBool("isMoving", movementX != 0f || movementY != 0f);

        if (movementX > 0 && !m_FacingRight)
            Flip();
        else if (movementX < 0 && m_FacingRight)
            Flip();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * speed, movementY * speed);
    }

    public void IncreaseScore(int value)
    {
        fishScore += value;
        Debug.Log("fish eaten: " + fishScore);

        if (fishScore % fishPerDuckling == 0)
        {
            if (babyDucks != null)
                babyDucks.addDuck();
            else
                Debug.LogWarning("MotherDuckCode: babyDucks reference is not assigned in the Inspector.");
        }

        int nextLevelThreshold = (currentLevel + 1) * fishPerLevel;
        int winThreshold = (4 + 1) * fishPerLevel;    // Level 4 + 20 more fish → 100 total

        // Level up while below level 4
        if (currentLevel < 4 && fishScore >= nextLevelThreshold)
        {
            LevelUp();
        }

        // After reaching level 4, 20 more fish → win
        if (currentLevel == 4 && fishScore >= winThreshold)
        {
            WinGame();
        }
    }

    public void DucklingEaten()
    {
        fishScore -= fishPerDuckling;
        if (fishScore < 0)
            fishScore = 0;

        Debug.Log("Duckling eaten. Fish score: " + fishScore);
    }

    private void LevelUp()
    {
        currentLevel++;
        Debug.Log("Level up! New level: " + currentLevel);

        if (Audio.Instance != null)
        Audio.Instance.LevelUpSound(currentLevel);

        if (babyDucks != null)
            babyDucks.removeAllDucks();

        ApplyLevelVisuals();

        if (currentLevel == 2)
        {
            SharkMovement.speedMultiplier += 0.25f;
        }

        if (currentLevel == 4 && shooting != null)
        {
            shooting.SetCooldown(0.3f);
        }
    }

    private void WinGame()
    {
        Debug.Log("YOU WIN! Reached level 4 + 20 more fish.");
        if (Audio.Instance != null)
        Audio.Instance.WinSound();

        if (!string.IsNullOrEmpty(winSceneName))
        {
            SceneManager.LoadScene(winSceneName);
        }
    }

    private void ApplyLevelVisuals()
    {
        Color c = level0Color;

        if (currentLevel == 0) c = level0Color;
        else if (currentLevel == 1) c = level1Color;
        else if (currentLevel == 2) c = level2Color;
        else if (currentLevel == 3) c = level3Color;
        else if (currentLevel == 4) c = level4Color;

        if (spriteRenderer != null)
            spriteRenderer.color = c;

        if (babyDucks != null)
            babyDucks.SetDuckColor(c);
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
