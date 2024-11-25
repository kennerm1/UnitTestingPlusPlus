using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    [SerializeField] private TrapTargetType trapType;

    private Trap trap;

    private void Awake()
    {
        trap = new Trap();
    }

    private void OnTriggerEnter(Collider other)
    {
        var characterMover = other.GetComponent<ICharacterMover>();
        var audioSource = other.GetComponent<AudioSource>();
        trap.HandleCharacterEntered(characterMover, trapType);
    }
}

public class TrapDamage
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

public class TrapDestroy
{
    public void HandleCharacterEntered(ICharacterMover characterMover, TrapTargetType trapTargetType)
    {
        if (characterMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                GameObject.SetActive = false;
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                GameObject.SetActive = false;
        }
    }
}

public class TrapSound
{
    public void HandleCharacterEntered(ICharacterMover characterMover, TrapTargetType trapTargetType)
    {
        if (characterMover.IsPlayer)
        {
            if (trapTargetType == TrapTargetType.Player)
                audioSource.PlayOneShot;
        }
        else
        {
            if (trapTargetType == TrapTargetType.Npc)
                audioSource.PlayOneShot;
        }
    }
}

public enum TrapTargetType { Player, Npc }
//public enum TrapEffectType { damage, destroy, sound }