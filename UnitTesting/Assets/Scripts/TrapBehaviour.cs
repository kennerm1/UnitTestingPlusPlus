using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    [SerializeField] private TrapTargetType trapType;

    private TrapDamage trapDamage;
    private TrapDestroy trapDestroy;
    private TrapSound trapSound;

    private void Awake()
    {
        trapDamage = new TrapDamage();
        trapDestroy = new TrapDestroy(this.gameObject);

        var audioSource = GetComponent<AudioSource>();
        var audioClip = audioSource?.clip;
        trapSound = new TrapSound(new AudioSourceWrapper(audioSource), audioClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        var characterMover = other.GetComponent<ICharacterMover>();
        var audioSource = other.GetComponent<AudioSource>();
        trapDamage.HandleCharacterEntered(characterMover, trapType);
        trapDestroy.HandleCharacterEntered(characterMover, trapType);
        trapSound.HandleCharacterEntered(characterMover, trapType);
    }
}

public class TrapDamage : ITrapEffect
{
    public void HandleCharacterEntered(ICharacterMover characterMover, TrapTargetType trapTargetType)
    {
        if (characterMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                characterMover.Health--;
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                characterMover.Health--;
        }
    }
}

public class TrapDestroy : ITrapEffect
{
    private GameObject targetGameObject;

    public TrapDestroy(GameObject gameObject)
    {
        this.targetGameObject = gameObject;
    }

    public void HandleCharacterEntered(ICharacterMover characterMover, TrapTargetType trapTargetType)
    {
        if (characterMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                targetGameObject.SetActive(false);
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                targetGameObject.SetActive(false);
        }
    }
}

public class TrapSound : ITrapEffect
{
    private IAudioSource audioSource;
    private AudioClip clip;

    public TrapSound(IAudioSource audioSource, AudioClip clip)
    {
        this.audioSource = audioSource;
        this.clip = clip;
    }

    public void HandleCharacterEntered(ICharacterMover characterMover, TrapTargetType trapTargetType)
    {
        if (characterMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                audioSource.PlayOneShot(clip);
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                audioSource.PlayOneShot(clip);
        }
    }
}

public enum TrapTargetType { Player, Npc }
//public enum TrapEffectType { damage, destroy, sound }