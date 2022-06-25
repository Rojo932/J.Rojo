using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Friends;
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
    public class FriendService : IFriendService
    {
        IDataProvider _data = null;
        public FriendService(IDataProvider data)
        {
            _data = data;
        }

        public void Update(FriendUpdateRequest friend)
        {
            string procName = "[dbo].[Friends_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", friend.Id);
                    col.AddWithValue("@Title", friend.Title);
                    col.AddWithValue("@Bio", friend.Bio);
                    col.AddWithValue("@Summary", friend.Summary);
                    col.AddWithValue("@Headline", friend.Headline);
                    col.AddWithValue("@Slug", friend.Slug);
                    col.AddWithValue("@PrimaryImageId", friend.PrimaryImageId);
                    col.AddWithValue("@StatusId", friend.StatusId);

                },
                returnParameters: null);

        }

        public int Add(FriendAddRequest friend)
        {
            int id = 0;

            string procName = "[dbo].[Friends_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Title", friend.Title);
                    col.AddWithValue("@Bio", friend.Bio);
                    col.AddWithValue("@Summary", friend.Summary);
                    col.AddWithValue("@Headline", friend.Headline);
                    col.AddWithValue("@Slug", friend.Slug);
                    col.AddWithValue("@PrimaryImageId", friend.PrimaryImageId);
                    col.AddWithValue("@StatusId", friend.StatusId);                    

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

        public Friend GetById(int id)
        {
            string procName = "[dbo].[Friends_SelectById]";

            Friend friend = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                //oneShape > secondShape
                //int > param(int)
                paramCollection.AddWithValue("@id", id);

            }, delegate (IDataReader reader, short set) // single Record Mapper
            {
                friend = MapFriend(reader);
            }
            );

            return friend;
        }

        public FriendV2 GetByIdV2(int id)
        {
            string procName = "[dbo].[Friends_SelectByIdV3]";

            FriendV2 friendV2 = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@id", id);

            }, delegate (IDataReader reader, short set) // single Record Mapper
            {
                // oneshape > secondShape
                //Reader from Db >>> Address
                friendV2 = MapFriendV2(reader);
            }
            );
            return friendV2;
        }

        public void Delete(int id)
        {
            string procName = "[dbo].[Friends_Delete]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
           {
                oneShape > secondShape
                int > param(int)
               paramCollection.AddWithValue("@id", id);

            }, null
            );
        }

        public List<Friend> GetAll()
        {
            List<Friend> list = null;

            string procName = "[dbo].[Friends_SelectAll]";

            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {
                // oneshape > secondShape
                //Reader from Db >>> Friend
                Friend aFriend = MapFriend(reader);

                if (list == null)
                {
                    list = new List<Friend>();
                }

                list.Add(aFriend);
            });

            return list;
        }

        public Paged<Friend> Paginate(int pageIndex, int pageSize)
        {
            Paged<Friend> pagedResult = null;
            List<Friend> result = null;
            int totalCount = 0;
            _data.ExecuteCmd(
                "[dbo].[Friends_Pagination]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Friend model = new Friend();
                    int index = 0;
                    model.Id = reader.GetSafeInt32(index++);
                    model.Bio = reader.GetSafeString(index++);
                    model.Summary = reader.GetSafeString(index++);
                    model.Title = reader.GetSafeString(index++);
                    model.Headline = reader.GetSafeString(index++);
                    model.Slug = reader.GetSafeString(index++);
                    model.PrimaryImageId = reader.GetSafeInt32Nullable(index++);
                    model.StatusId = reader.GetSafeInt32(index++);
                    model.DateCreated = reader.GetSafeDateTimeNullable(index++);
                    model.DateModified = reader.GetSafeDateTimeNullable(index++);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(index++);
                    }

                    if (result == null)
                    {
                        result = new List<Friend>();
                    }
                    result.Add(model);
                });
            if (result != null)
            {
                pagedResult = new Paged<Friend>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;

        }

        public Paged<FriendV2> PaginateV2(int pageIndex, int pageSize)
        {
            Paged<FriendV2> pagedResult = null;
            List<FriendV2> result = null;
            int totalCount = 0;
            _data.ExecuteCmd(
                "[dbo].[Friends_PaginationV3]",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@PageIndex", pageIndex);
                    parameterCollection.AddWithValue("@PageSize", pageSize);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {

                    FriendV2 aFriend = new FriendV2();
                    int startingIndex = 0;
                    aFriend.Id = reader.GetSafeInt32(startingIndex++);
                    aFriend.Bio = reader.GetSafeString(startingIndex++);
                    aFriend.Summary = reader.GetSafeString(startingIndex++);
                    aFriend.Title = reader.GetSafeString(startingIndex++);
                    aFriend.Headline = reader.GetSafeString(startingIndex++);
                    aFriend.Slug = reader.GetSafeString(startingIndex++);
                    aFriend.ImageId = reader.GetSafeInt32Nullable(startingIndex++);
                    aFriend.ImageTypeId = reader.GetSafeInt32Nullable(startingIndex++);
                    aFriend.ImageUrl = reader.GetSafeString(startingIndex++);
                    aFriend.StatusId = reader.GetSafeInt32(startingIndex++);
                    aFriend.Skill = reader.DeserializeObject<List<Skill>>(startingIndex++);
                    aFriend.DateCreated = reader.GetSafeDateTimeNullable(startingIndex++);
                    aFriend.DateModified = reader.GetSafeDateTimeNullable(startingIndex++);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (result == null)
                    {
                        result = new List<FriendV2>();
                    }
                    result.Add(aFriend);
                });
            if (result != null)
            {
                pagedResult = new Paged<FriendV2>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;

        }

        public Paged<Friend> SearchPaginate(int pageIndex, int pageSize, string query) 
        {
            Paged<Friend> pagedList = null;
            List<Friend> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Friends_Search_Pagination]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@Query", query);
            }, delegate (IDataReader reader, short set)
            {
                Friend model = new Friend();
                int index = 0;
                model.Id = reader.GetSafeInt32(index++);
                model.Bio = reader.GetSafeString(index++);
                model.Summary = reader.GetSafeString(index++);
                model.Title = reader.GetSafeString(index++);
                model.Headline = reader.GetSafeString(index++);
                model.Slug = reader.GetSafeString(index++);
                model.PrimaryImageId = reader.GetSafeInt32Nullable(index++);
                model.StatusId = reader.GetSafeInt32(index++);
                model.DateCreated = reader.GetSafeDateTimeNullable(index++);
                model.DateModified = reader.GetSafeDateTimeNullable(index++);
                totalCount = reader.GetSafeInt32(index++);
                if (list == null)
                {
                    list = new List<Friend>();
                }
                list.Add(model);
            });
            if (list != null)
            {
                pagedList = new Paged<Friend>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<FriendV2> SearchPaginateV2(int pageIndex, int pageSize, string query)
        {
            Paged<FriendV2> pagedList = null;
            List<FriendV2> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Friends_Search_PaginationV3]";
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@Query", query);
            }, delegate (IDataReader reader, short set)
            {
                FriendV2 aFriend = new FriendV2();
                int startingIndex = 0;
                aFriend.Id = reader.GetSafeInt32(startingIndex++);
                aFriend.Bio = reader.GetSafeString(startingIndex++);
                aFriend.Summary = reader.GetSafeString(startingIndex++);
                aFriend.Title = reader.GetSafeString(startingIndex++);
                aFriend.Headline = reader.GetSafeString(startingIndex++);
                aFriend.Slug = reader.GetSafeString(startingIndex++);
                aFriend.ImageId = reader.GetSafeInt32Nullable(startingIndex++);
                aFriend.ImageTypeId = reader.GetSafeInt32Nullable(startingIndex++);
                aFriend.ImageUrl = reader.GetSafeString(startingIndex++);
                aFriend.StatusId = reader.GetSafeInt32(startingIndex++);
                aFriend.Skill = reader.DeserializeObject<List<Skill>>(startingIndex++);
                aFriend.DateCreated = reader.GetSafeDateTimeNullable(startingIndex++);
                aFriend.DateModified = reader.GetSafeDateTimeNullable(startingIndex++);
                if (list == null)
                {
                    list = new List<FriendV2>();
                }
                list.Add(aFriend);
            });
            if (list != null)
            {
                pagedList = new Paged<FriendV2>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }


        private static Friend MapFriend(IDataReader reader)
        {
            Friend aFriend = new Friend();
            int startingIndex = 0;
            aFriend.Id = reader.GetSafeInt32(startingIndex++);
            aFriend.Bio = reader.GetSafeString(startingIndex++);
            aFriend.Summary = reader.GetSafeString(startingIndex++);
            aFriend.Title = reader.GetSafeString(startingIndex++);
            aFriend.Headline = reader.GetSafeString(startingIndex++);
            aFriend.Slug = reader.GetSafeString(startingIndex++);
            aFriend.PrimaryImageId = reader.GetSafeInt32Nullable(startingIndex++);
            aFriend.StatusId = reader.GetSafeInt32(startingIndex++);
            aFriend.DateCreated = reader.GetSafeDateTimeNullable(startingIndex++);
            aFriend.DateModified = reader.GetSafeDateTimeNullable(startingIndex++);
            return aFriend;
        }

        private static FriendV2 MapFriendV2(IDataReader reader)
        {
            FriendV2 aFriend = new FriendV2();
            int startingIndex = 0;
            aFriend.Id = reader.GetSafeInt32(startingIndex++);
            aFriend.Bio = reader.GetSafeString(startingIndex++);
            aFriend.Summary = reader.GetSafeString(startingIndex++);
            aFriend.Title = reader.GetSafeString(startingIndex++);
            aFriend.Headline = reader.GetSafeString(startingIndex++);
            aFriend.Slug = reader.GetSafeString(startingIndex++);
            aFriend.ImageId = reader.GetSafeInt32Nullable(startingIndex++);
            aFriend.ImageTypeId = reader.GetSafeInt32Nullable(startingIndex++);
            aFriend.ImageUrl = reader.GetSafeString(startingIndex++);
            aFriend.StatusId = reader.GetSafeInt32(startingIndex++);
            aFriend.Skill = reader.DeserializeObject<List<Skill>>(startingIndex++);
            aFriend.DateCreated = reader.GetSafeDateTimeNullable(startingIndex++);
            aFriend.DateModified = reader.GetSafeDateTimeNullable(startingIndex++);
            return aFriend;
        }
    }
}
