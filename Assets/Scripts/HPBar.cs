using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
	private Slider slider;
	public Image fill;
	public MeshRenderer plr;
	public bool poisoned = false;

	private void Start()
	{
		slider = GetComponentInChildren<Slider>();
		GetComponentInParent<IHealth>().OnHPPctChanged += HandleHPPctChanged;
	}

    private void Update()
    {
		slider.transform.LookAt(Camera.main.transform);

		if (poisoned)
		{
			fill.color = Color.green;
			plr.material.color = Color.green;
		}
		else
		{
			fill.color = Color.red;
			plr.material.color = Color.yellow;
		}
	}

    private void HandleHPPctChanged(float pct)
	{
		slider.value = pct;
		if (!poisoned)
		{
			poisoned = true;
			Invoke("Unpoison", 5f);
		}
	}

	private void Unpoison()
    {
		poisoned = false;
    }
}