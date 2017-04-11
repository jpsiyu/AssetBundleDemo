using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ABHashInfo
{
    public string ab;
    public string hash;
}

[Serializable]
public class ABHashCollection
{
    public List<ABHashInfo> abHashList = new List<ABHashInfo>();
    public List<string> CalModifyAB(ABHashCollection serverABHashCollection) {
        List<string> modifyABList = new List<string>();
        Dictionary<string, ABHashInfo> clientABHashDict = new Dictionary<string, ABHashInfo>();
        for (int i = 0; i < abHashList.Count; i++) {
            clientABHashDict.Add(abHashList[i].ab, abHashList[i]);
        }

        ABHashInfo tempS, tempC;
        for (int i = 0; i < serverABHashCollection.abHashList.Count; i++) {
            tempC = null;
            tempS = serverABHashCollection.abHashList[i];

            clientABHashDict.TryGetValue(tempS.ab, out tempC);
            if (tempC == null || !tempS.hash.Equals(tempC.hash)) {
                modifyABList.Add(tempS.ab);
                ABUtil.Log(string.Format("modify ab: {0}", tempS.ab));
            }
        }
        return modifyABList;
    }

    public List<string> GetNameList() {
        List<string> nameList = new List<string>();
        for (int i = 0; i < abHashList.Count; i++) {
            nameList.Add(abHashList[i].ab);
        }
        return nameList;
    }
}

public enum ABBuildPlatform { 
    Win64,
    Android,
    IOS,
}
