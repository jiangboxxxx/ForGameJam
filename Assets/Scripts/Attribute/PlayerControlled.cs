using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControlled : MonoBehaviour
{
    //���������ֱ࣬��д�ɱ���Ҳ�ǿ��Եġ�

    public class Ability
    {
        public string name;
        public int value;
       
        public Ability(string _name, int _value)
        {
            name = _name;
            value = _value;
        }
    }
    public class SubjectScore
    {
        public string name;
        public int value;

        public SubjectScore(string _name, int _value)
        {
            name = _name;
            value = _value;
        }
    }

    // �������������ı���
    public Ability liliang = new Ability("����", 0);//����
    public Ability tounao = new Ability("ͷ��", 0);//ͷ��
    public Ability meili = new Ability("����", 0);//����

    //��������ѧ�Ƶı���(��������Ժ���һ��д����֮�󿪷���û������)
    public SubjectScore jianshu = new SubjectScore("����", 0);//����
    public SubjectScore xinyang = new SubjectScore("����", 0);//����
    public SubjectScore liyi = new SubjectScore("����", 0);//����

 
    
    private int _xingdongli = 100;//�ж���
    //��װ,��д�ɲ�д�����������
    public GameObject meiyouxingdongli;
    public int xindongli
    {
        get { return _xingdongli; }
        set
        {
            // ȷ���ж�����С��0
            _xingdongli = Mathf.Max(value, 0);
            // ����һ������ж���100
            _xingdongli = Mathf.Min(_xingdongli, 100);

            // ���Ҫִ�������߼����������ж�ֵ���������顢��ұ仯ʱ���Ŷ�������������������Ӵ���
            
            // ����ж���������С�ڵ���0��ִ���������¼�
            if (_xingdongli <= 0)
            {
                meiyouxingdongli.GetComponent<Animator>().enabled =true;
            }
        }
    }


    public int jinbi = 0;//���
    public int xinqing = 100;//����

    // UI Text �������
    
    public Text xingdongliText;//�ж���
    public Text jinbiText;//���
    public Text xinqingText;//����

    //��Ҫ����
    public Text liliangText;//����
    public Text tounaoText;//ͷ��
    public Text meiliText;//����

    //�γ�
    public Text jiangshuText;//����
    public Text xinyangText;//����
    public Text liyiText;//����

    // ���� UI �ķ���
    void UpdateUI()
    {
        //��������
        liliangText.text = "����: " + liliang.value.ToString();
        tounaoText.text = "ͷ��: " + tounao.value.ToString();
        meiliText.text = "����: " + meili.value.ToString();

        //ѧ��
        jiangshuText.text = jianshu.value.ToString();
        xinyangText.text = xinyang.value.ToString();
        liyiText.text =  liyi.value.ToString();

        //�ж�������ҡ�����
        xingdongliText.text = "�ж���" + _xingdongli.ToString();
        jinbiText.text ="���" + jinbi.ToString();
        xinqingText.text ="����" + xinqing.ToString();

    }

    public void ModifyAbility(string abilityName, int modifier)//�޸��������޸Ŀγ̷�������ճ���Լ��޸ļ��ɡ�
    {
        switch (abilityName)
        {
            case "����"://��һ���ж���ֹ�����������Ϊ����
                liliang.value += modifier;
                if (liliang.value < 0)
                    liliang.value = 0;
                break;
            case "ͷ��":
                tounao.value += modifier;
                break;
            case "����":
                meili.value += modifier;
                break;
                
                //���Խ��ж������γ̵����ݷ��ڴ˴���Ҳ����дһ������

            default:
                Debug.LogWarning("û�и������� " + abilityName);
                break;
        }
    }

    void Start()
    {
        // ��ʼ��ʱ���� UI
        UpdateUI();
    }
    void Update()
    {
        UpdateUI();
    }
}

