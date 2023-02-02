using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    protected CancellationTokenSource combatExitDelayCancelationTokenSource;
    protected CancellationToken combatExitDelayCancelationToken;

    protected void Start()
    {
        isInCombat = false;
        if (EventManager.instance) EventManager.StartListening("EnterCombat", OnCombatStarted);
    }

    protected void Update()
    {
        if (!isInCombat && currentClip != peaceMusic)
        {
            audioSource.Stop();
            audioSource.clip = peaceMusic;
            audioSource.Play();
            currentClip = peaceMusic;
        } 
    }

    protected void OnCombatStarted(Dictionary<string, object> message)
    {
        if (combatExitDelayCancelationTokenSource != null) {
            combatExitDelayCancelationTokenSource.Cancel();
            combatExitDelayCancelationTokenSource.Dispose();
        }
        isInCombat = true;
        combatExitDelayCancelationTokenSource = new CancellationTokenSource();
        combatExitDelayCancelationToken = combatExitDelayCancelationTokenSource.Token;
        combatExitDelay = Task.Delay(10000).ContinueWith(t => {
            if (combatExitDelayCancelationToken.IsCancellationRequested) {
                return;
            }
            isInCombat = false;
        }, combatExitDelayCancelationToken);

        if (currentClip != combatMusic)
        {
            audioSource.Stop();
            audioSource.clip = combatMusic;
            audioSource.Play();
            currentClip = combatMusic;
        } 
    }
}
