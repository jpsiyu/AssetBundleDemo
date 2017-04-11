using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class ABToolSet : AssetPostprocessor
{
    /// <summary>
    /// 监听AB变化
    /// </summary>
    /// <param name="path"></param>
    /// <param name="previous"></param>
    /// <param name="next"></param>
    private void OnPostprocessAssetbundleNameChanged(string path, string previous, string next)
    {
        Debug.Log("AB: " + path + " old: " + previous + " new: " + next);
    }

    /// <summary>
    /// 打包
    /// </summary>
    public static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/" + ABGlobal.abDir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    /// <summary>
    /// 获取AB Name
    /// </summary>
    public static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            ABUtil.Log("AssetBundle: " + name);
    }

    /// <summary>
    /// 生成Json对比文件
    /// </summary>
    public static void GenABCompareFile() {
        string assetBundlePath = Path.Combine(Application.dataPath, ABGlobal.abDirBundle);

        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        if (assetBundle == null)
        {
            ABUtil.Log("Failed to load AssetBundle!");
            return;
        }

        AssetBundleManifest abManif = assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        GenFile(abManif);
        assetBundle.Unload(false);
    }

    /// <summary>
    /// 生成文件
    /// </summary>
    /// <param name="abManif"></param>
    private static void GenFile(AssetBundleManifest abManif) {
        string filePath = Path.Combine(Application.dataPath, Path.Combine(ABGlobal.abDir, ABGlobal.abCompareFileName));

        FileStream fs = new FileStream(filePath, FileMode.Create);
        StreamWriter mStreamWriter = new StreamWriter(fs, System.Text.Encoding.UTF8);

        ABHashCollection abHashCollection = new ABHashCollection();
        ABHashInfo abHashInfo = null;
        string[] abs = abManif.GetAllAssetBundles();
        for (int i = 0; i < abs.Length; i++)
        {
            abHashInfo = new ABHashInfo();
            abHashInfo.ab = abs[i];
            abHashInfo.hash = abManif.GetAssetBundleHash(abs[i]).ToString();
            abHashCollection.abHashList.Add(abHashInfo);
        }

        string jsonStr = JsonUtility.ToJson(abHashCollection);
        Debug.Log("Gen Json Success: " + jsonStr);
        mStreamWriter.Write(jsonStr);

        mStreamWriter.Close();
        fs.Close();
    }


}


