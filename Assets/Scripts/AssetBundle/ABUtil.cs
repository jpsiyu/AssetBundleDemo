using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ABUtil  {
    private static string yellowCode = "#FFFF00";

    public static void Log(params string[] logs) {
        for (int i = 0; i < logs.Length; i++) {
            string log = string.Format("<color={0}>ABFlow: {1}</color>", yellowCode, logs[i]);
            Debug.Log(log);
            LogToView(log);
        }
    }

    public static void LogToView(string log) {
        LogView logView = GameObject.FindObjectOfType<LogView>();
        if (logView != null) logView.AddLog(log);
    }
}
