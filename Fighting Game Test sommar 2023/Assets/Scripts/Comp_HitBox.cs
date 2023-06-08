using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_HitBox : MonoBehaviour, IHitDetector
{

    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LayerMask _layerMask;

    private float _thickness = 0.025f;
    private IHitResponder _hitResponder;

    public IHitResponder HitResponder { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void CheckHit()
    {
        Vector2 _scaleSize = new Vector2(_collider.size.x * transform.lossyScale.x, _collider.size.y*transform.lossyScale.y);

        float _distance = _scaleSize.y - _thickness;

        Vector2 _direction = transform.up;
     
    }
}
