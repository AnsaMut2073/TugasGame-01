# PTS Game Dev 11-09-2026

## Identitas Siswa
- **Nama:** Anisa Mutiara Tunggadewi
- **Absen:** 05
- **Kelas:** XI PPLG 1

## Fitur Game
1. **GameManager:** Mengatur 4 State (MainMenu, Playing, Paused, GameOver).
2. **PlayerMovement:** Pergerakan player menggunakan Rigidbody2D.
3. **OOP:**
   - *Inheritance:* Script `Zombie` mewarisi script `Enemy`.
   - *Polymorphism:* Script `Zombie` meng-override method `Attack()` dan `TakeDamage()`.
   - *Abstraction:* Menggunakan Interface `IDamageable`.
4. **Delegate & Events:**
   - `GameEvents.cs` (Delegate)
   - `Coin.cs` (Pemancar Event)
   - `UIManager.cs` (Penerima Event)

## Cara Menjalankan
1. Clone repository ini.
2. Buka project menggunakan Unity Hub.
3. Buka scene `MainScene.unity`.
4. Tekan tombol Play.
