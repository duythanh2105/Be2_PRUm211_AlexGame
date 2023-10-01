using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS : MonoBehaviour
{
    public Transform player; // Kéo và thả đối tượng người chơi vào đây trong trình chỉnh sửa Unity.

    private void Update()
    {
        if (player == null)
        {
            // Tránh lỗi nếu không tìm thấy người chơi.
            return;
        }

        // Tính toán hướng từ EW02 đến người chơi.
        Vector3 direction = player.position - transform.position;

        if (direction.x < 0)
        {
            // Lật theo hướng người chơi bên phải.
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x > 0)
        {
            // Lật theo hướng người chơi bên trái.
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
