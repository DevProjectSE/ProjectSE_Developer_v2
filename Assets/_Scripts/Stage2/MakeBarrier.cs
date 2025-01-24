using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBarrier : MonoBehaviour
{
    //used Dialog Object
    public DialogSystem usedDialogObj;

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
            print("부딪힘");
            barrierObj.isTrigger = false;
            usedDialogObj.gameObject.SetActive(true);   //대사 오브젝트 활성화 되면서 대사 스크립트 실행
            StartCoroutine(ChangeBrightnessCoroutine());

        }
    }

    private void IntensityControl()
    {
        RenderSettings.ambientIntensity = intensitycontrol;
    }
    private IEnumerator ChangeBrightnessCoroutine()
    {
        print("코루틴 시작됨");
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
            Debug.Log(RenderSettings.ambientIntensity);
            lightSource.transform.rotation = Quaternion.Euler(Mathf.LerpAngle(newAngle, targetAngle, elapsedTime / chageSpeed), 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        lightSource.intensity = targetIntensity;
        RenderSettings.ambientIntensity = targetIntensity;
        lightSource.transform.rotation = Quaternion.Euler(targetAngle, 0, 0);
        //Destroy(gameObject);
    }
}
