using UnityEngine;
using System.Collections;

public class ABMgr : MonoBehaviour {

    private ABDownLoader mABDownloader;
    private ABChecker mABChekcer;
    private bool mUpdating;


    private void Awake() {
        mUpdating = false;
        AddLogView();
        mABDownloader = gameObject.AddComponent<ABDownLoader>();
        mABChekcer = new ABChecker();
    }

    private void AddLogView() {
        if (!ABGlobal.showLogView) return;
        GameObject logViewPrefab = Resources.Load("Prefab/LogView") as GameObject;
        GameObject logViewGO = GameObject.Instantiate(logViewPrefab);
    }

	// Use this for initialization
	private IEnumerator StartAssetBundleUpdate () {
        mUpdating = true;
        if (ABGlobal.abCheck) {
            ABUtil.Log("start modify ab check");
            yield return StartCoroutine(mABChekcer.StartCheck());
        }

        if (mABChekcer.ModifyABs != null && mABChekcer.ModifyABs.Count > 0) {
            ABUtil.Log("start download modify abs");
            yield return StartCoroutine(mABDownloader.DownloadAndCacheABList(mABChekcer.ModifyABs));
            ABUtil.Log("finish download modify abs");
            if (mABDownloader.Err == ABDownLoadError.ERR_None)
            {
                mABChekcer.UpdateClientJsonFile();
            }
            else {
                ABUtil.Log("update modify ab failed");
            }
        }
        else
        {
            ABUtil.Log("Load local Asset bundles");
            yield return StartCoroutine(LoadLocalAssetBundle());
            ABUtil.Log("Load local Asset bundles finished");

        }
        yield return new WaitForSeconds(1f);
        mUpdating = false;
	}

    private IEnumerator LoadLocalAssetBundle() {
        ABHashCollection hashCollection = mABChekcer.ReadABCompareFile();
        yield return StartCoroutine(mABDownloader.DownloadAndCacheABList(hashCollection.GetNameList()));
    }

    public void UpdateAssetBundle() {
        if (mUpdating) return;
        StartCoroutine(StartAssetBundleUpdate());
    }

}
