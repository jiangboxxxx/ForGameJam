using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ceshi : MonoBehaviour//���ⲿ��дҪ�ı����ֵ
{
    public PlayerControlled playerControlled;
    public string[] shuxing;//����
    public int[] shuzhi;//��ֵ


   public void tianjia()
    {
            playerControlled.ModifyAbility(shuxing[0], shuzhi[0]);
        playerControlled.ModifyAbility(shuxing[1], shuzhi[1]);
        playerControlled.ModifyAbility(shuxing[2], shuzhi[2]);
    }

    public void jianshao()
    {
        playerControlled.xindongli -= 20;

    }
    public void TJ()
    {
        playerControlled.xindongli += 20;

    }
}


