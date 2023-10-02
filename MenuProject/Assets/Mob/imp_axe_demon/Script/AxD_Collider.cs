using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxD_Collider : MonoBehaviour
{
    public GameObject objectToIgnore; // Game object mà bạn muốn collider của game object này đi xuyên qua

    void Start()
    {
        // Kiểm tra xem objectToIgnore và collider của game object này có tồn tại
        if (objectToIgnore != null && GetComponent<Collider2D>() != null && objectToIgnore.GetComponent<Collider2D>() != null)
        {
            // Lấy collider của game object này và của objectToIgnore
            Collider2D thisCollider = GetComponent<Collider2D>();
            Collider2D otherCollider = objectToIgnore.GetComponent<Collider2D>();

            // Cho phép collider của game object này đi xuyên qua collider của objectToIgnore
            Physics2D.IgnoreCollision(thisCollider, otherCollider, true);
        }
    }
}
