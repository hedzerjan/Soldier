using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine;

public class Soldier : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Scanner _scanner;
    private Mover _mover;
    private Animator _animator;
    private GameObject _gun;
    private GameObject _target;
    private int _health = 4;
    [SerializeField] private GameObject _bulletPrefab;
    private float timeOfLastBulletFired;
    [SerializeField] private float _timeBetweenShots = 30f;

    public static event Action<string> OnDeath;

    void Start()
    {
        _scanner = GetComponent<Scanner>();
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
        _gun = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/WeaponContainer").gameObject;
        _stateMachine = new StateMachine();
        var scouting = CreateScoutingState();
        State firing = CreateFiringState();
    }

    internal void Hurt(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
            OnDeath(transform.tag);
        }
    }

    void Update()
    {
        _stateMachine.Update();
    }

    private State CreateScoutingState()
    {
        var scouting = _stateMachine.CreateState(States.Scouting);

        scouting.onEnter = delegate
        {
            // Debug.Log($"Entering state {scouting.id}");
            _animator.SetInteger("state", (int)scouting.id);
            _mover.IsStopped = false;
        };

        scouting.onFrame = delegate
        {
            var hitObject = _scanner.ScoutScan();
            if (hitObject != null)
            {
                _target = hitObject;
                _stateMachine.TransitionTo(States.Firing);
            }
            _mover.ScoutMove();
        };

        scouting.onExit = delegate
        {
            Debug.Log($"Exiting state {scouting.id}");
        };
        return scouting;
    }

    private State CreateFiringState()
    {
        var firing = _stateMachine.CreateState(States.Firing);

        firing.onEnter = delegate
        {
            Debug.Log($"Entering state {firing.id}");
            _animator.SetInteger("state", (int)firing.id);
            _mover.IsStopped = true;
        };

        firing.onFrame = delegate
        {
            var hitObject = _scanner.FireScan(_target);
            if (hitObject == null)
            {
                _target = hitObject;
                _stateMachine.TransitionTo(States.Scouting);
            }
            else
            {
                if ((Time.time - timeOfLastBulletFired) > _timeBetweenShots)
                {
                    Fire();
                }
                _mover.FireMove(_target);
            }
        };

        firing.onExit = delegate
        {
            Debug.Log($"Exiting state {firing.id}");
        };
        return firing;
    }

    private void Fire()
    {
        Debug.Log($"Time.time: {Time.time}, timeOfLastBulletFired: {timeOfLastBulletFired}. Diff: {Time.time - timeOfLastBulletFired}, _timeBetweenShots: {_timeBetweenShots}");
        timeOfLastBulletFired = Time.time;
        var bullet = Instantiate(_bulletPrefab) as GameObject;
        // bullet.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        bullet.transform.position = _gun.transform.TransformPoint(Vector3.right * 1f);
        bullet.transform.rotation = transform.rotation;
    }
}