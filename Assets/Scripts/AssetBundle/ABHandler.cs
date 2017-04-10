using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ABHandler
{
    public abstract string ABName();
    public abstract void Handle(AssetBundle ab);
}

public class ConfigAB : ABHandler
{
    private Dictionary<string, Object> m_ObjectDict = new Dictionary<string,Object>();
    private string mABName;

    public ConfigAB(string abName) {
        mABName = abName;
    }

    public override string ABName()
    {
        return mABName;
    }

    public override void Handle(AssetBundle ab)
    {
        string configName = "StdDropInfo";
        TextAsset textAsset = ab.LoadAsset(configName) as TextAsset;
    }
}

public class DefaultAB : ABHandler
{

    private Dictionary<string, Hash128> m_BundleHashCode;
    private string mABName;

    public DefaultAB(string abName) {

        mABName = abName;
    }

    public override string ABName()
    {
        return mABName;
    }

    public override void Handle(AssetBundle ab)
    {
        AssetBundleManifest manifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        
        InitBundleHashCode(manifest);
    }

    private void InitBundleHashCode(AssetBundleManifest manifest) {
        string[] bundles = manifest.GetAllAssetBundles();
        m_BundleHashCode = new Dictionary<string, Hash128>();
        for (int i = 0; i < bundles.Length; i++) {
            m_BundleHashCode.Add(bundles[i], manifest.GetAssetBundleHash(bundles[i]));
        }
    }
}
