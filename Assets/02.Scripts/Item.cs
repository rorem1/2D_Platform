using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    /*아이템
    1.체력회복
    2.기본공격증가
    3.드론
    https://www.youtube.com/watch?v=-47pjK-P888&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x
    */
    public ItemData data;
    public int level;
    

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }
    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1);
    }
    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Range:
                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

}
