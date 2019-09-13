using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestoyerScript : MonoBehaviour
{
    public GameObject platform;
    public Transform pos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            int rand = Random.Range(-5, 5);
            Destroy(collision.gameObject);
            Instantiate(platform, new Vector3(pos.position.x + rand, pos.position.y, pos.position.z), Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Player"))
            Debug.Log("gameOver");
    }
}
