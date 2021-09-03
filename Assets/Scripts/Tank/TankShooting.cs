﻿using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public ShellPoolManager.ShellType shellType;
    public BaseShootingStyle shootingStyle;
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce / 100;
    }


    private void Start()
    {
        ShellPoolManager.instance.CreatePool(shellType);
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce / 100;
        if (Input.GetButtonDown(m_FireButton))
        {
            // have we pressed fire for the first time?
            //m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;
            Fire();
            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        shootingStyle.UpdateFunc();
        //if(m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        //{
        //    // at max charge, not yet fired
        //    m_CurrentLaunchForce = m_MaxLaunchForce;
        //    Fire();
        //}
        //else if(Input.GetButtonDown(m_FireButton))
        //{
        //    // have we pressed fire for the first time?
        //    m_Fired = false;
        //    m_CurrentLaunchForce = m_MinLaunchForce;

        //    m_ShootingAudio.clip = m_ChargingClip;
        //    m_ShootingAudio.Play();
        //}
        //else if(Input.GetButton(m_FireButton) && !m_Fired)
        //{
        //    // holding the fire button, not yet fired
        //    m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

        //    m_AimSlider.value = m_CurrentLaunchForce / 100;
        //}
        //else if(Input.GetButtonUp(m_FireButton) && !m_Fired)
        //{
        //    // we released the button, having not fired yet
        //    Fire();
        //}
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        //m_Fired = true;

        //ShellBase shellInstance = ShellPoolManager.instance.GetShell(shellType);
        float forceAmount = Mathf.InverseLerp(m_MinLaunchForce, m_MaxLaunchForce, m_CurrentLaunchForce);
        //shellInstance.gameObject.SetActive(true);
        //shellInstance.Fire(m_FireTransform, forceAmount, gameObject);
        shootingStyle.Shooting(m_FireTransform, forceAmount, gameObject, shellType);

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
    public void SetUp(BaseShootingStyle shootingStyle)
    {
        this.shootingStyle = shootingStyle;
        m_FireButton = "Fire" + m_PlayerNumber;
        this.shootingStyle.SetUp(m_FireButton);
    }
}