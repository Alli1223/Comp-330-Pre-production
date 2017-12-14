using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MechIDConst : MonoBehaviour 
{
    [SerializeField]
    public string displayName;

    [SerializeField]
    public string assetNameHead;
    [SerializeField]
    public string assetNameUpperTorso;
    [SerializeField]
    public string assetNameLowerTorso;
    [SerializeField]
    public string assetNameLegs;
    [SerializeField]
    public string assetNameArms;

    [SerializeField]
    public int assetIDHead;
    [SerializeField]
    public int assetIDUpperTorso;
    [SerializeField]
    public int assetIDLowerTorso;
    [SerializeField]
    public int assetIDLegs;
    [SerializeField]
    public int assetIDArms;
    	
    [SerializeField]
    public Transform weaponMountL;
    [SerializeField]
    public Transform weaponMountR;
    [SerializeField]
    public Transform shieldMountL;
    [SerializeField]
    public Transform shieldMountR;
    [SerializeField]
    public Transform gimbalMountL;
    [SerializeField]
    public Transform gimbalMountR;

    public static int AssetNameToIDHead(string name)
    {
        return UTRigAssembler.AssetNameToIDHead(name);
    }

    public static int AssetNameToIDUpperTorso(string name)
    {
        return UTRigAssembler.AssetNameToIDUpperTorso(name);
    }

    public static int AssetNameToIDArms(string name)
    {
        return UTRigAssembler.AssetNameToIDArms(name);
    }

    public static int AssetNameToIDLowerTorso(string name)
    {
        return LTRigAssembler.AssetNameToIDLowerTorso(name);
    }

    public static int AssetNameToIDLegs(string name)
    {
        return LTRigAssembler.AssetNameToIDLegs(name);
    }

    public static string AssetIDToNameHead(int ID)
    {
        return UTRigAssembler.AssetIDToNameHead(ID);
    }

    public static string AssetIDToNameUpperTorso(int ID)
    {
        return UTRigAssembler.AssetIDToNameUpperTorso(ID);
    }

    public static string AssetIDToNameArms(int ID)
    {
        return UTRigAssembler.AssetIDToNameArms(ID);
    }

    public static string AssetIDToNameLegs(int ID)
    {
        return LTRigAssembler.AssetIDToNameLowerTorso(ID);
    }

    public static string AssetIDToNameLowerTorso(int ID)
    {
        return LTRigAssembler.AssetIDToNameLegs(ID); 
    }
}
