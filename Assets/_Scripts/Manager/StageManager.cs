using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class StageManager : MonoBehaviour
{
    public enum STAGE { STAGE1, STAGE2, STAGE3, STAGE4, STAGE5, STAGE6 };
    public STAGE stage;

    [Header("Stage1_DialogSystem")]
    public DialogSystem FirstTutorialDialog;
    public DialogSystem SecondTutorialDialog;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        switch (stage)
        {
            case STAGE.STAGE1:
                //오브젝트를 쭉 훑어본 후 플레이어의 위치로 카메라가 이동하며 이후 플레이어가 움직일 수 있다.
                GameManager.Instance.Player.GetComponentInChildren<InputActionManager>().enabled = false;
                //대사 내용 출력 이후 플레이어 캐릭터 조작 활성화
                FirstTutorialDialog.gameObject.SetActive(true);

                //퀘스트 알림 UI 활성화 2초간 활성화 이후 해당 UI가 축소되고 좌측 상단에 고정
                //시야 방향키 알림 UI 오른쪽에 출력
                //컨트롤러를 통해 시야 방향이 변경되면 시야 알림 UI 비활성화
                //이후 이동 방법을 알려주는 UI 출력
                //컨트롤러를 통해 이동하면 이동 UI 비활성화

                //냉장고 앞까지 이동 시 최초로 1번 그립 버튼에 대한 UI 우측에 활성화 
                //컨트롤러를 이용하여 그립 버튼이 눌리면 그립 UI 비활성화 (냉장고 문을 놓아도 닫히지 않는다.)
                //앉기 버튼에대한 UI 우측에 활성화
                //컨트롤러 키를 이용하여 캐릭터가 앉으면 앉기 UI 비활성화 
                //1초뒤 아이템 잡기 설명 UI출력
                //컨트롤러를 이용하여 음료수를 잡으면 1초뒤 아이템 잡기 설명 UI 비활성화
                //음료수를 잡으면 1초뒤 아이템을 사용하는 키를 알려주는 UI 활성화 
                //컨트롤러를 이용하여 캔 뚜껑을 따면 UI 비활성화 
                //음료수를 지정된 구역 내(헤드셋 부근)로 위치시키면 음료수를 마시는 효과음 재생.
                //효과음이 종료되면 좌측 상단에 있는 퀘스트 UI 비활성화 ->캐릭터 상태 변화 X
                //음료수를 마시고 1초뒤 아이템을 오브젝트와 상호작용을 하는 UI 활성화
                //화면 중앙 하단에 [페트병을 쓰레기통으로 버리세요] 텍스트 2초간 출력
                //이후 폰트와 텍스트 박스의 크기를 원래 크기의 75%로 축소한 후 우측 상단으로 고정 
                //아이템 오브젝트의 상호작용이 완료되면 1초뒤 텍스트와 우측 상단 UI 비활성화

                //버린 아이템은 상호작용이 불가능하다.
                //아이템 사용을 마치고 3초 뒤 전화벨 소리 재생, 이때 모든 컨트롤러는 전화벨이 울리는 주기에 맞춰 햅틱 반응
                //핸드폰 아이템을 사용하기 전까지 반복된다. 핸드폰과 유저 사이의 거리 상관 없이, 핸드폰의 음량은 동일하다.
                //화면 중앙 하단에 [핸드폰을 받으세요] 텍스트 2초간 출력
                //이후 폰트와 텍스트 크기가 75%로 축소된 후 좌측 상단으로 이동하여 고정된다.
                //이때 인벤토리 출력을 알려주는 UI가 활성화 된다.
                //인벤토리를 열면 인벤토리 알림 UI 비활성화
                //음료수를 마실 때와 같은 방법으로 핸드폰 상호작용
                //아이템 사용 버튼을 누르면 전화벨 소리와 햅틱 반응 종료, 좌측 상단에 있던 지시사항 UI비활성화
                //아이템 사용 버튼을 누르면 대사가 출력된다.
                SecondTutorialDialog.gameObject.SetActive(true);

                //이때 핸드폰 사용하는 도중에 인벤토리에 핸드폰을 넣을 수 없다.
                //모든 대사가 종료되면 화면에 검은색 화면으로 페이드 아웃된다.
                //핸드폰의 잡기 고정 상태가 해제한다.
                //2스테이지로 넘어가며 로딩 화면이 나타난다.
                GameManager.Instance.isStage1Clear = true;

                break;
            case STAGE.STAGE2:
                break;
            case STAGE.STAGE3:
                break;
            case STAGE.STAGE4:
                break;
            case STAGE.STAGE5:
                break;
            case STAGE.STAGE6:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
