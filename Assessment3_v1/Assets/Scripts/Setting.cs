using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Setting : MonoBehaviour
{
    [Header("Panel管理")]
    public List<GameObject> allPanels;  // 手动拖入需要控制的Panel
    public GameObject defaultPanel;     // 初始面板（如MainPanel）

    [Header("动画设置")]
    public float fadeDuration = 0.3f;

    private Stack<GameObject> panelStack = new Stack<GameObject>();

    void Start()
    {
        // 关闭所有注册的Panel
        foreach (var panel in allPanels)
        {
            panel.SetActive(false);
        }
        
        // 初始化第一个Panel
        OpenPanel(defaultPanel);
    }

    public void OpenPanel(GameObject targetPanel)
    {
        if (!allPanels.Contains(targetPanel))
        {
            Debug.LogError($"未注册的面板: {targetPanel.name}");
            return;
        }

        if (panelStack.Count > 0)
        {
            var currentPanel = panelStack.Peek();
            StartCoroutine(TransitionPanel(currentPanel, false));
        }

        StartCoroutine(TransitionPanel(targetPanel, true));
        panelStack.Push(targetPanel);
    }

    public void GoBack()
    {
        if (panelStack.Count <= 1) return;

        var currentPanel = panelStack.Pop();
        var prevPanel = panelStack.Peek();

        StartCoroutine(TransitionPanel(currentPanel, false));
        StartCoroutine(TransitionPanel(prevPanel, true));
    }

    private IEnumerator TransitionPanel(GameObject panel, bool show)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = panel.AddComponent<CanvasGroup>();

        if (show)
        {
            panel.SetActive(true);
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, fadeDuration).SetUpdate(true);
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, fadeDuration).SetUpdate(true);
            yield return new WaitForSecondsRealtime(fadeDuration);
            panel.SetActive(false);
        }
        yield return null;
    }
}