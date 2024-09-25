using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.BattleUIs
{
    public class BattleMenuUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.BattleAttack,
            Defines.MenuType.BattleSkill,
            Defines.MenuType.BattleItem,
            Defines.MenuType.BattleEscape,
            ];

        public Monster[] Monsters { get; private set; }
        private int monsterSelectedIndex = 0;
        private bool monsterSelectMode = false;
        private int skillDataId = 0;
        private int itemDataId = 0;
        private bool isBossBattle = false;
        private bool turnChangeFlag = false;
        private Random random = new Random();

        private Queue<CreatureBase> turns = new Queue<CreatureBase>();

        public BattleMenuUI(CreatureBase player) : base(player)
        {
        }
        public void SetMonsters(Monster[] monsters)
        {
            Monsters = monsters;
        }

        public override void Show(CreatureBase? visitor = null)
        {
            Hero hero = Owner as Hero;
            if (hero == null)
            {
                TextManager.HWriteLine("참조 가능한 플레이어 정보가 없습니다.");
                GameManager.Instance.WakeUpWorld();
                return;
            }
            if (Monsters.Length == 0)
            {
                TextManager.HWriteLine("몬스터가 존재하지 않습니다. 월드로 이동합니다.");
                GameManager.Instance.WakeUpWorld();
                return;
            }

            SortTurns();
            InitMonster();

            while (true)
            {
                // 우선 공격권 선택
                CreatureBase turn = turns.Peek();

                TextManager.Flush();

                WriteBattleInfo(hero, turn);

                if (turn.CreatureType == Defines.CreatureType.Monster)
                {
                    if (Monsters.All(s => s.IsDead))
                    {
                        BattleEnd(hero);
                        return;
                    }
                    // 몬스터 턴
                    if (turn.IsDead)
                    {
                        // 몬스터가 죽었을 경우
                        TextManager.BattleWriteLine($"{turn.Name}가 사망하였습니다.");
                        turns.Dequeue();
                    }
                    else
                    {
                        Monster? monster = turns.Dequeue() as Monster;
                        if (turnChangeFlag)
                        {
                            TextManager.MWriteLine($"몬스터 {monster?.Name}의 턴입니다.");
                            turnChangeFlag = false;
                        }
                        MonsterAction(monster);
                        // 몬스터 턴 뒤로 넣어줌
                        TurnChange(monster);

                        // 플레이어가 공격 후에 몬스터가 죽었으면
                        // 죽은 몬스터의 index를 턴이 끝난 몬스터에게로 가도록 보정
                        if (Monsters[monsterSelectedIndex].IsDead)
                            monsterSelectedIndex = Math.Max(Array.IndexOf(Monsters, monster), 0);
                    }
                }
                else
                {
                    if (hero.IsDead)
                    {
                        TextManager.Flush();
                        TextManager.LWriteLine("플레이어가 사망하여 귀환지로 이동합니다.");
                        Thread.Sleep(1000);
                        GameManager.Instance.Recall();
                        hero.Rest();
                        return;
                    }

                    if (turnChangeFlag)
                    {
                        TextManager.MWriteLine($"플레이어 {hero.Name}님의 턴입니다.");
                        turnChangeFlag = false;
                    }
                    // 플레이어 턴
                    ShowAllMenus();

                    ConsoleKey key = InputKey();
                    if (monsterSelectMode)
                    {
                        // 몬스터 선택 모드
                        HandleMonsterSelection(hero, key);
                    }
                    else
                    {
                        if (key == Defines.ACCEPT_KEY && Menus[selectedMenuIndex] == Defines.MenuType.BattleEscape)
                        {
                            if (isBossBattle)
                            {
                                TextManager.MWriteLine("보스의 강력한 힘으로 도망칠 수 없습니다.");
                                continue;
                            }

                            if (random.Next(0, 100) > Defines.BATTLE_ESCAPE_RATE)
                            {
                                TextManager.MWriteLine("전투에서 도망치지 못했습니다. 턴이 넘어갑니다.");
                                TurnChange(turns.Dequeue());
                                monsterSelectMode = false;
                                Thread.Sleep(1000);
                                continue;
                            }

                            // 도망 성공
                            TextManager.LWriteLine("전투에서 도망쳤습니다.");
                            Thread.Sleep(1000);
                            GameManager.Instance.WakeUpWorld();
                            return;
                        }
                        // 메뉴 선택 모드
                        HandleMenuSelection(hero, key);
                    }
                }
            }
        }

        private void TurnChange(CreatureBase? turn)
        {
            turnChangeFlag = true;
            if (turn == null) return;
            turns.Enqueue(turn);
        }

        private void WriteBattleInfo(Hero hero, CreatureBase turn)
        {
            int columnWidth = 24;
            int turnIdx = -1;

            {
                // 몬스터 턴 확인
                for (int i = 0; i < Monsters.Length; i++)
                {
                    if (Monsters[i].Uid == turn.Uid)
                    {
                        turnIdx = i;
                        break;
                    }
                }

                // 몬스트 정보 작성
                DrawLine(turnIdx, columnWidth, Monsters.Length);
                WriteRow(turnIdx, columnWidth, Monsters.Select(s => s.Name).ToArray(), monsterSelectMode);
                DrawLine(turnIdx, columnWidth, Monsters.Length);
                WriteRow(turnIdx, columnWidth, Monsters.Select(s => $"Lv.{s.Level}").ToArray());
                WriteRow(turnIdx, columnWidth, Monsters.Select(s => $"HP.{s.Hp}/{s.MaxHp}").ToArray());
                WriteRow(turnIdx, columnWidth, Monsters.Select(s => $"공.{s.Attack}/방.{s.Defense}/속.{s.Speed}").ToArray());
                WriteRow(turnIdx, columnWidth, Monsters.Select(s => s.IsDead ? "사망" : "생존").ToArray());
                DrawLine(turnIdx, columnWidth, Monsters.Length);
            }

            TextManager.WriteLine();
            TextManager.WriteLine();
            TextManager.WriteLine();

            {
                // 플레이어 턴 확인
                turnIdx = turn.Uid == hero.Uid ? 0 : -1;

                // 플레이어 정보 작성
                DrawLine(turnIdx, columnWidth, 1);
                WriteRow(turnIdx, columnWidth, [hero.Name]);
                DrawLine(turnIdx, columnWidth, 1);
                WriteRow(turnIdx, columnWidth, [$"Lv.{hero.Level}"]);
                WriteRow(turnIdx, columnWidth, [$"{Util.HeroTypeToString(hero.JobType)}"]);
                WriteRow(turnIdx, columnWidth, [$"HP.{hero.Hp}/{hero.MaxHp}"]);
                WriteRow(turnIdx, columnWidth, [$"공.{hero.Attack}/방.{hero.Defense}/속.{hero.Speed}"]);
                WriteRow(turnIdx, columnWidth, [$"EXP.{hero.Exp}/{hero.NextLevelExp}"]);
                DrawLine(turnIdx, columnWidth, 1);
            }

            TextManager.GuideLine('~');
        }

        private void HandleMonsterSelection(Hero hero, ConsoleKey key)
        {
            if (key == Defines.RIGHT_KEY)
                monsterSelectedIndex = (monsterSelectedIndex + 1) % Monsters.Length;
            else if (key == Defines.LEFT_KEY)
                monsterSelectedIndex = (monsterSelectedIndex - 1 + Monsters.Length) % Monsters.Length;
            else if (key == Defines.ACCEPT_KEY)
            {
                Monster monster = Monsters[monsterSelectedIndex];
                if (monster.IsDead)
                {
                    TextManager.LWriteLine("이미 죽은 몬스터는 공격할 수 없습니다.");
                    return;
                }

                int attackCount = hero.IsDoubleAttack() ? 2 : 1;
                Skill? skill = null;
                for (int i = 0; i < attackCount; i++)
                {
                    if (i > 0)
                    {
                        if (Monsters.All(x => x.IsDead))
                            break;

                        if (monster.IsDead)
                        {
                            monsterSelectedIndex = (monsterSelectedIndex - 1 + Monsters.Length) % Monsters.Length;
                            monster = Monsters[monsterSelectedIndex];
                        }

                        TextManager.LWriteLine($"{hero.Name}님이 추가 공격을 가합니다!");
                    }
                    // 공격
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.BattleAttack:
                            // 일반공격 실행
                            monster.OnDamaged(Owner.Attack, Owner);
                            Thread.Sleep(500);
                            break;
                        case Defines.MenuType.BattleSkill:
                            // 스킬 실행
                            skill = hero.SkillBook.GetSkill(skillDataId);
                            if (skill == null)
                            {
                                TextManager.MWriteLine("스킬을 찾을 수 없습니다.");
                                return;
                            }

                            for (int combo = 0; combo < skill.ComboCount; combo++)
                            {

                                if (skill.SkillType == Defines.SkillType.Attack)
                                    skill.Use([monster]);
                                else if (skill.SkillType == Defines.SkillType.AOE)
                                {
                                    // 광역 스킬이면 대상을 가져옴
                                    Monster[] targets = GetTargetMonsters(monsterSelectedIndex, skill.TargetCount);
                                    skill.Use(targets);
                                }
                            }
                            Thread.Sleep(500);
                            break;
                        default:
                            TextManager.LWriteLine("아무런 행위도 하지 못했습니다.");
                            break;
                    }
                }

                if (Menus[selectedMenuIndex] == Defines.MenuType.BattleSkill)
                    skill?.RemoveCount();

                TurnChange(turns.Dequeue());
                monsterSelectMode = false;
                return;
            }
            else if (key == Defines.CANCEL_KEY)
                monsterSelectMode = false;
        }

        private void HandleMenuSelection(Hero hero, ConsoleKey key)
        {
            SelectMenu(key);
            if (key == Defines.ACCEPT_KEY)
            {
                switch (Menus[selectedMenuIndex])
                {
                    case Defines.MenuType.BattleAttack:
                        // 공격 -> 몬스터 선택 모드 -> 몬스터 선택 후 공격
                        monsterSelectMode = true;
                        break;
                    case Defines.MenuType.BattleSkill:
                        // 스킬
                        UIManager.Instance.ShowBattleSkillJob(Owner, out skillDataId);
                        Skill? skill = hero.SkillBook.GetSkill(skillDataId);
                        if (skill == null) return;

                        if (skill.SkillType == Defines.SkillType.Attack || skill.SkillType == Defines.SkillType.AOE)
                        {
                            // 공격 스킬인 경우 몬스터를 선택해야함.
                            monsterSelectMode = true;
                            return;
                        }

                        // 공격 스킬이 아닌 경우 플레이어한테 스킬 바로 사용
                        skill.Use();
                        skill.RemoveCount();
                        TurnChange(turns.Dequeue());
                        break;
                    case Defines.MenuType.BattleItem:
                        // 아이템
                        UIManager.Instance.ShowBattleItemJob(Owner, out itemDataId);
                        ItemBase? item = hero.Inventory.GetItem(itemDataId);
                        if (item == null) return;

                        if (item.ItemType != Defines.ItemType.Consumable)
                        {
                            TextManager.MWriteLine("전투중에 사용할 수 없는 아이템입니다.");
                            return;
                        }

                        hero.Consume(item.CastItem<ConsumableItem>());
                        break;
                }
            }
        }

        private void MonsterAction(Monster? monster)
        {
            if (monster == null) return;

            // TODO : 몬스터 행동
            Thread.Sleep(1000);
            Defines.MenuType[] values = [
                Defines.MenuType.BattleAttack,
                Defines.MenuType.BattleAttack,
                Defines.MenuType.BattleAttack,
                Defines.MenuType.BattleAttack,
                Defines.MenuType.BattleAttack,
                Defines.MenuType.BattleSkill,
                Defines.MenuType.BattleSkill,
                Defines.MenuType.BattleItem
            ];
            Defines.MenuType monsterActionType;
            while (true)
            {
                monsterActionType = values[random.Next(values.Length)];

                if (monsterActionType == Defines.MenuType.BattleSkill && monster.SkillBook.Skills.Count == 0)
                    continue;
                if (monsterActionType == Defines.MenuType.BattleItem && monster.Inventory.IsEmpty())
                    continue;

                break;
            }

            if (monsterActionType == Defines.MenuType.BattleItem)
            {
                // 아이템
                ConsumableItem[] cItems = monster.Inventory.GetItems<ConsumableItem>();
                if (cItems.Length > 0)
                {
                    ConsumableItem cItem = cItems[random.Next(cItems.Length)];
                    if (cItem != null) monster.Consume(cItem);
                }
                return;
            }

            Hero hero = Owner as Hero;
            if (hero == null)
            {
                TextManager.HWriteLine("Hero is null");
                return;
            }

            int attackCount = monster.IsDoubleAttack() ? 2 : 1;
            Skill? skill = null;
            for (int i = 0; i < attackCount; i++)
            {
                if (i > 0)
                {
                    if (hero.IsDead) break;
                    TextManager.LWriteLine($"{monster.Name}이(가) 추가 공격을 가합니다.");
                }
                switch (monsterActionType)
                {
                    case Defines.MenuType.BattleAttack:
                        // 공격
                        hero.OnDamaged(monster.Attack, monster);
                        break;
                    case Defines.MenuType.BattleSkill:
                        // 스킬
                        int skillIndex = random.Next(monster.SkillBook.Skills.Count);
                        skill = monster.SkillBook.Skills[skillIndex];
                        for (int combo = 0; combo < skill.ComboCount; combo++)
                            monster.SkillBook.UseSkill(skill, [hero]);
                        break;
                }
            }

            if (monsterActionType == Defines.MenuType.BattleSkill)
                skill?.RemoveCount();
        }

        // 공격순서 정렬 -> Queue 에 저장
        private void SortTurns()
        {
            turns.Clear();

            List<CreatureBase> list = new List<CreatureBase>();
            list.Add(Owner);
            list.AddRange(Monsters);

            list.OrderByDescending(s => s.Speed).ToList().ForEach(s => turns.Enqueue(s));
        }

        // 구분선 그리기 함수
        private void DrawLine(int turnIdx, int columnWidth, int count)
        {
            TextManager.BattleWrite("+");
            for (int i = 0; i < count; i++)
            {
                if (turnIdx == i)
                {
                    TextManager.BattleTurnWrite(new string('=', columnWidth));
                }
                else
                {
                    TextManager.BattleWrite(new string('-', columnWidth));
                }
                TextManager.BattleWrite("+");
            }
            TextManager.WriteLine();
        }

        // 행 출력 함수
        private void WriteRow(int turnIdx, int columnWidth, string[] columns, bool isSelect = false)
        {
            TextManager.BattleWrite("|");
            for (int i = 0; i < columns.Length; i++)
            {
                string content = columns[i];
                int contentWidth = Util.GetStringWidth(content);
                int padding = columnWidth - contentWidth;
                int paddingLeft = padding / 2;
                int paddingRight = Math.Max(padding - paddingLeft, 0);


                if (turnIdx == i)
                {
                    TextManager.BattleTurnWrite(new string(' ', paddingLeft));
                    if (isSelect && i == monsterSelectedIndex)
                        TextManager.BattleSelectWrite(content);
                    else
                        TextManager.BattleTurnWrite(content);
                    TextManager.BattleTurnWrite(new string(' ', paddingRight));
                }
                else
                {
                    TextManager.BattleWrite(new string(' ', paddingLeft));
                    if (isSelect && i == monsterSelectedIndex)
                        TextManager.BattleSelectWrite(content);
                    else
                        TextManager.BattleWrite(content);
                    TextManager.BattleWrite(new string(' ', paddingRight));
                }
                TextManager.BattleWrite("|");
            }
            TextManager.WriteLine();
        }

        private Monster[] GetTargetMonsters(int selectedMonsterIndex, int totalCount)
        {
            // 선택한 몬스터를 기준으로 양옆에 있는 몬스터들을 가져옴
            List<Monster> targets = new List<Monster>();
            int i = 1;
            int count = totalCount > 1 ? (totalCount - 1) / 2 : 1;
            targets.Add(Monsters[selectedMonsterIndex]);
            if (targets.Count == totalCount) return targets.ToArray();
            do
            {
                int leftIndex = selectedMonsterIndex - i;
                leftIndex = leftIndex < 0 ? 0 : leftIndex;
                int rightIndex = selectedMonsterIndex + i;
                rightIndex = rightIndex >= Monsters.Length ? Monsters.Length - 1 : rightIndex;

                if (Monsters[leftIndex].IsDead == false && targets.Contains(Monsters[leftIndex]) == false)
                    targets.Add(Monsters[leftIndex]);

                if (targets.Count == totalCount)
                    break;

                if (Monsters[rightIndex].IsDead == false && targets.Contains(Monsters[rightIndex]) == false)
                    targets.Add(Monsters[rightIndex]);

                if (targets.Count == totalCount)
                    break;

                i++;
            }
            while (i < count + 1);

            return targets.ToArray();
        }

        private bool IsGameEnd()
        {
            Monster? boss = Monsters.FirstOrDefault(s => s.IsDead == true && Defines.LAST_BOSS_ID == s.MonsterDataId);
            if (boss == null) return false;
            if (boss.IsDead == false) return false;

            return true;
        }

        private void InitMonster()
        {
            Monster? boss = null;
            for (int i = 0; i < Monsters.Length; i++)
            {
                if (Defines.LAST_BOSS_ID == Monsters[i].MonsterDataId)
                    boss = Monsters[i];

                TextManager.HWriteLine($"{Monsters[i].Name} 등장! ({Monsters[i].Description})");
            }
            if (boss == null) return;
            isBossBattle = true;
            for (int i = 0; i < boss.Messages.Length; i++)
            {
                TextManager.MWriteLine(boss.Messages[i]);
                Thread.Sleep(1000);
            }
        }

        private void BattleEnd(Hero hero)
        {
            int expSum = Monsters.Sum(s => s.Exp);
            int goldSum = Monsters.Sum(s => s.Inventory.Gold);
            hero.AddExp(expSum);
            hero.Inventory.AddGold(goldSum);
            hero.BuffClear();
            List<ItemBase> items = new List<ItemBase>();
            foreach (Monster monster in Monsters)
            {
                if (monster.Inventory.IsEmpty())
                    continue;
                foreach (ItemBase item in monster.Inventory.GetItems())
                {
                    if (DataManager.Instance.ItemDict.TryGetValue(item.DataId, out ItemData? itemData) == false)
                        continue;

                    if (DataManager.Instance.MonsterDict.TryGetValue(monster.MonsterDataId, out MonsterData? monsterData) == false)
                        continue;

                    if (random.Next(0, 100) < monsterData.ItemDropRate * 100)
                        hero.Inventory.AddItem(itemData);
                }
            }

            if (IsGameEnd())
            {
                TextManager.HWriteLine("최종 보스를 처치했습니다.");
                Thread.Sleep(1000);
                TextManager.HWriteLine("엔딩크레딧이 올라갑니다.");
                Thread.Sleep(2000);
                TextManager.HWriteLine("수고하셨습니다.");
                Thread.Sleep(2000);
                TextManager.EndingCredit();
                GameManager.Instance.GameEnd();
            }
            else
            {
                TextManager.LWriteLine("모든 몬스터를 처치하였습니다.");
                Thread.Sleep(1000);
                GameManager.Instance.WakeUpWorld();
            }
        }
    }
}
