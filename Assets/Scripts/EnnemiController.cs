using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnnemiController : MonoBehaviour
{

    private Vector3 target;
    [SerializeField] private NavMeshAgent agent;
    float angle;
    Vector3 moveDirection;
    public int monsterHP;
    [SerializeField] public int monsterMana;
    public UnityEvent OnDestroyed;


    // Start is called before the first frame update
    void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        agent.enabled = true;
        target = GameObject.Find("Grotte").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target);

        moveDirection = agent.velocity.normalized;

        // Calculer l'angle de rotation en radians
        angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        // Appliquer la rotation au sprite
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Grotte")
        {
            Debug.Log("Ca touche");
            GameControler.Instance.CastleHP--;
            MonsterDestroy();
        }
        
    }
    public void MonsterDestroy()
    {
        OnDestroyed.Invoke();
        SpawnEnnemies.Instance.monsterInGame.Remove(gameObject);
        GameObject.Destroy(gameObject);
    }
}
