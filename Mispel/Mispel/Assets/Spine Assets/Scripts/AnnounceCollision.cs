﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }
}
