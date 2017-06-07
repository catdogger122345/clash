﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRC
{
    public delegate void DeathEventHandler();
    public interface IDamageable
    {
        event DeathEventHandler DeathEvent;

        KingTower Owner { get; }

        float MaxHealth { get; }
        float CurrentHealth { get; }

        void Hurt(float amount);
        void Heal(float amount);
    }

    public abstract class Damageable : MonoBehaviour, IDamageable
    {
        public event DeathEventHandler DeathEvent;

        [SerializeField]
        protected KingTower m_Owner;
        public KingTower Owner { get { return m_Owner; } }

        [SerializeField]
        protected float m_MaxHealth;
        public float MaxHealth { get { return m_MaxHealth; } }

        protected float m_CurrentHealth;
        public float CurrentHealth { get { return m_CurrentHealth; } }

        [SerializeField]
        private Text m_HPText;

        protected virtual void Awake()
        {
            m_CurrentHealth = m_MaxHealth;

            m_HPText.text = m_CurrentHealth.ToString();
        }

        public virtual void Hurt(float amount)
        {
            m_CurrentHealth -= amount;

            if (m_CurrentHealth <= .0f)
            {
                m_CurrentHealth = .0f;
                FireDeathEvent();
            }

            m_HPText.text = m_CurrentHealth.ToString();
        }

        public virtual void Heal(float amount)
        {
            m_CurrentHealth += amount;

            if (m_CurrentHealth > m_MaxHealth)
                m_CurrentHealth = m_MaxHealth;

            m_HPText.text = m_CurrentHealth.ToString();
        }

        protected virtual void FireDeathEvent()
        {
            if (DeathEvent != null)
                DeathEvent();
        }
    }
}
