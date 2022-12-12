using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    public bool IsLastLevel = false;

    [SerializeField]
    private GameObject PortalAppearedText;

    [SerializeField]
    private AudioClip PortalAppearedSound;

    [SerializeField]
    private GameObject Portal;

    [SerializeField]
    private List<GameObject> enemies;

    private bool CanShowText = true;

    void Update()
    {
        StartCoroutine(EnemyKilled());
        if (enemies.Count == 0)
        {
            Portal.SetActive(true);
            if (CanShowText)
            {
                StartCoroutine(ShowText());
            }
        }
    }

    public IEnumerator EnemyKilled()
    {
        yield return 0;
        enemies.RemoveAll(item => item == null);
    }

    public IEnumerator ShowText()
    {
        SoundManager.instance.PlaySound(PortalAppearedSound);
        PortalAppearedText.SetActive(true);
        if (!IsLastLevel)
        {
            yield return new WaitForSeconds(2);
            PortalAppearedText.SetActive(false);
            CanShowText = false;
        }
        else
        {
            CanShowText = false;
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene(0);
        }
    }
}
