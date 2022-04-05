using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    // Update is called once per frame
    private IEnumerator Flash()
    {
        int opacity = 0;
        for (int c = 0; c < 60; c++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
            if (c % 2 == 0)
            {
                opacity = 1;
            }
            else
            {
                opacity = 0;
            }
        }

        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
