using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [System.Serializable]
    public class Skill
    {
        public string Name;
        public int MaxLevel;
        public int Level { get; private set; }

        public Skill(string name, int maxLevel)
        {
            Name = name;
            MaxLevel = maxLevel;
            Level = 0;
        }

        public void LevelUp()
        {
            if (Level < MaxLevel)
            {
                Level++;
            }
        }
    }

    public Text[] skillLevelTexts; // �ı���ʾ��ǰ���ܵȼ�
    public Button[] upgradeButtons; // ������ť��ÿ����ť��Ӧһ������

    private Skill[] skills;
    public int totalSkillPointsUsed;
    public int Skillpoint_sum;//�츳��ʹ�ü���

    public GameObject[] Lock;//������

    public Text jinengText;
    

    void Start()
    {
        // ��ʼ����������
        skills = new Skill[]
        {
            new Skill("����", 3),        
            new Skill("����", 3),       
            new Skill("Ѫ������", 3),    
            new Skill("����", 3),        
            new Skill("����", 3)         
        };

        totalSkillPointsUsed = 0;
        

        // ��ÿ����ť��ӵ���¼�
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i; // ���浱ǰѭ��������
            upgradeButtons[i].onClick.AddListener(() => UpgradeSkill(index));
        }

        UpdateSkillUI();
    }

    void UpgradeSkill(int index)
    {
        if (totalSkillPointsUsed>0)
        {
            Skill skill = skills[index];
            if (skill.Level >= skill.MaxLevel)
            {
                Debug.LogWarning(skill.Name + "�����Ѵﵽ���ȼ���");
                return;
            }

            // ģ�����ļ��ܵ�����
            skill.LevelUp();
            totalSkillPointsUsed --;
            Skillpoint_sum++;

            // ����UI��ʾ
            UpdateSkillUI();
            Debug.Log(skill.Name + "�����������ȼ� " + skill.Level);

        }
        if(Skillpoint_sum>=3)
        {
            Lock[0].SetActive(false);
            Lock[1].SetActive(false);
        }
        if (Skillpoint_sum >= 5)
        {
            Lock[2].SetActive(false);
            Lock[3].SetActive(false);
        }


    }

    void UpdateSkillUI()
    {
        // ����ÿ�����ܵ�UI��ʾ
        jinengText.text = "�츳��:" + totalSkillPointsUsed.ToString(); 
        for (int i = 0; i < skills.Length; i++)
        {
            skillLevelTexts[i].text =  " �ȼ���" + skills[i].Level;
        }
    }

    public void geiwohenhendetianjiajineng1idan()//�ݺݵ���Ӽ��ܵ�
    {
        totalSkillPointsUsed++;
        UpdateSkillUI() ;
    }
}
