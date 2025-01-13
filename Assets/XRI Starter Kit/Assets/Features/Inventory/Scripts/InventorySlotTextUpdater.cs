using TMPro;
using UnityEngine;

namespace MikeNspired.UnityXRHandPoser
{
    public class InventorySlotTextUpdater : MonoBehaviour
    {
        public TextMeshProUGUI currentCount;
        public TextMeshProUGUI maxCount;

        private InventorySlot inventorySlot;

        void Start()
        {
            OnValidate();
            inventorySlot.inventorySlotUpdated.AddListener(CheckTypes);
            CheckTypes();
        }

        private void CheckTypes()
        {
            if (!inventorySlot.CurrentSlotItem) return;

        }

        private void OnValidate()
        {
            if (!inventorySlot)
                inventorySlot = GetComponent<InventorySlot>();
        }
    }
}