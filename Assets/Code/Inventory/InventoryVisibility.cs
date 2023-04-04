using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryVisibility : MonoBehaviour
{
    protected PlayerInput playerInput;
	protected InputAction InventoryUI;

	[SerializeField] private GameObject PlayerInventory;
    public bool inventorIsVisibil;

	public void Awake()
	{
		playerInput = new PlayerInput();
	}

	public void Resume()
	{
		PlayerInventory.SetActive(false);
		Time.timeScale = 1f;
		inventorIsVisibil = false;
	}

	public void Inventory(InputAction.CallbackContext context)
	{
		inventorIsVisibil = !inventorIsVisibil;

		if(inventorIsVisibil)
		{
			ActivateInventoy();
		}
		else
		{
			DeactivateInventory();
		}
	}

	public void OnEnable()
	{
		InventoryUI = playerInput.InventoryUI.TAB;
		InventoryUI.Enable();
		InventoryUI.performed += Inventory;
	}

	public void OnDisable()
	{
		InventoryUI.Disable();
	}

	public void ActivateInventoy()
	{
		PlayerInventory.SetActive(true);
	}

	public void DeactivateInventory()
	{
		PlayerInventory.SetActive(false);
		inventorIsVisibil = false;
	}
}
