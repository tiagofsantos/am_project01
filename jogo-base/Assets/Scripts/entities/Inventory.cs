using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    /* Limite máximo de items no inventário */
    private const int MAX_INVENTORY_SIZE = 5;

    private List<Item> items;

    public Inventory() {
        this.items = new List<Item>();
    }

    /* Adiciona um item ao inventário, verificando o limite máximo de items. */
    public void add(Item item) {

        if (item == null)
            return;

        if (items.Count >= MAX_INVENTORY_SIZE)
        {
            Debug.LogError("Inventário cheio. Limite = " + MAX_INVENTORY_SIZE);
            return;
        }

        items.Add(item);
        Debug.Log(items.Count);
    }

    /* Consome um item num dado índice (usa método consume(Item)), verificando se o índice é válido e dentro dos limites. */
    public void consume(int index) {

        if (index < 0 || items.Count <= index) {
            Debug.LogError("Item inválido");
            return;
        }
        consume(items[index]);
    }

    /* Consome um item dado, removendo-o da lista e evocando o seu evento de consumo. */
    public void consume(Item item) {

        if (item == null || !item.consumable)
            return;

        if (items.Contains(item)) {
            item.onConsume();
            items.Remove(item);
            Debug.Log(items.Count);
        }

    }

}
