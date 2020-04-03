using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    //Stores list of average state value at different snapshots in time. In list here are the values of indices:
    //0: speed, 1: perception, 2: sex drive, 3: poison, 4: mimicry
    private float[] dataHerbs;
    private float[] dataCarnis;
    private int timeWait;

    private void Awake()
    { 
        timeWait = Settings.DATA_COL_WAIT;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeWait > 0)
        {
            timeWait--;

            if (timeWait == 0)
            {
                CollectData();
                timeWait = Settings.DATA_COL_WAIT;
            }
        }
    }

    private void CollectData()
    {
        dataHerbs = new float[] { 0, 0, 0, 0, 0};
        dataCarnis = new float[] { 0, 0, 0, 0, 0 };

        foreach (Foo f in PopulationHandler.herbifoos)
        {
            float[] stats = f.GetStatistics();

            for (int i = 0; i < stats.Length; i++)
            {
                dataHerbs[i] += stats[i];
            }

        }

        foreach (Foo f in PopulationHandler.carnifoos)
        {
            float[] stats = f.GetStatistics();

            for (int i = 0; i < stats.Length; i++)
            {
                dataCarnis[i] += stats[i];
            }

        }

        string c = "Carnifoos: ";
        string h = "Herbifoos: ";

        for (int i = 0; i < 5; i++)
        {
            dataCarnis[i] /= PopulationHandler.carnifoos.Count;
            c += " " + (Mathf.Round(dataCarnis[i] * 100)) / 100.0 + ",";
            dataHerbs[i] /= PopulationHandler.herbifoos.Count;
            h += " " + (Mathf.Round(dataHerbs[i] * 100)) / 100.0 + ",";
        }
        Debug.Log("DATA POINT: ");
        Debug.Log(c);
        Debug.Log(h);
    }
}
