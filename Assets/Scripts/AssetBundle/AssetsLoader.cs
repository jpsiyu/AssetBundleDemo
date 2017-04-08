using UnityEngine;
using System.Collections;

public class AssetsLoader {

    public static Object Load(string path) {
        return LoadFromResources(path);
    }

    private static Object LoadFromResources(string path) {
        return Resources.Load(path);
    }

    private static Object LoadFromAssetBundle(string path) {
        AssetBundle ab = new AssetBundle();
        return ab.LoadAsset(path);
    }
}
