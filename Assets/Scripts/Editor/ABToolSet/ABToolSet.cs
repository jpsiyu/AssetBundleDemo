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

        BuildPipeline.BuildAssetBundles(ABSettings.AssetBundleBuildPath(), BuildAssetBundleOptions.None, ABSettings.GetBuildTarget());
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
        string assetBundlePath = ABSettings.FolderAssetBundleFetchPath();

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
        string filePath = ABSettings.AssetBundleCompareFilePath();

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
        ABUtil.Log("Gen Json Success: " + jsonStr);
        mStreamWriter.Write(jsonStr);

        mStreamWriter.Close();
        fs.Close();
    }

    /// <summary>
    /// 读取本地Local文件的比较文件
    /// </summary>
    public static void ReadABCompareFile()
    {
        ABHashCollection abHashCollection = new ABHashCollection();
        string filePath = ABSettings.LocalCompareFilePath();

        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fs);

            string jsonStr = streamReader.ReadToEnd();
            ABUtil.Log("read jsonStr: " + jsonStr);
            streamReader.Close();
            fs.Close();
        }
        catch (Exception e)
        {
            ABUtil.Log(e.Message);
        }
    }

    /// <summary>
    /// 删除对比文件
    /// </summary>
    public static void RemoveLocalCompareFile() { 
        string filePath = ABSettings.LocalCompareFilePath();
        try
        {
            File.Delete(filePath);
            ABUtil.Log("Delete AB Compare File Success");
        }
        catch (Exception e) {
            ABUtil.Log(e.Message);
        }
    }

}


