using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nom : MonoBehaviour
{

    //state
    private int amount;
    private int respawnWait;
    private int autoRespawnWait;
    public bool respawning;

    //cached
    public Transform cachedTransform;

    void Awake()
    {
        cachedTransform = GetComponent<Transform>();

        ResetState();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (respawnWait > 0)
        {
            respawnWait--;

            if (respawnWait == 0)
            {
                ResetState();
            }
        }

        if (autoRespawnWait > 0)
        {
            autoRespawnWait--;

            if (autoRespawnWait == 0)
            {
                ResetState();
            }
        }
    }

    public void getConsumed()
    {
        if (amount > 0)
        {
            amount--;
            float ns = 100f - (90f - ((amount * 90f) / (float)Settings.NOM_VALUE));
            cachedTransform.localScale = new Vector3(ns, ns, ns);
            
        }
        else
        {

            cachedTransform.localScale = Vector3.zero;

            respawnWait = Settings.NOM_RESPAWN_TIME;
            respawning = true;
        }
    }

    void ResetState()
    {
        amount = Settings.NOM_VALUE;

        respawnWait = 0;
        respawning = false;

        autoRespawnWait = Settings.AUTO_RESP_TIME;

        Vector2 xy = Random.insideUnitCircle * Settings.ENVIRONMENT_RANGE;
        cachedTransform.position = new Vector3(xy.x, 1, xy.y);
        cachedTransform.localScale = new Vector3(100, 100, 100);
    }

}
