using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Utils
{
    public class UpdateNullable
    {
        public static void SetNullableString(SqlCommand cmd, string param, string value)
        {
            if (value == null)
            {
                cmd.Parameters.AddWithValue(param, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param, value);
            }
        }

    }
}
