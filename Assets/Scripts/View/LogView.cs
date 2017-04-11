using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogView : MonoBehaviour {

    private GameObject mLogText;
    private GameObject mLogRoot;

    void Awake() {
        mLogText = transform.FindChild("Log").gameObject;
        mLogRoot = transform.FindChild("Scroll View/Viewport/Content").gameObject;
    }

    public void AddLog(string logStr) {
        GameObject aLog = GameObject.Instantiate(mLogText);
        aLog.SetActive(true);
        aLog.GetComponent<Text>().text = logStr;
        aLog.transform.SetParent(mLogRoot.transform);
        aLog.transform.localScale = Vector3.one;
    }
}
