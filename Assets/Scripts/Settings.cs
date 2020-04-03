using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{

    private const int SECOND = 120;

    //Herbifoos
    public static DNA HERB_PRESET_1 = new DNA(0.5f, 0.5f, 0.5f, 0.25f, 0.25f);

    public const int NUM_HERBIFOOS = 20;
    public const float EAT_RANGE = 2;
    public const float SEX_RANGE = 1;





    //Carnifoos
    public static DNA CARN_PRESET_1 = new DNA(0.5f, 0.5f, 0.5f);

    public const int NUM_CARNIFOOS = 0;

    //Noms
    public const int NUM_NOMS = 44;
    public const int NOM_VALUE = 35;
    public const int NOM_RESPAWN_TIME = 1000;
    public const int AUTO_RESP_TIME = 60 * SECOND;

    //Other
    public const float JUMP_HEIGHT = 3.5f; //Original 3.5f
    public const float ENVIRONMENT_RANGE = 40f;
    public const float SPEED_CONST = 5f;
    public const float MUTATION_FREQ = 0.5f; //[0, 1]
    public const int JUMP_WAIT = 60;
    public const int CHILD_WAIT = 360;


    //Cost functions
    public const float SPEED_COST = 3;
    public const float PERCEP_COST = 7;
    public const float MIMICRY_COST = 3;
    public const float POISON_COST = 6;

    //Max vals
    public const float MAX_SPEED = 5;
    public const float MAX_PERCEP = 30;
    public const float MAX_SEX_DRIVE = 1;
    public const float MAX_MIMICRY = 1;
    public const float MAX_POISON = 1;


    public const int TURNS = 50;
    public const float TURN_AMNT = 360 / TURNS;

    public const bool INVINCIBILITY = false;

    public const int NUM_ENVIRON_OBJS = 90;

    public const int HUNGER_LIMIT = 100;


    public const int ENVI_MASK = 1 << 8;


    public const int DATA_COL_WAIT = 2000;

    //Colors

    //Herbifoo default
    public static Color ORANGE = new Color(252.0f / 255, 186.0f / 255, 3.0f / 255);

    //Herbifoo poisonness
    public static Color RED = new Color(184.0f / 255, 104.0f / 255, 103.0f / 255);
    public static Color YELLOW = new Color(204.0f / 255, 204.0f / 255, 39.0f / 255);
    public static Color GREEN = new Color(123.0f / 255, 219.0f / 255, 134.0f / 255);

    //Carnifoo default
    public static Color BLUE = new Color(130.0f / 255, 200.0f / 255, 237.0f / 255);

}
