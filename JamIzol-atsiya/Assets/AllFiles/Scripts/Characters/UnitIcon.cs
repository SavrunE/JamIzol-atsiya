using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitIcon : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private GameObject _iconCount;
	[SerializeField] private Text _iconCountText;

	public int counter { get; set; }
	public int id { get; set; }

	public Image icon
	{
		get { return _icon; }
	}

	public GameObject iconCount
	{
		get { return _iconCount; }
	}

	public Text iconCountText
	{
		get { return _iconCountText; }
	}

	public void GetCurrent()
	{
		UnitSelect.Internal.GetCurrentUnit(id);
	}
}