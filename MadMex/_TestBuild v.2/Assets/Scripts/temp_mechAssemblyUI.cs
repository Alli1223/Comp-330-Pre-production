using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temp_mechAssemblyUI : MonoBehaviour 
{
    public GameObject headPanel;

    public Text textFieldHead;
    public Text textFieldUpperTorso;
    public Text textFieldLowerTorso;
    public Text textFieldArms;
    public Text textFieldLegs;

    int currentHeadID;
    int currentUpperTorsoID;
    int currentLowerTorsoID;
    int currentArmsID;
    int currentLegsID;

    GameObject currentUpperTorsoObj;
    GameObject currentLowerTorsoObj;

    Transform upperTorsoParent;

    Animator animUT;
    Animator animLT;

    MechIDConst currentMechConst;

	// Use this for initialization
	void Start () 
    {
        Debug.Log(Application.persistentDataPath);
        UTRigAssembler.Instance = Resources.Load("Data/UTRigAssembler") as UTRigAssembler;
        LTRigAssembler.Instance = Resources.Load("Data/LTRigAssembler") as LTRigAssembler;

        AssembleLT(0, 0, false);
        AssembleUT(0, 0, 0, false);
	}

    public void SaveCurrentMech()
    {
        currentMechConst.displayName = "testSaveMech";
        MechSaveData.SaveMech(currentMechConst);
    }

    public void LoadMech()
    {
        MechSaveData.LoadMech(ref currentMechConst, "testSaveMech");

        currentLegsID = currentMechConst.assetIDLegs;
        currentHeadID = currentMechConst.assetIDHead;
        currentArmsID = currentMechConst.assetIDArms;
        currentUpperTorsoID = currentMechConst.assetIDUpperTorso;
        currentLowerTorsoID = currentMechConst.assetIDLowerTorso;

        AssembleLT(currentLegsID, currentLowerTorsoID, false);
        AssembleUT(currentHeadID, currentUpperTorsoID, currentArmsID, false);
    }
	
    void EnableHeadPanel(bool enable)
    {
        headPanel.SetActive(enable);
    }

    public void ChangeHead(int Add)
    {
        AssembleUT(Add, 0, 0, true);
    }

    public void ChangeArms(int Add)
    {
        AssembleUT(0, 0, Add, true);
    }

    public void ChangeUT(int Add)
    {
        AssembleUT(0, Add, 0, true);
    }

    public void ChangeLT(int Add)
    {
        AssembleLT(0, Add, true);
    }

    public void ChangeLegs(int Add)
    {
        AssembleLT(Add, 0, true);
    }

    public void LTorsoTurnAngle(float turn)
    {
        animLT.SetFloat("turnDegree", turn);
    }

    public void LTorsoTurn()
    {
        animLT.SetTrigger("turn");
    }

    public void ToggleMove()
    {
        if (animLT.GetBool("walk"))
            animLT.SetBool("walk", false);
        else
            animLT.SetBool("walk", true);
    }

    public void SetAnimWalkSpeed(float speed)
    {
        animLT.SetFloat("walkSpeed", speed);
    }

    public void SetAnimWalkWeight(float weight)
    {
        animLT.SetFloat("walkWeight", weight);
    }

    public void SetAnimWalkSpeedMod(float mod)
    {
        animLT.SetFloat("walkSpeedMultiplier", mod);
    }

    public void AssembleUT(int headIDAdd, int torsoIDAdd, int armsIDAdd, bool additive)
    {
        if (currentUpperTorsoObj != null)
        {
            GameObject.Destroy(currentUpperTorsoObj);
        }

        if (additive)
        {
            int maxHeadID = UTRigAssembler.Instance.HDData.Length - 1;
            int maxTorsoID = UTRigAssembler.Instance.UTData.Length - 1;
            int maxArmID = UTRigAssembler.Instance.ARData.Length - 1;

            currentHeadID += headIDAdd;
            currentUpperTorsoID += torsoIDAdd;
            currentArmsID += armsIDAdd;

            if (currentHeadID > maxHeadID)
                currentHeadID = 0;
            else if (currentHeadID < 0)
                currentHeadID = maxHeadID;

            if (currentUpperTorsoID > maxTorsoID)
                currentUpperTorsoID = 0;
            else if (currentUpperTorsoID < 0)
                currentLowerTorsoID = maxTorsoID;

            if (currentArmsID > maxArmID)
                currentArmsID = 0;
            else if (currentArmsID < 0)
                currentArmsID = maxArmID;
        }



        currentUpperTorsoObj = UTRigAssembler.Instance.AssembleUpperTorso(currentUpperTorsoID, currentArmsID, currentHeadID, upperTorsoParent,
            out currentMechConst.weaponMountL, out currentMechConst.weaponMountR, out currentMechConst.shieldMountL, out currentMechConst.shieldMountR,
            out currentMechConst.gimbalMountL, out currentMechConst.gimbalMountR);

        if (UTRigAssembler.Instance.UTData[currentUpperTorsoID].hasHead)
        {
            EnableHeadPanel(true);
            textFieldHead.text = UTRigAssembler.Instance.HDData[currentHeadID].asset.name;
        }
        else
        {
            EnableHeadPanel(false);
        }

        textFieldUpperTorso.text = UTRigAssembler.Instance.UTData[currentUpperTorsoID].asset.name;
        textFieldArms.text = UTRigAssembler.Instance.ARData[currentArmsID].asset.name;

        UpdateMechInfoUpper(ref currentMechConst);
    }

    public void AssembleLT(int legIDAdd, int torsoIDAdd, bool additive)
    {
        GameObject oldLowerTorsoOBJ = currentLowerTorsoObj;
        float oldAnimWalkSpeed = 0f;
        float oldAnimWalkWeight = 0f;
        float oldAnimWalkSpeedModifier = 0f;
        float oldAnimTurnDegree = 0f;
        bool oldAnimWalk = false;
        if (currentLowerTorsoObj != null)
        {
            if (currentUpperTorsoObj != null)
            {
                currentUpperTorsoObj.transform.SetParent(null);
            }
            oldAnimWalk = animLT.GetBool("walk");
            oldAnimWalkSpeed = animLT.GetFloat("walkSpeed");
            oldAnimWalkWeight = animLT.GetFloat("walkWeight");
            oldAnimWalkSpeedModifier = animLT.GetFloat("walkSpeedMultiplier");
            oldAnimTurnDegree = animLT.GetFloat("turnDegree");


        }

        if (additive)
        {
            int maxLegID = LTRigAssembler.Instance.LGData.Length - 1;
            int maxTorsoID = LTRigAssembler.Instance.LTData.Length - 1;

            currentLegsID += legIDAdd;
            currentLowerTorsoID += torsoIDAdd;

            if (currentLegsID > maxLegID)
                currentLegsID = 0;
            else if (currentLegsID < 0)
                currentLegsID = maxLegID;

            if (currentLowerTorsoID > maxTorsoID)
                currentLowerTorsoID = 0;
            else if (currentLowerTorsoID < 0)
                currentLowerTorsoID = maxTorsoID;
        }

        currentLowerTorsoObj = LTRigAssembler.Instance.AssembleLowerTorso(currentLowerTorsoID, currentLegsID, Vector3.zero, Quaternion.identity, out upperTorsoParent, out animLT);

        textFieldLowerTorso.text = LTRigAssembler.Instance.LTData[currentLowerTorsoID].asset.name;
        textFieldLegs.text = LTRigAssembler.Instance.LGData[currentLegsID].asset.name;
        if(oldAnimWalk)
            ToggleMove();
        
        SetAnimWalkSpeed(oldAnimWalkSpeed);
        SetAnimWalkWeight(oldAnimWalkWeight);
        SetAnimWalkSpeedMod(oldAnimWalkSpeedModifier);
        LTorsoTurnAngle(oldAnimTurnDegree);

        if (currentUpperTorsoObj != null)
        {
            currentUpperTorsoObj.transform.SetParent(upperTorsoParent);
            currentUpperTorsoObj.transform.localPosition = Vector3.zero;
            currentUpperTorsoObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        MechIDConst newMechInfo = currentLowerTorsoObj.AddComponent<MechIDConst>();
        if (oldLowerTorsoOBJ != null)
        {
            CopyOldMechInfo(oldLowerTorsoOBJ.GetComponent<MechIDConst>(), ref newMechInfo);
            GameObject.Destroy(oldLowerTorsoOBJ);
        }
        else
        {
            UpdateMechInfoLower(ref newMechInfo);
        }

        currentMechConst = newMechInfo;
    }

    void UpdateMechInfoLower(ref MechIDConst mechInfo)
    {
        mechInfo.assetNameLowerTorso = MechIDConst.AssetIDToNameLowerTorso(currentLowerTorsoID);
        mechInfo.assetNameLegs = MechIDConst.AssetIDToNameLegs(currentLegsID);
        mechInfo.assetIDLowerTorso = currentLowerTorsoID;
        mechInfo.assetIDLegs = currentLegsID;
    }

    void UpdateMechInfoUpper(ref MechIDConst mechInfo)
    {
        mechInfo.assetNameHead = MechIDConst.AssetIDToNameHead(currentHeadID);
        mechInfo.assetNameUpperTorso = MechIDConst.AssetIDToNameUpperTorso(currentUpperTorsoID);
        mechInfo.assetNameArms = MechIDConst.AssetIDToNameArms(currentArmsID);

        mechInfo.assetIDHead = currentHeadID;
        mechInfo.assetIDUpperTorso = currentUpperTorsoID;
        mechInfo.assetIDArms = currentArmsID;
    }

    void CopyOldMechInfo(MechIDConst oldMech, ref MechIDConst newMech)
    {
        newMech.assetNameHead = MechIDConst.AssetIDToNameHead(currentHeadID);
        newMech.assetNameUpperTorso = MechIDConst.AssetIDToNameUpperTorso(currentUpperTorsoID);
        newMech.assetNameLowerTorso = MechIDConst.AssetIDToNameLowerTorso(currentLowerTorsoID);
        newMech.assetNameLegs = MechIDConst.AssetIDToNameLegs(currentLegsID);
        newMech.assetNameArms = MechIDConst.AssetIDToNameArms(currentArmsID);

        newMech.assetIDHead = currentHeadID;
        newMech.assetIDUpperTorso = currentUpperTorsoID;
        newMech.assetIDLowerTorso = currentLowerTorsoID;
        newMech.assetIDLegs = currentLegsID;
        newMech.assetIDArms = currentArmsID;

        newMech.weaponMountL = oldMech.weaponMountL;
        newMech.weaponMountR = oldMech.weaponMountR;
        newMech.shieldMountL = oldMech.shieldMountL;
        newMech.shieldMountR = oldMech.shieldMountR;
        newMech.gimbalMountL = oldMech.gimbalMountL;
        newMech.gimbalMountR = oldMech.gimbalMountR;
    }
}
