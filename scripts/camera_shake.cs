using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camera_shake : MonoBehaviour
{
	public Camera camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos;

	private float shake_amount_local;
	private float decrease_factor_local;
	private float shake_duration_local;

    private void Start()
    {
        originalPos = camTransform.transform.position;
		shake_duration_local =0f;
		shake_amount_local = shakeAmount;
		decrease_factor_local = decreaseFactor;
	}
	public void do_shake()
    {
		shake_duration_local = shakeDuration;
		shake_amount_local = shakeAmount;
		decrease_factor_local = decreaseFactor;
    }
    void Update()
	{
		originalPos = camTransform.transform.position;
		if (shake_duration_local > 0)
		{
			camTransform.transform.localPosition = originalPos + Random.insideUnitSphere * shake_amount_local;

			shake_duration_local -= Time.deltaTime * decrease_factor_local;
		}
		else
		{
			shake_duration_local = 0f;
			camTransform.transform.localPosition = originalPos;
		}
	}
}