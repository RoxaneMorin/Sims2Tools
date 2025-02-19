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
using System;
using System.Xml;

namespace Sims2Tools.DBPF.SLOT
{
    public enum SlotItemType : ushort
    {
        Container = 0,
        [Obsolete]
        Sprite = 1,
        [Obsolete]
        Snap = 2,
        Routing = 3,
        Target = 4
    }

    public class SlotItem
    {
        readonly uint version;

        // See https://modthesims.info/wiki.php?title=534C4F54 for possible explanations
        // All versions
        SlotItemType type;
        float unknownf1;
        float unknownf2;
        float unknownf3;
        int unknowni1;
        int unknowni2;
        int unknowni3;
        int unknowni4;
        int unknowni5;

        // Version >=5
        float unknownf4;
        float unknownf5;
        float unknownf6;
        int unknowni6;

        // Version >=6
        short unknowns1;
        short unknowns2;

        // Version >=7
        float unknownf7;

        // Version >=8
        int unknowni7;

        // Version >=9
        int unknowni8;

        // Version >=10
        float unknownf8;

        // Version >=40
        int unknowni9;
        int unknowni10;
        // SimPE reads and writes unknowni9 & unknowni10 regardless of Slot version. Not sure if it's because it updates all slots to v0x40 anyway.


        // To consider: Clone and Equals functions.


        #region Constructors
        public SlotItem(uint version) => this.version = version;
        #endregion


        #region Clean/Dirty State
        private bool _isDirty = false;

        public bool IsDirty => _isDirty;
        public void SetClean() => _isDirty = false;
        #endregion


        #region Properties
        public SlotItemType Type
        {
            get => type;
            set
            {
                type = value;
                _isDirty = true;
            }
        }

        public float F1
        {
            get => unknownf1;
            set 
            { 
                unknownf1 = value;
                _isDirty = true;
            }
        }
        public float F2
        {
            get => unknownf2;
            set 
            { 
                unknownf2 = value;
                _isDirty = true;
            }
        }
        public float F3
        {
            get => unknownf3;
            set 
            { 
                unknownf3 = value; 
                _isDirty = true;
            }
        }
        public float F4
        {
            get => unknownf4;
            set 
            { 
                unknownf4 = value;
                _isDirty = true;
            }
        }
        public float F5
        {
            get => unknownf5;
            set 
            { 
                unknownf5 = value;
                _isDirty = true;
            }
        }
        public float F6
        {
            get => unknownf6;
            set 
            { 
                unknownf6 = value;
                _isDirty = true;
            }
        }
        public float F7
        {
            get => unknownf7;
            set 
            { 
                unknownf7 = value;
                _isDirty = true;
            }
        }
        public float F8
        {
            get => unknownf8;
            set 
            { 
                unknownf8 = value;
                _isDirty = true;
            }
        }

        public int I1
        {
            get => unknowni1;
            set 
            { 
                unknowni1 = value;
                _isDirty = true;
            }
        }
        public int I2
        {
            get => unknowni2;
            set 
            { 
                unknowni2 = value; 
                _isDirty = true;
            }
        }
        public int I3
        {
            get => unknowni3;
            set 
            { 
                unknowni3 = value; 
                _isDirty = true;
            }
        }
        public int I4
        {
            get => unknowni4;
            set 
            { 
                unknowni4 = value;
                _isDirty = true;
            }
        }
        public int I5
        {
            get => unknowni5;
            set 
            { 
                unknowni5 = value;
                _isDirty = true;
            }
        }
        public int I6
        {
            get => unknowni6;
            set 
            { 
                unknowni6 = value;
                _isDirty = true;
            }
        }
        public int I7
        {
            get => unknowni7;
            set 
            { 
                unknowni7 = value;
                _isDirty = true;
            }
        }
        public int I8
        {
            get => unknowni8;
            set 
            { 
                unknowni8 = value;
                _isDirty = true;
            }
        }
        public int I9
        {
            get => unknowni9;
            set 
            { 
                unknowni9 = value; 
                _isDirty = true;
            }
        }
        public int I10
        {
            get => unknowni10;
            set 
            { 
                unknowni10 = value; 
                _isDirty = true;
            }
        }

        public short S1
        {
            get => unknowns1;
            set 
            { 
                unknowns2 = value;
                _isDirty = true;
            }
        }
        public short S2
        {
            get => unknowns2;
            set 
            { 
                unknowns2 = value;
                _isDirty = true;
            }
        }
        #endregion


        #region Serialization
        internal void Unserialize(DbpfReader reader)
        {
            type = (SlotItemType)reader.ReadUInt16();

            unknownf1 = reader.ReadSingle();
            unknownf2 = reader.ReadSingle();
            unknownf3 = reader.ReadSingle();

            unknowni1 = reader.ReadInt32();
            unknowni2 = reader.ReadInt32();
            unknowni3 = reader.ReadInt32();
            unknowni4 = reader.ReadInt32();
            unknowni5 = reader.ReadInt32();

            if (version >= 5)
            {
                unknownf4 = reader.ReadSingle();
                unknownf5 = reader.ReadSingle();
                unknownf6 = reader.ReadSingle();

                unknowni6 = reader.ReadInt32();
            }

            if (version >= 6)
            {
                unknowns1 = reader.ReadInt16();
                unknowns2 = reader.ReadInt16();
            }

            if (version >= 7)
            {
                unknownf7 = reader.ReadSingle();
            }

            if (version >= 8)
            {
                unknowni7 = reader.ReadInt32();
            }

            if (version >= 9)
            {
                unknowni8 = reader.ReadInt32();
            }

            if (version >= 0x10)
            {
                unknownf8 = reader.ReadSingle();
            }

            if (version >= 0x40)
            {
                unknowni9 = reader.ReadInt32();
                unknowni10 = reader.ReadInt32();
            }
        }


