using System;
using System.Collections.Generic;

[Serializable]
public class StoreItemDto
{
    public string id;
    public string name;
    public string descricao;
    public string imagepatch;
    public string prefabName;
    public string prefabpatch;
    public int price;
    public bool purchased;
    public Upgrade upgrade;
}

public class Upgrade
{
    public UpgradeKey key;
    public float value;
    public bool isAutoclick;
}

public enum UpgradeKey
{
    BetterAutoclick,
    FasterAutoclick,
    BetterClick
}

    [Serializable]
    public class StoreItemWrapper
    {
        public List<StoreItemDto> item;
    }
    
