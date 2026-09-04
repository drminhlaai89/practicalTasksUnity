# Player Hit Feedback System (Task 3 - Interview Assessment)

A polished, performance-optimized Player Hit Feedback system built in Unity, featuring dynamic UI updates, camera responses, and visual effects tied to health state changes.

---

## 🛠 Tech Stack & Dependencies
* **Engine:** Unity 6 (6000.0.83f1 - 3D Pipeline)
* **Animation Engine:** DOTween (HOTween v2)
* **Text Rendering:** TextMeshPro (World Space & UI)

---

## 🚀 Key Features & Architecture

### 1. Dynamic Health Bar (`HpBar.cs`)
* **Dual-Bar Visuals:** Features a primary health bar and a delayed secondary "damage fill" bar to emphasize impact.
* **Tween-Driven:** Utilizes `DOFillAmount` with custom easing functions (`Ease.OutQuad`) for smooth transitions instead of abrupt jumps.

### 2. Floating Damage Numbers (`NumbersSpawner.cs` & `DamageText.cs`)
* **Custom Object Pooling:** Avoids `Instantiate`/`Destroy` garbage collection (GC) overhead during high-frequency combat by recycling active text elements via a generic Queue pool.
* **World-Space Motion:** Animated using DOTween Sequences combining scaling, directional displacement (`DOMoveY`), and alpha fading (`DOFade`). Auto-rotates toward the main camera (`Camera.main.transform.forward`).

### 3. Centralized Feedback Coordination (`HitFeedbackManager.cs`)
Decoupled coordination triggered via C# Action Events (`PlayerHealth.OnDamage`):
* **Screen Hit Flash:** UI Overlay panel with rapid alpha fading (`DOFade`).
* **Camera Shake:** Procedural camera displacement (`DOShakePosition`) for physical hit response.
* **Impact & Blood VFX:** Triggers an initial impact wave (`RoundHitBlue`) and randomly selects from a set of particle variations (`BloodSplat`) to prevent visual monotony.
* **Animator Integration:** Triggers character hit reactions (`GetHit`) in sync with visual effects.

---

## 🎮 How to Test in Scene
1. Open scene: `Assets/Task 3/Scenes/Task 3.unity`.
2. Enter **Play Mode**.
3. Press **`Spacebar`** to trigger player damage and inspect the synchronized hit feedback loop.