using claimmicroservice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace claimmicroservice.Repository
{
    public class memberclaimrepo : Imemberclaimrepo
    {
        Uri baseAddress;
        HttpClient client;
        IConfiguration _config;
        public memberclaimrepo(IConfiguration config)
        {
            _config = config;
            baseAddress = new Uri(_config["Links:POLICY"]);
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }
        public static List<memberclaim> m = new List<memberclaim>()
        {
            new memberclaim()
            {
                memberid=1,
                claimid=19172,
                billedamount=1200,
                claimedamount=1000,
                claimstatus="PENDING",
                benefitid=1
            },
            new memberclaim()
            {
                memberid=2,
                claimid=18246,
                billedamount=1200,
                claimedamount=1000,
                claimstatus="PENDING",
                benefitid=1
            },
            new memberclaim()
            {
                memberid=3,
                claimid=18940,
                billedamount=1200,
                claimedamount=1000,
                claimstatus="PENDING",
                benefitid=1
            }
         };

        public void create(memberclaim ob)
        {
            Random rand = new Random();
            ob.claimid = rand.Next(15000, 18000);
            ob.claimstatus = "PENDING";
            m.Add(ob);
        }
        //it is not used anymore
        public List<memberclaim> fetchclaimsformember(int id)
        {
            List<memberclaim> l = new List<memberclaim>();
            foreach (var item in m)
            {
                if (item.memberid == id)
                {
                    l.Add(item);
                }
            }
            return l;
        }
        public List<memberclaim> give()
        {

            return m;
        }
      

        public memberclaim GetClaimStatus(int id, memberclaim obj)
        {
            string s1 = obj.claimstatus;
            List<int> ls = new List<int>();
            int p = 0;
            int op = 0;
            HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/policy/" + id).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                op = Convert.ToInt32(data);             
                p = JsonConvert.DeserializeObject<int>(data);
            }
            int d = obj.benefitid;
            HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "/policy/" + op + "/" + id + "/" + d).Result;
            int o = 0;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                o = Convert.ToInt32(data);               
                                                       
            }
            if (obj.claimedamount > obj.billedamount)
            {
                //  return "Rejected";
                obj.claimstatus = "REJECTED AS CLAIMED AMOUNT IS GREATER THAN BILLED AMOUNT";
            }
            else if (obj.claimedamount > o)
            {
                obj.claimstatus = "REJECTED AS THE CLAIMED AMOUNT EXCEEDS THE MAXIMUM AVAILABLE BENEFIT CHARGE";
            }
            else
            {
                obj.claimstatus = "ACCEPTED";
            }
            HttpResponseMessage response3 = client.GetAsync(client.BaseAddress + "/policy/" + op + "/" + id + "/" + d + "/1").Result;
            int qo = 0;//it has topup
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;
                qo = Convert.ToInt32(data);
            }
            if (qo == 0)
                obj.claimstatus = "REJECTED AS THERE IS NO TOPUP SUMMARY";
            foreach (memberclaim item in m)
            {
                if (item.claimid == obj.claimid)
                    item.claimstatus = obj.claimstatus;
            }
            return obj;

        }
    }
}
