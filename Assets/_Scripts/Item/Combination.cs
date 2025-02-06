using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Combination : MonoBehaviour
{
    public XRLockSocketInteractor lockSock_Inter;
    [Tooltip("장착할 수 있는 아이템의 개수")]
    public int itemCount;
    [Tooltip("Keys Length == Attach Length")]
    public List<Key> m_Keys;
    [Tooltip("Keys Length == Attach Length")]
    public List<Transform> m_Attach;
    [Tooltip("장착할 수 있는 KeyChain의 Key와 장착될 부위의 Transform")]
    public Dictionary<Key, Transform> m_CombiItem = new Dictionary<Key, Transform>();
    public LayerMask thisObjLayer;
    private Dictionary<Key, Transform> addedItemDic = new Dictionary<Key, Transform>();
    protected virtual void Awake()
    {
        lockSock_Inter = GetComponent<XRLockSocketInteractor>();
        lockSock_Inter.selectEntered.AddListener(OnItemConnected);
        lockSock_Inter.selectExited.AddListener(OnItemDeConnected);
        CombinationItemAdd();
    }
    protected virtual void CombinationItemAdd()
    {
        if (m_Keys.Count != m_Attach.Count)
        {
            Debug.LogError("Keys와 Attach의 개수가 동일해야합니다.");
            return;
        }
        try
        {
            for (int i = 0; i < itemCount; i++)
            {
                m_CombiItem.Add(m_Keys[i], m_Attach[i]);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        finally
        {
            Debug.Log("Complete");
        }
    }

    //장착 가능한 아이템을 찾음
    protected virtual void FindCombinableObj(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICombinable>(out ICombinable obj))
        {
            //등록해둔 requiredKeys를 other가 보유하는지 확인
            Debug.Log("감지");
            foreach (Key key in m_Keys)
            {
                Debug.Log("확인");
                if (obj.keychain.Contains(key) && m_CombiItem.ContainsKey(key))
                {
                    Debug.Log($"찾아온 아이템 이름 : {other.gameObject.name}");
                    //새로운 키 등록 전 requiredKeys초기화
                    lockSock_Inter.keychainLock.requiredKeys.Clear();
                    //other의 requiredKey를 Socket에 등록 
                    lockSock_Inter.keychainLock.requiredKeys.Add(key);
                    //requiredKey로 Dictionary에서 Attach 위치를 찾아와 Socket에 적용
                    lockSock_Inter.attachTransform = m_CombiItem[key];
                    break;
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        FindCombinableObj(other);
    }

    //아이템이 장착될 시 일어나는 이벤트
    protected virtual void OnItemConnected(SelectEnterEventArgs args)
    {
        //args.interactableObject->grab interactable
        //args.interactorObject->poke interacter, etc....

        Debug.Log("장착됨");

        args.interactableObject.transform.gameObject.
        GetComponent<BoxCollider>().excludeLayers = thisObjLayer;

        //TODO 좆버그 씨발련아
        if (args.interactableObject.transform.gameObject.
        TryGetComponent<Combinable>(out Combinable component))
        {
            addedItemDic.Add(component.GetRequiarKey(), m_CombiItem[component.GetRequiarKey()]);
            m_CombiItem.Remove(component.GetRequiarKey());
        }
    }

    protected virtual void OnItemDeConnected(SelectExitEventArgs args)
    {
        args.interactableObject.transform.gameObject.
        GetComponent<BoxCollider>().excludeLayers -= thisObjLayer;

        //TODO 좆버그 씨발련아
        if (args.interactableObject.transform.gameObject.
        TryGetComponent<Combinable>(out Combinable component))
        {
            if (addedItemDic.ContainsKey(component.GetRequiarKey()))
            {
                m_CombiItem.Add(component.GetRequiarKey(), addedItemDic[component.GetRequiarKey()]);
            }
        }
    }
}