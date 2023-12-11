
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class ItemModel
{
    public enum TypeOfItem
    {
        Restore,
        AddMax,
    }

    public TypeOfItem typeOfItem;
    public int amount;
    public int maxStack;
    public float hpRestore;
    public float manaRestore;
    public float hpAdd;
    public float manaAdd;
    public int jumMaxAdd;
    public float damageAdd;

    // Đối với Sprite, bạn có thể sử dụng tên hình ảnh hoặc địa chỉ để lưu trữ thông tin hình ảnh
    public string image;

    public string nameItem;
    public string description;

    // Hàm tạo không tham số để sử dụng khi chuyển đổi từ JSON
    public ItemModel(ScriptOBJ item)
    {
        nameItem = item.nameItem;
        description = item.description;
        amount = item.amount;
        maxStack = item.maxStack;
        hpRestore = item.hpRestore;
        manaRestore = item.manaRestore;
        hpAdd = item.hpAdd;
        manaAdd = item.manaAdd;


    }

    // Hàm tạo có tham số để tạo mới từ ScriptableObject
    [JsonConstructor]
    public ItemModel(TypeOfItem type, int amount, int maxStack, float hpRestore, float manaRestore,
                     float hpAdd, float manaAdd, int jumMaxAdd, float damageAdd, string image,
                     string nameItem, string description)
    {
        this.typeOfItem = type;
        this.amount = amount;
        this.maxStack = maxStack;
        this.hpRestore = hpRestore;
        this.manaRestore = manaRestore;
        this.hpAdd = hpAdd;
        this.manaAdd = manaAdd;
        this.jumMaxAdd = jumMaxAdd;
        this.damageAdd = damageAdd;
        this.image = image;
        this.nameItem = nameItem;
        this.description = description;
    }
}
