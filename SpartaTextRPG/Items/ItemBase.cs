using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Items
{
    public abstract class ItemBase
    {
        public virtual Defines.ItemType ItemType { get; protected set; }
        public CreatureBase Owner { get; protected set; }
        public int DataId { get; protected set; }
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public string Description { get; protected set; }

        public T CastItem<T>() where T : ItemBase
        {
            return (T)this;
        }


        public abstract ItemBase SetInfo(ItemData data, long uid = 0);
        public abstract void AddCount(int count = 1);
        public abstract void RemoveCount(int count = 1);
    }
}
