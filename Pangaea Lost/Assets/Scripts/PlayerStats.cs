using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{/*
    //Character Stats
    [SerializeField]public bool playerHealthEnabled = false; //will make player invulnerable or not

    [SerializeField]public float playerHealthMax =  100.0f; //health
    [SerializeField]public float playerHealthMin = 0.0f;
    [SerializeField]public float playerHealthCurrent;
    [SerializeField]public float playerHealthRegenAmount; //how much the player regens per step
    [SerializeField]public float playerHealthRegenStep; //how often the player regens

    [SerializeField]float playerStaminaMax; //stamina
    [SerializeField]float playerStaminaMin = 0.0f;
    [SerializeField]float playerStaminaCurrent;
    [SerializeField]float playerStaminaRegenRate;
    [SerializeField]float playerStaminaRegenStep;
//----------------------------------------------------
    //Armor Related
    [SerializeField]float armorBase;
    [SerializeField]float armorHead;
    [SerializeField]float armorTorso;
    [SerializeField]float armorHands;
    [SerializeField]float armorLegs;
    [SerializeField]float armorFeet;

    [SerializeField]float armorCurrent; //adds armor values from base & items
    //armorCurrent = armorBase + armorHead + armorTorso + armorHands + armorLegs + armorFeet;
    */
//----------------------------------------------------
    //Basic Movement
    [SerializeField]public float playerGravity; //forces that act on the player
    [SerializeField]public float playerFriction;
//----------------------------------------------------
    //Ground Movement
    [SerializeField]public bool movementEnabled; //controls whether any movement is allowed
    [SerializeField]public bool walkEnabled;
    [SerializeField]public bool runEnabled;
    [SerializeField]public bool crouchEnabled;
    [SerializeField]public bool proneEnabled;

    [SerializeField]public float walkSpeed; //basic speed variables for movement
    [SerializeField]public float runSpeed;
    [SerializeField]public float crouchSpeed;
    [SerializeField]public float proneSpeed;

    [SerializeField]public float runAccel; //these are for how long it takes to go from stop to full speed
    [SerializeField]public float crouchAccel;
    [SerializeField]public float proneAccel;
//----------------------------------------------------
    //Jump Related
    [SerializeField]public bool jumpEnabled;
    [SerializeField]public float jumpHeight;
    [SerializeField]public float accelerationTimeAirborne;
    [SerializeField]public float accelerationTimeGrounded;
    [SerializeField]public float jumpTimeToApex;
    [SerializeField]public float jumpApexHang;
//----------------------------------------------------
    //Dodge Related
    [SerializeField]public bool dodgeEnabled;
    [SerializeField]public float dodgeSpeed; //how fast player moves in dodge
    [SerializeField]public float dodgeTime;
    [SerializeField]public float dodgeTimeMax;
    [SerializeField]public float dodgeStepTime;
    [SerializeField]public float dodgeDistance; //how far the player moves in dodge
//----------------------------------------------------
    //SplineMovement Related
    [SerializeField]public GameObject currentSpline;
    [SerializeField]public float nearestSplinePosition;
    [SerializeField]public float minSplinePosition;
    [SerializeField]public float maxSplinePosition;
//----------------------------------------------------
    //LaneMovement Related
    [SerializeField]public Collider currentLaneCollider;
    [SerializeField]public float laneStepDistance;
    [SerializeField]public float laneNumber;
    [SerializeField]public float currentLaneZ;
//----------------------------------------------------
    //Climbing Related
    [SerializeField]float mantleSpeed; //speed to vault over from hang or vertical movement
    [SerializeField]float climbSpeed; //speed to climb ladders/ropes/etc.
//----------------------------------------------------
    //Grapple Related
    [SerializeField]float grappleLength;
    [SerializeField]float grappleSpeed;
//----------------------------------------------------
/*
    //Inventory Related
    [SerializeField]public bool inventoryEnabled;
    [SerializeField]public float inventoryMaxSlots;
    [SerializeField]public float inventoryCurrentSlots;

    [SerializeField]bool recipe01Enabled; //can change these to fit actual recipe names
    [SerializeField]bool recipe02Enabled;
    [SerializeField]bool recipe03Enabled;
    [SerializeField]bool recipe04Enabled;
    [SerializeField]bool recipe05Enabled;

    //[SerializeField]bool bandageRecipeEnabled;
    //[SerializeField]float bandageRecipeHealAmount;
    //[SerializeField]float bandageRecipeClothRequired;
    //[SerializeField]float bandageRecipeAlcoholRequired;
*/    
//----------------------------------------------------

    //Equipment Related

    //Gun
    [SerializeField]public bool henryRifleEnabled;
 /*   
    [SerializeField]bool stock01Enabled; //gun parts
    [SerializeField]bool stock02Enabled;

    [SerializeField]bool triggerAssembly01Enabled;
    [SerializeField]bool triggerAssembly02Enabled;

    [SerializeField]bool rearSight01Enabled;
    [SerializeField]bool rearSight02Enabled;

    [SerializeField]bool barrel01Enabled;
    [SerializeField]bool barrel02Enabled;

    [SerializeField]bool frontSight01Enabled;
    [SerializeField]bool frontSight02Enabled;

    //Armor
    [SerializeField]bool helmet01Enabled; //armor types
    [SerializeField]bool helmet02Enabled;

    [SerializeField]bool torso01Enabled;
    [SerializeField]bool torso02Enabled;

    [SerializeField]bool gloves01Enabled;
    [SerializeField]bool gloves02Enabled;

    [SerializeField]bool legs01Enabled;
    [SerializeField]bool legs02Enabled;

    [SerializeField]bool feet01Enabled;
    [SerializeField]bool feet02Enabled;
//----------------------------------------------------
    //Melee Attack Related
    [SerializeField]float meleeAttackSpeed;

    [SerializeField]float meleeAttackBaseDamage; //base level attack
    [SerializeField]float meleeAttackDamageModifier;
    [SerializeField]float meleeAttackDamageMin; //min/max for if we want randomized dmg
    [SerializeField]float meleeAttackDamageMax;
    [SerializeField]float meleeAttackDamage; //actual attack damage
//----------------------------------------------------
    //Gun Related
    [SerializeField]float gunAttackBaseDamage;
    [SerializeField]float gunAttackDamageModifier;
    [SerializeField]float gunAttackRate;
    [SerializeField]float gunAttackRateStep;
    [SerializeField]float gunAmmoMax;
    [SerializeField]float gunAmmoCurrent;
    [SerializeField]float gunReloadTimeMax;
    [SerializeField]float gunReloadTimeCurrent;
    [SerializeField]float gunReloadBullet;

    [SerializeField]float gunAttackCurrentDamageType; //for elemental damage
*/
}