using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Assets/Get AssetBundle names")]
    public static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }

    [MenuItem("Assets/GenABCompareFile")]
    public static void GenABCompareFile() {
        string dir = Path.Combine(Application.dataPath, "AssetBundles");
        string assetBundlePath = Path.Combine(dir, "AssetBundles");

        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return;
        }

        AssetBundleManifest abManif = assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        GenFile(abManif);
        assetBundle.Unload(false);
    }

    private static void GenFile(AssetBundleManifest abManif) {
        string dir = Path.Combine(Application.dataPath, "AssetBundles");
        string filePath = Path.Combine(dir, "ABCompare.json");


        FileStream fs = new FileStream(filePath, FileMode.Create);
        StreamWriter mStreamWriter = new StreamWriter(fs, System.Text.Encoding.UTF8);

        ABHashCollection abHashCollection = new ABHashCollection();
        ABHashInfo abHashInfo = new ABHashInfo();
        string[] abs = abManif.GetAllAssetBundles();
        for (int i = 0; i < abs.Length; i++)
        {
            abHashInfo.ab = abs[i];
            abHashInfo.hash = abManif.GetAssetBundleHash(abs[i]).ToString();
            abHashCollection.abHashList.Add(abHashInfo);
        }

        string jsonStr = JsonUtility.ToJson(abHashCollection);
        Debug.Log(jsonStr);
        mStreamWriter.Write(jsonStr);

        mStreamWriter.Close();
        fs.Close();
    }

    [MenuItem("Assets/ReadABCompareFile")]
    public static void ReadABCompareFile()
    {
        string dir = Path.Combine(Application.dataPath, "AssetBundles");
        string filePath = Path.Combine(dir, "ABCompare.json");


        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fs);

            string jsonStr = streamReader.ReadToEnd();
            ABHashCollection abHashCollection = JsonUtility.FromJson<ABHashCollection>(jsonStr);
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }
}


