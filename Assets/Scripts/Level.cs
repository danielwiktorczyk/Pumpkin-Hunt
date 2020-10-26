using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject gameController;
    private Cannon cannon;   
    private Score score;
    [SerializeField]
    private int startingLevel = 1;
    [SerializeField]
    private Text targetsLeftText;
    [SerializeField]
    private Text shotsLeftText;
    [SerializeField]
    private Text roundsText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject levelEndMenu;  
    [SerializeField]
    private GameObject gameEndMenu;
    public int currentLevel = 1;
    [SerializeField]
    private int targetObjective;
    [SerializeField]
    private int shotLimit;
    [SerializeField]
    public float globalSpeed = 1.0f;
    [SerializeField]
    public static float levelDuration = 30.0f;
    public static float timeLeftInLevel;
    public static bool isSpecialMode = false;
    private static float specialModeCooldownProgress = 0;
    [SerializeField]
    private static float specialModeCooldown = 10.0f;
    public bool isInBetweenLevels;
    public float timeBetweenLevels = 5.0f;
    public float timeLeftBetweenLevels = 0;
    public static bool isGamePaused;
    [SerializeField]
    public float difficultyCurve = 2.0f;

    void Start()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cannon = gameController.GetComponent<Cannon>();
        score = gameController.GetComponent<Score>();
    }

    public void StartNewGame()
    {
        currentLevel = startingLevel - 1;
        timeLeftInLevel = levelDuration;
        Score.scoreValue = 0;
        roundsText.text = $"Round: {currentLevel}";

        cannon.Reset();
        score.Reset();

        NewLevel();
        ResumeGame();
    }


    private void PauseGame()
    {
        PauseState();
        pauseMenu.SetActive(true);
    }

    private void PauseState()
    {
        UnityEngine.Cursor.visible = true;
        isGamePaused = true;
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        UnityEngine.Cursor.visible = false;
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        startMenu.SetActive(false);
        gameEndMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        PauseState();
        gameEndMenu.SetActive(true);
        UnityEngine.Cursor.visible = true;
        
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject target in targets)
            Destroy(target);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
            {
                PauseGame();
                return;
            }
        }


        if (isGamePaused)
        {
            return;
        }

        specialModeCooldownProgress -= Time.deltaTime;
        if (specialModeCooldownProgress <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
                InvokeSpecialMode();
            else {
                specialModeCooldownProgress = 0;
                isSpecialMode = false;
            }
           
        }
        
        TargetsLeft();
        //ShotsLeft();
        TimeLeftInLevel();
    }

    private void InvokeSpecialMode()
    {
        isSpecialMode = true;
        specialModeCooldownProgress = specialModeCooldown;
    }

    private void TimeLeftInLevel()
    {
        timeLeftInLevel -= Time.deltaTime;
        if(timeLeftInLevel <= 0)
        {
            if (Score.objectivesDestroyedInLevel < targetObjective)
            {
                EndGame();
            } else
            {
                EndLevel();
            }
        }
    }

    private void EndLevel()
    {
        PauseState();
        levelEndMenu.SetActive(true);
    }

    public void NewLevel()
    {
        levelEndMenu.SetActive(false);
        currentLevel += 1;
        globalSpeed = 1.0f + (currentLevel - 1) * difficultyCurve;
        timeLeftInLevel = levelDuration;

        timeLeftBetweenLevels = timeBetweenLevels;

        roundsText.text = $"Round: {currentLevel}";

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targets)
        {
            if (!target.name.Contains("Scarecrow"))
                Destroy(target.gameObject);
        }

        Score.NewLevel();
        Cannon.NewLevel();
        ResumeGame();
    }

    private void ShotsLeft()
    {
        // TODO lose if shots taken >= shot limit
        shotsLeftText.text =
                    $"Shots Left: {shotLimit - cannon.ShotsTaken()}";
    }

    private void TargetsLeft()
    {
        targetsLeftText.text = $"{Score.objectivesDestroyedInLevel} / {targetObjective}";
    }
}
