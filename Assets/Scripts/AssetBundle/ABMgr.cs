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
        ABUtil.Log("check");
        yield return StartCoroutine(mABChekcer.StartCheck());
        Debug.Log("download");
        yield return StartCoroutine(mABDownloader.DownloadAndCacheABList());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
