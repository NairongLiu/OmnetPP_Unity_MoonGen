using UnityEngine;
using System;

public class UeInfo : MonoBehaviour
{
    
    public VisualLine VisualLine;
    private double txPowerUl = 0.0f;
    private double txPowerBaseUl = 0.0f;
    private double txPowerDl = 0.0f;
    public int ueId = 0;
    private bool losCollision = false;


    public GnbInfo TargetGnb;
    public int targetGnbId = 0;

    public UeMovementControl prefabMovement;
    public Vector3 prefabPosition;

    public string mobilityInfo;

    public string ulInfo = "";
    public string dlInfo = "";

    void Start()
    {
        txPowerBaseUl = UnityEngine.Random.Range( 10.0f, 20.0f );
    }


    void Update()
    {
        prefabPosition = prefabMovement.positionLocal;
        mobilityInfo = ueId.ToString() + ": " + Math.Round(prefabPosition.x,1) +" "+ -Math.Round(prefabPosition.z, 1) + " "+ Math.Round(prefabPosition.y, 1);

        losCollision = VisualLine.collision;
        targetGnbId = TargetGnb.gnbId;

        if ( losCollision) { 
            txPowerUl = System.Math.Round(txPowerBaseUl * 0.05, 1);
            txPowerDl = TargetGnb.txPower * 0.05;
        }
        else { 
            txPowerUl = System.Math.Round(txPowerBaseUl * 1, 1);
            txPowerDl = TargetGnb.txPower * 1;
        }
        
        ulInfo = "txPower"+ueId.ToString()+": " + txPowerUl.ToString();
        dlInfo = "txPower" + targetGnbId.ToString() + "->" + ueId.ToString() + ": " + txPowerDl.ToString();
    }

    public int GetUeId() { return ueId; }
    public void SetUeId(int id) { ueId = id; }
    public double GetTxPower() { return txPowerUl; }
    public bool GetCollision() {  return losCollision; }
}