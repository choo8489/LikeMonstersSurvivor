using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBar : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        TryGetComponent(out rect);
    }

    private void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.Player.transform.position);
    }
}
