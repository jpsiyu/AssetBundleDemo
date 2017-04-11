using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABDownLoader : MonoBehaviour{
    private ABHandler mABHandler;
    private ABDownLoadError mErr;

    public ABDownLoadError Err
    {
        get { return mErr; }
    }

    void Start() {
        mErr = ABDownLoadError.ERR_None;
    }

    public IEnumerator DownloadAndCacheABList(List<string> modifyABs) {

        for (int i = 0; i < modifyABs.Count; i++) {
            mABHandler = new ConfigAB(modifyABs[i]);
            yield return StartCoroutine(DownloadAndCache(mABHandler));
        }
    }

    private IEnumerator DownloadAndCache(ABHandler abHandler)
    {
        ABUtil.Log("download or read ab: " + abHandler.ABName());
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
                mErr = ABDownLoadError.ERR_URL;
                yield break;
            }
          
            
            AssetBundle bundle = www.assetBundle;
            if (bundle == null) {
                ABUtil.Log("Null bundle: " + abHandler.ABName());
                mErr = ABDownLoadError.ERR_LoadAB;
                yield break;
            }

            abHandler.Handle(bundle);
            bundle.Unload(false);
            ABUtil.Log("download or read finished: " + abHandler.ABName());

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
