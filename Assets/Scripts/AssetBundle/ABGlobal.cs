using UnityEngine;
using System.Collections;

public class ABGlobal
{
    public static bool abCheck = true;
    public static string abURL = "http://192.168.3.60/";
    public static int abVersion = 1;
    public static string abDir = "AssetBundles";
    public static string abDirBundle = string.Format("{0}/{0}", abDir);
    public static string abCompareFileName = "ABCompare.json";
    public static string editorPersistentDir = "StreamingAssets";
    public static string abCompareFile = string.Format("{0}/{1}/{2}", editorPersistentDir,"ABCompareFile", abCompareFileName);
}
