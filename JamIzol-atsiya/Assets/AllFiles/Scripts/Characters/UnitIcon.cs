using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitIcon : MonoBehaviour
{
	[SerializeField] private Image icon;
	[SerializeField] private GameObject iconCount;
	[SerializeField] private Text iconCountText;

	public int Counter { get; set; }
	public int Id { get; set; }

	public Image Icon
	{
		get { return icon; }
	}

	public GameObject IconCount
	{
		get { return iconCount; }
	}

	public Text IconCountText
	{
		get { return iconCountText; }
	}

	public void GetCurrent()
	{
		UnitSelect.Internal.GetCurrentUnit(Id);
	}
}