using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Class ini bertugas sebagai pemancar (publisher) event.
public class PemancarEvent : MonoBehaviour
{
    // Event adalah notifikasi yang bisa didengarkan oleh script lain.
    // Action berarti event ini tidak mengirim parameter apa pun.
    // static membuat event dimiliki oleh class, bukan oleh satu object tertentu.
    public static event Action OnTekanSpasi;

    void Update()
    {
        // Keyboard.current bisa bernilai null jika perangkat keyboard belum tersedia.
        // wasPressedThisFrame hanya bernilai true pada frame ketika tombol baru ditekan.
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Pesan ini membantu melihat kapan event dipancarkan melalui Console Unity.
            Debug.Log("Pemancar: spasi ditekan, kirim event.");

            // Memanggil semua method yang sudah berlangganan event.
            // ?. membuat pemanggilan dilewati jika belum ada subscriber,
            // sehingga tidak terjadi NullReferenceException.
            OnTekanSpasi?.Invoke();
        }
    }
}
