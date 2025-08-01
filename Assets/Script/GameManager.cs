using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public DataFormat data = new DataFormat();

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
