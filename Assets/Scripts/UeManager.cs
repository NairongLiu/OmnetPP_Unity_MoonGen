using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UeManager : MonoBehaviour
{
    private GameObject[] ueObjects;
    public string ulInfo = "";
    public string dlInfo = "";
    public string mobilityInfo = "";
    public UeBase ueSelect = null;

    void Start()
    {
        StartCoroutine(FindUe());
        ueObjects = GameObject.FindGameObjectsWithTag("Ue");
        
    }

    private IEnumerator FindUe()
    {
        yield return new WaitForSeconds(2f);
        GameObject[] ueObjects = GameObject.FindGameObjectsWithTag("Ue");
    }

        private void Update()
    {
        ueSelect = null;

        GameObject[] ueObjects = GameObject.FindGameObjectsWithTag("Ue");
        if (ueObjects != null)
        {
            ulInfo = "";
            dlInfo = "";
            mobilityInfo = "";
            foreach (GameObject ueObject in ueObjects)
            {
                UeBase ueBase = ueObject.GetComponent<UeBase>();

                if (ueBase != null)
                {
                    ulInfo += ueBase.ulInfo + "\n";
                    dlInfo += ueBase.dlInfo + "\n";
                    mobilityInfo += ueBase.mobilityInfo + "\n";
                    if (ueBase.isSelect) { ueSelect = ueBase; }
                }
                else
                {
                    Debug.LogWarning("No UeBase component found on " + ueObject.name);
                }
            }

            //Debug.Log(ulInfo + dlInfo);

        }
    }
}