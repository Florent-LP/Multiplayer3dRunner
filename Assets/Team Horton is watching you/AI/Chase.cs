using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{

    [HideInInspector] public Transform target = null;
    public float distanceToCatch = 5f;

    [HideInInspector] public Animator anim = null;
    [HideInInspector] public NavMeshAgent agent = null;
    protected State behaviour
    {
        get
        {
            return _behaviour;
        }
        set
        {
            if (_behaviour != null)
                _behaviour.OnStateExit();

            _behaviour = value;

            if (_behaviour != null)
                _behaviour.OnStateEnter();
        }
    }
    protected State _behaviour = null;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        behaviour = new IdleBehaviour(this);

    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 direction = target.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        if (direction.magnitude < distanceToCatch)
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", true);
        }*/

        behaviour.OnStateUpdate();
    }


    /*****************************
     ******* State Machine *******
     *****************************/
    protected abstract class State
    {
        protected Chase owner = null;
        public State(Chase owner)
        {
            this.owner = owner;
        }
        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }

    protected class IdleBehaviour : State
    {
        public IdleBehaviour(Chase owner) : base(owner)
        { }
        public override void OnStateEnter()
        {
            owner.anim.SetBool("isAttacking", false);
            owner.anim.SetBool("isRunning", false);
        }

        public override void OnStateUpdate()
        {
            if (owner.target != null)
            {
                owner.behaviour = new RunBehaviour(owner);
            }
        }

        public override void OnStateExit()
        {

        }
    }

    // Run State
    protected class RunBehaviour : State
    {
        public RunBehaviour(Chase owner) : base(owner)
        { }
        public override void OnStateEnter()
        {
            owner.anim.speed = owner.agent.speed;
            owner.anim.SetBool("isAttacking", false);
            owner.anim.SetBool("isRunning", true);
        }

        public override void OnStateUpdate()
        {
            Vector3 toTarget = owner.target.position - owner.transform.position;
            owner.agent.destination = owner.target.position - toTarget.normalized * owner.distanceToCatch;

            if (toTarget.magnitude < owner.distanceToCatch)
                owner.behaviour = new AttackBehaviour(owner);
        }

        public override void OnStateExit()
        {

        }
    }

    // Attack State
    protected class AttackBehaviour : State
    {
        public AttackBehaviour(Chase owner) : base(owner)
        { }
        public override void OnStateEnter()
        {
            owner.anim.SetBool("isRunning", false);
            owner.anim.SetBool("isAttacking", true);
            owner.agent.Stop();
        }

        public override void OnStateUpdate()
        {
            Vector3 toTarget = owner.target.position - owner.transform.position;
            if (toTarget.magnitude > owner.distanceToCatch)
                owner.behaviour = new RunBehaviour(owner);
        }

        public override void OnStateExit()
        {
            owner.agent.Resume();
        }
    }
}
