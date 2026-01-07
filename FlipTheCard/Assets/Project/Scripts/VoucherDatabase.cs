using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VoucherDatabase", menuName = "Game/Voucher Database")]
public class VoucherDatabase : ScriptableObject {
    public List<Voucher> allVouchers;

    // Hàm lấy voucher ngẫu nhiên theo thứ tự
    public Voucher GetRandomVoucher() {
        int index = Random.Range(0, allVouchers.Count);
        return allVouchers[index];
    }
}