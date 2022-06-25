using Sabio.Data.Providers;
using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Data;
using Sabio.Models.Requests.Addresses;

namespace Sabio.Services
{
    public class AddressesService : IAddressesService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _data;

        public AddressesService(IAuthenticationService<int> authSerice, IDataProvider dataProvider)
        {
            _authenticationService = authSerice;
            _data = dataProvider;
        }

        public void Update(AddressUpdateRequest place)
        {
            string procName = "[dbo].[Sabio_Addresses_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", place.Id);
                    col.AddWithValue("@LineOne", place.LineOne);
                    col.AddWithValue("@SuiteNumber", place.SuiteNumber);
                    col.AddWithValue("@City", place.City);
                    col.AddWithValue("@State", place.State);
                    col.AddWithValue("@PostalCode", place.PostalCode);
                    col.AddWithValue("@IsActive", place.IsActive);
                    col.AddWithValue("@Lat", place.Lat);
                    col.AddWithValue("@Long", place.Long);

                },
                returnParameters: null);

        }

        public List<int> AddMany(AddressAddRequest place)
        {
            return null;
        }

        public int Add(AddressAddRequest place, int userId)
        {
            int id = 0;


            string procName = "[dbo].[Sabio_Addresses_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    //and 1 Output
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);

                    col.AddWithValue("@LineOne", place.LineOne);
                    col.AddWithValue("@SuiteNumber", place.SuiteNumber);
                    col.AddWithValue("@City", place.City);
                    col.AddWithValue("@State", place.State);
                    col.AddWithValue("@PostalCode", place.PostalCode);
                    col.AddWithValue("@IsActive", place.IsActive);
                    col.AddWithValue("@Lat", place.Lat);
                    col.AddWithValue("@Long", place.Long);

                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    Int32.TryParse(oId.ToString(), out id);

                });

            return id;
        }

        public Address GetById(int id)
        {
            string procName = "[dbo].[Sabio_Addresses_SelectById]";

            Address address = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
)
                paramCollection.AddWithValue("@id", id);

            }, delegate (IDataReader reader, short set) // single Record Mapper
            {
                address = MapAddress(reader);
            }
            );

            return address;
        }

        public void Delete(int id)
        {
            string procName = "[dbo].[Sabio_Addresses_DeleteById]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@id", id);

            }, null
            );
        }

        public List<Address> GetTop()
        {
            List<Address> list = null;

            string procName = "[dbo].[Sabio_Addresses_SelectRandom50]";


            _data.ExecuteCmd(procName, inputParamMapper: null
            , singleRecordMapper: delegate (IDataReader reader, short set)
            {

                Address aAddress = MapAddress(reader);

                if (list == null)
                {
                    list = new List<Address>();
                }

                list.Add(aAddress);
            });

            return list;
        }

        private static Address MapAddress(IDataReader reader)
        {
            Address aAddress = new Address();

            int startingIndex = 0;

            aAddress.Id = reader.GetSafeInt32(startingIndex++);
            aAddress.LineOne = reader.GetSafeString(startingIndex++);
            aAddress.SuiteNumber = reader.GetSafeInt32Nullable(startingIndex++);
            aAddress.City = reader.GetSafeString(startingIndex++);
            aAddress.State = reader.GetSafeString(startingIndex++);
            aAddress.PostalCode = reader.GetSafeString(startingIndex++);
            aAddress.IsActive = reader.GetSafeBool(startingIndex++);
            aAddress.Lat = reader.GetSafeDouble(startingIndex++);
            aAddress.Long = reader.GetSafeDouble(startingIndex++);
            return aAddress;
        }

        

    }
}
