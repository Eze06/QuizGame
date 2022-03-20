using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    enum x {left, right, up, down}
    x direction;
    string y = "North";

    private void Start()
    {
        switch (y)
        {
            case "South":
                direction = x.down;
                break;

            case "North":
                direction = x.up;
                break;

            case "West":
                direction = x.left;
                break;
            case "East":
                direction = x.down;
                break;
        }

        Debug.Log(direction);

    }


}
