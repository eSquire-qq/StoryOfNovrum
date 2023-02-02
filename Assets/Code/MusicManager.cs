using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip peaceMusic;

    [SerializeField]
    protected AudioClip combatMusic;

    protected AudioClip currentClip;

    protected bool isInCombat;

    protected Task combatExitDelay;

    protected void Start()
    {
        isInCombat = false;
        if (EventManager.instance) EventManager.StartListening("EnterCombat", OnCombatStarted);
    }

    protected void Update()
    {
        Debug.Log(currentClip);
        Debug.Log(peaceMusic);
        Debug.Log(combatMusic);
        if (!isInCombat && currentClip != peaceMusic)
        {
            audioSource.PlayOneShot(peaceMusic);
            currentClip = peaceMusic;
        } 
    }

    protected void OnCombatStarted(Dictionary<string, object> message)
    {
        isInCombat = true;
        if (combatExitDelay != null) {
            combatExitDelay.Dispose();
        }
        combatExitDelay = Task.Delay(10000).ContinueWith(t => {
            isInCombat = false;
        });
        if (currentClip != combatMusic)
        {
            audioSource.PlayOneShot(combatMusic);
            currentClip = combatMusic;
        } 
    }
}
