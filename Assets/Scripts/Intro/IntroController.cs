using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    [SerializeField] Animator _title;
    [SerializeField] Animator _Buttons;

    private void Start()
    {
        if (_title == null || _Buttons == null)
        {
            Debug.LogError("Animator components are not assigned in the Inspector.");
            return;
        }
        StartCoroutine(IntroSceneAnimation());
    }

    IEnumerator IntroSceneAnimation()
    {
        _title.Play("Title");
        yield return new WaitForSeconds(2f);
        _Buttons.Play("Buttons");
    }

    public void OnClickSelectHero(int id)
    {
        SelectedHeroID.SelectedHeroId = id;
        SceneManager.LoadScene(1);
    }
}
