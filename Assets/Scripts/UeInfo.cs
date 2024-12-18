using UnityEngine;
using System;



public class UeInfo : MonoBehaviour
{

    public VisualLine VisualLine;
    private double txPowerUl = 0.0f;
    private double txPowerBaseUl = 26.0f;
    private double txPowerDl = 0.0f;
    public int ueId = 0;
    public float frequency = 2000000000;




    private bool losCollision = false;

    public UeStatusDisplay statusDisplay;
    public bool isSelect = false;

    public GnbInfo TargetGnb;
    public int targetGnbId = 0;

    public UeMovementControl prefabMovement;
    public Vector3 prefabPosition;

    public string mobilityInfo;

    public string ulInfo = "";
    public string dlInfo = "";

    void Start()
    {
        //txPowerBaseUl = UnityEngine.Random.Range( 25.0f, 27.0f );

    }


    void Update()
    {
        prefabPosition = prefabMovement.positionLocal;
        mobilityInfo = ueId.ToString() + ": " + Math.Round(prefabPosition.x,1) +" "+ -Math.Round(prefabPosition.z, 1) + " "+ Math.Round(prefabPosition.y, 1);

        losCollision = VisualLine.collision;
        targetGnbId = TargetGnb.gnbId;


        txPowerUl = System.Math.Round(txPowerBaseUl+VisualLine.totalLossForwardInDB, 1);
        txPowerDl = System.Math.Round(TargetGnb.txPower+VisualLine.totalLossReverseInDB, 1);

        
        ulInfo = "txPower"+ueId.ToString()+": " + txPowerUl.ToString();
        dlInfo = "txPower" + targetGnbId.ToString() + "->" + ueId.ToString() + ": " + txPowerDl.ToString();

        isSelect = statusDisplay.status;

    }

    public int GetUeId() { return ueId; }
    public void SetUeId(int id) { ueId = id; }
    public double GetTxPower() { return txPowerBaseUl; }

    public double GetTxPowerUl() { return txPowerUl; }
    public double GetTxPowerDl() {  return txPowerDl; }
    public bool GetCollision() {  return losCollision; }
}
