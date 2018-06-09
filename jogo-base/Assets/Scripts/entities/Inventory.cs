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
        /* Para prevenir modificações concurrentes */
        Item toRemove = null;

        foreach (Item item in consumedItems)
        {
            /* Aumentar tempo ao contador do item */
            item.effectTimer += Time.deltaTime;

            /* Se o tempo passar do limite, chamar o onExpire e remover o item dos consumidos (destruir) */
            if (item.effectTimer >= item.effectDuration)
            {
                item.onExpire(localPlayer);
                toRemove = item;
            }
        }

        if (toRemove != null)
            consumedItems.Remove(toRemove);
    }

    public int quantityOf(int id)
    {
        int count = 0;

        foreach (Item item in items)
        {
            if (item.id == id)
                count++;
        }

        return count;
    }

    public int quantityOf(string name)
    {
        int count = 0;

        foreach (Item item in items)
        {
            if (item.name.Equals(name))
                count++;
        }

        return count;
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

    /* Consome um item num dado id (usa método consume(Item))*/
    public void consume(int id)
    {
        /* Para prevenir modificações concurrentes */
        Item toConsume = null;

        foreach (Item item in items)
        {
            if (item.id == id)
            {
                toConsume = item;
                break;
            }
        }

        consume(toConsume);

    }

    /* Consome um item dado, removendo-o da lista e evocando o seu evento de consumo. */
    public void consume(Item item)
    {
        if (item == null)
            return;

        if (items.Contains(item))
        {
            /* Consumir item */
            item.onConsume(localPlayer);

            /* Transferir o item dos items disponiveis para os items consumidos */
            consumedItems.Add(item);
            items.Remove(item);
        }
    }
}
