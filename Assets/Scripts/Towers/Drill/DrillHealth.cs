using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillHealth : MonoBehaviour
{
    [SerializeField] private int health = 50;
    private GameObject drillUIPrefab;
    private RectTransform drillUIParent;
    private Slider slider;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Root")
        {
            TakeDamage(1);
        }
    }

    public void SpawnUI(int goalCount, GameObject drill)
    {

        drillUIPrefab = drill;
        drillUIParent = GameObject.FindGameObjectWithTag("DrillUITag").GetComponent<RectTransform>();

        var spawnDUI = Instantiate(drillUIPrefab, new Vector3(drillUIParent.transform.position.x, drillUIParent.transform.position.y - 30 * goalCount, 0), Quaternion.identity);
        spawnDUI.transform.SetParent(drillUIParent, false);
        slider = spawnDUI.GetComponentInChildren<Slider>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;

        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameStateManager>().DoLossCondition();
        }
    }
}
