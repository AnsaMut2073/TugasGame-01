using UnityEngine;

// Class ini bertugas sebagai penerima (subscriber) event dari PemancarEvent.
public class PenerimaEvent : MonoBehaviour
{
    // Dipanggil Unity ketika GameObject dan component ini aktif.
    void OnEnable()
    {
        // += mendaftarkan method Reaksi sebagai pendengar event.
        // Setelah terdaftar, Reaksi akan dipanggil saat tombol spasi ditekan.
        PemancarEvent.OnTekanSpasi += Reaksi;
    }

    // Dipanggil Unity ketika GameObject atau component ini dinonaktifkan.
    void OnDisable()
    {
        // -= menghapus Reaksi dari daftar pendengar event.
        // Ini penting agar object nonaktif tidak lagi menerima event.
        PemancarEvent.OnTekanSpasi -= Reaksi;
    }

    // Method ini harus memiliki bentuk yang sama dengan event Action,
    // yaitu tanpa parameter dan tidak mengembalikan nilai.
    void Reaksi()
    {
        // Kode ini dijalankan ketika PemancarEvent mengirim event.
        Debug.Log("Penerima: aku dengar event spasi!");
    }
}
