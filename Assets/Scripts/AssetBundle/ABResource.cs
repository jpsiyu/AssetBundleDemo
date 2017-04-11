using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABResource  {

    private static ABResource mInstance;
    private Dictionary<string, Object> objDict;

    private ABResource() {
        objDict = new Dictionary<string, Object>();
    }

    public static ABResource Instance {
        get {
            if (mInstance == null)
                mInstance = new ABResource();
            return mInstance;
        }
    }

    public void CacheABObject(AssetBundle ab) { 
        string[] names = ab.GetAllAssetNames();
        for (int i = 0; i < names.Length; i++) {
            if (objDict.ContainsKey(names[i]))
                objDict[names[i]] = ab.LoadAsset(names[i]);
            else
                objDict.Add(names[i], ab.LoadAsset(names[i]));
        }
    }

    public Object LoadObj(string name) {
        Object obj = null;
        objDict.TryGetValue(name.ToLower(), out obj);
        return obj;
    }
}
