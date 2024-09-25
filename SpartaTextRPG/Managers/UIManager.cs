using SpartaTextRPG.Creatures;
using SpartaTextRPG.UIs.BattleUIs;
using SpartaTextRPG.UIs.GameUIs;
using SpartaTextRPG.UIs.PlayerUIs;
using SpartaTextRPG.UIs.ShopUIs;

namespace SpartaTextRPG.Managers
{
    public class UIManager
    {
        private static UIManager? _instance = null;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UIManager();

                return _instance;
            }
        }

        #region 게임 메뉴
        public void GameTitle()
        {
            JobManager.Instance.Push(() =>
            {
                GameTitleUI gameTitleUI = new GameTitleUI();
                gameTitleUI.Show();
            });
        }
        public void GameStart()
        {
            JobManager.Instance.Push(() =>
            {
                GameStartUI gameStartUI = new GameStartUI();
                gameStartUI.Show();
            });
        }
        public void GameLoad()
        {
            JobManager.Instance.Push(() =>
            {
                GameLoadUI gameLoadUI = new GameLoadUI();
                gameLoadUI.Show();
            });
        }
        public void GameExit()
        {
            TextManager.Confirm("정말로 종료하시겠습니까?", () =>
            {
                TextManager.LWriteLine("게임을 종료합니다.");
                JobManager.Instance.Exit();
            },
            () =>
            {
                JobManager.Instance.Push(UIManager.Instance.GameTitle);
            });
        }

        public void ShowGameSave(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 게임을 저장할 수 있습니다.");
            JobManager.Instance.Push(() => ShowGameSaveJob(owner));
        }
        public void ShowGameSaveJob(CreatureBase owner)
        {
            GameSaveUI gameSaveUI = new GameSaveUI();
            gameSaveUI.Show();
        }
        #endregion

        #region 캐릭터 정보
        public void ShowPlayerMenu(CreatureBase owner)
        {
            JobManager.Instance.Push(() => ShowPlayerMenuJob(owner));
        }
        public void ShowPlayerMenuJob(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 정보를 확인할 수 있습니다.");
            PlayerMenuUI playerMenuUI = new PlayerMenuUI(owner);
            playerMenuUI.Show();
        }

        public void ShowPlayerStatus(CreatureBase owner)
        {
            JobManager.Instance.Push(() => ShowPlayerStatusJob(owner));
        }
        public void ShowPlayerStatusJob(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 상태를 확인할 수 있습니다.");
            PlayerStatusUI statusUI = new PlayerStatusUI(owner);
            statusUI.Show();
        }

        public void ShowPlayerInventory(CreatureBase owner)
        {
            JobManager.Instance.Push(() => ShowPlayerInventoryJob(owner));
        }
        public void ShowPlayerInventoryJob(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 인벤토리를 확인할 수 있습니다.");
            PlayerInventoryUI inventoryUI = new PlayerInventoryUI(owner);
            inventoryUI.Show();
        }

        public void ShowPlayerSkill(CreatureBase owner)
        {
            JobManager.Instance.Push(() => ShowPlayerSkillJob(owner));
        }
        public void ShowPlayerSkillJob(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 스킬을 확인할 수 있습니다.");
            PlayerSkillUI skillUI = new PlayerSkillUI(owner);
            skillUI.Show();
        }

        public void ShowPlayerQuest(CreatureBase owner)
        {
            JobManager.Instance.Push(() => ShowPlayerQuestJob(owner));
        }
        public void ShowPlayerQuestJob(CreatureBase owner)
        {
            TextManager.LWriteLine($"{owner.Name}님의 퀘스트를 확인할 수 있습니다.");
            PlayerQuestUI questUI = new PlayerQuestUI(owner);
            questUI.Show();
        }

        #endregion

        #region 상점
        public void ShowShopVisit(CreatureBase owner, CreatureBase visitor)
        {
            JobManager.Instance.Push(() => ShowShopVisitJob(owner, visitor));
        }
        public void ShowShopVisitJob(CreatureBase owner, CreatureBase visitor)
        {
            TextManager.LWriteLine($"{owner.Name}에게 방문합니다.");
            ShopVisitUI itemShopUI = new ShopVisitUI(owner);
            itemShopUI.Show(visitor);
        }

        public void ShowShopBuy(CreatureBase owner, CreatureBase visitor)
        {
            JobManager.Instance.Push(() => ShowShopBuyJob(owner, visitor));
        }
        public void ShowShopBuyJob(CreatureBase owner, CreatureBase visitor)
        {
            TextManager.LWriteLine($"{owner.Name}에게 아이템을 구매할 수 있습니다.");
            ShopBuyUI shopBuyUI = new ShopBuyUI(owner);
            shopBuyUI.Show(visitor);
        }

        public void ShowShopSell(CreatureBase owner, CreatureBase visitor)
        {
            JobManager.Instance.Push(() => ShowShopSellJob(owner, visitor));
        }
        public void ShowShopSellJob(CreatureBase owner, CreatureBase visitor)
        {
            TextManager.LWriteLine($"{owner.Name}에게 아이템을 판매할 수 있습니다.");
            ShopSellUI shopSellUI = new ShopSellUI(owner);
            shopSellUI.Show(visitor);
        }

        public void ShowShopInn(CreatureBase owner, CreatureBase visitor)
        {
            JobManager.Instance.Push(() => ShowShopInnJob(owner, visitor));
        }
        public void ShowShopInnJob(CreatureBase owner, CreatureBase visitor)
        {
            TextManager.LWriteLine($"{owner.Name}에게 숙박할 수 있습니다.");
            ShopInnUI shopInnUI = new ShopInnUI(owner);
            shopInnUI.Show(visitor);
        }
        #endregion

        #region 전투
        public void ShowBattleStart(CreatureBase owner, Monster[] monsters)
        {
            JobManager.Instance.Push(() => ShowBattleStartJob(owner, monsters));
        }
        public void ShowBattleStartJob(CreatureBase owner, Monster[] monsters)
        {
            if (monsters.Length < 1)
            {
                GameManager.Instance.WakeUpWorld();
                return;
            }
            else if (monsters.Length == 1)
                TextManager.LWriteLine($"{monsters[0].Name} 몬스터와 전투를 시작합니다.");
            else
                TextManager.LWriteLine($"{monsters[0].Name} 외 {monsters.Length - 1}명의 몬스터와 전투를 시작합니다.");
            BattleMenuUI battleUI = new BattleMenuUI(owner);
            battleUI.SetMonsters(monsters);
            battleUI.Show();
        }
        public void ShowBattleSkillJob(CreatureBase owner, out int skillDataId)
        {
            skillDataId = 0;

            BattleSkillUI battleUI = new BattleSkillUI(owner);
            battleUI.Show();
            skillDataId = battleUI.GetSelectedSkillId();
        }
        public void ShowBattleItemJob(CreatureBase owner, out int itemDataId)
        {
            itemDataId = 0;

            BattleItemUI battleUI = new BattleItemUI(owner);
            battleUI.Show();
            itemDataId = battleUI.GetSelectedItemId();
        }
        #endregion
    }
}
