using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == GlobalConstants.Tags.PLAYER)
        {
            collision.GetComponent<PlayerInteractionManager>().AddGold(count);
            Destroy(gameObject);
        }
    }

}

