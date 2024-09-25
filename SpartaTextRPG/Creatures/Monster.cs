using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Creatures
{
    public class Monster : CreatureBase
    {
        public override Defines.CreatureType CreatureType => Defines.CreatureType.Monster;
        public Defines.MonsterType MonsterType { get; protected set; } = Defines.MonsterType.None;
        public int MonsterDataId { get; protected set; }

        public void SetInfo(MonsterData data)
        {
            // Level 데이터 + 기본 능력치
            if (GameManager.Instance.Hero == null)
            {
                TextManager.ErrorWriteLine("참조 가능한 플레이어 정보가 없습니다.");
                return;
            }

            SetLevel(GameManager.Instance.Hero.Level);
            this.Uid = Util.GenerateUid();
            this.Name = data.Name;
            this.JobType = data.JobType;
            this.MonsterType = data.MonsterType;
            this.Description = data.Description;
            this.DefaultMaxHp += data.MaxHp;
            this.Hp = MaxHp;
            this.DefaultAttack += data.Attack;
            this.DefaultDefense += data.Defense;
            this.DefaultSpeed += data.Speed;
            this.SkillBook = new SkillBook(this);
            this.SkillBook.InitSkills(data.SkillIds ?? []);
            this.Exp = data.Exp;
            this.Inventory = new ItemInventory(this);
            this.Inventory.SetItems(data.ItemIds ?? []);
            this.Inventory.SetGold(data.Gold);
        }

        public override void OnDamaged(int damage, CreatureBase? attacker = null)
        {
            base.OnDamaged(damage, attacker);

            if (attacker == null)
                TextManager.SystemWriteLine($"{Name}이(가) {damage}의 피해를 입었습니다.");
            else
                TextManager.SystemWriteLine($"{Name}이(가) {attacker.Name}에게 {damage}의 피해를 입었습니다.");
        }

        public override void OnHealed(int heal, CreatureBase? healer = null)
        {
            base.OnHealed(heal, healer);
            if (healer == null)
                TextManager.SystemWriteLine($"{Name}이(가) {heal}만큼 회복되었습니다.");
            else
                TextManager.SystemWriteLine($"{Name}이(가) {healer.Name}에게 {heal}만큼 회복되었습니다.");
        }

        public override void OnDead()
        {
            TextManager.SystemWriteLine($"{Name}이(가) 사망하였습니다.");
        }
    }
}
