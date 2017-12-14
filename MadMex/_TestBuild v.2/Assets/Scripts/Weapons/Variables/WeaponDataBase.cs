﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This Class is used purely to store weapon data sorted by Weapon.WeaponType
[Serializable]
public class WeaponDataBase : ScriptableObject 
{
    [SerializeField]
    public WeaponTypeStruct[] weaponTypeArray;
}

// This will be used primarly by WeaponDataBase ScriptableObject in order to save array of arrays
[Serializable]
public struct WeaponTypeStruct
{
    // These are used by customisation system to spawn in correct objects
    [SerializeField]
    public WeaponAsset[] weapons;
}

[Serializable]
public struct WeaponAsset
{
    [SerializeField]
    public string identifierName;
    [SerializeField]
    public GameObject weaponAsset;
    [SerializeField]
    public GameObject weaponGimbalMountAsset;
    [SerializeField]
    public GameObject weaponStaticMountAsset;
}
