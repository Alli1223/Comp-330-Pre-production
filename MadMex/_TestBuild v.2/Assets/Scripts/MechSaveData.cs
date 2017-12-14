using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class MechSaveData
{
    const string playerMechDataPath = "Assets/Resources/Data";

    public static void SaveMech(MechIDConst mech)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        file = File.Open(Application.persistentDataPath + "/" + mech.displayName + ".mech", FileMode.Create);


        MechData data = new MechData();
        data.name = mech.displayName;
        data.headID = mech.assetIDHead;
        data.armsID = mech.assetIDArms;
        data.legsID = mech.assetIDLegs;
        data.upperTorsoID = mech.assetIDUpperTorso;
        data.lowerTorsoID = mech.assetIDLowerTorso;

        Debug.Log(data.armsID);
        Debug.Log(data.headID);
        Debug.Log(data.legsID);
        Debug.Log(data.upperTorsoID);
        Debug.Log(data.lowerTorsoID);

        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadMech(ref MechIDConst mech, string name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".mech"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".mech", FileMode.Open);
            MechData data = bf.Deserialize(file) as MechData;
            file.Close();

            Debug.Log(data.armsID);
            Debug.Log(data.headID);
            Debug.Log(data.legsID);
            Debug.Log(data.upperTorsoID);
            Debug.Log(data.lowerTorsoID);

            mech.assetIDArms = data.armsID;
            mech.assetIDHead = data.headID;
            mech.assetIDLegs = data.legsID;
            mech.assetIDLowerTorso = data.lowerTorsoID;
            mech.assetIDUpperTorso = data.upperTorsoID;

            mech.displayName = data.name;
        }
    }


}


[Serializable]
class MechData
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int headID;
    [SerializeField]
    public int armsID;
    [SerializeField]
    public int legsID;
    [SerializeField]
    public int upperTorsoID;
    [SerializeField]
    public int lowerTorsoID;
}
