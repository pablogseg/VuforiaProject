using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    Rigidbody RB;
    bool collided = false;
    [SerializeField]
    float velocity;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity = RB.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Rock" && collision.gameObject.tag != "Catapult" && !collided)
        {
            collided = true;
            StartCoroutine(DestroyAfterTimeOrSpeed());
        }
    }


    IEnumerator DestroyAfterTimeOrSpeed()
    {
        float timer = 8f;
        float timeStopped = 0f;

        while ((timer -= Time.deltaTime) > 0)
        {
            if (RB.velocity.magnitude <= 2 && timer >= 2f)
            {
                timeStopped += Time.deltaTime;
                if (timeStopped >= 3f)
                {
                    timer = 2;
                }
            }
            yield return null;
        }
        catapultaLogic.getInstance.GameOver();
        Destroy(gameObject);
    }
}


