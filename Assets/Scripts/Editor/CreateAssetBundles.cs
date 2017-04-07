using UnityEditor;
using UnityEngine;
using System.IO;

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
        string filePath = Path.Combine(dir, "ABCompare.txt");

        SaveCSVData saveCSVData = new SaveCSVData();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        saveCSVData.Open(fs);

        saveCSVData.cat("ab");
        saveCSVData.cat("hash");
        saveCSVData.newRow();

        string[] abs = abManif.GetAllAssetBundles();
        for (int i = 0; i < abs.Length; i++)
        {
            saveCSVData.cat(abs[i]);
            saveCSVData.cat(abManif.GetAssetBundleHash(abs[i]).ToString());
            saveCSVData.newRow();
        }

        saveCSVData.Save();
        fs.Close();
    }
}
