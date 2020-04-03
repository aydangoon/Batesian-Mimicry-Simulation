using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    

    //Herbifoo
    [Header("Herbifoo")]
    public Herbifoo herbifooPrefab;

    [Header("Carnifoo")]
    //Carnifoo
    public Carnifoo carnifooPrefab;

    //Nom
    [Header("Nom")]
    public Nom nomPrefab;

    //Environment
    [Header("Environment")]
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject grassPrefab;

    void Awake()
    {

        for (int i = 0; i < Settings.NUM_ENVIRON_OBJS; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-Settings.ENVIRONMENT_RANGE, Settings.ENVIRONMENT_RANGE), 0, Random.Range(-Settings.ENVIRONMENT_RANGE, Settings.ENVIRONMENT_RANGE));
            Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(treePrefab, pos, rot);
                break;
                default:
                    Instantiate(grassPrefab, pos, rot);
                break;
            }
        }

        for (int i = 0; i < Settings.NUM_HERBIFOOS; i++)
        {
            Herbifoo herbifoo = Instantiate(herbifooPrefab);
            herbifoo.Initialize(DNA.Mutate(Settings.HERB_PRESET_1));

            PopulationHandler.herbifoos.Add(herbifoo);
        }

        for (int i = 0; i < Settings.NUM_NOMS; i++)
        {
            Nom nom = Instantiate(nomPrefab);

            PopulationHandler.noms.Add(nom);
        }

        for (int i = 0; i < Settings.NUM_CARNIFOOS; i++)
        {
            Carnifoo carnifoo = Instantiate(carnifooPrefab);
            carnifoo.Initialize(Settings.CARN_PRESET_1);

            PopulationHandler.carnifoos.Add(carnifoo);

        }
    }
}
