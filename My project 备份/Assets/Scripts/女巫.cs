using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WitchDialogueSystem : MonoBehaviour
{
    [Header("角色设置")]
    public Transform player;          // 玩家角色
    public string witchName = "女巫"; // 女巫名称(用于显示)
    public float interactionDistance = 10f; // 交互最大距离

    [Header("对话UI")]
    public GameObject dialoguePanel;  // 对话面板
    public Text nameText;             // 显示名字的文本
    public Text dialogueText;         // 显示对话内容的文本
    public Image witchImage;          // 女巫头像(可选)

    [Header("对话内容")]
    [TextArea(3, 10)]
    public string[] dialogueLines;    // 对话内容数组
    public float typingSpeed = 0.05f; // 打字效果速度

    private bool isInteracting = false;
    private bool isTyping = false;
    private int currentLine = 0;
    private Camera mainCamera;
    private RaycastHit hit;

    void Start()
    {
        // 获取主相机
        mainCamera = Camera.main;

        // 初始化隐藏对话面板
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        // 如果没有指定玩家，尝试通过标签查找
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0) && !isInteracting)
        {
            TryStartInteraction();
        }

        // 对话进行中，检测空格键继续
        if (isInteracting && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // 如果正在打字，立即完成当前行
                CompleteLine();
            }
            else
            {
                // 否则显示下一行
                NextLine();
            }
        }
    }

    void TryStartInteraction()
    {
        // 从鼠标位置发射射线
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // 检查是否点击了女巫
            if (hit.transform == transform)
            {
                // 检查距离是否足够
                if (player != null && Vector3.Distance(player.position, transform.position) <= interactionDistance)
                {
                    StartDialogue();
                }
                else
                {
                    Debug.Log("距离太远，无法与" + witchName + "交互");
                    // 这里可以添加距离提示UI
                }
            }
        }
    }

    void StartDialogue()
    {
        isInteracting = true;
        currentLine = 0;

        // 显示对话UI
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        // 设置女巫名字
        if (nameText != null)
        {
            nameText.text = witchName;
        }

        // 开始第一句对话
        StartCoroutine(TypeDialogue(dialogueLines[currentLine]));

        // 让玩家面向女巫
        if (player != null)
        {
            Vector3 lookDirection = new Vector3(transform.position.x, player.position.y, transform.position.z);
            player.LookAt(lookDirection);
        }
    }

    IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        // 逐字显示效果
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void CompleteLine()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[currentLine];
        isTyping = false;
    }

    void NextLine()
    {
        if (currentLine < dialogueLines.Length - 1)
        {
            currentLine++;
            StartCoroutine(TypeDialogue(dialogueLines[currentLine]));
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isInteracting = false;

        // 隐藏对话面板
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        Debug.Log("与" + witchName + "的对话结束");
    }

    // 在场景视图中显示交互距离
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}