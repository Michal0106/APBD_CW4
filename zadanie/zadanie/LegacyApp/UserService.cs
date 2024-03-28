using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly UserCreditService _userCreditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUser(firstName,lastName) 
                || !IsValidEmail(email) 
                || IsUnderAge(dateOfBirth)) return false;
            
            var client = _clientRepository.GetById(clientId);
            var user = SetNewUser(client, dateOfBirth, email, firstName, lastName);
            
            SetUsersCreditLimit(user);

            if (IsCreditLimitBelowThreshold(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        public bool IsValidUser(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }
        
        public bool IsValidEmail(string email)
        {
            return email.Contains("@") || email.Contains(".");
        }

        public bool IsUnderAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            
            return age < 21;
        }

        public User SetNewUser(Client client, DateTime dateOfBirth, string email, string firstName, string lastName)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public void SetUsersCreditLimit(User user)
        {
            var client = (Client) user.Client;
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                user.HasCreditLimit = true;
                var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (client.Type == "ImportantClient") creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }
        }

        public bool IsCreditLimitBelowThreshold(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
    }
}
