using TMPro;
using UnityEngine;
using static NRMCS;
using System;

public class NRMCS_Test : MonoBehaviour
{
    public NRAMC nrAmc;
    public NRMCS nrMcs;
    public CQI_Base cqiBase;
    public UeBase ueBase;

    public int cqi;
    public uint tbs;
    public Modulation mod;
    public float throughput;

    public int packetLength = 1344;
    public float pkts = 0f;

    void Start()
    {

    }

    public float timeSinceLastUpdate;
    private float lastUpdateTime;
    private float updateInterval = 0.1f;



    void Update()
    {
        timeSinceLastUpdate = Time.time - lastUpdateTime;
        if (timeSinceLastUpdate >= updateInterval)
        {
            CustomUpdate();
            lastUpdateTime = Time.time;
        }
    }
    void CustomUpdate()
    {
        cqi = cqiBase.cqi;
        int numBands = ueBase.ueParameters.numBands;
        int numLayers = ueBase.ueParameters.numLayers;
        tbs = nrAmc.ComputeCodewordTbs(numLayers,1,cqi,true,13,1);
        CQIelem entry = nrMcs.CQITable[cqi];
        mod = entry.ModulationType;
        throughput = nrAmc.ThroughputComputation(numBands,numLayers,tbs);
        pkts = throughput / 8 / packetLength;
    }

}
