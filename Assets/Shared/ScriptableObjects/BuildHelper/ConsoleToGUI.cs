using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
    [Header("   At Runtime Fire Alt+I To tooggle on and off ")]
    [SerializeField]
    string filename = "RuntimeLog";
    string myLog = "*begin log";
    bool doShow = false;
    int kChars = 700;
    public static ConsoleToGUI instance;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
        Application.logMessageReceived += Log;
    }
    void OnDisable() { Application.logMessageReceived -= Log; }
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftAlt)) && Input.GetKeyDown(KeyCode.I))
        {
            doShow = !doShow;
        }
    }
    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        myLog = myLog + "\n" + logString;
        if (myLog.Length > kChars) { myLog = myLog.Substring(myLog.Length - kChars); }

        // for the file ...
        if (filename == "")
        {
            string d = System.Environment.GetFolderPath(
               System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(d);
            string r = Random.Range(1000, 9999).ToString();
            filename = d + "/log-" + r + ".txt";
        }
        try { System.IO.File.AppendAllText(filename, logString + "\n"); }
        catch { }
    }

    void OnGUI()
    {
        if (!doShow) { return; }
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
           new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(10, 10, 540, 370), myLog);
    }
}