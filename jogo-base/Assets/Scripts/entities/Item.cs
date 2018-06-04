using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int id, spriteId, effectDuration;
    public string name;
    public float effectTimer;

    public abstract void onConsume(Player player);
    public abstract void onExpire(Player player);
}
