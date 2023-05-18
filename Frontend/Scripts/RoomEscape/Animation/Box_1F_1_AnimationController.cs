using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Box_1F_1_AnimationController : MonoBehaviour
{
    public Animator boxAnimator;
    public string openTrigger = "IsOpen";
    public string closeTrigger = "IsClose";
    public GameObject lockData;

    public bool isBoxOpen = false;

    private PhotonView photonView;

    private AudioSource openDrawer, closeDrawer;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();

        var audios = gameObject.GetComponents<AudioSource>();
        if (audios.Length > 0)
        {
            closeDrawer = audios[0];
            openDrawer = audios[1];
        }
    }

    void Update()
    {
        if (lockData.GetComponent<Lock>().isSolved 
            && ActionController.actionObjectActivated
            && ActionController.hitInfo.transform.name == gameObject.name)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isBoxOpen)
                {
                    boxAnimator.SetTrigger(closeTrigger);

                    if (closeDrawer != null)
                    {
                        closeDrawer.Play();
                        photonView.RPC("RPC_CloseAudio", RpcTarget.All);
                    }

                    // ��� ������ ����ȭ 
                    photonView.RPC("RPC_CloseBox", RpcTarget.All);
                }
                else
                {
                    boxAnimator.SetTrigger(openTrigger);

                    if (openDrawer != null)
                    {
                        openDrawer.Play();
                        photonView.RPC("RPC_OpenAudio", RpcTarget.All);
                    }

                    // ��� ������ ����ȭ
                    photonView.RPC("RPC_OpenBox", RpcTarget.All);
                }

                // ������ ����ϸ� �ڱ� �ڽſ��Ե� �޽����� ���� ������
                // �Ʒ� �ڵ�� ��ü ����
                //isDresserOpen = !isDresserOpen;

                // ��� ������ ����ȭ
                int viewId = gameObject.GetComponent<PhotonView>().ViewID;
                photonView.RPC("RPC_ChangeBox_1F_1_OpenState", RpcTarget.All, viewId);
            }
        }
    }

    [PunRPC]
    void RPC_OpenBox()
    {
        boxAnimator.SetTrigger(openTrigger);
    }

    [PunRPC]
    void RPC_CloseBox()
    {
        boxAnimator.SetTrigger(closeTrigger);
    }

    [PunRPC]
    void RPC_OpenAudio()
    {
        openDrawer.Play();
    }

    [PunRPC]
    void RPC_CloseAudio()
    {
        closeDrawer.Play();
    }

    [PunRPC]

    void RPC_ChangeBox_1F_1_OpenState(int viewId)
    {
        PhotonView itemPhotonView = PhotonView.Find(viewId);

        if (itemPhotonView != null)
        {
            itemPhotonView.gameObject.GetComponent<Box_1F_1_AnimationController>().isBoxOpen =
                !itemPhotonView.gameObject.GetComponent<Box_1F_1_AnimationController>().isBoxOpen;
        }
    }
}

