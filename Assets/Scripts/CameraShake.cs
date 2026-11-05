using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;
        float noiseSpeed = 20f;
        float seedOffset = 5f; // keeps X and Y sampling different points in the noise

        while (elapsed < duration)
        {
            float xNoise = Mathf.PerlinNoise(Time.time * noiseSpeed, 0f) - 0.5f;
            float yNoise = Mathf.PerlinNoise(0f, Time.time * noiseSpeed + seedOffset) - 0.5f;

            Vector3 offset = new Vector3(xNoise, yNoise, 0f) * magnitude;
            transform.position = originalPos + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}