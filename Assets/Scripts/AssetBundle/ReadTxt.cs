using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ReadTxt : MonoBehaviour {

    private string filePath = "Config/award/StdDropInfo";
    private Dictionary<int, StdDropInfo> m_DicDropInfo = new Dictionary<int, StdDropInfo>();
    

	void Start () {
        DontDestroyOnLoad(this);
        LoadConfig();
        SaySomething();
    }

    private void SaySomething() {
        UnityEngine.Debug.Log("first drop id: " + m_DicDropInfo[10].nDropId);
    }

    private void LoadConfig() {
        TextAsset textAsset = AssetsLoader.Load(filePath) as TextAsset;
        LoadCSVData loadCSVData = new LoadCSVData();
        loadCSVData.Load(textAsset.text);
        ReadCSV(loadCSVData);
        loadCSVData.Clear();
    }

    private void ReadCSV(LoadCSVData doc) { 
        int nIndex_Id = doc.getColumnIndex("id");
        int nIndex_GroupId = doc.getColumnIndex("GroupId");
        int nIndex_GroupType = doc.getColumnIndex("GroupType");
        int nIndex_GroupValue = doc.getColumnIndex("GroupValue");
        int nIndex_DropType = doc.getColumnIndex("DropType");
        int nIndex_DropId = doc.getColumnIndex("DropId");
        int nIndex_DropCount = doc.getColumnIndex("DropCount");

        int totalCount = (int)doc.numRows();

        for (int i = 0; i < totalCount; ++i)
        {
            StdDropInfo info = new StdDropInfo();
            info.nIndex = doc.getValue(i, nIndex_Id).ToInt32();
            info.nGroupId = doc.getValue(i, nIndex_GroupId).ToInt32();
            info.nGroupType = doc.getValue(i, nIndex_GroupType).ToInt32();
            info.nGroupValue = doc.getValue(i, nIndex_GroupValue).ToInt32();
            info.nDropType = doc.getValue(i, nIndex_DropType).ToInt32();
            info.nDropId = doc.getValue(i, nIndex_DropId).ToInt32();
            info.nDropCount = doc.getValue(i, nIndex_DropCount).ToInt32();
            
            if(info.nIndex == 0) continue;
            m_DicDropInfo.Add(info.nIndex, info);
        }
    }

}
