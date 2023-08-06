﻿using C._Domain.Entities;
using D._Repository.Services;
using Dapper;
using E._DAL.SQLServer.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace E._DAL.SQLServer.Services
{
    public class ApplicationUserStore : IApplicationUserStore<User>, IUserEmailStore<User>, IUserPhoneNumberStore<User>,
        IUserPasswordStore<User>
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Queries


        private readonly string _findByIdSql = @"SELECT * FROM Users WHERE DELETESTATUS = 0 AND ID = @userId";

        private readonly string _findByNameSql = @"SELECT * FROM Users WHERE DELETESTATUS = 0 AND Username = @username";

        private readonly string _findByEmailSql = @"
SELECT *  FROM Users
WHERE DELETESTATUS = 0 AND Email =  @email";

        private readonly string _deleteUserByIdSql = @"DELETE FROM Users WHERE Id = @id";

        private readonly string _getAllSql = @"
SELECT * FROM Users 
WHERE DELETESTATUS = 0
ORDER BY RowNum DESC";

        private readonly string _addSql = $@"INSERT INTO Users([Name],
                                                               [Surname],
                                                               [Username],
                                                               [Email],
                                                               [Password],
                                                               [UserType],
                                                               [RefreshTokenExpiryTime],
                                                               [RefreshToken],
                                                               [PhoneNumber],
                                                               [CreatedDate],
                                                               [DeleteStatus])
                                             VALUES(@{nameof(User.Name)},
                                                    @{nameof(User.Surname)},
                                                    @{nameof(User.SystemName)},
                                                    @{nameof(User.Email)},
                                                    @{nameof(User.Password)},
                                                    @{nameof(User.UserType)},
                                                    GETDATE(),
                                                    '',
                                                    @{nameof(User.Phone)},
                                                    @{nameof(User.CreateDate)},
                                                    0)";

        private readonly string _updateSql = @"UPDATE Users
                                               SET UserName = @userName,
                                               Email = @email,
                                               PhoneNumber = @phoneNumber
                                               WHERE Id = @id";

        private readonly string _updateTokenSql = @"UPDATE Users
                                                    SET RefreshTokenExpiryTime = @refreshTokenExpiryTime,
                                                        RefreshToken = @refreshToken
                                                        WHERE Id = @id";

        #endregion



        public ApplicationUserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.GetConnection().QueryAsync(_addSql, user, _unitOfWork.GetTransaction());

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_deleteUserByIdSql, new { id = user.Id }, _unitOfWork.GetTransaction());
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = ex.Message });
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByEmail(string email)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<User>(_findByEmailSql, new { email }, _unitOfWork.GetTransaction());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<User>(_findByEmailSql, new { email = normalizedEmail }, _unitOfWork.GetTransaction());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<User>(_findByIdSql, new { userId }, _unitOfWork.GetTransaction());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
              
            }
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<User>(_findByNameSql, new { username = normalizedUserName }, _unitOfWork.GetTransaction());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _unitOfWork.GetConnection().QueryAsync(_updateSql, new { userName = user.SystemName, email = user.Email, id = user.Id }, _unitOfWork.GetTransaction());

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = ex.Message });
            }
        }

        public async Task<IdentityResult> UpdateTokenAsync(User user)
        {
            try
            {
                await _unitOfWork.GetConnection().QueryAsync(_updateTokenSql, new { refreshTokenExpiryTime = user.RefreshTokenExpiryTime, refreshToken = user.RefreshToken, id = user.Id }, _unitOfWork.GetTransaction());

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "400", Description = ex.Message });
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<User>(_getAllSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SystemName);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Phone);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SystemName);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.Phone = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password != null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.SystemName = userName;
            return Task.FromResult(0);
        }

    }
}