        public uint FileSize
        {
            get
            {
                // Could a switch be used here instead? Do versions between 9, 0x10 and 0x40 exist?

                if (version >= 0x40)
                {
                    // >= 0x40 : 2 + 8 * 4 + 4 * 4 + 2 * 2 + 4 + 4 + 4 + 4 + 2 * 4
                    return 78;
                }
                else if (version >= 0x10)
                {
                    // >= 0x10 : 2 + 8 * 4 + 4 * 4 + 2 * 2 + 4 + 4 + 4 + 4
                    return 70;
                }
                else if (version >= 9)
                {
                    // >= 9 : 2 + 8 * 4 + 4 * 4 + 2 * 2 + 4 + 4 + 4
                    return 66;
                }
                else if (version >= 8)
                {
                    // >= 8 : 2 + 8 * 4 + 4 * 4 + 2 * 2 + 4 + 4
                    return 62;
                }
                else if (version >= 7)
                {
                    // >= 7 : 2 + 8 * 4 + 4 * 4 + 2 * 2 + 4
                    return 58;
                }
                else if (version >= 6)
                {
                    // >= 6 : 2 + 8 * 4 + 4 * 4 + 2 * 2
                    return 54;

                }
                else if (version >= 5)
                {
                    // >= 5: 2 + 8 * 4 + 4 * 4
                    return 50;
                }
                else
                {
                    // base: 2 + 8 * 4
                    return 34;
                }
            }
        }


        public void Serialize(DbpfWriter writer)
        {
            writer.WriteUInt16((ushort)type);

            writer.WriteSingle(unknownf1);
            writer.WriteSingle(unknownf2);
            writer.WriteSingle(unknownf3);

            writer.WriteInt32(unknowni1);
            writer.WriteInt32(unknowni2);
            writer.WriteInt32(unknowni3);
            writer.WriteInt32(unknowni4);
            writer.WriteInt32(unknowni5);

            if (version >= 5)
            {
                writer.WriteSingle(unknownf4);
                writer.WriteSingle(unknownf5);
                writer.WriteSingle(unknownf6);

                writer.WriteInt32(unknowni6);
            }

            if (version >= 6)
            {
                writer.WriteInt16(unknowns1);
                writer.WriteInt16(unknowns2);
            }

            if (version >= 7)
            {
                writer.WriteSingle(unknownf7);
            }

            if (version >= 8)
            {
                writer.WriteInt32(unknowni7);
            }

            if (version >= 9)
            {
                writer.WriteInt32(unknowni8);
            }

            if (version >= 0x10)
            {
                writer.WriteSingle(unknownf8);
            }

            if (version >= 0x40)
            {
                writer.WriteInt32(unknowni9);
                writer.WriteInt32(unknowni10);
            }
        }
        #endregion


        #region Xml Output
        public XmlElement AddXml(XmlElement parent)
        {
            XmlElement element = parent.OwnerDocument.CreateElement("item");
            parent.AppendChild(element);

            element.SetAttribute("Float1", F1.ToString());
            element.SetAttribute("Float2", F2.ToString());
            element.SetAttribute("Float3", F3.ToString());
            element.SetAttribute("Int1", I1.ToString());
            element.SetAttribute("Int2", I2.ToString());
            element.SetAttribute("Int3", I3.ToString());
            element.SetAttribute("Int4", I4.ToString());
            element.SetAttribute("Int5", I5.ToString());

            if (version >= 5)
            {
                element.SetAttribute("Float4", F4.ToString());
                element.SetAttribute("Float5", F5.ToString());
                element.SetAttribute("Float6", F6.ToString());
                element.SetAttribute("Int6", I6.ToString());
            }

            if (version >= 6)
            {
                element.SetAttribute("Short1", S1.ToString());
                element.SetAttribute("Short2", S2.ToString());
            }

            if (version >= 7)
            {
                element.SetAttribute("Float7", F7.ToString());
            }

            if (version >= 8)
            {
                element.SetAttribute("Int7", I7.ToString());
            }

            if (version >= 9)
            {
                element.SetAttribute("Int8", I8.ToString());
            }

            if (version >= 0x10)
            {
                element.SetAttribute("Float8", F8.ToString());
            }

            if (version >= 0x40)
            {
                element.SetAttribute("Int9", I9.ToString());
                element.SetAttribute("Int10", I10.ToString());
            }
            return element;
        }
        #endregion

        public string DiffString()
        {
            return $"{Type}; F1:{F1}; F2:{F2}; F3:{F3}; I1:{I1}; I2:{I2}; I3:{I3}; I4:{I4}; I5:{I5}; F4:{F4}; F5:{F5}; F6:{F6}; S1:{S1}; S2:{S2}; F7:{F7}; I7:{I7}; I8:{I8}; I9:{I9}; I10:{I10}";
        }
    }
}
