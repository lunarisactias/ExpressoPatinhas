using System.Collections;
using System.Collections.Generic;

    [System.Serializable]
    public class StoreItemDto
    {
        public string id;
        public string name;
        public string descricao;
        public string prefabName; 
        public int price;
        public bool purchased;
    }

    [System.Serializable]
    public class StoreItemWrapper
    {
        public List<StoreItemDto> item;
    }
