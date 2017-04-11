using UnityEngine;
using System.Collections;

public class ABGlobal
{
    public static bool showLogView = true;
    public static bool abCheck = true;
    public static int abVersion = 1;
    public static string abDir = "AssetBundles";
    public static string abDirBundle = "AssetBundles/AssetBundles";
    public static string abCompareFileName = "ABCompare.json";

    #if UNITY_EDITOR
    public static string abURL = "http://192.168.3.60/" + abDir + "/Win64/";
    #elif UNITY_STANDALONE_WIN
    public static string abURL = "http://192.168.3.60/" + abDir + "/Win64/";
    #elif UNITY_ANDROID
    public static string abURL = "http://192.168.3.60/" + abDir + "/Android/";
    #elif UNITY_IPHONE
    public static string abURL = "http://192.168.3.60/" + abDir + "/IOS/";
    #endif
}
