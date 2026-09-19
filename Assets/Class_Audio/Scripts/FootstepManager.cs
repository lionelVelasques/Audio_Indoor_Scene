using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public List<AudioClip> grassSteps = new List<AudioClip>();
    public List<AudioClip> waterSteps = new List<AudioClip>();
    public List<AudioClip> caveSteps = new List<AudioClip>();

    private enum Surface { grass, water, cave };
    private Surface surface;

    private List<AudioClip> currentList;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Nombre para la animación si busca "PlayStep"
    public void PlayStep()
    {
        ExecuteStepSound();
    }

    // Nombre para la animación si busca "PlayerStep"
    public void PlayerStep()
    {
        ExecuteStepSound();
    }

    // Lógica principal de reproducción
    private void ExecuteStepSound()
    {
        if (currentList == null || currentList.Count == 0)
            return;

        AudioClip clip = currentList[Random.Range(0, currentList.Count)];
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
    }

    public void NewEvent()
    {
        // Evento vacío por si la animación aún lo llama
    }

    private void SelectStepList()
    {
        switch (surface)
        {
            case Surface.grass:
                currentList = grassSteps;
                break;
            case Surface.water:
                currentList = waterSteps;
                break;
            case Surface.cave:
                currentList = caveSteps;
                break;
            default:
                currentList = null;
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Grass"))
        {
            surface = Surface.grass;
        }
        else if (hit.transform.CompareTag("Water"))
        {
            surface = Surface.water;
        }
        else if (hit.transform.CompareTag("Cave"))
        {
            surface = Surface.cave;
        }

        SelectStepList();
    }
}