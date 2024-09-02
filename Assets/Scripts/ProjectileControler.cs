using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ProjectileControler : MonoBehaviour
{
    public GameObject Target;
    public int Damage;
    private float speed;
    private float Scale;
    // Start is called before the first frame update
    void Start()
    {
        if (tag == "ProjectileArcher")
        {
            speed = 5f;
            Scale = 3;
        }
        else
        {
            speed = 7f;
            Scale = 8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MirorEffect();
    }

    public void Movement()
    {
        if (Target != null)
        {
            // Calculer la direction vers la cible
            Vector3 direction = Target.transform.position - transform.position;
            // Normaliser la direction pour obtenir une vitesse constante
            direction.Normalize();

            // Calculer le d�placement du projectile en fonction de la vitesse et du temps
            Vector3 movement = direction * speed * Time.deltaTime;

            // D�placer le projectile dans la direction calcul�e
            transform.Translate(movement);

            // Si le projectile est suffisamment proche de la cible, d�truire le projectile
            var x = Mathf.Ceil(transform.position.x - Target.transform.position.x);
            var y = Mathf.Ceil(transform.position.y - Target.transform.position.y);
            if (Mathf.Abs(x) <= 1f && Mathf.Abs(y) <= 1f)
            {
                Target.GetComponent<EnnemiController>().monsterHP -= Damage;
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void MirorEffect()
    {
        if (Target != null)
        {
            var TargetDir = Target.transform.position - gameObject.transform.position;
            TargetDir.Normalize(); // Normalise les vecteurs pour avoir une direction
            var rightDir = gameObject.transform.right;
            var dotProduct = Vector2.Dot(TargetDir, rightDir);

            if (dotProduct < 0)
            {
                // Activez l'effet miroir
                gameObject.transform.localScale = new Vector3(-Scale, Scale, 1f);
            }
            else
            {
                // R�initialisez l'effet miroir si la cible est devant le sprite
                gameObject.transform.localScale = new Vector3(Scale, Scale, 1f);
            }
        }
    }
}
