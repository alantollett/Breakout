using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDeltaTime : MonoBehaviour
{
    [SerializeField] private float fixedDeltaTime;

    void OnEnable()
    {
        Time.fixedDeltaTime = fixedDeltaTime;
    }
}
