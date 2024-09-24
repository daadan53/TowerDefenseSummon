using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Summon1" || collision.tag == "Summon2" || collision.tag == "Summon3" || collision.tag == "Summon4")
        {
            GameControler.Instance.CastleMP += collision.GetComponent<Summon4Controler>().SellPrice;
            GameControler.Instance.heroCount -= 1;
            GameControler.Instance.SummonInGame.Remove(collision.gameObject);
            Destroy(collision.gameObject);

            if(collision.tag == "Summon1")
            {
                GameControler.Instance.summon1Count--;
            }
            else if(collision.tag == "Summon2")
            {
                GameControler.Instance.summon2Count--;
            }
            else if(collision.tag == "Summon3")
            {
                GameControler.Instance.summon3Count--;
            }
            else if(collision.tag == "Summon4")
            {
                GameControler.Instance.summon4Count--;
            }
        }
    }
}
