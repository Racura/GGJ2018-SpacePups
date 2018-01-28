using UnityEngine;
using System.Collections;
using System;

public class ParticleDotListener : DotListener
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] bool playOnActive = true; 
    [SerializeField] bool playOnRetire = false; 
    [SerializeField] bool stopOnRetire = true; 


    protected override bool IsListenerActive { get {
        return particleSystem != null && particleSystem.IsAlive (true);
    } }

    public override void DotStateChanged(ResetDot.States state)
    {
        base.DotStateChanged (state);

        switch (state)
        {
            case ResetDot.States.Active:
                if (playOnActive && !particleSystem.isPlaying) {
                    particleSystem.Play();
                }
                break;
            case ResetDot.States.Retiring:
                if (playOnRetire && !particleSystem.isPlaying) {
                    particleSystem.Play();
                }
                if (stopOnRetire && particleSystem.isPlaying) {
                    particleSystem.Stop (false, ParticleSystemStopBehavior.StopEmitting);
                }
                break;
            default:
                if (particleSystem.isPlaying) {
                    particleSystem.Stop (false, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;

        }
    }
}   
