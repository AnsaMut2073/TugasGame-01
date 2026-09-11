using System;
using UnityEngine;

// MonoBehaviour membuat class ini bisa dipasang sebagai component pada GameObject.
public class BelajarDelegate : MonoBehaviour
{
    // Delegate adalah "tipe" yang bisa menyimpan referensi ke method.
    // AksiSederhana hanya bisa menunjuk method yang tidak menerima parameter
    // dan tidak mengembalikan nilai (void).
    delegate void AksiSederhana();

    void Start()
    {
        // Start dipanggil Unity satu kali saat object mulai aktif.
        // Tiga method berikut menunjukkan cara kerja delegate secara bertahap.
        Langkah1_SimpanSatuMethod();
        Langkah2_BeberapaMethodSekaligus();
        Langkah3_ActionSiapPakai();
    }

    void Langkah1_SimpanSatuMethod()
    {
        // Variabel kotak menyimpan referensi ke method TulisHalo.
        AksiSederhana kotak = TulisHalo;

        // Menulis kotak() sama artinya dengan memanggil TulisHalo().
        kotak();
    }

    void Langkah2_BeberapaMethodSekaligus()
    {
        // Delegate bisa dibuat dengan method pertama sebagai isinya.
        AksiSederhana kotak = TulisHalo;

        // Operator += menambahkan method lain ke daftar pemanggilan delegate.
        kotak += TulisDunia;

        // Semua method dalam daftar dipanggil sesuai urutan penambahannya:
        // TulisHalo() lalu TulisDunia().
        kotak();
    }

    void Langkah3_ActionSiapPakai()
    {
        // Action adalah delegate bawaan C# untuk method void tanpa parameter.
        // Jadi, Action bisa digunakan tanpa mendeklarasikan delegate sendiri.
        Action kotak = TulisHalo;
        kotak += TulisDunia;
        kotak();
    }

    void TulisHalo()
    {
        // Method ini cocok dengan bentuk AksiSederhana dan Action:
        // tidak menerima parameter dan hasilnya void.
        Debug.Log("Halo");
    }

    void TulisDunia()
    {
        // Method ini juga bisa ditambahkan ke delegate yang sama
        // karena memiliki bentuk/signature yang sesuai.
        Debug.Log("Dunia");
    }
}
