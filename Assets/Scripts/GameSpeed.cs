using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public float TargetMoveShift = 0.0f;
    public float Scaler = 0.005f;

    [Header("Essentials")]
    public PrefabManager PrefabManager;
    private TargetScript targetPrefab;
    private TargetScript targetHealPrefab;

    private void Start()
    {
        targetPrefab = PrefabManager.target.GetComponent<TargetScript>();
        targetHealPrefab = PrefabManager.targetHeal.GetComponent<TargetScript>();

    }

    // Update is called once per frame
    void Update()
    {

        TargetMoveShift = Mathf.Exp(Time.timeSinceLevelLoad * Scaler);

        targetPrefab.SetMoveSpeedShift(TargetMoveShift);
        targetHealPrefab.SetMoveSpeedShift(TargetMoveShift);

    }
}
