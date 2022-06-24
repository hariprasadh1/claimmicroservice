using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace claimmicroservice.Models
{
    public class memberclaim
    {
        public int memberid { get; set; }
        public int claimid { get; set; }
        public int billedamount { get; set; }
        public int claimedamount { get; set; }
        public int benefitid { get; set; }
        public string claimstatus { get; set; }
        //claim er modhe hospital id ache naki check koro tahle duto validation eksathe hoe jabe
        //there will be 4 validations
        //claimed amount<billed amount --1 =>done
        //eligible hospital id should be selected -->checked through drop down=>not done
        //topup should be there =>it has to be done through topfrequnecy column =>not done
        //claimed amount<=2*premium<billed amount
        //beneficial charges<=2*premium

    }
}
