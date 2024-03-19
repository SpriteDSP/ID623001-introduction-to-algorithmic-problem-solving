using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int money = 50;
    private AudioSource chaChingSFX;

    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            chaChingSFX.Play();
        }
    }
    
    public void SomeFunction()
    {
        Money = 50;
        Money += 50;
        Money = Money - 75;
    }
}
