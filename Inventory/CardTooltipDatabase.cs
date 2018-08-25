using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTooltipDatabase : MonoBehaviour
{

    public void FindCardInfo(string cardName, ref string element, ref string desc)
    {

        //====================================================================
        //                        Fire Arcana
        //====================================================================

        if (cardName == "Firebolt")
        {
            element = "Projectile";
            desc = "Shoot a fireball dealing moderate damage.";
        }
        else if (cardName == "Embers")
        {
            element = "Projectile";
            desc = "Fire off a spread of 3 embers dealing minor damage.";
        }
        else if (cardName == "Blaze")
        {
            element = "Projectile";
            desc = "Shoot a large fire that explodes into smaller embers when contacting an enemy or wall. Exploding after a few seconds otherwise.";
        }
        else if (cardName == "Enfire")
        {
            element = "Buff";
            desc = "Wreath yourself in flame, temporarily increasing your strength by 25% for 30 seconds.";
        }
        else if (cardName == "Fire Spin")
        {
            element = "Projectile";
            desc = "Conjure a whirlwind of flame which slowly advances forward damaging anything that touches it and periodically creating fireballs.";
        }
        else if (cardName == "Flame Wave")
        {
            element = "Projectile";
            desc = "Create a wave of flame which sweeps in an arc in front of you.";
        }
        else if (cardName == "Magma")
        {
            element = "Projectile";
            desc = "Hurl a piece of molten earth which grows larger as it travels, dealing more damage and leaving a pool of lava which damages any enemy standing in it.";
        }
        else if (cardName == "Molten")
        {
            element = "All";
            desc = "Cause the skies to rain down fiery destruction, creating meteors that strike the enemy.";
        }

        //====================================================================
        //                       Earth Arcana
        //====================================================================

        else if (cardName == "Boulder")
        {
            element = "Projectile";
            desc = "Conjure a large boulder that deals moderate damage to the first enemy hit, then splinters into smaller shards behind the enemy.";
        }
        else if (cardName == "Earthen")
        {
            element = "Buff";
            desc = "Create an earthen barrier that surrounds you for the duration of the room, blocking 5 projectiles before expiring.";
        }
        else if (cardName == "Enstone")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of earth increasing defense by 50% for 30 seconds.";
        }
        else if (cardName == "Impale")
        {
            element = "Target";
            desc = "Create earthen spires at the location of the mouse cursor, which radiate outward dealing moderate damage.";
        }
        else if (cardName == "Quake")
        {
            element = "All";
            desc = "Sunder the earth, periodically creating boulders and stones.";
        }
        else if (cardName == "Rupture")
        {
            element = "Target";
            desc = "Summon earthen spikes that follow your mouse cursor, periodically launching towards the cursor dealing damage.";
        }
        else if (cardName == "Stone")
        {
            element = "Projectile";
            desc = "Hurl a small stone at an enemy dealing moderate damage.";
        }
        else if (cardName == "Wall")
        {
            element = "Projectile";
            desc = "Erect a wall of earth blocking damage. Wall will expire over time or if it blocks enough damage.";
        }

        //====================================================================
        //                       Water Arcana
        //====================================================================

        else if (cardName == "Bubble")
        {
            element = "Projectile";
            desc = "Conjure a slow moving bubbble that deals moderate damage to the first enemy hit.";
        }
        else if (cardName == "Icicle")
        {
            element = "Target";
            desc = "Conjure a sharp shard of ice at the location of your mouse cursor, that deals moderate damage.";
        }
        else if (cardName == "Entomb")
        {
            element = "Projectile";
            desc = "Launch an icy porjectile which freezes the first target hit for 10 seconds. Cannot freeze bosses.";
        }
        else if (cardName == "Enwater")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of water increasing your mana regeneration for 30 seconds.";
        }
        else if (cardName == "Shatter")
        {
            element = "Projectile";
            desc = "Create a line of jagged pillars of ice towards the mouse cursor, which deals modeate damage.";
        }
        else if (cardName == "Spout")
        {
            element = "Target";
            desc = "Create a large water spout at the mouse cursor, periodically creating bubbles.";
        }
        else if (cardName == "Tidal")
        {
            element = "All";
            desc = "Summon a large waves of water that deals damage to all enemies.";
        }
        else if (cardName == "Water")
        {
            element = "Projectile";
            desc = "Strike all enemies in a line in front of you with high pressured water dealing moderate damage and knocking back.";
        }

        //====================================================================
        //                       Life Arcana
        //====================================================================

        else if (cardName == "Bless")
        {
            element = "Buff";
            desc = "Become temporarily invulnerable for 3 seconds.";
        }
        else if (cardName == "Bloom")
        {
            element = "Buff";
            desc = "Remove all negative debuffs on you.";
        }
        else if (cardName == "Enliven")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of life increasing your stamina regeneration for 30 seconds.";
        }
        else if (cardName == "Entangle")
        {
            element = "Projectile";
            desc = "Launch a seed that rapidly grows on the first enemy hit, dealing damage over time.";
        }
        else if (cardName == "Guardian")
        {
            element = "Buff";
            desc = "For the duration of the room or 60 seconds, summon as guardian to attack the closest enemy to you for modeate damage.";
        }
        else if (cardName == "Heal")
        {
            element = "Buff";
            desc = "Use to heal yourself for 150 health.";
        }
        else if (cardName == "Mend")
        {
            element = "Buff";
            desc = "Use to slightly heal yourself for 50 health.";
        }
        else if (cardName == "Venom")
        {
            element = "Projectile";
            desc = "Hurl concentrated balls of poison dealing damage and has a chance to poison.";
        }

        //====================================================================
        //                       Wind Arcana
        //====================================================================

        else if (cardName == "Zap")
        {
            element = "Target";
            desc = "Conjure a bolt of lightning that strikes the location of your mouse cursor dealing moderate damage.";
        }
        else if (cardName == "Twister")
        {
            element = "Projectile";
            desc = "Create a violent twister which slowly moves and damaging enemies that touch it.";
        }
        else if (cardName == "Ball")
        {
            element = "Projectile";
            desc = "Create a slow moving ball of lightning that follows the mouse cursor. The further the ball travels, the larger and more damage it deals.";
        }
        else if (cardName == "Razor")
        {
            element = "Projectile";
            desc = "Whip up cutting winds in a cone in front of you damaging any enemies you are facing.";
        }
        else if(cardName == "Aero")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of wind increasing movement speed for 15 seconds.";
        }
        else if(cardName == "Gust")
        {
            element = "Projectile";
            desc = "Create a pressurized cone of air that damages all enemies in front of you and knocking them back.";
        }
        else if(cardName == "Storm")
        {
            element = "Buff";
            desc = "Whip up a violent storm that rotates around you dealing damage to any enemy hit. Lasts 25 seconds.";
        }
        else if(cardName == "Tempest")
        {
            element = "Target";
            desc = "Create a violent electrical storm which constantly Zaps the location of your mouse cursor for 10 seconds.";
        }
    }
}
