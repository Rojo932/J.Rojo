using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Requests.UserV1;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class UserSevice : IUserService
    {
        //START

        IDataProvider _data = null;
        public UserSeviceV1(IDataProvider data)
        {
            _data = data;
        }

        public void Update(UserV1UpdateRequest user)
        {
            string procName = "[dbo].[Users_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", user.Id);
                    col.AddWithValue("@FirstName", user.FirstName);
                    col.AddWithValue("@LastName", user.LastName);
                    col.AddWithValue("@Email", user.Email);
                    col.AddWithValue("@AvatarUrl", user.AvatarUrl);
                    col.AddWithValue("@TenantId", user.TenantId);
                    col.AddWithValue("@Password", user.Password);
                    col.AddWithValue("@PasswordConfirm", user.PasswordConfirm);
                },
                returnParameters: null);
        }

        public int Add(UserV1AddRequest user)
        {
            int id = 0;

            string procName = "[dbo].[Users_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {

                    col.AddWithValue("@FirstName", user.FirstName);
                    col.AddWithValue("@LastName", user.LastName);
                    col.AddWithValue("@Email", user.Email);
                    col.AddWithValue("@AvatarUrl", user.AvatarUrl);
                    col.AddWithValue("@TenantId", user.TenantId);
                    col.AddWithValue("@Password", user.Password);
                    col.AddWithValue("@PasswordConfirm", user.PasswordConfirm);
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);

                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    Int32.TryParse(oId.ToString(), out id);

                });

            return id;
        }

        public User GetById(int id)
        {
            string procName = "[dbo].[Users_SelectById]";
            User user = null;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                //oneShape > secondShape
                //int > param(int)
                paramCollection.AddWithValue("@id", id);

            }, delegate (IDataReader reader, short set) // single Record Mapper
            {
                // oneshape > secondShape
                //Reader from Db >>> Address
                user = MapUser(reader);
            }
            );

            return user;
        }

        public void DeleteById(int id)
        {
            string procName = "[dbo].[Users_Delete]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                //oneShape > secondShape
                //int > param(int)
                paramCollection.AddWithValue("@id", id);
            }, null
            );
        }

        public List<User> GetAll()
        {
            List<User> list = null;

            string procName = "[dbo].[Users_SelectAll]";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                // oneshape > secondShape
                //Reader from Db >>> Address
                User aUser = MapUser(reader);

                if (list == null)
                {
                    list = new List<User>();
                }

                list.Add(aUser);
            });

            return list;
        }

        private static User MapUser(IDataReader reader)
        {
            User aUser = new User();

            int startingIndex = 0;

            aUser.Id = reader.GetSafeInt32(startingIndex++);
            aUser.FirstName = reader.GetSafeString(startingIndex++);
            aUser.LastName = reader.GetSafeString(startingIndex++);
            aUser.Email = reader.GetSafeString(startingIndex++);
            aUser.AvatarUrl = reader.GetSafeString(startingIndex++);
            aUser.TenantId = reader.GetSafeString(startingIndex++);
            aUser.Password = reader.GetSafeString(startingIndex++);
            aUser.PasswordConfirm = reader.GetSafeString(startingIndex++);
            aUser.DateCreated = reader.GetSafeDateTimeNullable(startingIndex++);
            aUser.DateModified = reader.GetSafeDateTimeNullable(startingIndex++);

            return aUser;
        }
    }
}
