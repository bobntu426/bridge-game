using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class catch_table : NetworkBehaviour
{
    public static uint id;
    public override void OnStartClient()
    {
        id=gameObject.GetComponent<NetworkIdentity>().netId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
