using System;
using System.Management.Instrumentation;
using System.Web.Configuration;
using System.Web.Security;
using WebMatrix.WebData;

namespace X.Web
{
    public class CredentialsMembershipProvider : ExtendedMembershipProvider
    {
        private FormsAuthenticationUserCollection _users;
        private FormsAuthPasswordFormat _passwordFormat;

        public const string ProviderName = "CredentialsMembershipProvider";

        private FormsAuthenticationUser GetUser(string username)
        {
            var users = GetUsers();
            var user = users[username];
            return user;
        }

        private FormsAuthenticationUserCollection GetUsers()
        {
            if (_users == null)
            {
                var section = GetAuthenticationSection();
                var creds = section.Forms.Credentials;
                _users = section.Forms.Credentials.Users;
            }

            return _users;
        }

        private static AuthenticationSection GetAuthenticationSection()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            return (AuthenticationSection)config.GetSection("system.web/authentication");
        }

        private static FormsAuthPasswordFormat GetPasswordFormat()
        {
            var authenticationSection = GetAuthenticationSection();
            return authenticationSection.Forms.Credentials.PasswordFormat;
        }

        private MembershipUser CreateMembershipUser(FormsAuthenticationUser user)
        {
            MembershipCreateStatus status;
            return CreateUser(user.Name, user.Password, String.Empty, String.Empty, String.Empty, true, null, out status);
        }

        #region MembershipProvider

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            _passwordFormat = GetPasswordFormat();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = GetUser(username);

            return CreateMembershipUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);

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

        public override string ApplicationName { get; set; }

        [Obsolete("Method not supported")]
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return false;
        }

        [Obsolete("Method not supported")]
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return false;
        }

        [Obsolete("Method not supported")]
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.UserRejected;

            return new MembershipUser(ProviderName, username, null, String.Empty, String.Empty, String.Empty, true,
                                      false, DateTime.Now.Date, DateTime.Now.Date, DateTime.Now.Date, DateTime.Now.Date,
                                      DateTime.Now.Date);
        }

        [Obsolete("Method not supported")]
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return false;
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var users = GetUsers();

            var membershipUserCollection = new MembershipUserCollection();

            foreach (FormsAuthenticationUser user in users)
            {
                membershipUserCollection.Add(CreateMembershipUser(user));
            }

            totalRecords = membershipUserCollection.Count;

            return membershipUserCollection;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 1; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                switch (GetPasswordFormat())
                {
                    case FormsAuthPasswordFormat.Clear:
                        {
                            return MembershipPasswordFormat.Clear;
                        }
                    case FormsAuthPasswordFormat.SHA1:
                        {
                            return MembershipPasswordFormat.Hashed;
                            //case FormsAuthPasswordFormat.MD5: return MembershipPasswordFormat.Encrypted;
                        }
                    case FormsAuthPasswordFormat.MD5:
                        {
                            throw new NotSupportedException();
                        }
                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotSupportedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        [Obsolete("Not supported")]
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region ExtendedMembershipProvider

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            throw new NotSupportedException();
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new NotSupportedException();
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new NotSupportedException();
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, System.Collections.Generic.IDictionary<string, object> values)
        {
            throw new NotSupportedException();
        }

        [Obsolete("Not supported")]
        public override bool DeleteAccount(string userName)
        {
            return false;
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotSupportedException();
        }

        public override System.Collections.Generic.ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetCreateDate(string userName)
        {
            var user = GetUser(userName);

            if (user != null)
            {
                return DateTime.Now.Date;
            }

            throw new InstanceNotFoundException(String.Format("User with [{0}] user name not found.", userName));
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotSupportedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotSupportedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotSupportedException();
        }

        public override bool IsConfirmed(string userName)
        {
            var user = GetUser(userName);
            return user != null;
        }

        [Obsolete("Method not supported")]
        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            return false;
        }

        #endregion
    }
}
