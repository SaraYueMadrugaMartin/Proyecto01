using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script que asocia las animaciones según el estado de corrupción en el que se encuentre Alex
public class ControladorAnimaciones
{
    public static Dictionary<int, string> diccionarioAnimaciones = new Dictionary<int, string>();
    public static void corrAnimaciones(int estadoCorr, int armaEquipada)
    {
        switch (estadoCorr)
        {
            case 0:
                switch (armaEquipada)
                {
                    case 0:
                        diccionarioAnimaciones[1] = "alex_idle_0";
                        diccionarioAnimaciones[2] = "alex_walk_0";
                        diccionarioAnimaciones[3] = "alex_hurt_0";
                    break;
                    case 1:
                        diccionarioAnimaciones[1] = "alex_idle_bat_0";
                        diccionarioAnimaciones[2] = "alex_walk_bat_0";
                        diccionarioAnimaciones[3] = "alex_hurt_bat_0";
                    break;
                    default:
                        diccionarioAnimaciones[1] = "alex_idle_gun_0";
                        diccionarioAnimaciones[2] = "alex_walk_gun_0";
                        diccionarioAnimaciones[3] = "alex_hurt_gun_0";
                    break;
                }               
                diccionarioAnimaciones[4] = "alex_run_0";
                diccionarioAnimaciones[5] = "alex_attack_0";
                diccionarioAnimaciones[6] = "alex_interact_0";
                diccionarioAnimaciones[7] = "alex_fire_0";
                diccionarioAnimaciones[8] = "alex_recharge_0";
                diccionarioAnimaciones[9] = "alex_die_0";
                diccionarioAnimaciones[10] = "alex_aim_0";
                break;
            case 1:
                switch (armaEquipada)
                {
                    case 0:
                        diccionarioAnimaciones[1] = "alex_idle_1";
                        diccionarioAnimaciones[2] = "alex_walk_1";
                        diccionarioAnimaciones[3] = "alex_hurt_1";
                    break;
                    case 1:
                        diccionarioAnimaciones[1] = "alex_idle_bat_1";
                        diccionarioAnimaciones[2] = "alex_walk_bat_1";
                        diccionarioAnimaciones[3] = "alex_hurt_bat_1";
                    break;
                    default:
                        diccionarioAnimaciones[1] = "alex_idle_gun_1";
                        diccionarioAnimaciones[2] = "alex_walk_gun_1";
                        diccionarioAnimaciones[3] = "alex_hurt_gun_1";
                    break;
                }
                diccionarioAnimaciones[4] = "alex_run_1";
                diccionarioAnimaciones[5] = "alex_attack_1";
                diccionarioAnimaciones[6] = "alex_interact_1";
                diccionarioAnimaciones[7] = "alex_fire_1";
                diccionarioAnimaciones[8] = "alex_recharge_1";
                diccionarioAnimaciones[9] = "alex_die_1";
                diccionarioAnimaciones[10] = "alex_aim_1";
                break;
            case 2:
                switch (armaEquipada)
                {
                    case 0:
                        diccionarioAnimaciones[1] = "alex_idle_2";
                        diccionarioAnimaciones[2] = "alex_walk_2";
                        diccionarioAnimaciones[3] = "alex_hurt_2";
                    break;
                    case 1:
                        diccionarioAnimaciones[1] = "alex_idle_bat_2";
                        diccionarioAnimaciones[2] = "alex_walk_bat_2";
                        diccionarioAnimaciones[3] = "alex_hurt_bat_2";
                    break;
                    default:
                        diccionarioAnimaciones[1] = "alex_idle_gun_2";
                        diccionarioAnimaciones[2] = "alex_walk_gun_2";
                        diccionarioAnimaciones[3] = "alex_hurt_gun_2";
                    break;
                }
                diccionarioAnimaciones[4] = "alex_run_2";
                diccionarioAnimaciones[5] = "alex_attack_2";
                diccionarioAnimaciones[6] = "alex_interact_2";
                diccionarioAnimaciones[7] = "alex_fire_2";
                diccionarioAnimaciones[8] = "alex_recharge_2";
                diccionarioAnimaciones[9] = "alex_die_2";
                diccionarioAnimaciones[10] = "alex_aim_2";
                break;
            case 3:
                switch (armaEquipada)
                {
                    case 0:
                        diccionarioAnimaciones[1] = "alex_idle_3";
                        diccionarioAnimaciones[2] = "alex_walk_3";
                        diccionarioAnimaciones[3] = "alex_hurt_3";
                    break;
                    case 1:
                        diccionarioAnimaciones[1] = "alex_idle_bat_3";
                        diccionarioAnimaciones[2] = "alex_walk_bat_3";
                        diccionarioAnimaciones[3] = "alex_hurt_bat_3";
                    break;
                    default:
                        diccionarioAnimaciones[1] = "alex_idle_gun_3";
                        diccionarioAnimaciones[2] = "alex_walk_gun_3";
                        diccionarioAnimaciones[3] = "alex_hurt_gun_3";
                    break;
                }
                diccionarioAnimaciones[4] = "alex_run_3";
                diccionarioAnimaciones[5] = "alex_attack_3";
                diccionarioAnimaciones[6] = "alex_interact_3";
                diccionarioAnimaciones[7] = "alex_fire_3";
                diccionarioAnimaciones[8] = "alex_recharge_3";
                diccionarioAnimaciones[9] = "alex_die_3";
                diccionarioAnimaciones[10] = "alex_aim_3";
                break;
            default:
                switch (armaEquipada)
                {
                    case 0:
                        diccionarioAnimaciones[1] = "alex_idle_4";
                        diccionarioAnimaciones[2] = "alex_walk_4";
                        diccionarioAnimaciones[3] = "alex_hurt_4";
                    break;
                    case 1:
                        diccionarioAnimaciones[1] = "alex_idle_bat_4";
                        diccionarioAnimaciones[2] = "alex_walk_bat_4";
                        diccionarioAnimaciones[3] = "alex_hurt_bat_4";
                    break;
                    default:
                        diccionarioAnimaciones[1] = "alex_idle_gun_4";
                        diccionarioAnimaciones[2] = "alex_walk_gun_4";
                        diccionarioAnimaciones[3] = "alex_hurt_gun_4";
                    break;
                }
                diccionarioAnimaciones[4] = "alex_run_4";
                diccionarioAnimaciones[5] = "alex_attack_4";
                diccionarioAnimaciones[6] = "alex_interact_4";
                diccionarioAnimaciones[7] = "alex_fire_4";
                diccionarioAnimaciones[8] = "alex_recharge_4";
                diccionarioAnimaciones[9] = "alex_die_4";
                diccionarioAnimaciones[10] = "alex_aim_4";
                break;
        }        
    }
}
