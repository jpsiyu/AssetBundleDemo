using UnityEngine;
using System.Collections;

public class ABMgr : MonoBehaviour {

    private ABDownLoader mABDownloader;
    private ABChecker mABChekcer;

    private void Awake() {
        mABDownloader = gameObject.AddComponent<ABDownLoader>();
        mABChekcer = gameObject.AddComponent<ABChecker>();
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
	}

}
