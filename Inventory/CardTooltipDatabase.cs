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
            element = "Projectile, Persistant";
            desc = "Shoot a large fire that explodes into smaller embers when contacting an enemy or wall. Explodes after a few seconds otherwise.";
        }
        else if (cardName == "Enfire")
        {
            element = "Buff";
            desc = "Wreath yourself in flame, temporarily increasing your strength by 25% for 30 seconds.";
        }
        else if (cardName == "Fire Spin")
        {
            element = "Projectile, Persistant";
            desc = "Conjure a whirlwind of flame which slowly advances forward, periodically creating fireballs.";
        }
        else if (cardName == "Flame Lash")
        {
            element = "Conal";
            desc = "Create a whip of flame which sweeps in a wide arc in front of you.";
        }
        else if (cardName == "Magma")
        {
            element = "Projectile";
            desc = "Hurl a piece of molten earth which grows larger as it travels and dealing more damage.";
        }
        else if (cardName == "Molten")
        {
            element = "Instant";
            desc = "The ground beneath the first enemy you are facing erupts dealing massive damage and creating blazes.";
        }

        //====================================================================
        //                       Earth Arcana
        //====================================================================

        else if (cardName == "Boulder")
        {
            element = "Projectile";
            desc = "Conjure a large boulder that deals moderate damage to the first enemy hit.";
        }
        else if (cardName == "Earthen")
        {
            element = "Buff";
            desc = "Create an earthen barrier that surrounds you, blocking 3 projectiles before expiring.";
        }
        else if (cardName == "Enstone")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of earth increasing defense by 50% for 30 seconds.";
        }
        else if (cardName == "Impale")
        {
            element = "Instant";
            desc = "Create a earthen spire under the first enemy you are facing.";
        }
        else if (cardName == "Quake")
        {
            element = "Projectile, Persistant";
            desc = "Sunder the earth, periodically creating boulders and stones.";
        }
        else if (cardName == "Rupture")
        {
            element = "Instant";
            desc = "Summon an earthen pillar under the first enemy you are facing, knocking them towards you.";
        }
        else if (cardName == "Stone")
        {
            element = "Projectile";
            desc = "Hurl a small stone at an enemy.";
        }
        else if (cardName == "Wall")
        {
            element = "Persistant";
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
            element = "Projectile";
            desc = "Conjure a sharp shard of ice that deals moderate damage to the first enemy hit.";
        }
        else if (cardName == "Entomb")
        {
            element = "Projectile, Persistant";
            desc = "Launch an icy porjectile which freezes the first target hit for 3 seconds.";
        }
        else if (cardName == "Enwater")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of water increasing your mana regeneration for 30 seconds.";
        }
        else if (cardName == "Shatter")
        {
            element = "Instant";
            desc = "Create a jagged pillar of ice under the first target you are facing dealing damage.";
        }
        else if (cardName == "Spout")
        {
            element = "Instant, Persistant";
            desc = "Create a large water spout under the first enemy you are facing, periodically creating bubbles.";
        }
        else if (cardName == "Tidal")
        {
            element = "Projectile";
            desc = "Summon a large wave of water that deals damage to all enemies.";
        }
        else if (cardName == "Water")
        {
            element = "Instant";
            desc = "Strike an enemy with high pressure water dealing moderate damage.";
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
            element = "Conal";
            desc = "Douse a large conal area in front of you with life arcana creating a rampant growth of plants damaging enemies.";
        }
        else if (cardName == "Enliven")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of life increasing your stamina regeneration for 30 seconds.";
        }
        else if (cardName == "Entangle")
        {
            element = "Instant, Persistant";
            desc = "Tangle the first enemy you are facing slowing them and dealing damage over time.";
        }
        else if (cardName == "Guardian")
        {
            element = "Persistant";
            desc = "Summon a guardian to fight for you for 10 seconds.";
        }
        else if (cardName == "Heal")
        {
            element = "Buff";
            desc = "Use to slightly heal yourself.";
        }
        else if (cardName == "Mend")
        {
            element = "Buff";
            desc = "Use to heal yourself.";
        }
        else if (cardName == "Venom")
        {
            element = "Projectile";
            desc = "Hurl a concentrated ball of poison dealing damage and has a chance to poison.";
        }

        //====================================================================
        //                       Wind Arcana
        //====================================================================

        else if (cardName == "Zap")
        {
            element = "Instant";
            desc = "Conjure a bolt of lightning to strike the first enemy you are facing.";
        }
        else if (cardName == "Twister")
        {
            element = "Projectile, Persistant";
            desc = "Create a violent twister which random moves damaging enemies that touch it.";
        }
        else if (cardName == "Ball Lightning")
        {
            element = "Projectile";
            desc = "Create a slow moving ball of lightning that spits if it hits an enemy.";
        }
        else if (cardName == "Razor Wind")
        {
            element = "Conal";
            desc = "Whip up cuttin winds in a cone in from of you damaging any enemies in it.";
        }
        else if(cardName == "Aero")
        {
            element = "Buff";
            desc = "Imbue yourself with the embodiment of wind increasing movement speed for 30 seconds.";
        }
        else if(cardName == "Gust")
        {
            element = "Projectile";
            desc = "Create a pressurized ball of air that damages the first enemy hit.";
        }
        else if(cardName == "Storm")
        {
            element = "Persistant";
            desc = "Whip up a violent storm that periodically creates twisters.";
        }
        else if(cardName == "Tempest")
        {
            element = "Persistant";
            desc = "Create a violent electrical storm which zaps random enemies.";
        }
    }
}
