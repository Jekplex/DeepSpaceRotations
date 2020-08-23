using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactoryResetScript : MonoBehaviour
{
    public void FactoryReset()
    {
        PlayerPrefs.DeleteAll();        
    }
}
