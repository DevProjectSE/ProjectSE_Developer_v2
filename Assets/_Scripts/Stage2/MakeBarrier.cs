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
    public float intensitycontrol;

    
    private void OnTriggerExit(Collider ohter)
    {
        if (ohter.CompareTag("Player"))
        {
            print("ºÎµúÈû");
            barrierObj.isTrigger = false;

            StartCoroutine(ChangeBrightnessCoroutine());

            
        }
    }


    private void IntensityControl()
    {
        RenderSettings.ambientIntensity = intensitycontrol;
    }
    private IEnumerator ChangeBrightnessCoroutine()
    {
        print("ÄÚ·çÆ¾ ½ÃÀÛµÊ");
        //float courrentIntensity = lightSource.intensity;
        //float currentAngleX = lightSource.transform.localRotation.eulerAngles.x;
        //float elapsedTime = 0f;
        float courrentIntensity = RenderSettings.ambientIntensity;
        float currentAngleX = lightSource.transform.localRotation.eulerAngles.x;
        float elapsedTime = 0f;


        while (elapsedTime < chageSpeed)
        {
            //lightSource.intensity = Mathf.Lerp(courrentIntensity, targetIntensity, elapsedTime / chageSpeed);
            RenderSettings.ambientIntensity = Mathf.Lerp(courrentIntensity, targetIntensity, elapsedTime / chageSpeed);
            float angleDifference = Mathf.DeltaAngle(currentAngleX, targetAngle);
            float newAngle = currentAngleX + angleDifference * (elapsedTime / chageSpeed);

            lightSource.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(newAngle, targetAngle, elapsedTime / chageSpeed), 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        lightSource.intensity = targetIntensity;
        RenderSettings.ambientIntensity = targetIntensity;
        lightSource.transform.rotation = Quaternion.Euler(targetAngle, 0, 0);
        Destroy(gameObject);
    }
}
