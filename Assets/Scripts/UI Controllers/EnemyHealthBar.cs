using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnemyHealthBar : MonoBehaviour
{
    public Transform target;
    public float offset;
    public EnemyController enemy;

	void Update ()
    {
        this.GetComponent<Slider>().maxValue = enemy.Data.Health;
        this.GetComponent<Slider>().minValue = 0.0f;
        this.GetComponent<Slider>().value = enemy.CurrentHealth;
        this.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(target.position + Vector3.forward * offset);
	}
}
