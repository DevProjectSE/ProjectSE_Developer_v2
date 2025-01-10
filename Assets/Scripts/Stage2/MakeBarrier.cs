using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBarrier : MonoBehaviour
{
    public Collider barrierObj;

    public Light lightSource;
    public float targetIntensity;
    public float targetAngle;
    private float chageSpeed = 5f;

    
    private void OnTriggerExit(Collider ohter)
    {
        if (ohter.CompareTag("Player"))
        {
            print("ºÎµúÈû");
            barrierObj.isTrigger = false;

            StartCoroutine(ChangeBrightnessCoroutine());

            
        }
    }


    private IEnumerator ChangeBrightnessCoroutine()
    {
        print("ÄÚ·çÆ¾ ½ÃÀÛµÊ");
        float courrentIntensity = lightSource.intensity;
        float currentAngleX = lightSource.transform.localRotation.eulerAngles.x;
        float elapsedTime = 0f;

        print(currentAngleX);


        while (elapsedTime < chageSpeed)
        {
            lightSource.intensity = Mathf.Lerp(courrentIntensity, targetIntensity, elapsedTime / chageSpeed);
            float angleDifference = Mathf.DeltaAngle(currentAngleX, targetAngle);
            float newAngle = currentAngleX + angleDifference * (elapsedTime / chageSpeed);

            lightSource.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(newAngle, targetAngle, elapsedTime / chageSpeed), 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        lightSource.intensity = targetIntensity;
        lightSource.transform.rotation = Quaternion.Euler(targetAngle, 0, 0);
        Destroy(gameObject);
    }
}
