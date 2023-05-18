using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

// �κ��丮 ���԰� ���õ� ����
public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Vector3 originPos;
    public Item item;       // ȹ���� ������
    public Image itemImage; // �������� �̹���
    public LayerMask groundLayer; // ���� ���̾�
    public Transform player; // �÷��̾� ����

    public static float currentDropAngle = 0f; // ���� ������ ��� ����
    public static float dropRadius = 0.45f; // �������� �������� ������
    public static float dropAngleStep = 65f; // ������ ��� ���� ����

    private PhotonView photonView;

    void Start()
    {
        originPos = transform.position;
    }

    // �̹��� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
    {
        Debug.Log("Slot.cs : AddItem() called");
        Debug.Log("Slot.cs : itemImage : " + _item.itemImage);
        item = _item;
        itemImage.sprite = item.itemImage;
        SetColor(1);
    }

    // ���� �ʱ�ȭ
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // �κ��丮���� ������ ������
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    if (item != null)
    //    {
    //        // �巡�� �� ����� ���� ��ġ�� �ִ� ��� ����ĳ��Ʈ ����� �����ɴϴ�.
    //        List<RaycastResult> raycastResults = new List<RaycastResult>();
    //        EventSystem.current.RaycastAll(eventData, raycastResults);

    //        // �κ��丮 ������ �巡�� �� ��ӵǾ����� Ȯ���ϱ� ���� ����
    //        bool isOutsideInventory = true;

    //        foreach (RaycastResult result in raycastResults)
    //        {
    //            if (result.gameObject != null && result.gameObject.name == "Inventory_Base")
    //            {
    //                // �巡�� �� ����� ���� ��ġ�� �κ��丮�� �ִٸ� �κ��丮 ������ �巡�� �� ��ӵ��� �ʾҴٰ� �Ǵ�
    //                isOutsideInventory = false;
    //                break;
    //            }
    //        }

    //        if (isOutsideInventory)
    //        {
    //            // ����߸� �������� �������� ���
    //            if (item.itemName == "������")
    //            {
    //                player.GetComponent<Light>().enabled = false;
    //                ActionController.isFlashOn = false;
    //            }

    //            // �������� ������ ��ġ ���
    //            Vector3 dropDirection = Quaternion.Euler(0, currentDropAngle, 0) * player.forward;
    //            Vector3 spawnPosition = player.position + dropDirection * dropRadius;

    //            // ������ ������ ����
    //            SpawnItem(item.itemPrefab, spawnPosition);

    //            // ������ ���� ��, �κ��丮���� ������ ����
    //            ClearSlot();

    //            // ���� ������ ��� ���� ������Ʈ
    //            currentDropAngle += dropAngleStep;

    //        }
    //    }

    //    DragSlot.instance.SetColor(0);
    //    DragSlot.instance.dragSlot = null;
    //}

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            // �巡�� �� ����� ���� ��ġ�� �ִ� ��� ����ĳ��Ʈ ����� �����ɴϴ�.
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            // �κ��丮 ������ �巡�� �� ��ӵǾ����� Ȯ���ϱ� ���� ����
            bool isOutsideInventory = true;

            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject != null && result.gameObject.name == "Inventory_Base")
                {
                    // �巡�� �� ����� ���� ��ġ�� �κ��丮�� �ִٸ� �κ��丮 ������ �巡�� �� ��ӵ��� �ʾҴٰ� �Ǵ�
                    isOutsideInventory = false;
                    break;
                }
            }

            if (isOutsideInventory)
            {
                // ����߸� �������� �������� ���
                if (item.itemName == "������")
                {
                    player.GetComponent<Light>().enabled = false;
                    ActionController.isFlashOn = false;
                }

                // �������� ������ ��ġ ���
                Vector3 dropDirection = Quaternion.Euler(0, currentDropAngle, 0) * player.forward;
                Vector3 spawnPosition = player.position + dropDirection * dropRadius;

                // ������ ������ ����
                SpawnItem(item.itemPrefab, spawnPosition);

                // ������ ���� ��, �κ��丮���� ������ ����
                ClearSlot();

                // ���� ������ ��� ���� ������Ʈ
                currentDropAngle += dropAngleStep;
            }
        }

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void SpawnItem(GameObject itemPrefab, Vector3 spawnPosition)
    {
        RaycastHit hit;
        Vector3 raycastStartPos = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z);
        // Player ���̾ �����ϴ� LayerMask ����
        int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        if (Physics.Raycast(raycastStartPos, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 groundedSpawnPosition = hit.point;
            PhotonNetwork.Instantiate(itemPrefab.name, groundedSpawnPosition + new Vector3(0, 0.01f, 0), Quaternion.identity * itemPrefab.transform.localRotation);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    public void ChangeSlot()
    {
        Item _tempItem = item;

        AddItem(DragSlot.instance.dragSlot.item);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
