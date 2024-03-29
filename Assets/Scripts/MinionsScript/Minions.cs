﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Minions : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Vector3 target;
    protected int level;
    protected Renderer m_renderer;
    public Texture[] textures = new Texture[5];
   


    //place holder
    public  int value = 5;
    public int damage = 1;
    
    public float Distance
    {
        get
        {
            // Check if path is working
            if (!agent ||
                agent.pathPending ||
                agent.pathStatus == NavMeshPathStatus.PathInvalid ||
                agent.path.corners.Length ==0)
                return 0;
            // Check if usual method is working
            var defaultDistance = agent.remainingDistance;
            if (!float.IsInfinity(defaultDistance)) return defaultDistance;
            // Fallback to big math
            float distance = 0;
            var corners = agent.path.corners;
            for (var i = 0; i < corners.Length - 1; ++i)
            {
                distance += Vector3.Distance(corners[i], corners[i + 1]);
            }
            return distance;
        }
    }
    
    protected virtual void Start()
    {
        value = level + 1;
        // will change when making the waves
        
        //level = 4;
        target = GameObject.FindGameObjectWithTag("End").transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;


        m_renderer = GetComponentInChildren<Renderer>();
        agent.SetDestination(target);
        setTexture();


        //have to use wait else agent wont be on the navmesh too small radius
        StartCoroutine(changeRadius());
       // StartCoroutine(tDmg());
    }

    IEnumerator changeRadius()
    {
        //only way found to make agent walk through each other
        yield return new WaitForSeconds(0.000001f);
        agent.radius = 0.001f;
    }
    //test tool
    IEnumerator tDmg()
    {
        yield return new WaitForSeconds(2f);
        takeDmg(1);
        StartCoroutine(tDmg());
    }


    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public virtual void setTexture()
    {
        if (level > -1)
        {
            m_renderer.material.mainTexture = textures[level];
        }
        else
        {
            Die();
        }


    }
    protected virtual void  Die()
    {

        Game.Player.Money += value;
        Game.WaveSpawner.EAlive--;
        Destroy(gameObject);
    }

    public virtual void takeDmg(int dmg)
    {
        Level -= dmg;
        setTexture();

    }


}
