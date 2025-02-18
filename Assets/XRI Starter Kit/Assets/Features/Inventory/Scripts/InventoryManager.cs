﻿using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace MikeNspired.UnityXRHandPoser
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference openMenuInputLeftHand;
        private InventorySlot[] inventorySlots;
        public ActionBasedController leftController = null, rightController = null;
        [SerializeField] private AudioSource enableAudio = null, disableAudio = null;

        [SerializeField] private bool lookAtController = false;
        private bool isActive = false;

        private void Start()
        {
            OnValidate();

            foreach (var itemSlot in inventorySlots)
                itemSlot.StartCoroutine(itemSlot.CreateStartingItemAndDisable());

            openMenuInputLeftHand.GetInputAction().performed += x => ToggleInventoryAtController(isActive);
        }

        private void OnValidate()
        {
            inventorySlots = GetComponentsInChildren<InventorySlot>();
        }
        private void OnEnable()
        {
            openMenuInputLeftHand.EnableAction();
        }

        private void OnDisable()
        {
            openMenuInputLeftHand.DisableAction();
        }

        public void ToggleInventoryAtController(bool isRightHand)
        {
            if (isRightHand)
                TurnOnInventory(rightController.gameObject);
            else
                TurnOnInventory(leftController.gameObject);
        }

        private void TurnOnInventory(GameObject hand)
        {
            isActive = !isActive;
            Debug.Log($"Inventory Active State: {isActive}"); // 상태 확인 로그
            ToggleInventoryItems(isActive, hand);
            //PlayAudio(isActive);
        }

        private void PlayAudio(bool state)
        {
            if (state)
                enableAudio.Play();
            else
                disableAudio.Play();
        }

        private void ToggleInventoryItems(bool state, GameObject hand)
        {
            foreach (var itemSlot in inventorySlots)
            {
                if (!state)
                {
                    itemSlot.DisableSlot();
                    itemSlot.gameObject.SetActive(false);
                }
                else
                {
                    if (itemSlot.gameObject.activeSelf)
                        itemSlot.EnableSlot();
                    itemSlot.gameObject.SetActive(state);
                    SetPositionAndRotation(hand);
                }
            }
        }

        private void SetPositionAndRotation(GameObject hand)
        {
            transform.position = hand.transform.position;
            transform.localEulerAngles = Vector3.zero;

            if (lookAtController)
                SetPosition(hand.transform);
            else
                transform.LookAt(Camera.main.transform);
        }

        private void SetPosition(Transform hand)
        {
            var handDirection = hand.transform.forward;
            transform.transform.forward = Vector3.ProjectOnPlane(-handDirection, transform.up);
        }
    }
}