using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifeform : MonoBehaviour
{
    public Transform cachedTransform { get; private set; }

    private void Awake()
    {
        cachedTransform = GetComponent<Transform>();
    }
}
