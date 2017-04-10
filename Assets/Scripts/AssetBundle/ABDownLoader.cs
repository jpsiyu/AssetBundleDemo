using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABDownLoader : MonoBehaviour{

    private List<ABHandler> m_Handlers;


    private void InitABList() {
        m_Handlers = new List<ABHandler>();
        m_Handlers.Add(new ConfigAB());
        m_Handlers.Add(new DefaultAB());
    }

    public IEnumerator DownloadAndCacheABList() {
        InitABList();

        for (int i = 0; i < m_Handlers.Count; i++) {
            yield return StartCoroutine(DownloadAndCache(m_Handlers[i]));
        }
    }

    private IEnumerator DownloadAndCache(ABHandler abHandler)
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(ABGlobal.abURL + abHandler.ABName(), ABGlobal.abVersion))
        {
            yield return www;
            if (www.error != null)
                throw new System.Exception(string.Format("WWW download error: {0}, name:{1}", www.error, abHandler.ABName()));
            
            AssetBundle bundle = www.assetBundle;
            if (bundle == null)
                throw new System.Exception("Null bundle: " + abHandler.ABName());

            abHandler.Handle(bundle);
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
