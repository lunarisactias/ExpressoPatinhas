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

    //UPGRADES
    //public int upgradeType;
    // 1 Autoclick mais rapido | 2 melhor potencia autoclick | 3 melhora click



}

public class Upgrade
{
    public UpgradeKey key;
    public float value;
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
    
