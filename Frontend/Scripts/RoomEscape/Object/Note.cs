using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static bool noteActivated = false;
    public string noteName;

    public GameObject noteDetail;
    public GameObject noteBackground;

    // ���� ��Ʈ �󼼺��� ���� ��ü�� ��ũ��Ʈ�� ������ �� �ֵ��� ��
    public bool isShowing;

    // ��� ���� ��Ʈ���� ������ �� �ֵ��� ��
    public bool isQuit;

    void Update()
    {
        if (isShowing)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //gameObject.GetComponent<BoxCollider>().enabled = true;
                CloseNoteDetail();
                isQuit = true;
            }
        }
    }

    public void ReadNoteDetail()
    {
        Debug.Log("��Ʈ �ڼ��� ����� ��� ~~");

        isShowing = true;
        noteActivated = true;

        noteBackground.transform.SetSiblingIndex(0);

        CrossHair.preIsCrossHair = !CrossHair.crossHairActivated;
        CrossHair.crossHairActivated = true;
        noteBackground.SetActive(true);
        noteDetail.SetActive(true);
        CrossHair.ToggleCrossHair();
    }

    public void CloseNoteDetail()
    {
        Debug.Log("��Ʈ �ڼ��� ���� ���⵵ ���!!");

        isShowing = false;
        noteActivated = false;

        noteBackground.transform.SetSiblingIndex(0);

        CrossHair.crossHairActivated = CrossHair.preIsCrossHair;
        noteBackground.SetActive(false);
        noteDetail.SetActive(false);
        CrossHair.ToggleCrossHair();
    }
}
