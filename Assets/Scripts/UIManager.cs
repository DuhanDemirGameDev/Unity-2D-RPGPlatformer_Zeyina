using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += characterTakeDamage;
        CharacterEvents.characterHealed += characterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= characterTakeDamage;
        CharacterEvents.characterHealed -= characterHealed;

    }
    public void characterTakeDamage( GameObject chracter, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(chracter.transform.position) + new Vector3(0, 90f, 0);

        TMP_Text tmp_text = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmp_text.text = damageReceived.ToString();

    }

    public void characterHealed(GameObject chracter, int healthReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(chracter.transform.position);

        TMP_Text tmp_text = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmp_text.text = healthReceived.ToString();

    }
}
