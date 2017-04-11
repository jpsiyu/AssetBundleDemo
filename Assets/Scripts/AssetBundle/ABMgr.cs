using UnityEngine;
using System.Collections;

public class ABMgr : MonoBehaviour {

    private ABDownLoader mABDownloader;
    private ABChecker mABChekcer;

    private void Awake() {
        AddLogView();
        mABDownloader = gameObject.AddComponent<ABDownLoader>();
        mABChekcer = gameObject.AddComponent<ABChecker>();
    }

    private void AddLogView() {
        if (!ABGlobal.showLogView) return;
        GameObject logViewPrefab = Resources.Load("Prefab/LogView") as GameObject;
        GameObject logViewGO = GameObject.Instantiate(logViewPrefab);
    }

	// Use this for initialization
	private IEnumerator Start () {
        if (ABGlobal.abCheck) {
            ABUtil.Log("start modify ab check");
            yield return StartCoroutine(mABChekcer.StartCheck());
        }

        if (mABChekcer.ModifyABs != null && mABChekcer.ModifyABs.Count > 0) {
            ABUtil.Log("start download modify abs");
            yield return StartCoroutine(mABDownloader.DownloadAndCacheABList(mABChekcer.ModifyABs));
            ABUtil.Log("finish download modify abs");
            mABChekcer.UpdateClientJsonFile();
        }
        else
        {
            ABUtil.Log("No need do update ab");
        }
	}

}
