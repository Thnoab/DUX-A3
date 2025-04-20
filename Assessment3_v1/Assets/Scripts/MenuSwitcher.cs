using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSwitcher : MonoBehaviour
{
    public static MenuSwitcher Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject panelA;  // 例如：设置菜单面板
    [SerializeField] private GameObject panelB;  // 例如：音频菜单面板
    [SerializeField] private Button switchToBButton; // 从A切换到B的按钮
    [SerializeField] private Button switchToAButton; // 从B切换到A的按钮

    private void Awake()
    {
        // 单例模式，确保全局唯一
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 强制初始化状态
        panelA.SetActive(true);
        panelB.SetActive(false);
    }

    private void Start()
    {
        // 绑定按钮事件
        switchToBButton.onClick.AddListener(() => SwitchPanels(panelA, panelB));
        switchToAButton.onClick.AddListener(() => SwitchPanels(panelB, panelA));

        // 监听场景加载事件（解决跨场景问题）
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 通用切换逻辑
    private void SwitchPanels(GameObject closePanel, GameObject openPanel)
    {
        closePanel.SetActive(false);
        openPanel.SetActive(true);
        Debug.Log($"已切换：{closePanel.name} → {openPanel.name}");
        //爬这个
    }

    // 场景加载后重新绑定UI引用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 动态查找UI元素（需提前设置Tag）
        panelA = GameObject.FindWithTag("PanelA");
        panelB = GameObject.FindWithTag("PanelB");

        // 重新绑定按钮
        switchToBButton = GameObject.FindWithTag("SwitchToBButton").GetComponent<Button>();
        switchToAButton = GameObject.FindWithTag("SwitchToAButton").GetComponent<Button>();

        // 重新初始化状态
        panelA.SetActive(true);
        panelB.SetActive(false);
    }
}