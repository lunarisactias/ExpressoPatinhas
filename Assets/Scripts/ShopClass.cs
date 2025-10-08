using UnityEngine;

public class ShopClass
{
    private string nome;
    private int preço;
    private string descrição;

    public ShopClass(string nome, int preço, string descrição)
    {
        this.nome = nome;
        this.preço = preço;
        this.descrição = descrição;
    }

    public string GetNome()
    {
        return nome;
    }
    public int GetPreço()
    {
        return preço;
    }
    public string GetDescrição()
    {
        return descrição;
    }
}
