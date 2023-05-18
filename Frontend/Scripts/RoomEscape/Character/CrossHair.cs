using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public static bool crossHairActivated = true;

    // Ư�� ��ȣ�ۿ� ���� ũ�ν��� Ȱ��ȭ�Ǿ��־��°�?
    public static bool preIsCrossHair;

    public static void ToggleCrossHair()
    {
        GameObject FingerCursor = GameObject.Find("CrossHair").transform.Find("Dot").gameObject;
        crossHairActivated = !crossHairActivated;

        if (crossHairActivated)
        {
            FingerCursor.SetActive(true);
        }
        else
        {
            FingerCursor.SetActive(false);
        }
    }
}
