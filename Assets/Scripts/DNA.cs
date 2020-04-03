using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{




    public readonly float speed; //[0, 1]
    public readonly float perception; // [0, 1]
    public readonly float sexDrive; //[0, 1]
    public readonly float poison; //[0, 1], 0 : Carnifoo
    public readonly float mimicry; //[0, 1], 0 : Carnifoo
    public readonly float vividness; //[0, 1], -1 : Carnifoo

    //Herbifoo constructor
    public DNA(float speed, float perception, float sexDrive, float poison, float mimicry)
    {
        this.speed = speed;
        this.perception = perception;
        this.sexDrive = sexDrive;
        this.poison = poison;
        this.mimicry = mimicry;

        vividness = Mathf.Max(poison, mimicry);

    }



    //Carnifoo constructor
    public DNA(float speed, float perception, float sexDrive)
    {
        this.speed = speed;
        this.perception = perception;
        this.sexDrive = sexDrive;
        poison = 0;
        mimicry = 0;
        vividness = -1;
    }

    public static DNA Mutate(DNA d1)
    {
        //both
        float s = Bound(d1.speed + (Norp() * Mutate()));
        float pe = Bound(d1.perception + (Norp() * Mutate()));
        float h = Bound(d1.sexDrive + (Norp() * Mutate()));

        //Carnifoo
        if (d1.vividness == -1)
        {
            return new DNA(s, pe, h);
        }
      
        //Herbifoo        
        float p = Bound(d1.poison + (Norp() * Mutate()));
        float m = Bound(d1.mimicry + (Norp() * Mutate()));

        return new DNA(s, pe, h, p, m);

    }


    private static float Bound(float n)
    {
        if (n < 0)
        {
            return 0;
        }
        if (n > 1)
        {
            return 1;
        }
        return n;
    }

    private static float Mutate() => Random.Range(0, 1f) < Settings.MUTATION_FREQ ? 0.2f : 0;

    private static int Norp() => Random.Range(0, 1f) > 0.5 ? -1 : 1;



}
