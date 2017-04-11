using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABDownLoader : MonoBehaviour{
    private ABHandler mABHandler;

    public IEnumerator DownloadAndCacheABList(List<string> modifyABs) {

        for (int i = 0; i < modifyABs.Count; i++) {
            if (modifyABs[i].Equals(ABGlobal.abDir))
                mABHandler = new DefaultAB(modifyABs[i]);
            else
                mABHandler = new ConfigAB(modifyABs[i]);
            yield return StartCoroutine(DownloadAndCache(mABHandler));
        }
    }

    private IEnumerator DownloadAndCache(ABHandler abHandler)
    {
        ABUtil.Log("download ab: " + abHandler.ABName());
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(ABGlobal.abURL + abHandler.ABName(), ABGlobal.abVersion))
        {
            yield return www;
            if (www.error != null) {
                string err = string.Format("WWW download error: {0}, name:{1}", www.error, abHandler.ABName());
                ABUtil.Log(err);
                yield break;
            }
          
            
            AssetBundle bundle = www.assetBundle;
            if (bundle == null) {
                ABUtil.Log("Null bundle: " + abHandler.ABName());
                yield break;
            }

            abHandler.Handle(bundle);
            bundle.Unload(false);
            ABUtil.Log("download finished: " + abHandler.ABName());

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
