using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class LogView : MonoBehaviour {

    private GameObject mLogText;
    private GameObject mLogRoot;

    private Button mBtnClean;
    private Button mBtnUpdate;
    private Button mBtnFlush;
    private Button mBtnCleanCache;

    private Text config;

    void Awake() {
        mLogText = transform.FindChild("Log").gameObject;
        mLogRoot = transform.FindChild("Scroll View/Viewport/Content").gameObject;
        mBtnClean = transform.FindChild("BtnRoot/ButtonClean").GetComponent<Button>();
        mBtnUpdate = transform.FindChild("BtnRoot/ButtonUpdate").GetComponent<Button>();
        mBtnFlush = transform.FindChild("BtnRoot/ButtonFlush").GetComponent<Button>();
        mBtnCleanCache = transform.FindChild("BtnRoot/ButtonCleanCache").GetComponent<Button>();
        config = transform.FindChild("Config").GetComponent<Text>();
    }

    void Start() {
        mBtnClean.onClick.AddListener(OnBtnCleanClick);
        mBtnUpdate.onClick.AddListener(OnBtnUpdateClick);
        mBtnFlush.onClick.AddListener(OnBtnFlushClick);
        mBtnCleanCache.onClick.AddListener(OnBtnCleanCacheClick);
    }

    public void AddLog(string logStr) {
        GameObject aLog = GameObject.Instantiate(mLogText);
        aLog.SetActive(true);
        aLog.GetComponent<Text>().text = logStr;
        aLog.transform.SetParent(mLogRoot.transform);
        aLog.transform.localScale = Vector3.one;
    }

    private void OnBtnCleanClick() {
        CleanLog();
        string filePath = Path.Combine(Application.persistentDataPath, ABGlobal.abCompareFileName);
        File.Delete(filePath);

    }

    private void CleanLog() {
        for (int i = mLogRoot.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(mLogRoot.transform.GetChild(i).gameObject);
        }
    }

    private void OnBtnCleanCacheClick() {
        CleanLog();
        Caching.CleanCache();
        ABUtil.Log("Clean Cache Success");
    }

    private void OnBtnUpdateClick() {
        CleanLog();
        GameObject.FindObjectOfType<ABMgr>().UpdateAssetBundle();
    }
    private void OnBtnFlushClick() {
        Object obj = ABResource.Instance.LoadObj("Assets/Resources/Config/modify/modify.txt");
        if (obj == null) config.text = "no config data";
        else {
            TextAsset textAsset = obj as TextAsset;
            config.text = textAsset.text;
        }
    }
}
