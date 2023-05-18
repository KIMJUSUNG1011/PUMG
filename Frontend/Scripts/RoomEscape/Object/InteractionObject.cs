using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public static bool objectDetailTextActivated;
    public string objectName;
    public string[] messages;

    // ����, ���� ���� ��� �ڽſ��� �޷��ִ� �ڹ���
    public GameObject myLock;

    private InteractionObject parentInteractionObject;

    void Start()
    {
        // �θ� ������Ʈ�� InteractionObject ��ü�� �����ɴϴ�.
        parentInteractionObject = GetComponent<InteractionObject>();

        // �ڽ� ������Ʈ�� �θ� ������Ʈ�� InteractionObject ��ü�� ������ �����մϴ�.
        // �̷��� �ϴ� ������ ������Ʈ�� ���� �ڽ� ������Ʈ��� �̷���� ���� ��, ����ĳ������ �ϸ�
        // �ڽ� ������Ʈ�� ����ä�� ���������� ����ĳ������ �ȵǴ� ��찡 ����
        // ���� �ڽ� ������Ʈ�鿡�� ��� ��ũ��Ʈ�� �����ϵ�����
        // ���� �ʿ������� �𸣰���. �׽�Ʈ �ʿ�.
        AddScriptToChildren(transform);

        objectDetailTextActivated = false;
    }

    void AddScriptToChildren(Transform parent)
    {
        // �ڽ� ������Ʈ�鿡�� �θ� ������Ʈ�� InteractionObject ��ü�� ������ �����ϴ� �޼ҵ��Դϴ�.
        foreach (Transform child in parent)
        {
            // �ڽ� ������Ʈ�� �θ� ������Ʈ�� InteractionObject ��ü�� ������ �����մϴ�.
            child.gameObject.AddComponent<InteractionObject>();
            child.gameObject.GetComponent<InteractionObject>().objectName = parentInteractionObject.objectName;
            child.gameObject.GetComponent<InteractionObject>().messages = parentInteractionObject.messages;

            // �ڽ� ������Ʈ�� �ڽ� ������Ʈ�鿡�Ե� �θ� ������Ʈ�� ActionController ��ü�� ������ �����մϴ�.
            if (child.childCount > 0)
            {
                AddScriptToChildren(child);
            }
        }
    }
}
