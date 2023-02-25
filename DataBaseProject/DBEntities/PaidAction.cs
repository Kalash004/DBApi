using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseProject.Interfaces;

namespace DataBaseProject.DBEntities
{
    internal class PaidAction:IBaseClass
    {
        private int id;
        private Haircut haircut;
        private Paint paint;

        public PaidAction(Haircut haircut, Paint paint)
        {
            this.id = -1;
            this.haircut = haircut;
            this.paint = paint;
        }
        public PaidAction() { }

        public PaidAction(int id, Haircut haircut, Paint paint)
        {
            this.id = id;
            this.haircut = haircut;
            this.paint = paint;
        }

        public int ID { get => id; set => id = value; }
        internal Haircut Haircut { get => haircut; set => haircut = value; }
        internal Paint Paint { get => paint; set => paint = value; }
    }

}
