﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnData : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Sprite;

    public float BuffEfect;
}