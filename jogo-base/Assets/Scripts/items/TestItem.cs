using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item {
    
    public TestItem()
    {
        id = 0;
        consumable = true;
        spriteId = 0;
    }

    public override void onConsume() {
        Debug.Log("consumido!!");
    }

}
