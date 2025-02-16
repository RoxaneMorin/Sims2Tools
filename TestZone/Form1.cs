using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Sims2Tools;
using Sims2Tools.DBPF;
using Sims2Tools.DBPF.Data;
using Sims2Tools.DBPF.CPF;
using Sims2Tools.DBPF.Package;
using Sims2Tools.DBPF.SceneGraph.BINX;
using Sims2Tools.DBPF.SceneGraph.COLL;
using Sims2Tools.DBPF.SceneGraph.GZPS;
using Sims2Tools.DBPF.SceneGraph.IDR;
using Sims2Tools.DBPF.SceneGraph.TXMT;
using Sims2Tools.DBPF.SceneGraph.TXTR;
using Sims2Tools.DBPF.SceneGraph.XTOL;
using Sims2Tools.DBPF.STR;
using Sims2Tools.DBPF.Utils;
using Sims2Tools.DbpfCache;
using System.Diagnostics;
using Sims2Tools.DBPF.SceneGraph;
using Microsoft.VisualBasic;
using Sims2Tools.DBPF.SceneGraph.XMOL;
using Sims2Tools.Updates;
using Sims2Tools.DBPF.SLOT;

namespace TestZone
{
    public partial class Form1 : Form
    {
        DBPFFile sourcePackage;
        DBPFFile copiedPackage;




        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            if (selectPackageDialog.ShowDialog() == DialogResult.OK)
            {
                sourcePackage = new DBPFFile(selectPackageDialog.FileName);

                string newPackageFilepath = string.Format("{0}/SlotSerializationTest.package", sourcePackage.PackageDir);
                copiedPackage = new DBPFFile(newPackageFilepath);

                List<DBPFEntry> slotEntries = sourcePackage.GetEntriesByType(Slot.TYPE);
                foreach (DBPFEntry entry in slotEntries)
                {
                    Slot slotResource = (Slot)sourcePackage.GetResourceByEntry(entry);

                    copiedPackage.Commit(slotResource, true);
                }

                copiedPackage.Update(false);

                copiedPackage.Close();
                sourcePackage.Close();
            }
        }
    }
}
