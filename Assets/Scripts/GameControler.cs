using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler Instance;
    [SerializeField] private Button Summon1Button;
    [SerializeField] private Button Summon2Button;
    [SerializeField] private Button Summon3Button;
    [SerializeField] private Button Summon4Button;
    [SerializeField] private GameObject Summon1Prefab;
    [SerializeField] private GameObject Summon2Prefab;
    [SerializeField] private GameObject Summon3Prefab;
    [SerializeField] private GameObject Summon4Prefab;
    [SerializeField] private TMP_Text TimerVagueText;
    [SerializeField] private TMP_Text MPText;
    [SerializeField] private TMP_Text HPText;
    [SerializeField] private TMP_Text VagueCount;
    [SerializeField] private TMP_Text limitText;
    [SerializeField] private TMP_Text summonCountText;
    [SerializeField] private TMP_Text summon1CountText;
    [SerializeField] private TMP_Text summon2CountText;
    [SerializeField] private TMP_Text summon3CountText;
    [SerializeField] private TMP_Text summon4CountText;
    [SerializeField] private GameObject particleSys;
    [SerializeField] private GameObject pauseMenu;
    public List<GameObject> SummonInGame;
    public GameObject player;
    private Animator animator;
    private SpawnEnnemies spawnEnnemies;
    private bool Summon1;
    private bool Summon2;
    private bool Summon3;
    private bool Summon4;

    public int CastleHP;
    public int CastleMP;
    private bool isPause;
    public int heroCount;
    public int heroMax;
    private bool canInstantiate;
    public int summon1Count { get; set;} 
    public int summon2Count;
    public int summon3Count;
    public int summon4Count;
    private int totalSummonOnGame;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        ButtonSet();
        SummonInGame = new List<GameObject>();
        SummonInGame.Capacity = 20;
        CastleHP = 10;
        isPause = false;
        animator = player.GetComponent<Animator>();
        spawnEnnemies = SpawnEnnemies.Instance;
        canInstantiate = true;
        heroMax = 0;
        totalSummonOnGame = 2;
    }

    // Update is called once per frame
    void Update()
    {
        summonCountText.text = "Summon : " + heroCount + "/" + heroMax;
        summon1CountText.text = summon1Count + "/" + totalSummonOnGame;
        summon2CountText.text = summon2Count + "/" + totalSummonOnGame;
        summon3CountText.text = summon3Count + "/" + totalSummonOnGame;
        summon4CountText.text = summon4Count + "/" + totalSummonOnGame;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           //Pas en pause
            Pause();
        }

        MPText.text = CastleMP.ToString();
        HPText.text = CastleHP.ToString();
        VagueTimerText();
        Summon();
        if (CastleHP <= 0)
        {
            SceneManager.LoadScene("SceneGameOver");
        }

        if(heroCount == heroMax)
        {
            canInstantiate = false;
        }
        else{canInstantiate = true;}
    }

    //Charge les evenement à l'appui du boutton
    public void ButtonSet()
    {
        Summon1Button.onClick.AddListener(Summon1ButtonOnClick); 
        Summon2Button.onClick.AddListener(Summon2ButtonOnClick);
        Summon3Button.onClick.AddListener(Summon3ButtonOnClick);
        Summon4Button.onClick.AddListener(Summon4ButtonOnClick);
    }
    public void Summon1ButtonOnClick()
    {
        if (CastleMP >= 100 && canInstantiate)
        {
            Summon1 = true;
            limitText.text = "";
        } 
        else if (!canInstantiate)
        {
            StartCoroutine(LimitTxt());
        }
    }
    public void Summon2ButtonOnClick()
    {
        if (CastleMP >= 200 && canInstantiate)
        {
            Summon2 = true;
        }
        else if (!canInstantiate)
        {
            StartCoroutine(LimitTxt());
        }
    }
    public void Summon3ButtonOnClick()
    {
        if (CastleMP >= 300 && canInstantiate)
        {
            Summon3 = true;
        }
        else if (!canInstantiate)
        {
            StartCoroutine(LimitTxt());
        }
    }
    public void Summon4ButtonOnClick()
    {
        if (CastleMP >= 400 && canInstantiate)
        {
            Summon4 = true;
        }
        else if (!canInstantiate)
        {
            StartCoroutine(LimitTxt());
        }
    }
    public void Summon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Summon1 && !isPause && (summon1Count < totalSummonOnGame))
            {
                summon1Count++;
                CastleMP -= 100;
                var prefab = Instantiate(Summon1Prefab);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                prefab.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                SummonInGame.Add(prefab);
                Summon1 = false;
                Instantiate(particleSys, prefab.transform.position, Quaternion.identity);
                heroCount++;
                StartCoroutine(AnimPlayer());


            }
            else if (Summon2 && !isPause && (summon2Count < totalSummonOnGame))
            {
                summon2Count++;
                CastleMP -= 200;
                var prefab = Instantiate(Summon2Prefab);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                prefab.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                SummonInGame.Add(prefab);
                Summon2 = false;
                Instantiate(particleSys, prefab.transform.position, Quaternion.identity);
                heroCount++;
                StartCoroutine(AnimPlayer());
            }
            else if (Summon3 && !isPause && (summon3Count < totalSummonOnGame))
            {
                summon3Count++;
                CastleMP -= 300;
                var prefab = Instantiate(Summon3Prefab);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                prefab.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                SummonInGame.Add(prefab);
                Summon3 = false;
                Instantiate(particleSys, prefab.transform.position, Quaternion.identity);
                heroCount++;
                StartCoroutine(AnimPlayer());
            }
            else if (Summon4 && !isPause && (summon4Count < totalSummonOnGame))
            {
                summon4Count++;
                CastleMP -= 400;
                var prefab = Instantiate(Summon4Prefab);
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = -Camera.main.transform.position.z;
                prefab.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                SummonInGame.Add(prefab);
                Summon4 = false;
                Instantiate(particleSys, prefab.transform.position, Quaternion.identity);
                heroCount++;
                StartCoroutine(AnimPlayer());
            }
        }
    }
    public void VagueTimerText()
    {
        if (SpawnEnnemies.Instance.startTimer)
        {
            TimerVagueText.text = "Next wave in " + Mathf.Ceil(SpawnEnnemies.Instance.timerVague) + " seconds";
            VagueCount.text = "Vague " + (SpawnEnnemies.Instance.nbVague+1) + "/7";
        }
        else
        {
            TimerVagueText.text = "";
            VagueCount.text = "";
        }
    }

    public void Pause()
    {
         if(!isPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f; // On arrete le temps
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        isPause = !isPause;
    }

    public void QuitApp() 
    {
        Application.Quit();
    }

    IEnumerator AnimPlayer()
    {
        animator.enabled = true;

        yield return new WaitForSeconds(0.5f);

        animator.enabled = false;
    }

    IEnumerator LimitTxt()
    {
        limitText.text = "Summoning limit reached";

        yield return new WaitForSeconds(1f);

        limitText.text = "";
    }

}
