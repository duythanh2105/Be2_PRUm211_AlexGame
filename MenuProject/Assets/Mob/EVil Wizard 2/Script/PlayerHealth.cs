using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Số lượng sức kháng cự tối đa của người chơi
    private int currentHealth;  // Sức kháng cự hiện tại của người chơi

    void Start()
    {
        // Khởi tạo sức kháng cự hiện tại bằng sức kháng cự tối đa
        currentHealth = maxHealth;
    }

    // Phương thức để gây sát thương cho người chơi
    public void TakeDamage(int damageAmount)
    {
        // Giảm sức kháng cự của người chơi dựa trên lượng sát thương
        currentHealth -= damageAmount;

        // Kiểm tra nếu sức kháng cự hiện tại âm hoặc bằng 0
        if (currentHealth <= 0)
        {
            // Xử lý logic khi người chơi chết
            Destroy(gameObject);
        }
    }
}
