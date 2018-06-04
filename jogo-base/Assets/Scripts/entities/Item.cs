using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {

    public int id;
    public string name;
    public int effectDuration;
    public float effectTimer;
    public int spriteId;

    public abstract void onConsume(Player player);
    public abstract void onExpire(Player player);
}
