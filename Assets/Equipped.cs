using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This script is being called from the animator that this script is attached to

public class Equipped : MonoBehaviour
{
    [SerializeField] GameObject weapon;

    public void StartDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealerScript>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealerScript>().EndDealDamage();
    }

}
