using Microsoft.Data.SqlClient;
using System.Data;

namespace BackendExamHub.Services
{
    public class SqlDbService
    {
        private readonly string _connectionString;

        // 正確初始化連接字串
        public SqlDbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "DefaultConnection string is not configured");
        }

        public async Task<string?> ExecuteStoredProcedureAsync(string storedProcedureName, string jsonInput, string outputParamName = "@Result")
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using SqlCommand command = new SqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter jsonParam = new SqlParameter("@JsonData", SqlDbType.NVarChar, -1) { Value = jsonInput };
            command.Parameters.Add(jsonParam);

            SqlParameter outputParam = new SqlParameter(outputParamName, SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output };
            command.Parameters.Add(outputParam);

            await command.ExecuteNonQueryAsync();

            return outputParam.Value?.ToString();
        }
    }
}