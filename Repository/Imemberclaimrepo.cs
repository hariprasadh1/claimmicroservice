using claimmicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace claimmicroservice.Repository
{
    public interface Imemberclaimrepo
    {
        public void create(memberclaim obj);

        public List<memberclaim> fetchclaimsformember(int id);

        public List<memberclaim> give();

        public memberclaim GetClaimStatus(int id, memberclaim obj);

    }
}
