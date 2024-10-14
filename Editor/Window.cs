using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Window : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

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

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
