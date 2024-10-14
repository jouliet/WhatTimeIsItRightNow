using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Collections;
using System.Net;

public class Window : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private Label time;
    private string currentTime = "Fetching...";
    private string url = "https://worldtimeapi.org/api/ip";
    UnityWebRequestAsyncOperation asyncOperation;

    [MenuItem("Window/WTIIRN?")]
    public static void ShowExample()
    {
        Window wnd = GetWindow<Window>();
        wnd.titleContent = new GUIContent("What time is it right now?");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        time = new Label(currentTime);
        root.Add(time);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }

    public void OnGUI()
    {
        time.text = currentTime;
        Repaint();
    }

    public void OnEnable()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        asyncOperation = webRequest.SendWebRequest();
    }

    private void Update()
    {
        if (asyncOperation.isDone)
        {
            if (asyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Failed fetching time.");
            }
            else
            {
                APIResponse timeResponse = JsonUtility.FromJson<APIResponse>(asyncOperation.webRequest.downloadHandler.text);
                currentTime = timeResponse.datetime;
            }
        }
    }

    private struct APIResponse
    {
        public string datetime;
    }
}
