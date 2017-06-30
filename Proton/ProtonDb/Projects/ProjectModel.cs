using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonDb.Projects
{
    public class ProjectModel
    {
        public int ProjeId { get; set; }

        public string ProjeAdi { get; set; }

        public string ProjeSehir { get; set; }

        public string ProjeIlce { get; set; }

        public Boolean ProjeDurum { get; set; }

        public string ProjeAciklama { get; set; }
    }
}
