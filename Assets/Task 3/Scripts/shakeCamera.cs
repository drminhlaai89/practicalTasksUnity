using UnityEngine;
using System.Collections;

public class shakeCamera : MonoBehaviour
{
    public static shakeCamera Instance { get; private set; }

    private Vector3 originalPos;

    private void Awake()
    {
        // Singleton đơn giản để dễ gọi từ bất kỳ đâu
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    // Hàm gọi hiệu ứng rung camera
    public void Shake(float duration = 0.15f, float magnitude = 0.1f)
    {
        StopAllCoroutines(); // Dừng hiệu ứng cũ nếu đang rung
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null; // Chờ frame tiếp theo
        }

        transform.localPosition = originalPos; // Trả camera về vị trí ban đầu
    }
}
