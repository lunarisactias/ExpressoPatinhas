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
    public string key;
    public float value;
}

    [Serializable]
    public class StoreItemWrapper
    {
        public List<StoreItemDto> item;
    }
    
