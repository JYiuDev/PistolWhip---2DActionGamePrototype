using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitBox : MonoBehaviour
{
    private float damage;
    [SerializeField] Knife knife;
    private bool hitEnemy = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int parentLayer = transform.parent.parent.gameObject.layer;
        string parentLayerName = LayerMask.LayerToName(parentLayer);

        switch(parentLayerName)
        {
            case "PlayerObjects":
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.GetComponent<EnemyHP>().takeDamage(transform.parent.GetComponent<Knife>().meleeDamagePlayer);
                    hitEnemy = true;
                }
            break;

            case "Enemy":
                if (collision.gameObject.tag == "Player")
                {
                    styleScriptTwo player = collision.GetComponent<styleScriptTwo>();
                    player.takeDamage();
                }
            break;
        }   
    }

    public void calcDurability()
    {
        if(hitEnemy)
        {
            hitEnemy = false;
            knife.useDurability(1);
        }
    }
}
