 using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SkillsController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject warriorTree;
    [SerializeField] GameObject rougeTree;
    [SerializeField] GameObject mysticTree;
    [SerializeField] GameObject mainTree;
    [SerializeField] TextMeshProUGUI skillPoints;
    XpController xpController;
    // Rouge bools
    public bool dashUnlocked = false;
    bool activeRecoveryUnlocked = false;
    bool athleteUnlocked = false;
    bool deadlyPoisonUnlocked = false;
    bool perceptionUnlocked = false;


    // Warrior bools
    public bool extraDamageUnlocked = false;
    bool effecintSwingsUnlocked = false;
    bool doubleSwingUnlocked = false;
    bool cripplingStrikeUnlocked = false;
    bool FrenzyUnlocked = false;
    bool bulwarkUnlocked = false;
    bool heartyUnlocked = false;
    bool armourplatesUnlocked = false;
    bool counterSliceUnlocked = false;

    // Mystic bools
    public bool bloodThristUnlocked = false;
    bool sinisterWardUnlocked = false;
    bool holyWarthUnlocked = false;
    bool soulCrushUnlocked = false;

    PlayerAttack playerAttack;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;
    PlayerStamina playerStamina;

    [Header("Skill Settings")]
    [SerializeField] float sharpSword = 2.5f;

    [SerializeField] int skillCost = 1;



    void Awake()
    {
        xpController = GetComponent<XpController>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerStamina = FindFirstObjectByType<PlayerStamina>();
        warriorTree.SetActive(false);
        mysticTree.SetActive(false);
        rougeTree.SetActive(false);

    }

    void Update()
    {
        skillPoints.text = xpController.GetSkillPoints().ToString("00");
    }

    public void OpenWarriorTree()
    {
        CloseTree(mainTree);
        OpenTree(warriorTree);
    }

    public void OpenMysticTree()
    {
        CloseTree(mainTree);
        OpenTree(mysticTree);
    }

    public void OpenRougeTree()
    {
        CloseTree(mainTree);
        OpenTree(rougeTree);
    }

    public void CloseWarriorTree()
    {
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void CloseMysticTree()
    {
        CloseTree(mysticTree);
        OpenTree(mainTree);
    }

    public void CloseRougeTree()
    {
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    void OpenTree(GameObject tree)
    {
        tree.SetActive(true);
    }
    void CloseTree(GameObject tree)
    {
        tree.SetActive(false);
    }


    // Rouge Skills
    public void SkillDash()
    {
        if (dashUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        dashUnlocked = true;
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    public void SkillActiveRecovery()
    {
        if (activeRecoveryUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        activeRecoveryUnlocked = true;
        playerStamina.UnlockActiveRecovery();
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    public void SkillAthlete()
    {
        if (athleteUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        athleteUnlocked = true;
        playerStamina.UnlockAthlete();
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    public void SkillDeadlyPoison()
    {
        if (deadlyPoisonUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        deadlyPoisonUnlocked = true;
        playerAttack.UnlockDeadlyPoison();
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    public void SkillPerception()
    {
        if (perceptionUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        perceptionUnlocked = true;
        playerHealth.UnlockPerception();
        CloseTree(rougeTree);
        OpenTree(mainTree);
    }

    // warrior skills

    public void SkillMoreDamage()
    {
        if (extraDamageUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        extraDamageUnlocked = true;
        playerAttack.AddAttackDamage(sharpSword);
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void SkillEffecientSwings()
    {
        if (effecintSwingsUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        effecintSwingsUnlocked = true;
        playerAttack.EffecientAttack();
        CloseTree(warriorTree);
        OpenTree(mainTree);

    }

    public void SkillDoubleSwing()
    {
        if (doubleSwingUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        doubleSwingUnlocked = true;
        playerAttack.DoubleSwing();
        CloseTree(warriorTree);
        OpenTree(mainTree);

    }

    public void SkillCripplingStrike()
    {
        if (cripplingStrikeUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        cripplingStrikeUnlocked = true;
        playerAttack.CripplingStrike();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void SkillFrenzy()
    {
        if (FrenzyUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        FrenzyUnlocked = true;
        playerMovement.UnlockFrenzy();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

     public void SkillBulwark()
    {
        if (bulwarkUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        bulwarkUnlocked = true;
        playerHealth.UnlockBulwark();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void SkillHearty()
    {
        if (heartyUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        heartyUnlocked = true;
        playerHealth.UnlockHearty();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void SkillArmourPlates()
    {
        if (armourplatesUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        armourplatesUnlocked= true;
        playerHealth.UnlockArmourPlates();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    public void SkillCounterSlice()
    {
        if (counterSliceUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        counterSliceUnlocked = true;
        playerAttack.UnlockCounterSlice();
        CloseTree(warriorTree);
        OpenTree(mainTree);
    }

    //Mystic Skills

    public void SkillBloodThrist()
    {
        if (bloodThristUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        bloodThristUnlocked = true;
        CloseTree(mysticTree);
        OpenTree(mainTree);
    }

    public void SkillSinisterWard()
    {
        if (sinisterWardUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        sinisterWardUnlocked = true;
        playerHealth.UnlockSinisterWard();
        CloseTree(mysticTree);
        OpenTree(mainTree);
    }

     public void SkillHolyWarth()
    {
        if (holyWarthUnlocked || xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        holyWarthUnlocked = true;
        playerAttack.UnlockHolyWarth();
        CloseTree(mysticTree);
        OpenTree(mainTree);
    }

    public void SkillSoulCrush()
    {
        if (soulCrushUnlocked|| xpController.GetSkillPoints() < skillCost)
        {
            return;
        }
        xpController.SkillChoosen(skillCost);
        soulCrushUnlocked = true;
        playerAttack.UnlockSoulCrush();
        CloseTree(mysticTree);
        OpenTree(mainTree);
    }

    public void PointsSaved()
    {
        xpController.PointsSaved();
    }
}
