using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [Header("Attacks")]
    [SerializeField] private GameObject _kickHitBox;
    [SerializeField] private GameObject _punchHitBox;



    //Hitbox
    private void EnableHitbox(GameObject hitbox)
    {
        hitbox.SetActive(true);
    }
    private void DisableHitbox(GameObject hitbox)
    {
        hitbox.SetActive(false);
    }


    //Kick
    public void HighKick()
    {
        _anim.SetTrigger("High_Kick");

    }
    public void EnableKick()
    {
        EnableHitbox(_kickHitBox);
    }
    public void DisableKick()
    {
        DisableHitbox(_kickHitBox);
    }


    //Punch
    public void Punch()
    {
        _anim.SetTrigger("Punch");

    }
    public void EnablePunch()
    {
        Debug.LogError("yo");
        EnableHitbox(_punchHitBox);
    }
    public void DisablePunch()
    {
        Debug.LogError("noo");
        DisableHitbox(_punchHitBox);
    }



}
