using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {

    public int id;
    public bool consumable;
    public int spriteId;

    public abstract void onConsume();

}
