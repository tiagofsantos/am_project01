using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /* O player ao qual o inventário está associado */
    private Player localPlayer;

    /* Limite máximo de items no inventário */
    private const int MAX_INVENTORY_SIZE = 5;

    /* Lista de items no inventário, ainda por usar */
    private List<Item> items;

    /* Lista de items já consumidos, cujo tempo de efeito ainda não expirou */
    private List<Item> consumedItems;

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();

        items = new List<Item>();
        consumedItems = new List<Item>();
    }

    void Update()
    {
        foreach (Item item in consumedItems)
        {

            item.effectTimer += Time.deltaTime;

            if (item.effectTimer >= item.effectDuration)
            {
                item.onExpiration(localPlayer);
                consumedItems.Remove(item);
            }

        }
    }

    /* Adiciona um item ao inventário, verificando o limite máximo de items. */
    public void add(Item item)
    {
        if (item == null)
            return;

        if (items.Count >= MAX_INVENTORY_SIZE)
        {
            Debug.LogError("Inventário cheio. Limite = " + MAX_INVENTORY_SIZE);
            return;
        }

        items.Add(item);
    }

    /* Consome um item num dado índice (usa método consume(Item)), verificando se o índice é válido e dentro dos limites. */
    public void consume(int index)
    {
        if (index < 0 || items.Count <= index)
        {
            Debug.LogError("Item inválido");
            return;
        }

        consume(items[index]);
    }

    /* Consome um item dado, removendo-o da lista e evocando o seu evento de consumo. */
    public void consume(Item item)
    {
        if (item == null)
            return;

        if (items.Contains(item))
        {
            item.onConsume(localPlayer);

            consumedItems.Add(item);
            items.Remove(item);
        }

    }

}
