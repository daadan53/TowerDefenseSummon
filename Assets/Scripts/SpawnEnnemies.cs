using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnnemies : MonoBehaviour
{
    public static SpawnEnnemies Instance;
    // Créer une liste de tous les lieux de spawn
    // Piocher aléatoirement dans cette liste
    // Instancier au lieu de spawn choisi

    // Faire en sorte d'attendre quelques secondes avant de repiocher 

    [SerializeField] List<GameObject> lSpawn;
    [SerializeField] List<GameObject> monstrePref;
    // Créer une liste qui récup les monstre instancié
    [SerializeField] List<GameObject> monsterToPutOnGame;
    public List<GameObject> monsterInGame;
    public int rdmSpawn;
    private int rdmMonster;
    public Vector3 spawnPos;
    private bool isSpawned;
    public int nbMonster;
    public int monsterCount;
    private float timer;
    [SerializeField] float maxTimer;
    public float timerVague;
    public float maxTimerVague;
    public int nbVague;
    private bool canTake;
    public bool startTimer;
    bool vagueEnd;
    public int manaToGive;
    private bool ManaGiven;

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
        monsterInGame = new List<GameObject>();
        monsterInGame.Capacity = nbMonster;
        ManaGiven = false;
        //isSpawned = true; // A enelever quand on mettra le timer
        canTake = true;
        startTimer = true;
        maxTimer = 1.5f;
        timer = maxTimer;
        nbVague = 0;
        nbMonster = 5;
        //maxTimerVague = 30;
        timerVague = maxTimerVague;

        manaToGive = 200;
    }

    // Update is called once per frame
    void Update()
    {
        // MonsterToPut doit récup le premier, puis deux premier, puis 3 premier...
        if (isSpawned)
        {

            if(canTake)
            {
                monsterToPutOnGame.Clear();
                for (int k = 0; k <= nbVague; k++)
                {
                    monsterToPutOnGame.Add(monstrePref[k]);
                }
                canTake = false;
            }
            

            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = maxTimer;
                rdmSpawn = Random.Range(0, lSpawn.Count);
                rdmMonster = Random.Range(0, monsterToPutOnGame.Count);
                var monster = Instantiate(monsterToPutOnGame[rdmMonster]);
                if (rdmSpawn == 0)
                {
                    monster.transform.position = new Vector3(-13, -9.5f, 0);
                    
                }
                else if(rdmSpawn == 1)
                {
                    monster.transform.position = new Vector3(17.3f, 0.15f, 0);
                }
                else if(rdmSpawn == 2)
                {
                    monster.transform.position = new Vector3(12.3f, 9.5f, 0);
                }
                else if (rdmSpawn == 3)
                {
                    monster.transform.position = new Vector3(5.5f, 9.5f, 0);
                }
                else
                {
                    monster.transform.position = new Vector3(-2.4f, -2.2f, 0);
                }
                //monster.transform.position = new Vector3(0,0,0);
                monsterInGame.Add(monster);
                monsterCount++;

            }

            // TOUS SPAWN
            if(monsterCount == nbMonster)
            {
                isSpawned = false;
                vagueEnd = true;
                monsterCount = 0;
            }
            
        }

        // TOUS MORT
        if(monsterInGame.Count == 0 && vagueEnd)
        {
            canTake = true;
            nbVague++;
            nbMonster += 5;
            startTimer = true;
            vagueEnd = false;
            manaToGive = 50;
        }

        if(startTimer)
        {
            timerVague -= Time.deltaTime;
            if (!ManaGiven)
            {
                GameControler.Instance.CastleMP += manaToGive;
                GameControler.Instance.heroMax += 2;
                ManaGiven = true;
            }
        }
        if (nbVague < 7 && timerVague <= 0)
        {
            isSpawned = true;
            startTimer = false;
            ManaGiven = false;
            timerVague = maxTimerVague;
        }
        if (nbVague == 7)
        {
            SceneManager.LoadScene("SceneVictory");
        }
    }

    // 30 sec de battement entre chaques vagues
}
