using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Security;

namespace X.Web.Security
{
    public class CredentialsMembershipProvider : MembershipProvider
    {
        private FormsAuthenticationUserCollection _users = null;
        private FormsAuthPasswordFormat _passwordFormat;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            _passwordFormat = GetPasswordFormat();
        }

        public override bool ValidateUser(string username, string password)
        {
            var users = GetUsers();
            var user = users[username];

            if (user == null)
            {
                return false;
            }

            if (_passwordFormat == FormsAuthPasswordFormat.Clear)
            {
                if (user.Password == password)
                {
                    return true;
                }
            }
            else
            {
                if (user.Password == FormsAuthentication.HashPasswordForStoringInConfigFile(password, _passwordFormat.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        protected FormsAuthenticationUserCollection GetUsers()
        {
            if (_users == null)
            {
                AuthenticationSection section = GetAuthenticationSection();
                FormsAuthenticationCredentials creds = section.Forms.Credentials;
                _users = section.Forms.Credentials.Users;
            }
            return _users;
        }

        protected static AuthenticationSection GetAuthenticationSection()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            return (AuthenticationSection)config.GetSection("system.web/authentication");
        }

        protected FormsAuthPasswordFormat GetPasswordFormat()
        {
            return GetAuthenticationSection().Forms.Credentials.PasswordFormat;
        }

        public override string ApplicationName { get; set; }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }
}
