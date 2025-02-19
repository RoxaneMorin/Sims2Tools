/*
 * Sims2Tools - a toolkit for manipulating The Sims 2 DBPF files
 *
 * William Howard - 2020-2024
 *
 * Parts of this code derived from the SimPE project - https://sourceforge.net/projects/simpe/
 * Parts of this code derived from the SimUnity2 project - https://github.com/LazyDuchess/SimUnity2 
 * Parts of this code may have been decompiled with the JetBrains decompiler
 *
 * Permission granted to use this code in any way, except to claim it as your own or sell it
 */

using Sims2Tools.DBPF.IO;
using Sims2Tools.DBPF.Package;
using Sims2Tools.DBPF.STR;
using Sims2Tools.DBPF.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using static Sims2Tools.DBPF.Data.MetaData;

namespace Sims2Tools.DBPF.SLOT
{
    public class Slot : DBPFResource
    {
        // See https://modthesims.info/wiki.php?title=List_of_Formats_by_Name
        public static readonly TypeTypeID TYPE = (TypeTypeID)0x534C4F54;
        public const string NAME = "SLOT";

        private uint typeHandler = 1397509972; // SimPE's code calls this ID instead. "1397509972U" is the value given there and observed in tested packages.
        private uint version = 4;
        private uint classID = 0; // Named 'Unknown' in SimPE. "Always null" as per https://modthesims.info/wiki.php?title=534C4F54

        private List<SlotItem> items;


        // Seen in SimPE's code, to consider:
        // Bool CheckVersion(uint version) function.
        // Get & Set slot item at index.
        // Insert slot item at index.
        // Add slot item.
        // Remove slot item.
        // Check whether a slot item exists.
        // Count items.
        // Clone both items and the Slot resource itself.


        #region Constructors
        public Slot(DBPFEntry entry) : base(entry) 
        {
            items = new List<SlotItem>();
        }

        public Slot(DBPFEntry entry, DbpfReader reader) : base(entry)
        {
            Unserialize(reader);
        }
        #endregion


        #region Clean/Dirty State
        public override bool IsDirty
        {
            get
            {
                if (base.IsDirty) return true;

                foreach (SlotItem item in items)
                {
                    if (item.IsDirty) return true;
                }

                return false;
            }
        }

        public override void SetClean()
        {
            foreach (SlotItem item in items)
            {
                item.SetClean();
            } 

            base.SetClean();
        }
        #endregion


        #region Properties
        public uint Version
        {
            get => this.version;
            set
            {
                version = value;
                _isDirty = true;
            }
        }

        public uint TypeHandler => typeHandler;
        public uint ClassID => classID;

        public ReadOnlyCollection<SlotItem> Slots => items.AsReadOnly();
        #endregion


        #region Serialization
        protected void Unserialize(DbpfReader reader)
        {
            this._keyName = Helper.ToString(reader.ReadBytes(0x40));

            typeHandler = reader.ReadUInt32();
            version = reader.ReadUInt32();
            classID = reader.ReadUInt32();

            int entries = reader.ReadInt32();

            this.items = new List<SlotItem>(entries);
            while (this.items.Count < entries)
            {
                SlotItem item = new SlotItem(version);
                item.Unserialize(reader);

                this.items.Add(item);
            }
        }

        public override uint FileSize
        {
            get
            {
                uint size = 0x40 + 4 * 4;

                // Add in the items' sizes.
                foreach (SlotItem slotItem in this.items)
                {
                    size += slotItem.FileSize;
                }

                return size;
            }
        }

        public override void Serialize(DbpfWriter writer)
        {
            writer.WriteBytes(Encoding.ASCII.GetBytes(KeyName), 0x40);

            writer.WriteUInt32((uint)typeHandler);
            writer.WriteUInt32((uint)version);
            writer.WriteUInt32((uint)classID);

            int count = items.Count;
            writer.WriteUInt32((uint)count);

            foreach (SlotItem slotItem in items)
            {
                slotItem.Serialize(writer);
            }
        }
        #endregion


        #region Xml Output
        public override XmlElement AddXml(XmlElement parent)
        {
            XmlElement element = XmlHelper.CreateResElement(parent, NAME, this);
            element.SetAttribute("version", Version.ToString());

            for (int i = 0; i < items.Count; ++i)
            {
                items[i].AddXml(element).SetAttribute("index", i.ToString());
            }

            return element;
        }
        #endregion
    }
}
