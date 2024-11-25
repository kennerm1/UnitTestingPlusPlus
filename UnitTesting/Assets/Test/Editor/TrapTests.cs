using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTests 
{
    [Test]
    public void PlayerEntering_PlayerTargetedTrap_ReducesHealthByOne()
    {
        //Arrange
        TrapDamage trapDamage = new TrapDamage();
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();
        characterMover.IsPlayer.Returns(true);
        //Act
        trapDamage.HandleCharacterEntered(characterMover, TrapTargetType.Player);
        //Assert
        Assert.AreEqual(-1, characterMover.Health);
    }

    [Test]
    public void NpcEntering_NpcTargetedTrap_ReducesHealthByOne()
    {
        TrapDamage trapDamage = new TrapDamage();
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();

        trapDamage.HandleCharacterEntered(characterMover, TrapTargetType.Npc);

        Assert.AreEqual(-1, characterMover.Health);
    }

    [Test]
    public void PlayerEntering_PlayerTargetedTrap_DestroyTarget()
    {
        GameObject gameObject = new GameObject();
        TrapDestroy trapDestroy = new TrapDestroy(gameObject);
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();
        characterMover.IsPlayer.Returns(true);

        trapDestroy.HandleCharacterEntered(characterMover, TrapTargetType.Player);

        Assert.IsFalse(gameObject.activeSelf);
    }

    [Test]
    public void NpcEntering_NpcTargetedTrap_DestroyTarget()
    {
        GameObject gameObject = new GameObject();
        TrapDestroy trapDestroy = new TrapDestroy(gameObject);
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();

        trapDestroy.HandleCharacterEntered(characterMover, TrapTargetType.Npc);

        Assert.IsFalse(gameObject.activeSelf);
    }

    [Test]
    public void PlayerEntering_PlayerTargetedTrap_PlaySound()
    {
        var audioSource = Substitute.For<IAudioSource>();
        var clip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        var trapSound = new TrapSound(audioSource, clip);
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();
        characterMover.IsPlayer.Returns(true);

        trapSound.HandleCharacterEntered(characterMover, TrapTargetType.Player);

        audioSource.Received(1).PlayOneShot(clip);
    }

    [Test]
    public void NpcEntering_NpcTargetedTrap_PlaySound()
    {
        var audioSource = Substitute.For<IAudioSource>();
        var clip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        var trapSound = new TrapSound(audioSource, clip);
        ICharacterMover characterMover = Substitute.For<ICharacterMover>();

        trapSound.HandleCharacterEntered(characterMover, TrapTargetType.Npc);

        audioSource.Received(1).PlayOneShot(clip);
    }
}
