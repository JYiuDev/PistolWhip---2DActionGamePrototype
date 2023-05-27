using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitBox : MonoBehaviour
{
    private float damage;
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
                }
            break;

            case "Enemy":
                if (collision.gameObject.tag == "PlayerObjects")
                {
                    styleScriptTwo player = collision.GetComponent<styleScriptTwo>();
                    player.takeDamage();
                }
            break;
        }
        
    }
}
