using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldScript : MonoBehaviour
{
    public int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerInteractionManager>().AddGold(count);
            Destroy(gameObject);

        }
    }
}
