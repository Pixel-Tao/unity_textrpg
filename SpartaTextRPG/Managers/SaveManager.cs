using Newtonsoft.Json;
using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Managers
{
    public class SaveManager
    {
        private static SaveManager? _instance = null;
        public static SaveManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SaveManager();

                return _instance;
            }
        }

        public void Save()
        {
            Hero hero = GameManager.Instance.Hero;
            if (hero == null)
            {
                TextManager.SystemWriteLine("저장 가능한 캐릭터가 없습니다.");
                return;
            }

            SaveHeroData data = new SaveHeroData();
            data.Uid = hero.Uid;
            data.Name = hero.Name;
            data.Level = hero.Level;
            data.JobType = hero.JobType;
            data.Exp = hero.Exp;
            data.Hp = hero.Hp;
            data.MapType = hero.CurrentMapType;
            data.Position = hero.CurrentPosition;
            data.RecallPoint = GameManager.Instance.SavedRecallPoint;
            data.EuqippedAccessoryId = hero.EAccessory?.DataId ?? 0;
            data.EuqippedArmorId = hero.EArmor?.DataId ?? 0;
            data.EuqippedSubWeaponId = hero.ESubWeapon?.DataId ?? 0;
            data.EuqippedWeaponId = hero.EWeapon?.DataId ?? 0;
            data.ConsumableItemId = hero.CBuff?.DataId ?? 0;
            data.BuffSkillId = hero.SBuff?.DataId ?? 0;
            data.InventoryData = new SaveInventoryData();
            data.InventoryData.Gold = hero.Inventory.Gold;
            data.InventoryData.Items = new List<SaveItemData>();
            foreach (var item in hero.Inventory.Items)
            {
                if(item == null)
                {
                    data.InventoryData.Items.Add(null);
                }
                else
                {
                    SaveItemData saveItem = new SaveItemData();
                    saveItem.DataId = item.DataId;
                    if (item.ItemType == Utils.Defines.ItemType.Consumable)
                        saveItem.Count = item.CastItem<ConsumableItem>().Count;
                    else if (item.ItemType == Utils.Defines.ItemType.Etc)
                        saveItem.Count = item.CastItem<EtcItem>().Count;
                    else
                        saveItem.Count = 1;
                    data.InventoryData.Items.Add(saveItem);
                }
            }

            data.Skills = new List<SaveSkillData>();
            foreach (var skill in hero.SkillBook.Skills)
            {
                SaveSkillData saveSkill = new SaveSkillData();
                saveSkill.DataId = skill.DataId;
                saveSkill.Count = skill.CurrentCastCount;
                data.Skills.Add(saveSkill);
            }

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText("save.json", json);
        }

        public SaveHeroData? Load()
        {
            if(File.Exists("save.json") == false)
            {
                return null;
            }

            string josn = File.ReadAllText("save.json");

            return JsonConvert.DeserializeObject<SaveHeroData>(josn);
        }
    }
}