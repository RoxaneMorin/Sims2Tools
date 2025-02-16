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
using Sims2Tools.DBPF.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace Sims2Tools.DBPF.SLOT
{
    public class Slot : DBPFResource
    {
        // See https://modthesims.info/wiki.php?title=List_of_Formats_by_Name
        public static readonly TypeTypeID TYPE = (TypeTypeID)0x534C4F54;
        public const string NAME = "SLOT";

        private uint typeHandler = 1397509972; // Common value found in all but one tested packages.
        private uint version = 4;
        private uint classID = 0; // "Always null" as per https://modthesims.info/wiki.php?title=534C4F54

        private List<SlotItem> items;

        public ReadOnlyCollection<SlotItem> Slots => items.AsReadOnly();

        public Slot(DBPFEntry entry, DbpfReader reader) : base(entry)
        {
            Unserialize(reader);
        }

        public uint Version
        {
            get => this.version;
        }

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
    }
}
