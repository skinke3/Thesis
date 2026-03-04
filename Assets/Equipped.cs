using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
