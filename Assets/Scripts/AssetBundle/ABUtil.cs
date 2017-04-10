using UnityEngine;
using System.Collections;

public class ABUtil  {
    private static string yellowCode = "#FFFF00";

    public static void Log(params string[] logs) {
        for (int i = 0; i < logs.Length; i++) {
            Debug.Log(string.Format("<color={0}>ABFlow: {1}</color>", yellowCode, logs[i]));
        }
    }
         
}
