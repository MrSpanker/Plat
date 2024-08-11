using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class RehabObject : MonoBehaviour
{
    private Collider2D _collider2D;
    private DecayManager _decayManager;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _decayManager = FindObjectOfType<DecayManager>();
        transform.DOScale(1.6f,3f).From().SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _decayManager.ResetDecay();
            Destroy(gameObject);
        }
    }
}