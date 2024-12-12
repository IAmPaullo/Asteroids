using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
[HelpURL("https://www.whatupgames.com/blog/code-the-grid-based-inventory-system")]
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    private VisualElement m_Root;
    private VisualElement m_InventoryGrid;

    private static Label m_ItemDetailHeader;
    private static Label m_ItemDetailBody;
    private static Label m_ItemDetailPrice;
    private bool m_IsInventoryReady;
    public static Dimensions SlotDimension { get; private set; }

    public List<StoredItem> StoredItems = new();
    public Dimensions InventoryDimensions;

    private VisualElement m_Telegraph;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Configure();
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    private void Start() => LoadInventory();

    private async void LoadInventory()
    {
        await UniTask.WaitUntil(() => m_IsInventoryReady);

        foreach (StoredItem loadedItem in StoredItems)
        {
            ItemVisual inventoryItemVisual = new(loadedItem.Details);
            AddItemToInventoryGrid(inventoryItemVisual);

            bool inventoryHasSpace = await GetPositionForItem(inventoryItemVisual);
            if (!inventoryHasSpace)
            {
                Debug.Log("No Space - Cannot Pickup item");
                RemoveItemFromInventoryGrid(inventoryItemVisual);
                continue;
            }
            ConfigureInventoryItem(loadedItem, inventoryItemVisual);
        }
    }
    private void AddItemToInventoryGrid(VisualElement item) => m_InventoryGrid.Add(item);
    private void RemoveItemFromInventoryGrid(VisualElement item) => m_InventoryGrid.Remove(item);
    private static void ConfigureInventoryItem(StoredItem item, ItemVisual visual)
    {
        item.RootVisual = visual;
        visual.style.visibility = Visibility.Visible;
    }
    private async void Configure()
    {
        m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;
        m_InventoryGrid = m_Root.Q<VisualElement>("Grid");

        VisualElement itemDetails = m_Root.Q<VisualElement>("ItemDetails");
        m_ItemDetailHeader = itemDetails.Q<Label>("ItemName");
        m_ItemDetailBody = itemDetails.Q<Label>("ItemDescription");
        m_ItemDetailPrice = itemDetails.Q<Label>("SellPrice");
        ConfigureInventoryTelegraph();
        await UniTask.WaitForEndOfFrame();

        ConfigureSlotDimensions();

        m_IsInventoryReady = true;
    }
    private void ConfigureSlotDimensions()
    {
        VisualElement firstSlot = m_InventoryGrid.Children().First();

        SlotDimension = new Dimensions
        {
            Width = Mathf.RoundToInt(firstSlot.worldBound.width),
            Height = Mathf.RoundToInt(firstSlot.worldBound.height)
        };
    }
    private void ConfigureInventoryTelegraph()
    {
        m_Telegraph = new VisualElement
        {
            name = "Telegraph",
            style =
            {
                position = Position.Absolute,
                visibility = Visibility.Hidden,
            }
        };
        m_Telegraph.AddToClassList("slot-icon-highlighted");
        AddItemToInventoryGrid(m_Telegraph);
    }
    public static void UpdateItemDetails(ItemDefinition item)
    {
        m_ItemDetailHeader.text = item.FriendlyName;
        m_ItemDetailBody.text = item.Description;
        m_ItemDetailPrice.text = item.SellPrice.ToString();
    }
    private async Task<bool> GetPositionForItem(VisualElement newItem)
    {
        for (int y = 0; y < InventoryDimensions.Height; y++)
        {
            for (int x = 0; x < InventoryDimensions.Width; x++)
            {
                SetItemPosition(newItem, new(SlotDimension.Width * x,
                    SlotDimension.Height * y));

                await UniTask.WaitForEndOfFrame();

                StoredItem overlappingItem = StoredItems.FirstOrDefault(s =>
                s.RootVisual != null &&
                s.RootVisual.layout.Overlaps(newItem.layout));

                if (overlappingItem == null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public (bool canPlace, Vector2 position) ShowPlacementTarget(ItemVisual draggedItem)
    {
        if (!m_InventoryGrid.layout.Contains(new Vector2(draggedItem.localBound.xMax,
            draggedItem.localBound.yMax))) // is out of the edge?
        {
            m_Telegraph.style.visibility = Visibility.Hidden;
            return (canPlace: false, position: Vector2.zero);
        }

        VisualElement targetSlot = m_InventoryGrid.Children().Where(x =>
            x.layout.Overlaps(draggedItem.layout) && x != draggedItem).OrderBy(x =>
            Vector2.Distance(x.worldBound.position,
            draggedItem.worldBound.position)).First(); //Finds the closest inventory grid slot relative to
                                                       //the dragged item by checking for all overlapping elements and sorting by distance.

        m_Telegraph.style.width = draggedItem.style.width;
        m_Telegraph.style.height = draggedItem.style.height;


        SetItemPosition(m_Telegraph, new Vector2(targetSlot.layout.position.x,
            targetSlot.layout.position.y));

        m_Telegraph.style.visibility = Visibility.Visible;

        var overlappingItems = StoredItems.Where(x => x.RootVisual != null &&
            x.RootVisual.layout.Overlaps(m_Telegraph.layout)).ToArray();  //Check whether the target location of the dragged item is
                                                                          //overlapping any other ItemVisuals,
                                                                          //and if so returns false and Vector2.zero.

        if (overlappingItems.Length > 1)
        {
            m_Telegraph.style.visibility = Visibility.Hidden;
            return (canPlace: false, position: Vector2.zero);
        }
        return (canPlace: true, targetSlot.worldBound.position); // everything went well and you can place the item
    }
    private static void SetItemPosition(VisualElement element, Vector2 vector)
    {
        element.style.left = vector.x;
        element.style.top = vector.y;
    }
}
