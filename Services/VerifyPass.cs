using namespace.Dtos;
using account_ms.Services;
using account_ms.Repositories;

namespace account_ms
{
    public class VerifyPass
    {   
        PassCrip passCrip = new PassCrip();
        public String verify(AutenticateClientDto acd, Client client)
        {   
            string hashed = passCrip.hashPass(acd.password);
            if(hashed == client.password){
                return("Correct");
            }else{
                return("Incorrect");
            }
        }
    }
}