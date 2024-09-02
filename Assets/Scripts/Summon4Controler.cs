using System.Collections.Generic;
using UnityEngine;

public class Summon4Controler : MonoBehaviour
{
    [SerializeField] private Animator Summon4Animator;
    [SerializeField] private RuntimeAnimatorController Run;
    [SerializeField] private RuntimeAnimatorController Iddle;
    [SerializeField] private RuntimeAnimatorController Attack;
    [SerializeField] private GameObject Projectile;

    //private NavMeshAgent agent;
    [SerializeField] private List<string> STATES = new List<string>();
    private List<GameObject> Projectiles = new List<GameObject>();
    public string ActualState;
    private List<GameObject> EnnemisInGame;
    public GameObject Target;
    public Vector3 Targetpos;
    private float Range;
    private float RangeAttack;
    private int Damage;
    private float AttackSpeed;
    public Vector3 IddlePosition;
    private float AttackTimer;
    public bool TargetAlive;
    private float scale;
    private float speed = 5f;
    public bool isDragging = false;
    private Vector3 offset;
    public int SellPrice;
    public float Timer;
    private float MaxTimer = 5;


    // Start is called before the first frame update
    void Start()
    {
        Timer = MaxTimer;
        IddlePosition = transform.position;
        ActualState = STATES[0];
        if (tag == "Summon1")
        {
            Range = 5;
            RangeAttack = 4;
            Damage = 4;
            AttackSpeed = 1;
            scale = 3;
            SellPrice = 50;
        }
        //RANGE
        else if (tag == "Summon2")
        {
            Range = 3;
            RangeAttack = 2;
            Damage = 4;
            AttackSpeed = 1.5f;
            scale = 5;
            SellPrice = 100;
        }
        else if (tag == "Summon3")
        {
            Range = 5;
            RangeAttack = 5;
            AttackSpeed = 2;
            Damage = 5;
            scale = 3;
            SellPrice = 150;
        }
        //MELE DPS
        else if (tag == "Summon4")
        {
            Range = 4;
            RangeAttack = 2;
            Damage = 10;
            AttackSpeed = 1f;
            scale = 5;
            SellPrice = 200;
        }
        AttackTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        EnnemisInGame = SpawnEnnemies.Instance.monsterInGame;
        MachineEtats();
        MirorEffect();
    }
    public void ResetTarget()
    {
        Target = null;
    }
    public void MachineEtats()
    {
        // NONE
        if (ActualState == STATES[0])
        {
            ActualState = STATES[1];
        }

        // IDLE
        else if (ActualState == STATES[1])
        {
            GetComponent<Animator>().runtimeAnimatorController = Iddle;
            for (int i = 0; i < EnnemisInGame.Count; i++)
            {
                if (Range >= Vector2.Distance(transform.position, EnnemisInGame[i].transform.position))
                {
                    if (tag == "Summon2" || tag == "Summon4")
                    {
                        if (EnnemisInGame[i].tag != "Fly")
                        {
                            Target = EnnemisInGame[i];
                            TargetAlive = true;
                            ActualState = STATES[2];
                            break;
                        }
                    }
                    else
                    {
                        Target = EnnemisInGame[i];
                        TargetAlive = true;
                        ActualState = STATES[2];
                        break;
                    }
                }
            }
        }

        // RUN
        else if (ActualState == STATES[2])
        {
            GetComponent<Animator>().runtimeAnimatorController = Run;
            if (!TargetAlive)
            {
                ActualState = STATES[4];
            }
            else
            {
                Movement(Target.transform.position);
                if (RangeAttack >= Vector2.Distance(transform.position, Target.transform.position))
                {
                    ActualState = STATES[3];
                }
            }
            if (Target.GetComponent<EnnemiController>().monsterHP <= 0)
            {
                TargetAlive = false;
            }
        }

        // ATTACK
        else if (ActualState == STATES[3])
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                TargetAlive = false; 
                Timer = MaxTimer;
            }
            if (!TargetAlive)
            {
                ActualState = STATES[4];
            }
            else 
            {
                GetComponent<Animator>().runtimeAnimatorController = Attack;
                AttackTimer -= Time.deltaTime;
                
                if (AttackTimer < 0)
                {
                    if (tag != "Summon2" && tag != "Summon4")
                    {
                        GameObject projectile = Instantiate(Projectile);
                        projectile.transform.position = transform.position;
                        projectile.GetComponent<ProjectileControler>().Target = Target;
                        projectile.GetComponent<ProjectileControler>().Damage = Damage;
                        Projectiles.Add(projectile);
                    }
                    else
                    {
                        Target.GetComponent<EnnemiController>().monsterHP -= Damage;
                    }
                    AttackTimer = AttackSpeed;
                }
                if (Target.GetComponent<EnnemiController>().monsterHP <= 0)
                {
                    MonsterDestroy();
                }

            }

            
        }

        // RETURN
        else if (ActualState == STATES[4])
        {
            Movement(IddlePosition);
            this.GetComponent<Animator>().runtimeAnimatorController = Run as RuntimeAnimatorController;
            var x = transform.position.x - IddlePosition.x;
            var y = transform.position.y - IddlePosition.y;
            if (Mathf.Abs(x) <= 0.1f && Mathf.Abs(y) <= 0.1f)
            {
                ActualState = STATES[1];
            }
        }
    }
    public void MonsterDestroy()
    {
        GameControler.Instance.CastleMP += Target.GetComponent<EnnemiController>().monsterMana;
        for (int i = 0; i < GameControler.Instance.SummonInGame.Count; i++)
        {
            if(gameObject != GameControler.Instance.SummonInGame[i])
            {
                if(Target == GameControler.Instance.SummonInGame[i].GetComponent<Summon4Controler>().Target)
                {
                    GameControler.Instance.SummonInGame[i].GetComponent<Summon4Controler>().TargetAlive = false;
                }
            }
        }
        TargetAlive = false;
        SpawnEnnemies.Instance.monsterInGame.Remove(Target);
        GameObject.Destroy(Target);
    }

    public void MirorEffect()
    {
        if (TargetAlive)
        {
            var TargetDir = Target.transform.position - gameObject.transform.position;
            TargetDir.Normalize(); // Normalise les vecteurs pour avoir une direction
            var rightDir = gameObject.transform.right;
            var dotProduct = Vector2.Dot(TargetDir, rightDir);

            if (dotProduct < 0)
            {
                // Activez l'effet miroir
                gameObject.transform.localScale = new Vector3(-scale, scale, 1f);
            }
            else
            {
                // R�initialisez l'effet miroir si la cible est devant le sprite
                gameObject.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }

    public void DestroyHero()
    {
        Destroy(gameObject);
        // Il faut remove ce game object de la liste summon in game
        GameControler.Instance.SummonInGame.Remove(gameObject);
    }
    public void Movement(Vector3 target)
    {
        // Calculer la direction vers la cible
        Vector3 direction = target - transform.position;
        // Normaliser la direction pour obtenir une vitesse constante
        direction.Normalize();

        // Calculer le d�placement du projectile en fonction de la vitesse et du temps
        Vector3 movement = direction * speed * Time.deltaTime;

        // D�placer le projectile dans la direction calcul�e
        transform.Translate(movement);
    }
    void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        IddlePosition = transform.position;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
