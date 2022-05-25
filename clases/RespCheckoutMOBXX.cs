using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.clases
{
    public class RespCheckoutMOBXX
    {

        public Boolean result { get; set; }
        public dataRespCheckoutMobex data { get; set; }


        public RespCheckoutMOBXX(string id, string url, string description, decimal total, string created)
        {
            data = new dataRespCheckoutMobex();
            this.data.id = id;
            this.data.url = url;
            this.data.description = description;
            this.data.total = total;
            this.data.created = created;
            }


    }
}
