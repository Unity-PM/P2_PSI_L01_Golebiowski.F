using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Text AmmunitionText1;
    [SerializeField]
    private Text AmmunitionText2;
    [SerializeField]
    private GameObject HitMarker;
    [SerializeField] 
    private Weapon playerWeapon;

    void Start()
    {
        playerWeapon.AmmoUpdate += AmmoUpdate;
        playerWeapon.Hit += Hit;

        HitMarker.gameObject.SetActive(false);
    }

    private void AmmoUpdate(int Ammunition, int maxAmmunition)
    {

        AmmunitionText1.text = "" + Ammunition;
        AmmunitionText2.text = "" + maxAmmunition;
    }
    private void Hit()
    {
        HitMarker.gameObject.SetActive(true);
        StartCoroutine(DisableHitMarker());
    }

    IEnumerator DisableHitMarker()
    {
        yield return new WaitForSeconds(0.05f);
        HitMarker.gameObject.SetActive(false);
    }
}
