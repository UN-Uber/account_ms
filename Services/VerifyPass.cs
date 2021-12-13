using account_ms.Dtos;
using account_ms.Services;
using account_ms.Repositories;
using account_ms.Models;

namespace account_ms.Services
{
    public class VerifyPass
    {   
        PassCrip passCrip = new PassCrip();
        public string verify(AtenticateClientDto acd, Client client)
        {   
            bool result = passCrip.rehash(acd.password, client.password);
            if(result){
                return("Correct");
            }else{
                return("Incorrect");
            }
        }
    }
}