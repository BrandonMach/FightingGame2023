using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData 
{
    public int damage;
    public Vector2 hitPoint;
    public Vector2 hitNormal;
    public IHurtbox hurtBox;
    public IHitDetector hitDetectector;

    
    public bool Validate()
    {
        if(hurtBox != null)
        {
            if (hurtBox.Checkhit(this))
            {
                if(hurtBox.HurtResponder == null || hurtBox.HurtResponder.CheckHit(this))
                {
                    if (hitDetectector.HitResponder == null || hitDetectector.HitResponder.CheckHit(this))
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }
}

public interface IHitResponder
{
    int Damage { get; }

    public bool CheckHit(HitData data);
    public void Response(HitData data);
}
public interface IHitDetector
{
    public IHitResponder HitResponder { get; set; }
    public void CheckHit();
}
public interface IHurtResponder
{
    public bool CheckHit(HitData data);
    public void Response(HitData data);
}
public interface IHurtbox
{
    public bool Active { get; }

    public GameObject Owner { get; }
    public Transform Transform { get; }
    public IHurtResponder HurtResponder { get; set; }
    public bool Checkhit(HitData data);
}
