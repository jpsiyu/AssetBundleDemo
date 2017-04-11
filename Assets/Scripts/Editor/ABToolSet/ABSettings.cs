using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ABSettings {

    /// <summary>
    /// 打包目录
    /// </summary>
    /// <returns></returns>
    public static string AssetBundleBuildPath() {
        string path = "";
        ABWindow abWindow = EditorWindow.GetWindow(typeof(ABWindow)) as ABWindow;
        switch (abWindow.BuildPlatform) { 
            case ABBuildPlatform.Win64:
                path = "Assets/" + ABGlobal.abDir + "/Win64";
                break;
            case ABBuildPlatform.Android:
                path = "Assets/" + ABGlobal.abDir + "/Android";
                break;
            case ABBuildPlatform.IOS:
                path = "Assets/" + ABGlobal.abDir + "/IOS";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 查询目录
    /// </summary>
    /// <returns></returns>
    public static string AssetBundleFetchPath()
    {
        string path = "";
        ABWindow abWindow = EditorWindow.GetWindow(typeof(ABWindow)) as ABWindow;
        switch (abWindow.BuildPlatform)
        {
            case ABBuildPlatform.Win64:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/Win64";
                break;
            case ABBuildPlatform.Android:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/Android";
                break;
            case ABBuildPlatform.IOS:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/IOS";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 根据根文件夹生成的AssetBunlde的获取目录
    /// </summary>
    /// <returns></returns>
    public static string FolderAssetBundleFetchPath() {
        string path = "";
        ABWindow abWindow = EditorWindow.GetWindow(typeof(ABWindow)) as ABWindow;
        switch (abWindow.BuildPlatform)
        {
            case ABBuildPlatform.Win64:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/Win64/Win64";
                break;
            case ABBuildPlatform.Android:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/Android/Android";
                break;
            case ABBuildPlatform.IOS:
                path = Application.dataPath + "/" + ABGlobal.abDir + "/IOS/IOS";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 生成对比文件的路径
    /// </summary>
    /// <returns></returns>
    public static string AssetBundleCompareFilePath() { 
        string filePath = Path.Combine(AssetBundleFetchPath(), ABGlobal.abCompareFileName);
        return filePath;
    }

    /// <summary>
    /// 运行时的文件路径在不同平台是不一样的
    /// </summary>
    /// <returns></returns>
    public static string LocalCompareFilePath() { 
        string filePath = Path.Combine(Application.dataPath, ABGlobal.abCompareFile);
        return filePath;
    }
}
