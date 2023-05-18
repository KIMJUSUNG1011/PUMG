using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������ ���õ� �ڵ�
// ScriptableObject : GameObject �� ���� �ʿ䰡 ����
[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public ItemType itemType;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public string weaponType;

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }
}
